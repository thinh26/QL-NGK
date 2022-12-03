﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBN.Class;
using COMExcel = Microsoft.Office.Interop.Excel;

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
            sql = "SELECT HDN.MaHD, CTHDN.MaNGK, HDN.MaNCC, CTHDN.SoLuong, CTHDN.ThanhTien, HDN.NgayNhanHD FROM HoaDonNhap HDN join ChiTietHoaDonNhap CTHDN on HDN.MaHD = CTHDN.MaHD";
            tblhdn = Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvHDN.DataSource = tblhdn; //Nguồn dữ liệu
            sql = "SELECT HD.MaHD, HD.MaKH, HD.MaNV, NGK.TenNGK, CTHD.SoLuong,NGK.Gia * CTHD.SoLuong as ThanhTien, HD.NgayXuatHD FROM HoaDon HD join ChiTietHoaDon CTHD on HD.MaHD = CTHD.MaHD join NuocGiaiKhat NGK on CTHD.MaNGK = NGK.MaNGK";
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
            txtMaNGKHD.Enabled = false;
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
            btnThemChiTietHD.Enabled = false;
            btnThemChiTietHDN.Enabled = false;
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
            txtThanhTienHDN.Text = dgvHDN.CurrentRow.Cells["ThanhTien"].Value.ToString();
            dtNgayNhapHDHDN.Text = dgvHDN.CurrentRow.Cells["NgayNhanHD"].Value.ToString();
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
            txtTenNGKHD.Text = dgvHD.CurrentRow.Cells["TenNGK"].Value.ToString();
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
            txtThanhTienHDN.Text = "";
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
            txtTenNGKHD.Text = "";
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
                sql = "DELETE NhanVien WHERE MaNV='" + txtMaNVNV.Text + "'";
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
                sql = "DELETE KhachHang WHERE MaKH='" + txtMaKHKH.Text + "'";
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
                sql = "DELETE NhaCungCap WHERE MaNCC='" + txtMaNCCNCC.Text + "'";
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
                sql = "DELETE NuocGiaiKhat WHERE MaNGK='" + txtMaNGKNGK.Text + "'";
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
                sql = "DELETE LoaiNGK WHERE MaLoaiNGK='" + txtMaLoaiNGKLNGK.Text + "'";
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
            if (MessageBox.Show("Bạn có muốn xoá cả hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE ChiTietHoaDonNhap WHERE MaHD='" + txtMaHDHDN.Text + "'";
                Class.Functions.RunSqlDel(sql);
                sql = "DELETE HoaDonNhap WHERE MaHD='" + txtMaHDHDN.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValueNhanVien();
            }
            else if (MessageBox.Show("Bạn có muốn xoá chi tiết hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE ChiTietHoaDonNhap WHERE MaNGK='" + txtMaNGKHDN.Text + "' and MaHD='" + txtMaHDHDN.Text + "'";
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
            if (MessageBox.Show("Bạn có muốn xoá cả hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE ChiTietHoaDon WHERE MaHD='" + txtMaHDHD.Text + "'";
                Class.Functions.RunSqlDel(sql);
                sql = "DELETE HoaDon WHERE MaHD='" + txtMaHDHD.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValueNhanVien();
            }
            else if (MessageBox.Show("Bạn có muốn xoá chi tiết hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE ChiTietHoaDon WHERE MaNGK='" + txtMaNGKHD.Text + "' and MaHD='" + txtMaHDHD.Text + "'";
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
        //Nhân Viên
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
        //Khách Hàng
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
        //Nhà Cung Cấp
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
        //Mặt Hàng
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
        //Loại Mặt Hàng
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
        //Hóa Đơn Nhập
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
        //Hóa Đơn
        private void btnThemHD_Click(object sender, EventArgs e)
        {
            btnCapNhatHD.Enabled = false;
            btnXoaHD.Enabled = false;
            btnHuyHD.Enabled = true;
            btnLuuHD.Enabled = true;
            btnThemHD.Enabled = false;
            ResetValueHoaDon(); //Xoá trắng các textbox
            txtMaHDHD.Enabled = true; //cho phép nhập mới
            txtMaNGKHD.Enabled = true;
            txtTenNGKHD.Enabled = false;
            txtMaHDHD.Focus();
        }
        //Lưu Thay Đổi
        //Nhân Viên
        private void btnLuuNV_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaNVNV.Text.Trim().Length == 0) //Nếu chưa nhập mã nhân viên
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNVNV.Focus();
                return;
            }
            if (txtHoTenNV.Text.Trim().Length == 0) //Nếu chưa nhập tên nhân viên
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTenNV.Focus();
                return;
            }
            if (txtGioiNV.Text.Trim().Length == 0) //Nếu chưa nhập giới tính của nhân viên
            {
                MessageBox.Show("Bạn phải nhập giới tính của nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGioiNV.Focus();
                return;
            }
            if (txtDiaChiNV.Text.Trim().Length == 0) //Nếu chưa nhập địa chỉ của nhân viên
            {
                MessageBox.Show("Bạn phải nhập địa chỉ của nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChiNV.Focus();
                return;
            }
            if (txtSdtNV.Text.Trim().Length == 0) //Nếu chưa nhập số điện thoại của nhân viên
            {
                MessageBox.Show("Bạn phải nhập số điện thoai của nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSdtNV.Focus();
                return;
            }
            if (txtLuongNV.Text.Trim().Length == 0) //Nếu chưa nhập lương nhân viên
            {
                MessageBox.Show("Bạn phải nhập lương của nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLuongNV.Focus();
                return;
            }
            sql = "Select MaNV From NhanVien where MaNV=N'" + txtMaNVNV.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNVNV.Focus();
                return;
            }
            sql = "INSERT INTO NhanVien VALUES ('" + txtMaNVNV.Text + "','" + txtHoTenNV.Text + "','" + dtNgSinhNV.Text + "','" + txtGioiNV.Text + "','" + txtDiaChiNV.Text + "','" + txtSdtNV.Text + "','" + txtLuongNV.Text + "','" + txtChucVu.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValueNhanVien();
            btnXoaNV.Enabled = true;
            btnThemNV.Enabled = true;
            btnCapNhatNV.Enabled = true;
            btnHuyNV.Enabled = false;
            btnLuuNV.Enabled = false;
            txtMaNVNV.Enabled = false;
        }
        //Khách Hàng
        private void btnLuuKH_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaKHKH.Text.Trim().Length == 0) //Nếu chưa nhập mã khách hàng
            {
                MessageBox.Show("Bạn phải nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKHKH.Focus();
                return;
            }
            if (txtHotenKH.Text.Trim().Length == 0) //Nếu chưa nhập mã khách hàng
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHotenKH.Focus();
                return;
            }
            if (txtGioiKH.Text.Trim().Length == 0) //Nếu chưa nhập giới tính của khách hàng
            {
                MessageBox.Show("Bạn phải nhập giới tính của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGioiKH.Focus();
                return;
            }
            if (txtDiachiKH.Text.Trim().Length == 0) //Nếu chưa nhập địa chỉ của khách hàng
            {
                MessageBox.Show("Bạn phải nhập địa chỉ của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiachiKH.Focus();
                return;
            }
            if (txtSdtKH.Text.Trim().Length == 0) //Nếu chưa nhập số điện thoại của khách hàng
            {
                MessageBox.Show("Bạn phải nhập số điện thoại của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSdtKH.Focus();
                return;
            }
            sql = "Select MaKH From KhachHang where MaKH=N'" + txtMaKHKH.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã khách hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKHKH.Focus();
                return;
            }
            sql = "INSERT INTO KhachHang VALUES ('" + txtMaKHKH.Text + "','" + txtHotenKH.Text + "','" + txtGioiKH.Text + "','" + txtDiachiKH.Text + "','" + txtSdtKH.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValueKhachHang();
            btnXoaKH.Enabled = true;
            btnThemKH.Enabled = true;
            btnCapNhatKH.Enabled = true;
            btnHuyKH.Enabled = false;
            btnLuuKH.Enabled = false;
            txtMaKHKH.Enabled = false;
        }
        //Nhà cung cấp
        private void btnLuuNCC_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaNCCNCC.Text.Trim().Length == 0) //Nếu chưa nhập mã nhà cung cấp
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCCNCC.Focus();
                return;
            }
            if (txtTenNCCNCC.Text.Trim().Length == 0) //Nếu chưa nhập tên nhà cung cấp
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNCCNCC.Focus();
                return;
            }
            if (txtDiachiNCC.Text.Trim().Length == 0) //Nếu chưa nhập địa chỉ của nhà cung cấp
            {
                MessageBox.Show("Bạn phải nhập địa chỉ của nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiachiNCC.Focus();
                return;
            }
            if (txtSdtNCC.Text.Trim().Length == 0) //Nếu chưa nhập số điện thoại nhà cung cấp
            {
                MessageBox.Show("Bạn phải nhập số điện thoại nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSdtNCC.Focus();
                return;
            }
            sql = "Select MaNCC From NhaCungCap where MaNCC=N'" + txtMaNCCNCC.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhà cung cấp này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNCCNCC.Focus();
                return;
            }
            sql = "INSERT INTO NhaCungCap VALUES ('" + txtMaNCCNCC.Text + "','" + txtTenNCCNCC.Text + "','" + txtDiachiNCC.Text + "','" + txtSdtNCC.Text + "','" + dtThoiHanHopDongNCC.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValueNhaCungCap();
            btnXoaNCC.Enabled = true;
            btnThemNCC.Enabled = true;
            btnCapNhatNCC.Enabled = true;
            btnHuyNCC.Enabled = false;
            btnLuuNCC.Enabled = false;
            txtMaNCCNCC.Enabled = false;
        }
        //Mặt Hàng
        private void btnLuuNGK_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaNGKNGK.Text.Trim().Length == 0) //Nếu chưa nhập mã mặt hàng
            {
                MessageBox.Show("Bạn phải nhập mã mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNGKNGK.Focus();
                return;
            }
            if (txtTenNGKNGK.Text.Trim().Length == 0) //Nếu chưa nhập tên mặt hàng
            {
                MessageBox.Show("Bạn phải nhập tên mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNGKNGK.Focus();
                return;
            }
            if (txtGiaNGK.Text.Trim().Length == 0) //Nếu chưa nhập giá mặt hàng
            {
                MessageBox.Show("Bạn phải nhập giá mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiaNGK.Focus();
                return;
            }
            if (txtMaLoaiNGKNGK.Text.Trim().Length == 0) //Nếu chưa nhập mã loại mặt hàng
            {
                MessageBox.Show("Bạn phải nhập mã loại mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiNGKNGK.Focus();
                return;
            }
            if (txtMaNCCNGK.Text.Trim().Length == 0) //Nếu chưa nhập mã nhà cung cấp
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCCNGK.Focus();
                return;
            }
            if (txtThanhPhanNGK.Text.Trim().Length == 0) //Nếu chưa nhập thành phần loại mặt hàng
            {
                MessageBox.Show("Bạn phải nhập thành phần loại mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtThanhPhanNGK.Focus();
                return;
            }
            if (txtSoLuongNGK.Text.Trim().Length == 0) //Nếu chưa nhập số lượng mặt hàng
            {
                MessageBox.Show("Bạn phải nhập số lượng mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuongNGK.Focus();
                return;
            }
            sql = "Select MaNGK From NuocGiaiKhat where MaNGK=N'" + txtMaNGKNGK.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã mặt hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNGKNGK.Focus();
                return;
            }
            sql = "Select MaLoaiNGK From LoaiNGK where MaLoaiNGK=N'" + txtMaLoaiNGKNGK.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã loại mặt hàng này không tồn tại, bạn phải nhập mã loại mặt hàng có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoaiNGKNGK.Focus();
                return;
            }
            sql = "Select MaNCC From NhaCungCap where MaNCC=N'" + txtMaNCCNGK.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhà cung cấp này không tồn tại, bạn phải nhập mã nhà cung cấp có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNCCNGK.Focus();
                return;
            }
            sql = "INSERT INTO NuocGiaiKhat VALUES ('" + txtMaNGKNGK.Text + "','" + txtTenNGKNGK.Text + "','" + txtGiaNGK.Text + "','" + txtMaLoaiNGKNGK.Text + "','" + txtMaNCCNGK.Text + "','" + txtThanhPhanNGK.Text + "','" + dtNgaySanXuatNGK.Text + "','" + dtHanSuDungNGK.Text + "','" + txtSoLuongNGK.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValueMatHang();
            btnXoaNGK.Enabled = true;
            btnThemNGK.Enabled = true;
            btnCapNhatNGK.Enabled = true;
            btnHuyNGK.Enabled = false;
            btnLuuNGK.Enabled = false;
            txtMaNGKNGK.Enabled = false;
        }
        //Loại Mặt hàng
        private void btnLuuLNGK_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaLoaiNGKLNGK.Text.Trim().Length == 0) //Nếu chưa nhập mã loại mặt hàng
            {
                MessageBox.Show("Bạn phải nhập mã loại mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiNGKLNGK.Focus();
                return;
            }
            if (txtTenLoaiNGKLNGK.Text.Trim().Length == 0) //Nếu chưa nhập tên loại mặt hàng
            {
                MessageBox.Show("Bạn phải nhập tên loại mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenLoaiNGKLNGK.Focus();
                return;
            }
            sql = "Select MaLoaiNGK From LoaiNGK where MaLoaiNGK=N'" + txtMaLoaiNGKLNGK.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã loại mặt hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoaiNGKLNGK.Focus();
                return;
            }
            sql = "INSERT INTO LoaiNGK VALUES ('" + txtMaLoaiNGKLNGK.Text + "','" + txtTenLoaiNGKLNGK.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValueLoaiMatHang();
            btnXoaLNGK.Enabled = true;
            btnThemLNGK.Enabled = true;
            btnCapNhatLNGK.Enabled = true;
            btnHuyLNGK.Enabled = false;
            btnLuuLNGK.Enabled = false;
            txtMaLoaiNGKLNGK.Enabled = false;
        }
        //Hóa Đơn Nhập
        private void btnLuuHDN_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaHDHDN.Text.Trim().Length == 0) //Nếu chưa nhập mã hóa đơn nhập
            {
                MessageBox.Show("Bạn phải nhập mã hóa đơn nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHDHDN.Focus();
                return;
            }
            if (txtMaNGKHDN.Text.Trim().Length == 0) //Nếu chưa nhập mã mặt hàng
            {
                MessageBox.Show("Bạn phải nhập mã mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNGKHDN.Focus();
                return;
            }
            if (txtMaNCCHDN.Text.Trim().Length == 0) //Nếu chưa nhập mã nhà cung cấp
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCCHDN.Focus();
                return;
            }
            if (txtSoLuongHDN.Text.Trim().Length == 0) //Nếu chưa nhập số lượng hàng nhập
            {
                MessageBox.Show("Bạn phải nhập số lượng hàng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuongHDN.Focus();
                return;
            }
            sql = "Select MaLoaiNGK From LoaiNGK where MaLoaiNGK=N'" + txtMaHDHDN.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã loại hóa đơn nhập này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHDHDN.Focus();
                return;
            }
            sql = "Select MaNGK From NuocGiaiKhat where MaNGK=N'" + txtMaNGKHDN.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã mặt hàng này không tồn tại, bạn phải nhập mã mặt hàng có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNGKHDN.Focus();
                return;
            }
            sql = "Select MaNCC From NhaCungCap where MaNCC=N'" + txtMaNCCHDN.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhà cung cấp này không tồn tại, bạn phải nhập mã nhà cung cấp có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNCCNGK.Focus();
                return;
            }
            sql = "INSERT INTO HoaDonNhap VALUES ('" + txtMaHDHDN.Text + "','" + txtMaNGKHDN.Text + "','" + dtNgayNhapHDHDN.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            sql = "INSERT INTO ChiTietHoaDonNhap VALUES ('" + txtMaHDHDN.Text + "','" + txtMaNCCHDN.Text + "','" + txtSoLuongHDN.Text + "', '" + txtThanhTienHDN + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValueHoaDonNhap();
            btnXoaHDN.Enabled = true;
            btnThemHDN.Enabled = true;
            btnCapNhatHDN.Enabled = true;
            btnHuyHDN.Enabled = false;
            btnLuuHDN.Enabled = false;
            txtMaHDHDN.Enabled = false;
        }
        //Hóa Đơn Xuất
        private void btnLuuHD_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaHDHD.Text.Trim().Length == 0) //Nếu chưa nhập mã hóa đơn xuất
            {
                MessageBox.Show("Bạn phải nhập mã hóa đơn xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHDHD.Focus();
                return;
            }
            if (txtMaNGKHD.Text.Trim().Length == 0) //Nếu chưa nhập mã mặt hàng
            {
                MessageBox.Show("Bạn phải nhập mã mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNGKHD.Focus();
                return;
            }
            if (txtMaKHHD.Text.Trim().Length == 0) //Nếu chưa nhập mã khách hàng
            {
                MessageBox.Show("Bạn phải nhập mã khách Hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKHHD.Focus();
                return;
            }
            if (txtSoLuongHD.Text.Trim().Length == 0) //Nếu chưa nhập số lượng hàng xuất
            {
                MessageBox.Show("Bạn phải nhập số lượng hàng xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuongHD.Focus();
                return;
            }
            sql = "Select MaHD From HoaDon where MaHD=N'" + txtMaHDHD.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hoa đơn này đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHDHD.Focus();
                return;
            }
            sql = "Select MaNGK From NuocGiaiKhat where MaNGK=N'" + txtMaNGKHD.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã mặt hàng này không tồn tại, bạn phải nhập mã mặt hàng có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNGKHD.Focus();
                return;
            }
            sql = "Select MaKH From KhachHang where MaKH=N'" + txtMaKHHD.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã khách hàng này không tồn tại, bạn phải nhập mã khách hàng có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKHHD.Focus();
                return;
            }
            sql = "Select MaNV From NhanVien where MaNV=N'" + txtMaNVHD.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này không tồn tại, bạn phải nhập mã nhân viên có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNVHD.Focus();
                return;
            }
            sql = "INSERT INTO HoaDon VALUES ('" + txtMaHDHD.Text + "','" + txtMaKHHD.Text + "','" + txtMaNVHD.Text + "','" + dtNgayNhapHDHDN.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            sql = "INSERT INTO ChiTietHoaDon VALUES ('" + txtMaHDHD.Text + "','" + txtMaNGKHD.Text + "','" + txtSoLuongHD.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValueHoaDon();
            btnXoaHD.Enabled = true;
            btnThemHD.Enabled = true;
            btnCapNhatHD.Enabled = true;
            btnHuyHD.Enabled = false;
            btnLuuHD.Enabled = false;
            btnThemChiTietHDN.Enabled = false;
            txtMaHDHD.Enabled = false;
            txtMaNGKHD.Enabled = false;
        }
        //Thêm chi tiết Hóa Đơn Xuất
        private void btnThemChiTietHD_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaHDHD.Text.Trim().Length == 0) //Nếu chưa nhập mã hóa đơn xuất
            {
                MessageBox.Show("Bạn phải nhập mã hóa đơn xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHDHD.Focus();
                return;
            }
            if (txtMaNGKHD.Text.Trim().Length == 0) //Nếu chưa nhập mã mặt hàng
            {
                MessageBox.Show("Bạn phải nhập mã mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNGKHD.Focus();
                return;
            }
            if (txtSoLuongHD.Text.Trim().Length == 0) //Nếu chưa nhập số lượng hàng xuất
            {
                MessageBox.Show("Bạn phải nhập số lượng hàng xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuongHD.Focus();
                return;
            }
            sql = "Select MaHD From HoaDon where MaHD=N'" + txtMaHDHD.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hóa đơn này không tồn tại, bạn phải nhập mã hóa đơn có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHDHD.Focus();
                return;
            }
            sql = "Select MaNGK From NuocGiaiKhat where MaNGK=N'" + txtMaNGKHD.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã mặt hàng này không tồn tại, bạn phải nhập mã mặt hàng có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNGKHD.Focus();
                return;
            }
            sql = "INSERT INTO ChiTietHoaDon VALUES ('" + txtMaHDHD.Text + "','" + txtMaNGKHD.Text + "','" + txtSoLuongHD.Text + "')";
            Class.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValueHoaDon();
            btnXoaHD.Enabled = true;
            btnThemHD.Enabled = true;
            btnCapNhatHD.Enabled = true;
            btnHuyHD.Enabled = false;
            btnLuuHD.Enabled = false;
            btnThemChiTietHD.Enabled = false;
            txtMaHDHD.Enabled = false;
        }
        //Cập nhật
        //Nhân Viên
        private void btnCapNhatNV_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblnv.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtHoTenNV.Text.Trim().Length == 0) //Nếu chưa nhập tên nhân viên
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHoTenNV.Focus();
                return;
            }
            if (txtGioiNV.Text.Trim().Length == 0) //Nếu chưa nhập giới tính của nhân viên
            {
                MessageBox.Show("Bạn phải nhập giới tính của nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGioiNV.Focus();
                return;
            }
            if (txtDiaChiNV.Text.Trim().Length == 0) //Nếu chưa nhập địa chỉ của nhân viên
            {
                MessageBox.Show("Bạn phải nhập địa chỉ của nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChiNV.Focus();
                return;
            }
            if (txtSdtNV.Text.Trim().Length == 0) //Nếu chưa nhập số điện thoại của nhân viên
            {
                MessageBox.Show("Bạn phải nhập số điện thoai của nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSdtNV.Focus();
                return;
            }
            if (txtLuongNV.Text.Trim().Length == 0) //Nếu chưa nhập lương nhân viên
            {
                MessageBox.Show("Bạn phải nhập lương của nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLuongNV.Focus();
                return;
            }
            sql = "UPDATE NhanVien SET HoTen='" + txtHoTenNV.Text.ToString() +
                    "',Gioi='" + txtGioiNV.Text.ToString() +
                    "',DiaChi='" + txtDiaChiNV.Text.ToString() +
                    "',Sdt='" + txtSdtNV.Text.ToString() +
                    "',Luong='" + txtLuongNV.Text.ToString() +
                    "',ChucVu='" + txtChucVu.Text.ToString() +
                    "',NgSinh='" + dtNgSinhNV.Text.ToString() +
                    "' WHERE MaNV='" + txtMaNVNV.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValueNhanVien();
            btnHuyNV.Enabled = false;
        }
        //Khách Hàng
        private void btnCapNhatKH_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblkh.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtHotenKH.Text.Trim().Length == 0) //Nếu chưa nhập tên khách hàng
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHotenKH.Focus();
                return;
            }
            if (txtGioiKH.Text.Trim().Length == 0) //Nếu chưa nhập giới tính của khách hàng
            {
                MessageBox.Show("Bạn phải nhập giới tính của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGioiKH.Focus();
                return;
            }
            if (txtDiachiKH.Text.Trim().Length == 0) //Nếu chưa nhập địa chỉ của khách hàng
            {
                MessageBox.Show("Bạn phải nhập địa chỉ của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiachiKH.Focus();
                return;
            }
            if (txtSdtKH.Text.Trim().Length == 0) //Nếu chưa nhập số điện thoại của khách hàng
            {
                MessageBox.Show("Bạn phải nhập số điện thoại của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSdtKH.Focus();
                return;
            }
            sql = "UPDATE KhachHang SET HoTen='" + txtHotenKH.Text.ToString() +
                    "',Gioi='" + txtGioiKH.Text.ToString() +
                    "',DiaChi='" + txtDiachiKH.Text.ToString() +
                    "',Sdt='" + txtSdtKH.Text.ToString() +
                    "' WHERE MaKH='" + txtMaKHKH.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValueKhachHang();
            btnHuyKH.Enabled = false;
        }
        //Nhà Cung Cấp
        private void btnCapNhatNCC_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblncc.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNCCNCC.Text.Trim().Length == 0) //Nếu chưa nhập tên nhà cung cấp
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNCCNCC.Focus();
                return;
            }
            if (txtDiachiNCC.Text.Trim().Length == 0) //Nếu chưa nhập địa chỉ của nhà cung cấp
            {
                MessageBox.Show("Bạn phải nhập địa chỉ của nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiachiNCC.Focus();
                return;
            }
            if (txtSdtNCC.Text.Trim().Length == 0) //Nếu chưa nhập số điện thoại nhà cung cấp
            {
                MessageBox.Show("Bạn phải nhập số điện thoại nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSdtNCC.Focus();
                return;
            }
            sql = "UPDATE NhaCungCap SET TenNCC='" + txtTenNCCNCC.Text.ToString() +
                    "',Diachi='" + txtDiachiNCC.Text.ToString() +
                    "',ThoiHanHopDOng='" + dtThoiHanHopDongNCC.Text.ToString() +
                    "',Sdt='" + txtSdtNCC.Text.ToString() +
                    "' WHERE MaNCC='" + txtMaNCCNCC.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValueNhaCungCap();
            btnHuyNCC.Enabled = false;
        }
        //Mặt Hàng
        private void btnCapNhatNGK_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblngk.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNGKNGK.Text.Trim().Length == 0) //Nếu chưa nhập tên mặt hàng
            {
                MessageBox.Show("Bạn phải nhập tên mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNGKNGK.Focus();
                return;
            }
            if (txtGiaNGK.Text.Trim().Length == 0) //Nếu chưa nhập giá mặt hàng
            {
                MessageBox.Show("Bạn phải nhập giá mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiaNGK.Focus();
                return;
            }
            if (txtMaLoaiNGKNGK.Text.Trim().Length == 0) //Nếu chưa nhập mã loại mặt hàng
            {
                MessageBox.Show("Bạn phải nhập mã loại mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiNGKNGK.Focus();
                return;
            }
            if (txtMaNCCNGK.Text.Trim().Length == 0) //Nếu chưa nhập mã nhà cung cấp
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCCNGK.Focus();
                return;
            }
            if (txtThanhPhanNGK.Text.Trim().Length == 0) //Nếu chưa nhập thành phần loại mặt hàng
            {
                MessageBox.Show("Bạn phải nhập thành phần loại mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtThanhPhanNGK.Focus();
                return;
            }
            if (txtSoLuongNGK.Text.Trim().Length == 0) //Nếu chưa nhập số lượng mặt hàng
            {
                MessageBox.Show("Bạn phải nhập số lượng mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuongNGK.Focus();
                return;
            }
            sql = "Select MaLoaiNGK From LoaiNGK where MaLoaiNGK=N'" + txtMaLoaiNGKNGK.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã loại mặt hàng này không tồn tại, bạn phải nhập mã loại mặt hàng có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLoaiNGKNGK.Focus();
                return;
            }
            sql = "Select MaNCC From NhaCungCap where MaNCC=N'" + txtMaNCCNGK.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhà cung cấp này không tồn tại, bạn phải nhập mã nhà cung cấp có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNCCNGK.Focus();
                return;
            }
            sql = "UPDATE NuocGiaiKhat SET TenNGK='" + txtTenNGKNGK.Text.ToString() +
                    "',Gia='" + txtGiaNGK.Text.ToString() +
                    "',MaLoaiNGK='" + txtMaLoaiNGKNGK.Text.ToString() +
                    "',MaNCC='" + txtMaNCCNGK.Text.ToString() +
                    "',ThanhPhan='" + txtThanhPhanNGK.Text.ToString() +
                    "',NgaySanXuat='" + dtNgaySanXuatNGK.Text.ToString() +
                    "',HanSuDung='" + dtHanSuDungNGK.Text.ToString() +
                    "',SoLuong='" + txtSoLuongNGK.Text.ToString() +
                    "' WHERE MaNGK='" + txtMaNGKNGK.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValueMatHang();
            btnHuyNGK.Enabled = false;
        }
        //Loại Mặt Hàng
        private void btnCapNhatLNGK_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tbllngk.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenLoaiNGKLNGK.Text.Trim().Length == 0) //Nếu chưa nhập tên loại mặt hàng
            {
                MessageBox.Show("Bạn phải nhập tên loại mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenLoaiNGKLNGK.Focus();
                return;
            }
            sql = "UPDATE LoaiNGK SET TenLoaiNGK='" + txtTenLoaiNGKLNGK.Text.ToString() +
                    "' WHERE MaLoaiNGK='" + txtMaLoaiNGKLNGK.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValueLoaiMatHang();
            btnHuyLNGK.Enabled = false;
        }

        private void btnCapNhatHDN_Click(object sender, EventArgs e)
        {
        }

        private void btnCapNhatHD_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblncc.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNGKHD.Text.Trim().Length == 0) //Nếu chưa nhập mã mặt hàng
            {
                MessageBox.Show("Bạn phải nhập mã mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNGKHD.Focus();
                return;
            }
            if (txtMaKHHD.Text.Trim().Length == 0) //Nếu chưa nhập mã khách hàng
            {
                MessageBox.Show("Bạn phải nhập mã khách Hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKHHD.Focus();
                return;
            }
            if (txtSoLuongHD.Text.Trim().Length == 0) //Nếu chưa nhập số lượng hàng xuất
            {
                MessageBox.Show("Bạn phải nhập số lượng hàng xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuongHD.Focus();
                return;
            }
            sql = "Select MaNGK From NuocGiaiKhat where MaNGK=N'" + txtMaNGKHD.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã mặt hàng này không tồn tại, bạn phải nhập mã mặt hàng có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNGKHD.Focus();
                return;
            }
            sql = "Select MaKH From KhachHang where MaKH=N'" + txtMaKHHD.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhà cung cấp này không tồn tại, bạn phải nhập mã nhà cung cấp có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKHHD.Focus();
                return;
            }
            sql = "Select MaNV From NhanVien where MaNV=N'" + txtMaNVHD.Text.Trim() + "'";
            if (!Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này không tồn tại, bạn phải nhập mã nhân viên có tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNVHD.Focus();
                return;
            }
            sql = "UPDATE HoaDon SET MaKH='" + txtMaKHHD.Text.ToString() +
                    "',MaNV='" + txtMaNVHD.Text.ToString() +
                    "',NgayXuatHD='" + dtNgayXuatHDHD.Text.ToString() +
                    "' WHERE MaHD='" + txtMaHDHD.Text + "'";
            Class.Functions.RunSQL(sql);
            sql = "UPDATE ChiTietHoaDon SET MaNGK='" + txtMaNGKHD.Text.ToString() +
                    "',SoLuong='" + txtSoLuongHD.Text.ToString() +
                    "' WHERE MaNGK='" + txtMaNGKHD.Text + "' and MaHD='" + txtMaHDHD.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValueHoaDon();
            btnHuyHD.Enabled = false;
        }

        private void btnInHDX_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman";
            //Font chữ và định dạng từng cái
            //Công ty nước giải khát Thiện Chí
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:C1"].Value = "Công ty nước giải khát Thiện Chí";
            //Nha Trang - Khánh Hoà
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nha Trang - Khánh Hoà";
            //Điện thoại
            exRange.Range["A3:C3"].MergeCells = true;
            exRange.Range["A3:C3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:C3"].Value = "Điện thoại: (04)38526419";
            //HOÁ ĐƠN BÁN HÀNG
            exRange.Range["D2:F2"].Font.Size = 16;
            exRange.Range["D2:F2"].Font.Bold = true;
            exRange.Range["D2:F2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["D2:F2"].MergeCells = true;
            exRange.Range["D2:F2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D2:F2"].Value = "HÓA ĐƠN BÁN HÀNG";
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "select HD.MaHD,KH.Hoten, KH.DiaChi, KH.Sdt from HoaDon HD join KhachHang KH on HD.MaKH = KH.MaKH where HD.MaHD='" + txtMaHDHD.Text + "' and HD.MaKH ='" + txtMaKHHD.Text + "'";
            tblThongtinHD = Functions.GetDataToTable(sql);
            exRange.Range["C6:D9"].Font.Size = 12;
            exRange.Range["C6:C9"].ColumnWidth = 13;
            exRange.Range["C6:C9"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["D6:D9"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignLeft;
            exRange.Range["C6:C6"].Value = "Mã hóa đơn:";
            exRange.Range["D6:D6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["C7:C7"].Value = "Khách hàng:";
            exRange.Range["D7:F7"].MergeCells = true;
            exRange.Range["D7:F7"].Value = tblThongtinHD.Rows[0][1].ToString();
            exRange.Range["C8:C8"].Value = "Địa chỉ:";
            exRange.Range["D8:F8"].MergeCells = true;
            exRange.Range["D8:F8"].Value = tblThongtinHD.Rows[0][2].ToString();
            exRange.Range["C9:C9"].Value = "Điện thoại:";
            exRange.Range["D9:D9"].ColumnWidth = 15;
            exRange.Range["D9:D9"].Value = tblThongtinHD.Rows[0][3].ToString();
            //Lấy thông tin các mặt hàng
            sql = "SELECT NGK.TenNGK, CTHD.SoLuong,NGK.Gia as DonGia,NGK.Gia * CTHD.SoLuong as ThanhTien FROM HoaDon HD join ChiTietHoaDon CTHD on HD.MaHD = CTHD.MaHD join NuocGiaiKhat NGK on CTHD.MaNGK = NGK.MaNGK where HD.MAHD = '" + txtMaHDHD.Text + "'";
            tblThongtinHang = Functions.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["B12:F12"].Font.Bold = true;
            exRange.Range["B12:F17"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C12:C12"].ColumnWidth = 20;
            exRange.Range["D12:G12"].ColumnWidth = 12;
            exRange.Range["B12:B12"].Value = "STT";
            exRange.Range["C12:C12"].Value = "Tên hàng";
            exRange.Range["D12:D12"].Value = "Số lượng";
            exRange.Range["E12:E12"].Value = "Đơn giá";
            exRange.Range["F12:F12"].Value = "Thành tiền";
            //Các hàng đã bán
            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 2 từ dòng 13
                exSheet.Cells[2][hang + 13] = hang + 1;
                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 3, dòng 13
                {
                    exSheet.Cells[cot + 3][hang + 13] = tblThongtinHang.Rows[hang][cot].ToString();
                    if (cot == 4) exSheet.Cells[cot + 3][hang + 13] = tblThongtinHang.Rows[hang][cot].ToString() + "%";
                }
            }
            //Tổng tiền
            exRange = exSheet.Cells[cot+1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            DataTable tblTongTien;
            sql = "SELECT sum(NGK.Gia * CTHD.SoLuong) as TongTien FROM HoaDon HD join ChiTietHoaDon CTHD on HD.MaHD = CTHD.MaHD join NuocGiaiKhat NGK on CTHD.MaNGK = NGK.MaNGK where HD.MAHD = '" + txtMaHDHD.Text + "'";
            tblTongTien = Functions.GetDataToTable(sql);
            exRange = exSheet.Cells[cot + 2][hang + 14];
            exRange.Value2 = tblTongTien.Rows[0][0].ToString();
            //In ngày xuất hoá đơn
            DataTable tblNgayXuatHD;
            sql = "SELECT NgayXuatHD from HoaDon where MaHD = '" + txtMaHDHD.Text + "'";
            tblNgayXuatHD = Functions.GetDataToTable(sql);
            DateTime d = Convert.ToDateTime(tblNgayXuatHD.Rows[0][0]);
            exRange = exSheet.Cells[cot][hang + 17];
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Value2 = "Nha Trang, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            //Tên người bán hàng
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            DataTable tblTenNguoiBanHang;
            sql = "select NV.HoTen from HoaDon HD join NhanVien NV on HD.MaNV = NV.MaNV where HD.MaHD = '" + txtMaHDHD.Text + "' and HD.MaNV = '" + txtMaNVHD.Text + "'";
            tblTenNguoiBanHang = Functions.GetDataToTable(sql);
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Bold = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblTenNguoiBanHang.Rows[0][0].ToString();

            exSheet.Name = "Hóa đơn bán";
            exApp.Visible = true;
        }

        private void btnInHDN_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman";
            //Font chữ và định dạng từng cái
            //Công ty nước giải khát Thiện Chí
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:C1"].Value = "Công ty nước giải khát Thiện Chí";
            //Nha Trang - Khánh Hoà
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nha Trang - Khánh Hoà";
            //Điện thoại
            exRange.Range["A3:C3"].MergeCells = true;
            exRange.Range["A3:C3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:C3"].Value = "Điện thoại: (04)38526419";
            //HOÁ ĐƠN NHẬP HÀNG
            exRange.Range["D2:F2"].Font.Size = 16;
            exRange.Range["D2:F2"].Font.Bold = true;
            exRange.Range["D2:F2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["D2:F2"].MergeCells = true;
            exRange.Range["D2:F2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D2:F2"].Value = "HÓA ĐƠN NHẬP HÀNG";
            // Biểu diễn thông tin chung của hóa đơn nhập
            sql = "SELECT HDN.MaHD, NCC.TenNCC, NCC.Diachi, NCC.Sdt FROM HoaDonNhap HDN join ChiTietHoaDonNhap CTHDN on HDN.MaHD = CTHDN.MaHD join NhaCungCap NCC on HDN.MaNCC = NCC.MaNCC where HDN.MaHD = '" + txtMaHDHDN.Text + "'";
            tblThongtinHD = Functions.GetDataToTable(sql);
            exRange.Range["C6:D9"].Font.Size = 12;
            exRange.Range["C6:C9"].ColumnWidth = 13;
            exRange.Range["C6:C9"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["D6:D9"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignLeft;
            exRange.Range["C6:C6"].Value = "Mã hoá đơn:";
            exRange.Range["D6:D6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["C7:C7"].Value = "Tên nhà cung cấp:";
            exRange.Range["D7:F7"].MergeCells = true;
            exRange.Range["D7:F7"].Value = tblThongtinHD.Rows[0][1].ToString();
            exRange.Range["C8:C8"].Value = "Địa chỉ:";
            exRange.Range["D8:F8"].MergeCells = true;
            exRange.Range["D8:F8"].Value = tblThongtinHD.Rows[0][2].ToString();
            exRange.Range["C9:C9"].Value = "Điện thoại:";
            exRange.Range["D9:D9"].ColumnWidth = 15;
            exRange.Range["D9:D9"].Value = tblThongtinHD.Rows[0][3].ToString();
            //Lấy thông tin các mặt hàng
            sql = "SELECT NGK.TenNGK, CTHDN.SoLuong, CTHDN.ThanhTien FROM HoaDonNhap HDN join ChiTietHoaDonNhap CTHDN on HDN.MaHD = CTHDN.MaHD join NuocGiaiKhat NGK on CTHDN.MaNGK = NGK.MaNGK where HDN.MaHD = '" + txtMaHDHDN.Text + "'";
            tblThongtinHang = Functions.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["B12:F12"].Font.Bold = true;
            exRange.Range["B12:F17"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C12:C12"].ColumnWidth = 20;
            exRange.Range["D12:G12"].ColumnWidth = 12;
            exRange.Range["B12:B12"].Value = "STT";
            exRange.Range["C12:C12"].Value = "Tên hàng";
            exRange.Range["D12:D12"].Value = "Số lượng";
            exRange.Range["E12:E12"].Value = "Thành tiền";
            //Các hàng đã bán
            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 2 từ dòng 13
                exSheet.Cells[2][hang + 13] = hang + 1;
                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 3, dòng 13
                {
                    exSheet.Cells[cot + 3][hang + 13] = tblThongtinHang.Rows[hang][cot].ToString();
                    if (cot == 4) exSheet.Cells[cot + 3][hang + 13] = tblThongtinHang.Rows[hang][cot].ToString() + "%";
                }
            }
            //In ngày xuất hoá đơn
            DataTable tblNgayXuatHD;
            sql = "SELECT NgayNhanHD from HoaDonNhap where MaHD = '" + txtMaHDHDN.Text + "'";
            tblNgayXuatHD = Functions.GetDataToTable(sql);
            DateTime d = Convert.ToDateTime(tblNgayXuatHD.Rows[0][0]);
            exRange = exSheet.Cells[cot][hang + 17];
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Value2 = "Nha Trang, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            //Tên người nhập hàng
            exRange.Range["B2:C2"].MergeCells = true;
            exRange.Range["B2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B2:C2"].Value = "Người nhập hàng";
            DataTable tblTenNguoiBanHang;
            sql = "select NV.HoTen from HoaDon HD join NhanVien NV on HD.MaNV = NV.MaNV where HD.MaHD = '" + txtMaHDHD.Text + "' and HD.MaNV = '" + txtMaNVHD.Text + "'";
            tblTenNguoiBanHang = Functions.GetDataToTable(sql);
            exRange.Range["B6:C6"].MergeCells = true;
            exRange.Range["B6:C6"].Font.Bold = true;
            exRange.Range["B6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["B6:C6"].Value = "Nguyễn Đức Thịnh";

            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;
        }
    }
}
