using InternSystem.Application.Common.Persistences.IRepositories.IBaseRepositories;

namespace InternSystem.Application.Common.Persistences.IRepositories
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IInternInfoRepository InternInfoRepository { get; }
        ITruongHocRepository TruongHocRepository { get; }
        IKyThucTapRepository KyThucTapRepository { get; }
        IDuAnRepository DuAnRepository { get; }
        IUserDuAnRepository UserDuAnRepository { get; }
        INhomZaloRepository NhomZaloRepository { get; }
        IUserNhomZaloRepository UserNhomZaloRepository { get; }
        ICommentRepository CommentRepository { get; }
        IEmailUserStatusRepository EmailUserStatusRepository { get; }
        ILichPhongVanRepository LichPhongVanRepository { get; }
        IPhongVanRepository PhongVanRepository { get; }
        ICauHoiRepository CauHoiRepository { get; }
        ICauHoiCongNgheRepository CauHoiCongNgheRepository { get; }
        ICongNgheRepository CongNgheRepository { get; }
        ICongNgheDuAnRepository CongNgheDuAnRepository { get; }
        IThongBaoRepository ThongBaoRepository { get; }
        IViTriRepository ViTriRepository { get; }
        IUserViTriRepository UserViTriRepository { get; }
        IDashboardRepository DashboardRepository { get; }
        IMessageRepository MessageRepository { get; }
        Task CommitAsync();0
        ITaskRepository TaskRepository { get; }
        IReportTaskRepository ReportTaskRepository { get; }
        IUserTaskRepository UserTaskRepository { get; }
        INhomZaloTaskRepository NhomZaloTaskRepository { get; }

    }
}
