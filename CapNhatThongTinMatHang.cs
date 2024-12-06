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
    public partial class CapNhatThongTinMatHang : Form
    {
        public CapNhatThongTinMatHang()
        {
            InitializeComponent();
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

        private void btnLuuVaThoat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("bạn đã lưu thành công");
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMau_TextChanged(object sender, EventArgs e)
        {

        }

        private void nudSoLuong_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtGiaBan_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtMatHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaLoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLuuVaTaoMoi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("bạn đã lưu thành công");
            this.Close();
            var form = new ThemMatHang();
            form.ShowDialog();
        }

        private void txtImg_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ptbShoe_Click(object sender, EventArgs e)
        {

        }
    }
}
