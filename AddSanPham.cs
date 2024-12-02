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
    public partial class AddSanPham : Form
    {
        public AddSanPham()
        {
            InitializeComponent();
        }

        private void AddSanPham_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ListViewItem lv1 = new ListViewItem(txtMaHangAdd.Text);
            lv1.SubItems.Add(txtTenHangAdd.Text);
            lv1.SubItems.Add(txtGiaSiAdd.Text);
            lv1.SubItems.Add(txtGiaLeAdd.Text);
            lv1.SubItems.Add(txtSLCAdd.Text);

        
        }
    }
}
