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
    public partial class ThemMatHang : Form
    {
        public ThemMatHang()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void btnBrowser_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog fileImage = new OpenFileDialog();
        //    if (fileImage.ShowDialog() == DialogResult.OK)
        //    {
        //        ptbShoe.Image = new Bitmap(fileImage.FileName);
        //        txtImg.Text = fileImage.FileName;
        //        ptbShoe.BackgroundImageLayout = ImageLayout.Stretch;
        //    }
        //}

        private void ptbShoe_Click(object sender, EventArgs e)
        {

        }

        private void btnLuuVaTaoMoi_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";

            string maLoai = txtMaLoai.Text.Trim();
            string matHang = txtMatHang.Text.Trim();
            string mauSize = txtMauSize.Text.Trim();
            string donGia = txtGiaBan.Text.Trim();
            string soLuong = txtSL.Text.Trim();
            if (string.IsNullOrEmpty(maLoai) || string.IsNullOrEmpty(matHang)|| string.IsNullOrEmpty(mauSize)|| string.IsNullOrEmpty(donGia)|| string.IsNullOrEmpty(soLuong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo kết nối đến cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();

                
                cmd.CommandText = "INSERT INTO MatHang (MaLoai, TenMatHang, IDMauSize, DonGia, SoLuong) VALUES (@MaLoai,@TenMatHang,@IDMauSize,@DonGia,@SoLuong)";
                cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                cmd.Parameters.AddWithValue("@TenMatHang", matHang);
                cmd.Parameters.AddWithValue("@IDMauSize", mauSize);
                cmd.Parameters.AddWithValue("@DonGia", donGia);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                try
                {
                    conn.Open(); 
                    int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm MH thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Xóa dữ liệu trên các TextBox sau khi thêm thành công
                        txtMaHang.Clear();
                        txtMaLoai.Clear();
                        txtMauSize.Clear();
                        txtGiaBan.Clear();
                        txtSL.Clear();
                        this.Close();
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

        private void txtMaHang_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
