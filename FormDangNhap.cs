using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapNhom
{
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (KiemTraDangNhap(txtTaiKhoan.Text, txtMatKhau.Text))
            {
                FormChuongTrinh f = new FormChuongTrinh();
                f.ShowDialog();//hiển thị       
                this.Hide();//ẩn form

            }
        }
        string tentaikhoan = "admin";
        string matkhau = "1";
        bool KiemTraDangNhap(string tentaikhoan,string matkhau)
        {
            if(tentaikhoan == this.tentaikhoan && matkhau == this.matkhau)
            {  return true; }
            return false;
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
