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
    public partial class DanhSachMatHang : Form
    {
        
        public DanhSachMatHang()
        {
            InitializeComponent();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbNhomMatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nhomDuocChon = cmbNhomMatHang.SelectedItem.ToString();

            foreach (DataGridViewRow row in dgvDanhSach.Rows)
            {
                // Hiển thị dòng nếu nhóm khớp hoặc chọn "--Tất cả--"
                row.Visible = nhomDuocChon == "--Tất cả--" || row.Cells["NhomMatHang"].Value.ToString() == nhomDuocChon;
                cmbNhomMatHang.SelectedIndexChanged += cmbNhomMatHang_SelectedIndexChanged;
            }
        }
        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThemMatHang form = new ThemMatHang();
            form.Show();
          
        }

        private void DanhSachMatHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachSP();
        }
        public void LoadDanhSachSP()
        {
            string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";

            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM MatHang";
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Kiểm tra nếu DataTable có dữ liệu
                if (dt.Rows.Count > 0)
                {
                    dgvDanhSach.DataSource = dt;
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

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadDanhSachSP();
        }
    }
}
