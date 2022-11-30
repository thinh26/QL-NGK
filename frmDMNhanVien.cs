using QLBN.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLBN
{
    public partial class frmDMNhanVien : Form
    {
        DataTable tblNV;
        public frmDMNhanVien()
        {
            InitializeComponent();
        }
        private void frmDMNhanVien_Load(object sender, EventArgs e)
        {
            txtMaNhanVien.Enabled = false;
            // btnLuu.Enabled = false;
            // btnBoQua.Enabled = false;
            LoadDataGridView();
        }
        public void LoadDataGridView()
        {
            string sql;
            sql = "select MaNV, HoTen from NhanVien";
            tblNV = Functions.GetDataToTable(sql); //lấy dữ liệu
            dgvNhanVien.DataSource = tblNV;
            dgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns[1].HeaderText = "Tên nhân viên";

            dgvNhanVien.Columns[0].Width = 100;
            dgvNhanVien.Columns[1].Width = 300;

            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
    }
}
