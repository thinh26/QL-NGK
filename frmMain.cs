using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBN
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Class.Functions.Disconnect(); //Đóng kết nối
            this.Close();
        }

        
        private void mnuKhuVuc_Click(object sender, EventArgs e)
        {
            Form frmKhuVuc = new frmDMKhuVuc();
            frmKhuVuc.Show();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            Form frmNhanVien = new frmDMNhanVien();
            frmNhanVien.Show();
        }
    }
}
