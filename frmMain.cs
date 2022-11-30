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
        DataTable tblnv;
        DataTable tblkh;
        DataTable tblncc;
        DataTable tblngk;
        DataTable tbllngk;
        public frmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadDataGridView(); //Hiển thị bảng tblChatLieu
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM NhanVien";
            tblnv = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvNV.DataSource = tblnv; //Nguồn dữ liệu
            sql = "SELECT * FROM KhachHang";
            tblkh = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvNV.DataSource = tblkh; //Nguồn dữ liệu  
            sql = "SELECT * FROM NhaCungCap";
            tblncc = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvNV.DataSource = tblncc; //Nguồn dữ liệu  
            sql = "SELECT * FROM NuocGiaiKhat";
            tblngk = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvNV.DataSource = tblngk; //Nguồn dữ liệu  
            sql = "SELECT * FROM LoaiNGK";
            tbllngk = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvNV.DataSource = tbllngk; //Nguồn dữ liệu  
            //dgvCuaHang.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            //dgvCuaHang.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Functions.Disconnect(); //Đóng kết nối
            this.Close();
        }
    }
}
