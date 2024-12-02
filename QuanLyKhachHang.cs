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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BaiTapNhom
{
    public partial class QuanLyKhachHang : Form
    {
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        string Conn = "Data Source=boo;Initial Catalog=QLShopGiayDep;Integrated Security=True";
        public QuanLyKhachHang()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

       
        private void DisplayKH(SqlDataReader reader)
        {
            lvKH.Items.Clear();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["IDKhachHang"].ToString());
                lvKH.Items.Add(item);
                item.SubItems.Add(reader["HoVaTen"].ToString());
                item.SubItems.Add(reader["SDT"].ToString());
                item.SubItems.Add(reader["DiaChi"].ToString());
            }
        }

        
        private void lvKH_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvKH.SelectedItems[0];
            txtID.Text = item.Text;
            txtHvT.Text = item.SubItems[1].Text;
            txtSDT.Text = item.SubItems[2].Text;
            txtDC.Text = item.SubItems[3].Text;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        

        

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();

         
            
            string query = "Delete  from KKhachHang where IDKhachHang ='" + txtID.Text + "'";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            MessageBox.Show("Xóa thành công");

        }

        private void lvKH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     

        private void btnLoad_Click_1(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            string query = "SELECT IDKhachHang, HoVaTen, SDT, DiaChi FROM KKhachHang";
            sqlCommand.CommandText = query;
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            this.DisplayKH(sqlDataReader);
            sqlConnection.Close();
            txtID.Clear();
            txtHvT.Clear();
            txtSDT.Clear();
            txtDC.Clear();
        }

        private void btnLayDS_Click_1(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            string query = "SELECT IDKhachHang, HoVaTen, SDT, DiaChi FROM KKhachHang";
            sqlCommand.CommandText = query;
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            this.DisplayKH(sqlDataReader);
            sqlConnection.Close();

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();

            
           
            string tenKhach = txtHvT.Text;
            string soDienThoai = txtSDT.Text;
            string diaChiGiao = txtDC.Text;
            string IdKhachHang = txtID.Text;
          
            string query = "update KKhachHang set HoVaTen = N'" + tenKhach + "', SDT = N'" + soDienThoai + "', DiaChi = N'" + diaChiGiao + "' where IDKhachHang ='" + IdKhachHang + "'";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            MessageBox.Show("Lưu thông tin hóa đơn thành công và đã cập nhật thông tin!");
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            string connectionString = "server=.; database = QLShopGiayDep; Integrated Security = true; ";
            string hoVaTen = txtHvT.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDC.Text.Trim();

            // Kiểm tra thông tin hợp lệ
            if (string.IsNullOrWhiteSpace(hoVaTen) || string.IsNullOrWhiteSpace(sdt) || string.IsNullOrWhiteSpace(diaChi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "INSERT INTO KKhachHang (HoVaTen, SDT, DiaChi) VALUES (@HoVaTen, @SDT, @DiaChi)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@HoVaTen", hoVaTen);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Bạn đã thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Xóa các ô textbox sau khi thêm thành công
            txtHvT.Clear();
            txtSDT.Clear();
            txtDC.Clear();


        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
