using AutoMapper;
using InternSystem.Application.Features.AuthManagement.LoginManagement.Models;
using InternSystem.Application.Features.AuthManagement.RoleManagement.Models;
using InternSystem.Application.Features.AuthManagement.UserManagement.Commands;
using InternSystem.Application.Features.AuthManagement.UserManagement.Models;
using InternSystem.Application.Features.ClaimManagement.Commands;
using InternSystem.Application.Features.ClaimManagement.Models;
using InternSystem.Application.Features.ComunicationManagement.ThongBaoManagement.Commands;
using InternSystem.Application.Features.ComunicationManagement.ThongBaoManagement.Models;
using InternSystem.Application.Features.GroupAndTeamManagement.NhomZaloManagement.Commands;
using InternSystem.Application.Features.GroupAndTeamManagement.NhomZaloManagement.Models;
using InternSystem.Application.Features.GroupAndTeamManagement.UserNhomZaloManagement.Commands;
using InternSystem.Application.Features.GroupAndTeamManagement.UserNhomZaloManagement.Models;
using InternSystem.Application.Features.InternManagement.CommentManagement.Commands;
using InternSystem.Application.Features.InternManagement.CommentManagement.Models;
using InternSystem.Application.Features.InternManagement.CuocPhongVanManagement.Commands;
using InternSystem.Application.Features.InternManagement.CuocPhongVanManagement.Models;
using InternSystem.Application.Features.InternManagement.EmailUserStatusManagement.Commands;
using InternSystem.Application.Features.InternManagement.EmailUserStatusManagement.Models;
using InternSystem.Application.Features.InternManagement.InternManagement.Commands;
using InternSystem.Application.Features.InternManagement.InternManagement.Models;
using InternSystem.Application.Features.InternManagement.KyThucTapManagement.Commands;
using InternSystem.Application.Features.InternManagement.KyThucTapManagement.Models;
using InternSystem.Application.Features.InternManagement.LichPhongVanManagement.Commands;
using InternSystem.Application.Features.InternManagement.LichPhongVanManagement.Models;
using InternSystem.Application.Features.InternManagement.LichPhongVanManagement.Queries;
using InternSystem.Application.Features.InternManagement.TruongHocManagement.Commands;
using InternSystem.Application.Features.InternManagement.TruongHocManagement.Models;
using InternSystem.Application.Features.InternManagement.UserViTriManagement.Commands;
using InternSystem.Application.Features.InternManagement.UserViTriManagement.Models;
using InternSystem.Application.Features.InternManagement.ViTriManagement.Commands;
using InternSystem.Application.Features.InternManagement.ViTriManagement.Models;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.CongNgheDuAnManagement.Commands;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.CongNgheDuAnManagement.Models;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.CongNgheManagement.Commands;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.CongNgheManagement.Models;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.DuAnManagement.Commands;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.DuAnManagement.Models;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.UserDuAnManagement.Commands;
using InternSystem.Application.Features.ProjectAndTechnologyManagement.UserDuAnManagement.Models;
using InternSystem.Application.Features.QuestionManagement.CauHoiCongNgheManagement.Commands;
using InternSystem.Application.Features.QuestionManagement.CauHoiCongNgheManagement.Models;
using InternSystem.Application.Features.QuestionManagement.CauHoiManagement.Commands;
using InternSystem.Application.Features.QuestionManagement.CauHoiManagement.Models;
using InternSystem.Application.Features.TasksAndReports.NhomZaloTaskManagement.Commands;
using InternSystem.Application.Features.TasksAndReports.NhomZaloTaskManagement.Models;
using InternSystem.Application.Features.TasksAndReports.ReportTaskManagement.Commands;
using InternSystem.Application.Features.TasksAndReports.ReportTaskManagement.Models;
using InternSystem.Application.Features.TasksAndReports.TaskManagement.Commands;
using InternSystem.Application.Features.TasksAndReports.TaskManagement.Models;
using InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Commands;
using InternSystem.Application.Features.TasksAndReports.UserTaskManagement.Models;
using InternSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

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
            CreateMap<AspNetUser, GetCurrentUserResponse>();
            CreateMap<AspNetUser, CreateUserResponse>().ReverseMap();
            CreateMap<IdentityRole, GetRoleResponse>().ReverseMap();
            CreateMap<KyThucTap, GetKyThucTapsByNameResponse>().ReverseMap();
            CreateMap<TruongHoc, GetTruongHocByNameResponse>().ReverseMap();
            CreateMap<AspNetUser, GetPagedUsersResponse>().ReverseMap();
            CreateMap<IdentityRole, GetPagedRolesResponse>().ReverseMap();

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
            CreateMap<InternInfo, GetPagedInternInfosResponse>().ReverseMap();

            CreateMap<EmailUserStatus, GetDetailEmailUserStatusResponse>().ReverseMap();
            CreateMap<EmailUserStatus, CreateEmailUserStatusCommand>().ReverseMap();
            CreateMap<EmailUserStatus, UpdateEmailUserStatusCommand>().ReverseMap();

            // Csv Mappings
            CreateMap<ImportDataDto, AspNetUser>().ReverseMap();
            CreateMap<ImportDataDto, InternInfo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();

            // TruongHoc mapping
            CreateMap<TruongHoc, GetTruongHocByIdResponse>();
            CreateMap<TruongHoc, CreateTruongHocCommand>().ReverseMap();
            CreateMap<TruongHoc, CreateTruongHocResponse>();
            CreateMap<TruongHoc, UpdateTruongHocCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<TruongHoc, UpdateTruongHocResponse>();
            CreateMap<TruongHoc, GetAllTruongHocResponse>();
            CreateMap<TruongHoc, GetPagedTruongHocsResponse>().ReverseMap();

            // KyThucTap Mapping
            CreateMap<KyThucTap, CreateKyThucTapCommand>().ReverseMap();
            CreateMap<KyThucTap, CreateKyThucTapResponse>();
            CreateMap<KyThucTap, GetKyThucTapByIdResponse>();
            CreateMap<KyThucTap, GetAllKyThucTapResponse>();
            CreateMap<KyThucTap, GetKyThucTapByNameResponse>().ReverseMap();
            CreateMap<KyThucTap, UpdateKyThucTapCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<KyThucTap, UpdateKyThucTapResponse>();
            CreateMap<KyThucTap, GetPagedKyThucTapsResponse>().ReverseMap();

            // DuAn mapping
            CreateMap<DuAn, GetDuAnByIdResponse>()
                .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.Leader.HoVaTen));
            CreateMap<DuAn, CreateDuAnCommand>().ReverseMap();
            CreateMap<DuAn, CreateDuAnResponse>();
            CreateMap<DuAn, UpdateDuAnCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<DuAn, UpdateDuAnResponse>();
            CreateMap<DuAn, GetAllDuAnResponse>()
                .ForMember(dest => dest.LeaderName, opt => opt.MapFrom(src => src.Leader.HoVaTen))
                .ForMember(dest => dest.TenCongNghe, opt => opt.MapFrom(src => src.CongNgheDuAns.Select(cnda => cnda.CongNghe.Ten).ToList()))
                .ReverseMap();
            CreateMap<DuAn, GetDuAnByTenResponse>();
            CreateMap<DuAn, GetPagedDuAnsResponse>().ReverseMap();


            // CauHoi mapping
            CreateMap<CauHoi, GetCauHoiByIdResponse>();
            CreateMap<CauHoi, GetAllCauHoiResponse>();
            CreateMap<CauHoi, CreateCauHoiCommand>().ReverseMap();
            CreateMap<CauHoi, CreateCauHoiResponse>();
            CreateMap<CauHoi, UpdateCauHoiCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CauHoi, UpdateCauHoiResponse>();
            CreateMap<CauHoi, GetCauHoiByNoiDungResponse>();
            CreateMap<CauHoi, GetPagedCauHoisResponse>().ReverseMap();

            // CauHoiCongNghe mapping
            CreateMap<CauHoiCongNghe, GetCauHoiCongNgheByIdResponse>();
            CreateMap<CauHoiCongNghe, GetAllCauHoiCongNgheResponse>();
            CreateMap<CauHoiCongNghe, CreateCauHoiCongNgheCommand>().ReverseMap();
            CreateMap<CauHoiCongNghe, CreateCauHoiCongNgheResponse>();
            CreateMap<CauHoiCongNghe, UpdateCauHoiCongNgheCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CauHoiCongNghe, UpdateCauHoiCongNgheResponse>();
            CreateMap<CauHoiCongNghe, GetPagedCauHoiCongNghesResponse>().ReverseMap();

            // PhongVan mapping
            CreateMap<PhongVan, CreatePhongVanCommand>().ReverseMap();
            CreateMap<PhongVan, CreatePhongVanResponse>();
            CreateMap<PhongVan, GetPhongVanByIdResponse>();
            CreateMap<PhongVan, UpdatePhongVanCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PhongVan, UpdatePhongVanResponse>();
            CreateMap<PhongVan, PhongVan>();

            // Nhom Zalo mapping
            CreateMap<NhomZalo, CreateNhomZaloResponse>().ReverseMap();
            CreateMap<NhomZalo, GetNhomZaloResponse>();
            CreateMap<NhomZalo, UpdateNhomZaloResponse>().ReverseMap();
            CreateMap<NhomZalo, DeleteNhomZaloResponse>().ReverseMap();
            CreateMap<NhomZalo, CreateNhomZaloCommand>().ReverseMap();

            CreateMap<AddUserToNhomZaloCommand, UserNhomZalo>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.IdNhomZaloChung, opt => opt.MapFrom(src => src.NhomZaloId))
                .ForMember(dest => dest.IdNhomZaloRieng, opt => opt.MapFrom(src => src.NhomZaloId));

            CreateMap<UserNhomZalo, UpdateUserNhomZaloCommand>().ReverseMap();
            CreateMap<UserNhomZalo, GetUserNhomZaloResponse>().ReverseMap();
            CreateMap<UserNhomZalo, GetPagedUserNhomZaloResponse>().ReverseMap();

            // CongNghe mapping
            CreateMap<CongNghe, CreateCongNgheCommand>().ReverseMap();
            CreateMap<CongNghe, CreateCongNgheResponse>()
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.DateTime));
            CreateMap<CongNghe, UpdateCongNgheCommand>()
                .ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CongNghe, UpdateCongNgheResponse>();
            CreateMap<CongNghe, GetAllCongNgheResponse>().ReverseMap();
            CreateMap<CongNghe, GetCongNgheByIdResponse>()
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.DateTime));
            CreateMap<CongNghe, GetCongNgheByTenResponse>();
            CreateMap<CongNghe, GetPagedCongNghesResponse>().ReverseMap();

            // CongNgheDuAn mapping
            CreateMap<CongNgheDuAn, CreateCongNgheDuAnCommand>().ReverseMap();
            CreateMap<CongNgheDuAn, CreateCongNgheDuAnResponse>()
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.DateTime));
            CreateMap<CongNgheDuAn, UpdateCongNgheDuAnCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CongNgheDuAn, UpdateCongNgheDuAnResponse>();
            CreateMap<CongNgheDuAn, GetAllCongNgheDuAnResponse>().ReverseMap();
            CreateMap<CongNgheDuAn, GetCongNgheDuAnByIdResponse>()
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime.DateTime));
            CreateMap<CongNgheDuAn, GetPagedCongNgheDuAnsResponse>().ReverseMap();

            // ThongBao mapping 
            CreateMap<ThongBao, CreateThongBaoCommand>().ReverseMap();
            CreateMap<ThongBao, CreateThongBaoResponse>();
            CreateMap<ThongBao, UpdateThongBaoCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ThongBao, UpdateThongBaoResponse>();
            CreateMap<ThongBao, GetThongBaoByIdResponse>();
            CreateMap<ThongBao, GetAllThongBaoResponse>().ReverseMap();
            CreateMap<ThongBao, GetPagedThongBaosResponse>().ReverseMap();

            // LichPhongVan mapping
            CreateMap<LichPhongVan, GetLichPhongVanByIdResponse>();
            CreateMap<LichPhongVan, CreateLichPhongVanCommand>().ReverseMap();
            CreateMap<LichPhongVan, CreateLichPhongVanResponse>();
            CreateMap<LichPhongVan, UpdateLichPhongVanCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<LichPhongVan, UpdateLichPhongVanResponse>();
            CreateMap<DateTimeOffset, DateTime>().ConvertUsing(dt => dt.UtcDateTime);
            CreateMap<LichPhongVan, GetAllLichPhongVanResponse>();
            CreateMap<LichPhongVan, GetLichPhongVanByTodayQuery>();

            // PhongVan mapping
            CreateMap<PhongVan, CreatePhongVanCommand>().ReverseMap();
            CreateMap<PhongVan, CreatePhongVanResponse>();
            CreateMap<PhongVan, GetPhongVanByIdResponse>();
            CreateMap<PhongVan, UpdatePhongVanCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PhongVan, UpdatePhongVanResponse>();
            CreateMap<PhongVan, GetPagedPhongVansResponse>().ReverseMap();

            // Task mapping
            CreateMap<Tasks, CreateTaskCommand>().ReverseMap();
            CreateMap<Tasks, TaskResponse>();
            CreateMap<Tasks, UpdateTaskCommand>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Tasks, GetPagedTasksResponse>().ReverseMap();

            // User Task mapping
            CreateMap<ReportTask, TaskReportResponse>().ReverseMap();
            CreateMap<ReportTask, CreateTaskReportCommand>().ReverseMap();
            CreateMap<ReportTask, UpdateTaskReportCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserTask, GetPagedUserTaskResponse>().ReverseMap();
            CreateMap<ReportTask, GetPagedReportTasksResponse>().ReverseMap();

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

            // Claim - UserCliam mapping
            CreateMap<ApplicationClaim, GetClaimResponse>().ReverseMap();
            CreateMap<ApplicationClaim, AddClaimCommand>().ReverseMap();
            CreateMap<ApplicationClaim, UpdateClaimCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // UserDuAn mapping
            CreateMap<UserDuAn, GetUserDuAnByIdResponse>();
            CreateMap<UserDuAn, GetAllUserDuAnResponse>();
            CreateMap<UserDuAn, CreateUserDuAnCommand>().ReverseMap();
            CreateMap<UserDuAn, CreateUserDuAnResponse>();
            CreateMap<UserDuAn, UpdateUserDuAnCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserDuAn, UpdateUserDuAnResponse>();
            CreateMap<UserDuAn, GetAllUserDuAnResponse>();
            CreateMap<UserDuAn, GetPagedUserDuAnResponse>().ReverseMap();

            //ViTri mapping
            CreateMap<ViTri, GetViTriByIdResponse>();
            CreateMap<ViTri, CreateViTriCommand>().ReverseMap();
            CreateMap<ViTri, CreateViTriResponse>();
            CreateMap<ViTri, UpdateViTriCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ViTri, UpdateViTriResponse>();
            CreateMap<ViTri, GetViTriByTenResponse>();
            CreateMap<ViTri, GetAllViTriResponse>();
            CreateMap<ViTri, GetPagedViTrisResponse>().ReverseMap();

            //UserViTri mapping
            CreateMap<UserViTri, GetUserViTriByIdResponse>();
            CreateMap<UserViTri, CreateUserViTriCommand>().ReverseMap();
            CreateMap<UserViTri, CreateUserViTriResponse>();
            CreateMap<UserViTri, UpdateUserViTriCommand>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UserViTri, UpdateUserViTriResponse>();
            CreateMap<UserViTri, GetAllUserViTriResponse>();
            CreateMap<UserViTri, GetPagedUserViTriResponse>().ReverseMap();

            // Mapping for PaginatedList
            //CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>))
            //    .ConvertUsing(typeof(PaginatedListConverter<,>));
        }
    }

    // Custom converter for PaginatedList
    //public class PaginatedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>>
    //{
    //    public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
    //    {
    //        var mappedItems = context.Mapper.Map<IReadOnlyCollection<TDestination>>(source.Items);
    //        return new PaginatedList<TDestination>(mappedItems, source.TotalCount, source.PageNumber, source.PageSize);
    //    }
    //}
}
