using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSenDa.Models
{
    public class ViewModel
    {
        public SanPham sanPham { get; set; }
        public LoaiSanPham loaiSanPham { get; set; }
        public LoaiSenDa loaiSenDa { get; set; }
        public LoaiChauCay loaiChauCay { get; set; }
        public LoaiGiaThe loaiGiaThe { get; set; }
        public KhuyenMai khuyenMai { get; set; }
        public Kho kho { get; set; }
        public NhapKho nhapKho { get; set; }
        public DanhGia danhGia { get; set; }
        public KhachHang khachHang { get; set; }

        public IEnumerable<SanPham> ListSanPham { get; set; }
        public IEnumerable<LoaiSanPham> ListLoaiSanPham { get; set; }
        public IEnumerable<LoaiSenDa> ListLoaiSenDa { get; set; }
        public IEnumerable<LoaiChauCay> ListLoaiChauCay { get; set; }
        public IEnumerable<LoaiGiaThe> ListLoaiGiaThe { get; set; }
        public IEnumerable<KhuyenMai> ListKhuyenMai { get; set; }
        public IEnumerable<Kho> ListKho { get; set; }
        public IEnumerable<NhapKho> ListNhapKho { get; set; }
        public IEnumerable<DanhGia> ListDanhGia { get; set; }
        public IEnumerable<DanhGia> ListKhachHang { get; set; }


        //public SanPham[] ListSanPham { get; set; }
        //public LoaiSanPham[] ListLoaiSanPham { get; set; }
        //public LoaiSenDa[] ListLoaiSenDa { get; set; }
        //public LoaiChauCay[] ListLloaiChauCay { get; set; }
        //public LoaiGiaThe[] ListLoaiGiaThe { get; set; }
        //public KhuyenMai[] ListKhuyenMai { get; set; }
        //public Kho[] ListKho { get; set; }
        //public NhapKho[] ListNhapKho { get; set; }
        //public DanhGia[] ListDanhGia { get; set; }

    }
}