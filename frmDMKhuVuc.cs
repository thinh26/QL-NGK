using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //Sử dụng thư viện để làm việc SQL server
using QLBN.Class; //Sử dụng class Functions.cs

namespace QLBN
{
    
    public partial class frmDMKhuVuc : Form
    {
        private DataTable KhuVuc;
        public frmDMKhuVuc()
        {
            InitializeComponent();
        }

        private void frmDMKhuVuc_Load(object sender, EventArgs e)
        {
            txtMaKhuVuc.Enabled = false;
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "select * from KHUVUC";
            KhuVuc = Class.Functions.GetDataToTable(sql);
            dgvKhuVuc.DataSource = KhuVuc; //Nguồn dữ liệu            
            dgvKhuVuc.Columns[0].HeaderText = "Mã khu vực";
            dgvKhuVuc.Columns[1].HeaderText = "Tên khu vực";
            dgvKhuVuc.Columns[0].Width = 200;
            dgvKhuVuc.Columns[1].Width = 300;
            dgvKhuVuc.AllowUserToAddRows = true; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvKhuVuc.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
