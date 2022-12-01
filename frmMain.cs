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
        DataTable tblhdn;
        DataTable tblhd;
        public frmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Functions.Connect(); //Mở kết nối
            LoadDataGridView(); //Load tất cả bảng
            Initialize();
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM NhanVien";
            tblnv = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvNV.DataSource = tblnv; //Nguồn dữ liệu
            sql = "SELECT * FROM KhachHang";
            tblkh = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvKH.DataSource = tblkh; //Nguồn dữ liệu  
            sql = "SELECT * FROM NhaCungCap";
            tblncc = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvNCC.DataSource = tblncc; //Nguồn dữ liệu  
            sql = "SELECT * FROM NuocGiaiKhat";
            tblngk = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvNGK.DataSource = tblngk; //Nguồn dữ liệu  
            sql = "SELECT * FROM LoaiNGK";
            tbllngk = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvLNGK.DataSource = tbllngk; //Nguồn dữ liệu  
            sql = "SELECT HDN.MaHD, CTHDN.MaNGK, HDN.MaNCC, CTHDN.SoLuong, HDN.NgayNhanHD FROM HoaDonNhap HDN join ChiTietHoaDonNhap CTHDN on HDN.MaHD = CTHDN.MaHD";
            tblhdn = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvHDN.DataSource = tblhdn; //Nguồn dữ liệu
            sql = "SELECT HD.MaHD, HD.MaKH, HD.MaNV, CTHD.MaNGK, CTHD.SoLuong, HD.NgayXuatHD FROM HoaDon HD join ChiTietHoaDon CTHD on HD.MaHD = CTHD.MaHD";
            tblhd = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvHD.DataSource = tblhd; //Nguồn dữ liệu  
            dgvNV.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvNV.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
            dgvKH.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvKH.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
            dgvNCC.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvNCC.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
            dgvNGK.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvNGK.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
            dgvLNGK.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvLNGK.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
            dgvHDN.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvHDN.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
            dgvHD.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvHD.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }
        //Setup tất cả các nút
        private void Initialize()
        {
            txtMaNVNV.Enabled = false;
            txtMaKHKH.Enabled = false;
            txtMaNCCNCC.Enabled = false;
            txtMaNGKNGK.Enabled = false;
            txtMaLoaiNGKLNGK.Enabled = false;
            txtMaHDHDN.Enabled = false;
            txtMaHDHD.Enabled = false;
            btnLuuNV.Enabled = false;
            btnHuyNV.Enabled = false;
            btnLuuKH.Enabled = false;
            btnHuyKH.Enabled = false;
            btnLuuNCC.Enabled = false;
            btnHuyNCC.Enabled = false;
            btnLuuNGK.Enabled = false;
            btnHuyNGK.Enabled = false;
            btnLuuLNGK.Enabled = false;
            btnHuyLNGK.Enabled = false;
            btnLuuHDN.Enabled = false;
            btnHuyHDN.Enabled = false;
            btnLuuHD.Enabled = false;
            btnHuyHD.Enabled = false;
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Functions.Disconnect(); //Đóng kết nối
            this.Close();
        }
        //Cell click
        //Nhân Viên
        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThemNV.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNVNV.Focus();
                return;
            }
            if (tblnv.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNVNV.Text = dgvNV.CurrentRow.Cells["MaNVNV"].Value.ToString();
            txtHoTenNV.Text = dgvNV.CurrentRow.Cells["HotenNV"].Value.ToString();
            txtGioiNV.Text = dgvNV.CurrentRow.Cells["GioiNV"].Value.ToString();
            txtDiaChiNV.Text = dgvNV.CurrentRow.Cells["DiaChiNV"].Value.ToString();
            txtSdtNV.Text = dgvNV.CurrentRow.Cells["SdtNV"].Value.ToString();
            txtLuongNV.Text = dgvNV.CurrentRow.Cells["LuongNV"].Value.ToString();
            txtChucVu.Text = dgvNV.CurrentRow.Cells["ChucVuNV"].Value.ToString();
            dtNgSinhNV.Text = dgvNV.CurrentRow.Cells["NgSinhNV"].Value.ToString();
            btnCapNhatNV.Enabled = true;
            btnXoaNV.Enabled = true;
            btnHuyNV.Enabled = true;
        }
        //Khách Hàng
        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThemKH.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKHKH.Focus();
                return;
            }
            if (tblkh.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaKHKH.Text = dgvKH.CurrentRow.Cells["MaKHKH"].Value.ToString();
            txtHotenKH.Text = dgvKH.CurrentRow.Cells["HotenKH"].Value.ToString();
            txtGioiKH.Text = dgvKH.CurrentRow.Cells["GioiKH"].Value.ToString();
            txtDiachiKH.Text = dgvKH.CurrentRow.Cells["DiaChiKH"].Value.ToString();
            txtSdtKH.Text = dgvKH.CurrentRow.Cells["SdtKH"].Value.ToString();
            btnCapNhatKH.Enabled = true;
            btnXoaKH.Enabled = true;
            btnHuyKH.Enabled = true;
        }
        //Nhà Cung Cấp
        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThemNCC.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCCNCC.Focus();
                return;
            }
            if (tblncc.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNCCNCC.Text = dgvNCC.CurrentRow.Cells["MaNCCNCC"].Value.ToString();
            txtTenNCCNCC.Text = dgvNCC.CurrentRow.Cells["TenNCCNCC"].Value.ToString();
            txtSdtNCC.Text = dgvNCC.CurrentRow.Cells["SdtNCC"].Value.ToString();
            txtDiachiNCC.Text = dgvNCC.CurrentRow.Cells["DiaChiNCC"].Value.ToString();
            dtThoiHanHopDongNCC.Text = dgvNCC.CurrentRow.Cells["ThoiHanHopDongNCC"].Value.ToString();
            btnCapNhatNCC.Enabled = true;
            btnXoaNCC.Enabled = true;
            btnHuyNCC.Enabled = true;
        }
        //Mặt Hàng
        private void dgvNGK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThemNGK.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNGKNGK.Focus();
                return;
            }
            if (tblngk.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNGKNGK.Text = dgvNGK.CurrentRow.Cells["MaNGKNGK"].Value.ToString();
            txtMaLoaiNGKNGK.Text = dgvNGK.CurrentRow.Cells["MaLoaiNGKNGK"].Value.ToString();
            txtMaNCCNGK.Text = dgvNGK.CurrentRow.Cells["MaNCCNGK"].Value.ToString();
            txtGiaNGK.Text = dgvNGK.CurrentRow.Cells["GiaNGK"].Value.ToString();
            txtSoLuongNGK.Text = dgvNGK.CurrentRow.Cells["SoLuongNGK"].Value.ToString();
            txtTenNGKNGK.Text = dgvNGK.CurrentRow.Cells["TenNGKNGK"].Value.ToString();
            txtThanhPhanNGK.Text = dgvNGK.CurrentRow.Cells["ThanhPhanNGK"].Value.ToString();
            dtNgaySanXuatNGK.Text = dgvNGK.CurrentRow.Cells["NgaySanXuatNGK"].Value.ToString();
            dtHanSuDungNGK.Text = dgvNGK.CurrentRow.Cells["HanSuDungNGK"].Value.ToString();
            btnCapNhatNGK.Enabled = true;
            btnXoaNGK.Enabled = true;
            btnHuyNGK.Enabled = true;
        }
        //Loại Mặt Hàng
        private void dgvLNGK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThemLNGK.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiNGKLNGK.Focus();
                return;
            }
            if (tbllngk.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaLoaiNGKLNGK.Text = dgvLNGK.CurrentRow.Cells["MaLoaiNGKLNGK"].Value.ToString();
            txtTenLoaiNGKLNGK.Text = dgvLNGK.CurrentRow.Cells["TenLoaiNGKLNGK"].Value.ToString();
            btnCapNhatLNGK.Enabled = true;
            btnXoaLNGK.Enabled = true;
            btnHuyLNGK.Enabled = true;
        }
        //Hóa Đơn Nhập
        private void dgvHDN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThemHDN.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHDHDN.Focus();
                return;
            }
            if (tblhdn.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaHDHDN.Text = dgvHDN.CurrentRow.Cells["MaHD"].Value.ToString();
            txtMaNCCHDN.Text = dgvHDN.CurrentRow.Cells["MaNCC"].Value.ToString();
            txtMaNGKHDN.Text = dgvHDN.CurrentRow.Cells["MaNGK"].Value.ToString();
            txtSoLuongHDN.Text = dgvHDN.CurrentRow.Cells["SoLuong"].Value.ToString();
            dtNgayNhapHDHDN.Text = dgvHDN.CurrentRow.Cells["NgayNhapHD"].Value.ToString();
            btnCapNhatHDN.Enabled = true;
            btnXoaHDN.Enabled = true;
            btnHuyHDN.Enabled = true;
        }
        //Hóa Đơn Xuất
        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThemHD.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHDHD.Focus();
                return;
            }
            if (tblhd.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaHDHD.Text = dgvHD.CurrentRow.Cells["MaHD"].Value.ToString();
            txtMaKHHD.Text = dgvHD.CurrentRow.Cells["MaKH"].Value.ToString();
            txtMaNGKHD.Text = dgvHD.CurrentRow.Cells["MaNGK"].Value.ToString();
            txtMaNVHD.Text = dgvHD.CurrentRow.Cells["MaNV"].Value.ToString();
            txtSoLuongHD.Text = dgvHD.CurrentRow.Cells["SoLuong"].Value.ToString();
            dtNgayXuatHDHD.Text = dgvHD.CurrentRow.Cells["NgayXuatHD"].Value.ToString();
            btnCapNhatHD.Enabled = true;
            btnXoaHD.Enabled = true;
            btnHuyHD.Enabled = true;
        }
        //Reset value
        //Nhân Viên
        private void ResetValueNhanVien()
        {
            txtMaNVNV.Text = "";
            txtHoTenNV.Text = "";
            txtGioiNV.Text = "";
            txtDiaChiNV.Text = "";
            txtSdtNV.Text = "";
            txtLuongNV.Text = "";
            txtChucVu.Text = "";
            dtNgSinhNV.Text = "";
        }
        //Khách Hàng
        private void ResetValueKhachHang()
        {
            txtMaKHKH.Text = "";
            txtHotenKH.Text = "";
            txtDiachiKH.Text = "";
            txtGioiKH.Text = "";
            txtSdtKH.Text = "";
        }
        //Nhà Cung Cấp
        private void ResetValueNhaCungCap()
        {
            txtMaNCCNCC.Text = "";
            txtTenNCCNCC.Text = "";
            txtDiachiNCC.Text = "";
            txtSdtNCC.Text = "";
            dtThoiHanHopDongNCC.Text = "";
        }
        //Mặt Hàng
        private void ResetValueMatHang()
        {
            txtMaNGKNGK.Text = "";
            txtTenNGKNGK.Text = "";
            txtMaLoaiNGKNGK.Text = "";
            txtGiaNGK.Text = "";
            txtMaNCCNGK.Text = "";
            txtThanhPhanNGK.Text = "";
            txtSoLuongNGK.Text = "";
            dtNgaySanXuatNGK.Text = "";
            dtHanSuDungNGK.Text = "";
        }
        //Nhà Cung Cấp
        private void ResetValueLoaiMatHang()
        {
            txtMaLoaiNGKLNGK.Text = "";
            txtTenLoaiNGKLNGK.Text = "";
        }
        //Hóa Đơn Nhập
        private void ResetValueHoaDonNhap()
        {
            txtMaHDHDN.Text = "";
            txtMaNCCHDN.Text = "";
            txtSoLuongHDN.Text = "";
            txtMaNGKHDN.Text = "";
            dtNgayNhapHDHDN.Text = "";
        }
        //Hóa Đơn Xuất
        private void ResetValueHoaDon()
        {
            txtMaHDHD.Text = "";
            txtMaKHHD.Text = "";
            txtMaNVHD.Text = "";
            txtSoLuongHD.Text = "";
            txtMaNGKHD.Text = "";
            dtNgayXuatHDHD.Text = "";
        }
        //Xóa Dữ Liệu
        //Nhân Viên
        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblnv.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNVNV.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE NhanVien WHERE MaNV=N'" + txtMaNVNV.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValueNhanVien();
            }
        }
        //Khách Hàng
        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblkh.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKHKH.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE KhachHang WHERE MaKH=N'" + txtMaKHKH.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValueNhanVien();
            }
        }
        //Nhà Cung Cấp
        private void btnXoaNCC_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblncc.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNCCNCC.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE NhaCungCap WHERE MaNCC=N'" + txtMaNCCNCC.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValueNhanVien();
            }
        }
        //Mặt Hàng
        private void btnXoaNGK_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblngk.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNGKNGK.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE NuocGiaiKhat WHERE MaNGK=N'" + txtMaNGKNGK.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValueNhanVien();
            }
        }
        //Loại Mặt Hàng
        private void btnXoaLNGK_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbllngk.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaLoaiNGKLNGK.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE LoaiNGK WHERE MaLoaiNGK=N'" + txtMaLoaiNGKLNGK.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValueNhanVien();
            }
        }
        //Hóa Đơn Nhập
        private void btnXoaHDN_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblhdn.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHDHDN.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE ChiTietHoaDonNhap WHERE MaHD=N'" + txtMaHDHDN.Text + "'";
                Class.Functions.RunSqlDel(sql);
                sql = "DELETE HoaDonNhap WHERE MaHD=N'" + txtMaHDHDN.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValueNhanVien();
            }
        }
        //Hóa Đơn Xuất
        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblhd.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHDHD.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE ChiTietHoaDon WHERE MaHD=N'" + txtMaHDHD.Text + "'";
                Class.Functions.RunSqlDel(sql);
                sql = "DELETE HoaDon WHERE MaHD=N'" + txtMaHDHD.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValueNhanVien();
            }
        }
        //Hủy thao tác
        //Nhân Viên
        private void btnHuyNV_Click(object sender, EventArgs e)
        {
            ResetValueNhanVien();
            btnHuyNV.Enabled = false;
            btnThemNV.Enabled = true;
            btnXoaNV.Enabled = true;
            btnCapNhatNV.Enabled = true;
            btnLuuNV.Enabled = false;
            txtMaNVNV.Enabled = false;
        }
        //Khách Hàng
        private void btnHuyKH_Click(object sender, EventArgs e)
        {
            ResetValueKhachHang();
            btnHuyKH.Enabled = false;
            btnThemKH.Enabled = true;
            btnXoaKH.Enabled = true;
            btnCapNhatKH.Enabled = true;
            btnLuuKH.Enabled = false;
            txtMaKHKH.Enabled = false;
        }
        //Nhà Cung Cấp
        private void btnHuyNCC_Click(object sender, EventArgs e)
        {
            ResetValueNhaCungCap();
            btnHuyNCC.Enabled = false;
            btnThemNCC.Enabled = true;
            btnXoaNCC.Enabled = true;
            btnCapNhatNCC.Enabled = true;
            btnLuuNCC.Enabled = false;
            txtMaNCCNCC.Enabled = false;
        }
        //Mặt Hàng
        private void btnHuyNGK_Click(object sender, EventArgs e)
        {
            ResetValueMatHang();
            btnHuyNGK.Enabled = false;
            btnThemNGK.Enabled = true;
            btnXoaNGK.Enabled = true;
            btnCapNhatNGK.Enabled = true;
            btnLuuNGK.Enabled = false;
            txtMaNGKNGK.Enabled = false;
        }
        //Loại Mặt Hàng
        private void btnHuyLNGK_Click(object sender, EventArgs e)
        {
            ResetValueLoaiMatHang();
            btnHuyLNGK.Enabled = false;
            btnThemLNGK.Enabled = true;
            btnXoaLNGK.Enabled = true;
            btnCapNhatLNGK.Enabled = true;
            btnLuuLNGK.Enabled = false;
            txtMaLoaiNGKLNGK.Enabled = false;
        }
        //Hóa Đơn Nhập
        private void btnHuyHDN_Click(object sender, EventArgs e)
        {
            ResetValueHoaDonNhap();
            btnHuyHDN.Enabled = false;
            btnThemHDN.Enabled = true;
            btnXoaHDN.Enabled = true;
            btnCapNhatHDN.Enabled = true;
            btnLuuHDN.Enabled = false;
            txtMaHDHDN.Enabled = false;
        }
        //Hóa Đơn Xuất
        private void btnHuyHD_Click(object sender, EventArgs e)
        {
            ResetValueHoaDon();
            btnHuyHD.Enabled = false;
            btnThemHD.Enabled = true;
            btnXoaHD.Enabled = true;
            btnCapNhatHD.Enabled = true;
            btnLuuHD.Enabled = false;
            txtMaHDHD.Enabled = false;
        }
        //Thêm dữ liệu
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            btnCapNhatNV.Enabled = false;
            btnXoaNV.Enabled = false;
            btnHuyNV.Enabled = true;
            btnLuuNV.Enabled = true;
            btnThemNV.Enabled = false;
            ResetValueNhanVien(); //Xoá trắng các textbox
            txtMaNVNV.Enabled = true; //cho phép nhập mới
            txtMaNVNV.Focus();
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            btnCapNhatKH.Enabled = false;
            btnXoaKH.Enabled = false;
            btnHuyKH.Enabled = true;
            btnLuuKH.Enabled = true;
            btnThemKH.Enabled = false;
            ResetValueKhachHang(); //Xoá trắng các textbox
            txtMaKHKH.Enabled = true; //cho phép nhập mới
            txtMaKHKH.Focus();
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            btnCapNhatNCC.Enabled = false;
            btnXoaNCC.Enabled = false;
            btnHuyNCC.Enabled = true;
            btnLuuNCC.Enabled = true;
            btnThemNCC.Enabled = false;
            ResetValueNhaCungCap(); //Xoá trắng các textbox
            txtMaNCCNCC.Enabled = true; //cho phép nhập mới
            txtMaNCCNCC.Focus();
        }

        private void btnThemNGK_Click(object sender, EventArgs e)
        {
            btnCapNhatNGK.Enabled = false;
            btnXoaNGK.Enabled = false;
            btnHuyNGK.Enabled = true;
            btnLuuNGK.Enabled = true;
            btnThemNGK.Enabled = false;
            ResetValueMatHang(); //Xoá trắng các textbox
            txtMaNGKNGK.Enabled = true; //cho phép nhập mới
            txtMaNGKNGK.Focus();
        }

        private void btnThemLNGK_Click(object sender, EventArgs e)
        {
            btnCapNhatLNGK.Enabled = false;
            btnXoaLNGK.Enabled = false;
            btnHuyLNGK.Enabled = true;
            btnLuuLNGK.Enabled = true;
            btnThemLNGK.Enabled = false;
            ResetValueLoaiMatHang(); //Xoá trắng các textbox
            txtMaLoaiNGKLNGK.Enabled = true; //cho phép nhập mới
            txtMaLoaiNGKLNGK.Focus();
        }

        private void btnThemHDN_Click(object sender, EventArgs e)
        {
            btnCapNhatHDN.Enabled = false;
            btnXoaHDN.Enabled = false;
            btnHuyHDN.Enabled = true;
            btnLuuHDN.Enabled = true;
            btnThemHDN.Enabled = false;
            ResetValueHoaDonNhap(); //Xoá trắng các textbox
            txtMaHDHDN.Enabled = true; //cho phép nhập mới
            txtMaHDHDN.Focus();
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            btnCapNhatHD.Enabled = false;
            btnXoaHD.Enabled = false;
            btnHuyHD.Enabled = true;
            btnLuuHD.Enabled = true;
            btnThemHD.Enabled = false;
            ResetValueHoaDon(); //Xoá trắng các textbox
            txtMaHDHD.Enabled = true; //cho phép nhập mới
            txtMaHDHD.Focus();
        }
    }
}
