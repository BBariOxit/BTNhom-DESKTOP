using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapNhom
{   
    public partial class NhomMatHang : Form
    {
        public NhomMatHang()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtTenNhomMatHang.Text = string.Empty;
            txtMaLoai.Text = string.Empty;
            txtLuuY.Text = string.Empty;

            MessageBox.Show("Nội dung đã được xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";
            string tenNhomMatHang = txtTenNhomMatHang.Text.Trim();
            string maLoai = txtMaLoai.Text.Trim();
            string luuY = txtLuuY.Text.Trim();
            

            if (string.IsNullOrEmpty(tenNhomMatHang) || string.IsNullOrEmpty(maLoai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();

                // Câu lệnh SQL để thêm dữ liệu vào bảng NhaCC
                cmd.CommandText = "INSERT INTO LoaiGiayDep (MaLoai,TenLoaiHang,GhiChu,Enable) VALUES (@MaLoai,@TenLoaiHang,@GhiChu,@enable)";
                cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                cmd.Parameters.AddWithValue("@TenLoaiHang", tenNhomMatHang);
                cmd.Parameters.AddWithValue("@GhiChu", luuY);
                if (checkBox1.Checked)
                {
                    cmd.Parameters.AddWithValue("@enable", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@enable", 0);
                } 
                
                try
                {
                    conn.Open(); // Mở kết nối
                    int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm MH thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Xóa dữ liệu trên các TextBox sau khi thêm thành công
                        txtTenNhomMatHang.Clear();
                        txtMaLoai.Clear();
                        txtLuuY.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Không thêm được MH. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu xảy ra ngoại lệ
                    MessageBox.Show($"Lỗi khi thêm nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NhomMatHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachSP();
        }
        public void LoadDanhSachSP()
        {
            string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";

            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT LoaiGiayDep.MaLoai,TenLoaiHang,GhiChu,Enable FROM LoaiGiayDep";
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Kiểm tra nếu DataTable có dữ liệu
                if (dt.Rows.Count > 0)
                {
                    dgvLoaiHang.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không có sản phẩm nào để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối hoặc truy vấn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLoaiHang.SelectedRows.Count > 0) // Kiểm tra nếu có hàng được chọn
            {

                string selectedID = dgvLoaiHang.SelectedRows[0].Cells["MaLoai"].Value.ToString();

                string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Tạo câu lệnh xóa
                    string query = "DELETE FROM LoaiGiayDep WHERE MaLoai = @MaLoai";
                    using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                    {
                        // Truyền tham số vào câu truy vấn để tránh SQL Injection
                        sqlCommand.Parameters.AddWithValue("@MaLoai", selectedID);

                        // Thực thi câu lệnh
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Đã xóa thành công mục được chọn.");

                            // Xóa hàng khỏi DataGridView
                            dgvLoaiHang.Rows.Remove(dgvLoaiHang.SelectedRows[0]);
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa mục được chọn.");
                        }
                    }

                }
            }
        }

        private void dgvLoaiHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadDanhSachSP();
        }
    }
}
