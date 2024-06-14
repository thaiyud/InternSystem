using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace InternSystem.Application.Features.InternManagement.Queries
{
    public class GetInternInfoQuery : IRequest<GetInternInfoResponse<InternInfo>>
    {
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public bool? GioiTinh { get; set; }
        public string? MSSV { get; set; }
        public string? EmailTruong { get; set; }
        public string? EmailCaNhan { get; set; }
        public string? Sdt { get; set; }
        public string? DiaChi { get; set; }
        public string? TruongHoc { get; set; }
        public string? KyThucTap { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public GetInternInfoQuery() { }

        public GetInternInfoQuery(
           string? hoTen,
           DateTime? ngaySinh,
           bool? gioiTinh,
           string? mssv,
           string? emailTruong,
           string? emailCaNhan,
           string? sdt,
           string? diaChi,
           string? truongHoc,
           string? kyThucTap,
           DateTime? startDate,
           DateTime? endDate)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            MSSV = mssv;
            EmailTruong = emailTruong;
            EmailCaNhan = emailCaNhan;
            Sdt = sdt;
            DiaChi = diaChi;
            TruongHoc = truongHoc;
            KyThucTap = kyThucTap;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
