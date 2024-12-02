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
using System.Xml.Linq;

namespace BaiTapNhom
{
    public partial class FromChiTietHoaDon : Form
    {
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        string Conn = "Data Source=boo;Initial Catalog=QLShopGiayDep;Integrated Security=True";
        public FromChiTietHoaDon()
        {
            InitializeComponent();
        }
        private void Control_TextChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
        }
        private void ChiTietHoaDon_Load(object sender, EventArgs e)
        {
          
        }
        public FromChiTietHoaDon(string maDon, string nhanVien, string ngayHoaDon, string khachHang, string soDT,
                             string tienHang, string giamGia, string phiShip, string tienThue, DataTable chiTietSanPham)
        {
            InitializeComponent();

            // Gán dữ liệu vào các TextBox hoặc Label
            txtMaDon.Text = maDon;
            txtNgayLap.Text = ngayHoaDon;
            txtKhachHang.Text = khachHang;
            txtSDT.Text = soDT;
            txtTienhang.Text = tienHang;
            txtSale.Text = giamGia;
            txtShip.Text = phiShip;
            txtTienThue.Text = tienThue;

            // Gán dữ liệu vào DataGridView (bảng chi tiết sản phẩm)
            dgvDonHang.DataSource = chiTietSanPham;
        }
      

        private void btnLuu_Click(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();

            // Lấy thông tin khách hàng từ các TextBox
            string maDon = txtMaDon.Text;
            string tenKhach = txtKhachHang.Text;
            string soDienThoai = txtSDT.Text;
            string diaChiGiao = txtDC.Text;
            string tienHang = txtTienhang.Text;
            string trangThai = cboTrangThai.SelectedItem.ToString();
            decimal giamGia = decimal.Parse(txtSale.Text);
            decimal phiShip = decimal.Parse(txtShip.Text);
            string ghiChu = txtnote.Text;
            string query = "update HoaDon2 set KhachHang = N'" + tenKhach + "', SoDT = N'" + soDienThoai + "', TienHang = N'" + tienHang + "' where MaDon ='" + maDon + "'";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            MessageBox.Show("Lưu thông tin hóa đơn thành công và đã cập nhật thông tin!");
            this.Close();
        }

       

        private void btnHuy_Click(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();

            // Lấy thông tin khách hàng từ các TextBox
            string query = "Delete  from HoaDon2 where MaDon ='" + txtMaDon.Text + "'";



            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            MessageBox.Show("Xóa thành công");
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // e.RowIndex: Chỉ số của dòng được click
            {
                DataGridViewRow selectedRow = dgvDonHang.Rows[e.RowIndex];

                // Lấy dữ liệu từ các cột
                string Ten = selectedRow.Cells["Khách hàng"].Value.ToString();
                string soDienThoai = selectedRow.Cells["Số ĐT"].Value.ToString();

                // Gán giá trị vào các TextBox
                txtKhachHang.Text = Ten;
                txtSDT.Text = soDienThoai;
            }
        }

        private bool isDataChanged = false;

        private void dgvDonHang_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            isDataChanged = true; // Đánh dấu dữ liệu đã thay đổi
            btnLuu.Enabled = true; // Kích hoạt nút Lưu
        }

        private void dgvDonHang_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (isDataChanged)
            {
                btnLuu.Enabled = true; // Chỉ kích hoạt nút "Lưu" khi có thay đổi
            }
        }

        private void dgvDonHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
