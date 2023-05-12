﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PBL3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            this.GioHangs = new HashSet<GioHang>();
        }
    
        public int ID_SP { get; set; }
        public Nullable<int> ID_Danhmuc { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống tên sản phẩm")]
        public string TenSP { get; set; }
        public Nullable<int> ID_KM { get; set; }
        public Nullable<int> SoLuong { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống giá bán")]
        [RegularExpression("^[0-9]{1,}$", ErrorMessage = "Giá bán không hợp lệ (chỉ gồm số)")]
        public Nullable<int> GiaBan { get; set; }
        public string NhanHieuSP { get; set; }
        public string MauSP { get; set; }
        public string MoTaSP { get; set; }
        public string Anh { get; set; }
        public Nullable<int> Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DanhMucSP DanhMucSP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }
        public virtual KhuyenMai KhuyenMai { get; set; }
    }
}
