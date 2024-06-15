using AutoMapper;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Commands;
using InternSystem.Application.Features.CauHoiCongNgheManagement.Models;
using InternSystem.Application.Features.CauHoiManagement.Commands;
using InternSystem.Application.Features.CauHoiManagement.Models;
using InternSystem.Application.Features.DuAnManagement.Commands;
using InternSystem.Application.Features.DuAnManagement.Models;
using InternSystem.Application.Features.InternManagement.Commands;
using InternSystem.Application.Features.InternManagement.Models;
using InternSystem.Application.Features.Interview.Commands;
using InternSystem.Application.Features.Interview.Models;
using InternSystem.Application.Features.KyThucTapManagement.Commands;
using InternSystem.Application.Features.KyThucTapManagement.Models;
using InternSystem.Application.Features.Search.Models;
using InternSystem.Application.Features.ThongBaoManagement.Commands;
using InternSystem.Application.Features.ThongBaoManagement.Models;
using InternSystem.Application.Features.TruongHocManagement.Commands;
using InternSystem.Application.Features.TruongHocManagement.Models;
using InternSystem.Application.Features.LichPhongVanManagement.Commands;
using InternSystem.Application.Features.LichPhongVanManagement.Models;
using InternSystem.Application.Features.PhongVanManagement.Commands;
using InternSystem.Application.Features.PhongVanManagement.Models;
using InternSystem.Application.Features.User.Commands.User;
using InternSystem.Application.Features.User.Models;
using InternSystem.Application.Features.User.Models.UserModels;
using InternSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using InternSystem.Application.Features.Comunication.Models;
using InternSystem.Application.Features.Comunication.Commands;
using InternSystem.Application.Features.CongNgheDuAnManagement.Commands;
using InternSystem.Application.Features.CongNgheDuAnManagement.Models;
using InternSystem.Application.Features.CongNgheManagement.Commands;
using InternSystem.Application.Features.CongNgheManagement.Models;
using InternSystem.Application.Features.TaskManage.Commands.Create;
using InternSystem.Application.Features.TaskManage.Models;
using InternSystem.Application.Features.TaskManage.Commands.Update;
using InternSystem.Application.Features.TaskManage.Commands;

namespace InternSystem.Application.Common.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Auth Mappings
            CreateMap<AspNetUser, GetUserDetailResponse>();
            CreateMap<AspNetUser, GetAllUserResponse>().ReverseMap();
            CreateMap<AspNetUser, CreateUserCommand>().ReverseMap();
            CreateMap<AspNetUser, GetAllUserResponse>();
            CreateMap<AspNetUser, CreateUserResponse>().ReverseMap();
            CreateMap<IdentityRole, GetRoleResponse>().ReverseMap();
            CreateMap<KyThucTap, GetKyThucTapsByNameResponse>().ReverseMap();
            CreateMap<TruongHoc, GetTruongHocByNameResponse>().ReverseMap();
            CreateMap<InternInfo, GetInternInfosResponse>().ReverseMap();

            // Interview Mappings
            CreateMap<Comment, GetDetailCommentResponse>().ReverseMap();
            CreateMap<Comment, CreateCommentCommand>().ReverseMap();
            CreateMap<Comment, UpdateCommentCommand>().ReverseMap();

            // Report Mappings

            // Communication Mappings
            CreateMap<AspNetUser, GetAllUserResponse>();
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest); //convert int? to int

            // InternInfo mapping
            CreateMap<InternInfo, GetInternInfoByIdResponse>();
            CreateMap<InternInfo, GetInternInfoResponse>().ReverseMap();
            CreateMap<InternInfo, CreateInternInfoCommand>().ReverseMap();
            CreateMap<InternInfo, CreateInternInfoResponse>();
            CreateMap<InternInfo, UpdateInternInfoCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
            CreateMap<InternInfo, SelfUpdateInternInfoCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
            CreateMap<InternInfo, UpdateInternInfoResponse>();
            CreateMap<EmailUserStatus, GetDetailEmailUserStatusResponse>().ReverseMap();
            CreateMap<EmailUserStatus, CreateEmailUserStatusCommand>().ReverseMap();
            CreateMap<EmailUserStatus, UpdateEmailUserStatusCommand>().ReverseMap();

            // Csv Mappings
            CreateMap<ImportCsvDto, AspNetUser>().ReverseMap();
            CreateMap<ImportCsvDto, InternInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();

            // TruongHoc mapping
            CreateMap<TruongHoc, GetTruongHocByIdResponse>();
            CreateMap<TruongHoc, CreateTruongHocCommand>().ReverseMap();
            CreateMap<TruongHoc, CreateTruongHocResponse>();
            CreateMap<TruongHoc, UpdateTruongHocCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<TruongHoc, UpdateTruongHocResponse>();

            // KyThucTap Mapping
            CreateMap<KyThucTap, CreateKyThucTapCommand>().ReverseMap();
            CreateMap<KyThucTap, CreateKyThucTapResponse>();
            CreateMap<KyThucTap, GetKyThucTapByIdResponse>();
            CreateMap<KyThucTap, UpdateKyThucTapCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<KyThucTap, UpdateKyThucTapResponse>();

            // DuAn mapping
            CreateMap<DuAn, GetDuAnByIdResponse>();
            CreateMap<DuAn, CreateDuAnCommand>().ReverseMap();
            CreateMap<DuAn, CreateDuAnResponse>();
            CreateMap<DuAn, UpdateDuAnCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<DuAn, UpdateDuAnResponse>();

            // CauHoi mapping
            CreateMap<CauHoi, GetCauHoiByIdResponse>();
            CreateMap<CauHoi, GetAllCauHoiResponse>();
            CreateMap<CauHoi, CreateCauHoiCommand>().ReverseMap();
            CreateMap<CauHoi, CreateCauHoiResponse>();
            CreateMap<CauHoi, UpdateCauHoiCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CauHoi, UpdateCauHoiResponse>();

            // CauHoiCongNghe mapping
            CreateMap<CauHoiCongNghe, GetCauHoiCongNgheByIdResponse>();
            CreateMap<CauHoiCongNghe, GetAllCauHoiCongNgheResponse>();
            CreateMap<CauHoiCongNghe, CreateCauHoiCongNgheCommand>().ReverseMap();
            CreateMap<CauHoiCongNghe, CreateCauHoiCongNgheResponse>();
            CreateMap<CauHoiCongNghe, UpdateCauHoiCongNgheCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CauHoiCongNghe, UpdateCauHoiCongNgheResponse>();
            // PhongVan mapping
            CreateMap<PhongVan, CreatePhongVanCommand>().ReverseMap();
            CreateMap<PhongVan, CreatePhongVanResponse>();
            CreateMap<PhongVan, GetPhongVanByIdResponse>();
            CreateMap<PhongVan, UpdatePhongVanCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PhongVan, UpdatePhongVanResponse>();
            CreateMap<PhongVan, PhongVan>();

            CreateMap<NhomZalo, CreateNhomZaloResponse>().ReverseMap();
            CreateMap<NhomZalo, GetNhomZaloResponse>();
            CreateMap<NhomZalo, UpdateNhomZaloResponse>().ReverseMap();
            CreateMap<NhomZalo, DeleteNhomZaloResponse>().ReverseMap();

            CreateMap<AddUserToNhomZaloCommand, UserNhomZalo>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.IdNhomZaloChung, opt => opt.MapFrom(src => src.NhomZaloId))
                .ForMember(dest => dest.IdNhomZaloRieng, opt => opt.MapFrom(src => src.NhomZaloId));

            CreateMap<UserNhomZalo, GetUserNhomZaloResponse>().ReverseMap();

            // CongNghe mapping
            CreateMap<CongNghe, CreateCongNgheCommand>().ReverseMap();
            CreateMap<CongNghe, CreateCongNgheResponse>().ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.DateTime));
            CreateMap<CongNghe, UpdateCongNgheCommand>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CongNghe, UpdateCongNgheResponse>();
            CreateMap<CongNghe, GetAllCongNgheResponse>().ReverseMap();
            CreateMap<CongNghe, GetCongNgheByIdResponse>().ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.DateTime));

            // CongNgheDuAn mapping

            CreateMap<CongNgheDuAn, CreateCongNgheDuAnCommand>().ReverseMap();
            CreateMap<CongNgheDuAn, CreateCongNgheDuAnResponse>().ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.DateTime));
            CreateMap<CongNgheDuAn, UpdateCongNgheDuAnCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CongNgheDuAn, UpdateCongNgheDuAnResponse>();
            CreateMap<CongNgheDuAn, GetAllCongNgheDuAnResponse>().ReverseMap();
            CreateMap<CongNgheDuAn, GetCongNgheDuAnByIdResponse>().ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.DateTime));
            // ThongBao mapping 
            CreateMap<ThongBao, CreateThongBaoCommand>().ReverseMap();
            CreateMap<ThongBao, CreateThongBaoResponse>();
            CreateMap<ThongBao, UpdateThongBaoCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ThongBao, UpdateThongBaoResponse>();
            CreateMap<ThongBao, GetThongBaoByIdResponse>();

            // LichPhongVan mapping
            CreateMap<LichPhongVan, GetLichPhongVanByIdResponse>();
            CreateMap<LichPhongVan, CreateLichPhongVanCommand>().ReverseMap();
            CreateMap<LichPhongVan, CreateLichPhongVanResponse>();
            CreateMap<LichPhongVan, UpdateLichPhongVanCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<LichPhongVan, UpdateLichPhongVanResponse>();
            CreateMap<DateTimeOffset, DateTime>().ConvertUsing(dt => dt.UtcDateTime);

            CreateMap<LichPhongVan, LichPhongVan>();

            // PhongVan mapping
            CreateMap<PhongVan, CreatePhongVanCommand>().ReverseMap();
            CreateMap<PhongVan, CreatePhongVanResponse>();
            CreateMap<PhongVan, GetPhongVanByIdResponse>();
            CreateMap<PhongVan, UpdatePhongVanCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PhongVan, UpdatePhongVanResponse>();
            CreateMap<PhongVan, PhongVan>();
            // Task mapping
            CreateMap<Tasks, CreateTaskCommand>().ReverseMap();
            CreateMap<Tasks, TaskResponse>();
            CreateMap<Tasks, UpdateTaskCommand>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // User Task mapping
            CreateMap<ReportTask, TaskReportResponse>().ReverseMap();
            CreateMap<ReportTask, CreateTaskReportCommand>().ReverseMap();
            CreateMap<ReportTask, UpdateTaskReportCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            //Task Report mapping
            CreateMap<UserTask, UserTaskReponse>().ReverseMap();
            CreateMap<UserTask, CreateUserTaskCommand>().ReverseMap();
            CreateMap<UserTask, UpdateUserTaskCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Task - NhomZalo mapping
            CreateMap<NhomZaloTask, NhomZaloTaskReponse>().ReverseMap();
            CreateMap<NhomZaloTask, CreateNhomZaloTaskCommand>().ReverseMap();
            CreateMap<NhomZaloTask, UpdateNhomZaloTaskCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserNhomZalo, LeaderResponse>().ReverseMap();

        }
    }
}
