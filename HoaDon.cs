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
    public partial class HoaDon : Form
    {
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        string Conn = "Data Source=boo;Initial Catalog=QLShopGiayDep;Integrated Security=True";
        public HoaDon()
        {
            InitializeComponent();
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM HoaDon2";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvKhachHang.DataSource = dt;
        }

        private void CapNhatHoaDon(string maDon)
        {
            string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; "; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                string query = @"
            UPDATE HoaDon2
            SET 
                TienHang = (SELECT SUM(ThanhTien) FROM ChiTietHoaDon WHERE MaDon = @MaDon),
                TienThue = (SELECT SUM(ThanhTien) * 0.1 FROM ChiTietHoaDon WHERE MaDon = @MaDon), -- 10% VAT
                ThanhTien = (SELECT SUM(ThanhTien) FROM ChiTietHoaDon WHERE MaDon = @MaDon) 
                            + PhiShip - GiamGia + (SELECT SUM(ThanhTien) * 0.1 FROM ChiTietHoaDon WHERE MaDon = @MaDon)
            WHERE MaDon = @MaDon";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaDon", maDon);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string tenKhachHang = txtKhachHang.Text.Trim();
            string soDienThoai = txtSDT.Text.Trim();
            string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";
            SqlConnection connection = new SqlConnection(connectionString);
            
            string query = "SELECT * FROM HoaDon2 WHERE 1=1";
            if (!string.IsNullOrEmpty(tenKhachHang))
            {
                query += " AND KhachHang LIKE @KhachHang";
            }
            if (!string.IsNullOrEmpty(soDienThoai))
            {
                query += " AND SoDT LIKE @SoDT";
            }

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);

                
                if (!string.IsNullOrEmpty(tenKhachHang))
                {
                    cmd.Parameters.AddWithValue("@KhachHang", "%" + tenKhachHang + "%");
                }
                if (!string.IsNullOrEmpty(soDienThoai))
                {
                    cmd.Parameters.AddWithValue("@SoDT", "%" + soDienThoai + "%");
                }

                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                
                dgvKhachHang.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                
                string maDon = row.Cells["MaDon"].Value.ToString();
                string nhanVien = row.Cells["NhanVien"].Value.ToString();
                string ngayHoaDon = row.Cells["NgayHoaDon"].Value.ToString();
                string khachHang = row.Cells["KhachHang"].Value.ToString();
                string soDT = row.Cells["SoDT"].Value.ToString();
                string tienHang = row.Cells["TienHang"].Value.ToString();
                string giamGia = row.Cells["GiamGia"].Value.ToString();
                string phiShip = row.Cells["PhiShip"].Value.ToString();
                string tienThue = row.Cells["TienThue"].Value.ToString();

                
                DataTable chiTietSanPham = LayChiTietSanPham(maDon);

                
                FromChiTietHoaDon chiTietForm = new FromChiTietHoaDon(maDon, nhanVien, ngayHoaDon, khachHang, soDT, tienHang, giamGia, phiShip, tienThue, chiTietSanPham);
                chiTietForm.FormClosing += (s, args) =>
                {
                    string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";
                    SqlConnection connection = new SqlConnection(connectionString);

                    string query = "SELECT * FROM HoaDon2";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvKhachHang.DataSource = dt;   
                };
                chiTietForm.Show();
            }
        }

        private DataTable LayChiTietSanPham(string maDon)
        {
            
            DataTable dt = new DataTable();
            dt.Columns.Add("MaHang");
            dt.Columns.Add("TenHang");
            dt.Columns.Add("GiaBan");
            dt.Columns.Add("SoLuong");
            dt.Columns.Add("ThanhTien");
            dt.Columns.Add("GhiChu");

            
            dt.Rows.Add("MH001", "Giày gucci", "50000", "2", "100000", "Ghi chú A");
            dt.Rows.Add("MH002", "Dép MLB", "70000", "1", "70000", "Ghi chú B");
            dt.Rows.Add("MH001", "Sandan LV", "50000", "2", "100000", "Ghi chú A");
            dt.Rows.Add("MH002", "Giày MLB", "70000", "1", "70000", "Ghi chú B");

            return dt;
        }

        private void cboLapBoi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM HoaDon2";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvKhachHang.DataSource = dt;
        }
    }
}
