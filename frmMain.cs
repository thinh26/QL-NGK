using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBN.Class;

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
            // TODO: This line of code loads data into the 'qLBN_DBDataSet.NuocGiaiKhat' table. You can move, or remove it, as needed.
            this.nuocGiaiKhatTableAdapter.Fill(this.qLBN_DBDataSet.NuocGiaiKhat);
            // TODO: This line of code loads data into the 'qLBN_DBDataSet.NhaCungCap' table. You can move, or remove it, as needed.
            this.nhaCungCapTableAdapter.Fill(this.qLBN_DBDataSet.NhaCungCap);
            // TODO: This line of code loads data into the 'qLBN_DBDataSet.KhachHang' table. You can move, or remove it, as needed.
            this.khachHangTableAdapter.Fill(this.qLBN_DBDataSet.KhachHang);
            // TODO: This line of code loads data into the 'qLBN_DBDataSet.NhanVien' table. You can move, or remove it, as needed.
            this.nhanVienTableAdapter.Fill(this.qLBN_DBDataSet.NhanVien);
            Functions.Connect();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Functions.Disconnect(); //Đóng kết nối
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
