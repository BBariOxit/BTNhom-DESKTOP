using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BaiTapNhom
{
    public partial class FormChuongTrinh : Form
    {
        public FormChuongTrinh()
        {
            InitializeComponent();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormDangNhap();
            form.ShowDialog();
        }
        

        private void thêmHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new NhomMatHang();
            form.ShowDialog();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
       
        private void lvTTSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lay san pham duoc chon
            if (lvSP.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvSP.SelectedItems[0];
                string productCode = selectedItem.SubItems[5].Text;
                string price = selectedItem.SubItems[6].Text;

                // Tạo sản phẩm mới cho ListView 2 dựa trên thông tin từ ListView 1
                ListViewItem newItem = new ListViewItem(selectedItem.SubItems[0].Text); // Mã hàng
                newItem.SubItems.Add(selectedItem.SubItems[5].Text); //Ma loai
                newItem.SubItems.Add(selectedItem.SubItems[1].Text);//Ten mat hang
               
                newItem.SubItems.Add(selectedItem.SubItems[6].Text);//size
                newItem.SubItems.Add(selectedItem.SubItems[7].Text);//mau
                newItem.SubItems.Add(selectedItem.SubItems[2].Text);//gia ban
                newItem.SubItems.Add(selectedItem.SubItems[2].Text);//thanh tien
                newItem.SubItems.Add("1");  // Số lượng mặc định là 1
                // Thành tiền (Giá bán * 1)
                bool exists = false;

                //foreach (ListViewItem item in lvTTSP.Items)
                //{
                //    if (item.SubItems[0].Text == productCode) // So sánh mã hàng
                //    {
                //        // Tăng số lượng
                //        int quantity = int.Parse(item.SubItems[5].Text); // Số lượng hiện tại
                //        quantity++;
                //        item.SubItems[5].Text = quantity.ToString();    // Cập nhật số lượng
                //        item.SubItems[6].Text = (quantity * double.Parse(price)).ToString("F2"); // Thành tiền
                //        exists = true;
                //        break;
                //    }
                //}


             
                

                    // Thêm sản phẩm mới vào ListView 2
                 lvTTSP.Items.Add(newItem);
                foreach (ListViewItem item in lvTTSP.Items)
                {
                    if (item.SubItems[0].Text == productCode) // So sánh mã hàng
                    {
                        // Tăng số lượng
                        int quantity = int.Parse(item.SubItems[7].Text); // Số lượng hiện tại
                        quantity++;
                        item.SubItems[7].Text = quantity.ToString();    // Cập nhật số lượng
                        item.SubItems[6].Text = (quantity * double.Parse(price)).ToString("F2"); // Thành tiền
                        exists = true;
                        break;
                    }
                }
            }
        }

        private void btnThoatMain_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblTongTienThanhToan_Click(object sender, EventArgs e)
        {
          
            int soTien = int.Parse(txtTongThanhToan.Text);
            lblTongTienThanhToan.Text = soTien.ToString();
        }

        private void danhSáchMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new DanhSachMatHang();
            form.ShowDialog();
        }

        private void thêmMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ThemMatHang();
            form.ShowDialog();
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new QuanLyKhachHang();
            form.ShowDialog();
        }

        private void thêmMặtHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ThemMatHang form = new ThemMatHang();
            form.Show();
        }

        private void lvSP_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void cậpNhậpThôngTinMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CapNhatThongTinMatHang form = new CapNhatThongTinMatHang();
            form.Show();
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoaDon form = new HoaDon();
            form.Show();
        }

        private void FormChuongTrinh_Load(object sender, EventArgs e)
        {
            txtTimKiemHang.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTimKiemHang.AutoCompleteSource = AutoCompleteSource.CustomSource;
            LoadAutoCompleteData();

            // Gắn sự kiện TextChanged để tìm kiếm
          
        }
        private void LoadAutoCompleteData()
        {
            // Khởi tạo AutoCompleteCustomSource
            AutoCompleteStringCollection autoCompleteData = new AutoCompleteStringCollection();

            // Lấy dữ liệu từ cột "Tên Hàng" trong ListView
            foreach (ListViewItem item in lvSP.Items)
            {
                autoCompleteData.Add(item.SubItems[0].Text); // Giả sử cột Tên Hàng là SubItem[1]
           
            }

            // Gắn danh sách dữ liệu vào TextBox
            txtTimKiemHang.AutoCompleteCustomSource = autoCompleteData;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtTimKiemHang.Text = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiemHang.Text.Trim().ToLower();

            foreach (ListViewItem item in lvSP.Items)
            {
                // Kiểm tra nếu sản phẩm khớp từ khóa
                bool match = item.SubItems[1].Text.ToLower().Contains(keyword); // SubItem[1] là cột Tên Hàng

            }
        }

        private void btnTimKiem_TextChanged(object sender, EventArgs e)
        {

        }



        //private void UpdateTotalPrice(ListViewItem item)
        //{
        //    // Lấy giá bán và số lượng
        //    decimal price = Convert.ToDecimal(item.SubItems[2].Text);  // Giá bán
        //    int quantity = Convert.ToInt32(item.SubItems[3].Text);  // Số lượng

        //    // Tính thành tiền
        //    decimal totalPrice = price * quantity;

        //    // Cập nhật thành tiền vào cột
        //    item.SubItems[4].Text = totalPrice.ToString("N0");  // Định dạng thành tiền theo số
        //}


    }
}
