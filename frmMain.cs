﻿using System;
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
            //dgvCuaHang.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            //dgvCuaHang.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
            //AAAAAAAAAAAAAAAAAAAAAAA
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Functions.Disconnect(); //Đóng kết nối
            this.Close();
        }
        // Cell Click
        // Nhân Viên
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
        // Khách Hàng
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
        // Nhà Cung Cấp
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
        // Mặt Hàng
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
        // Loại Mặt Hàng
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
        // Hóa Đơn Nhập
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
        // Hóa Đơn Xuất
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
    }
}
