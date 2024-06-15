
using AutoMapper;
using CsvHelper;
using ExcelDataReader;
using InternSystem.Application.Common.EmailService;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Models;
using InternSystem.Application.Features.User.Utility;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Application.Features.User.Handlers
{
    public class ImportXlsxHandler : IRequestHandler<ImportXlsxCommand, UploadFileResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public ImportXlsxHandler(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AspNetUser> userManager, IEmailService emailService, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailService = emailService;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<UploadFileResponse> Handle(ImportXlsxCommand request, CancellationToken cancellationToken)
        {
            var response = new UploadFileResponse();
            response.IsSuccess = true;
            response.Message = "successful";
            //Read csv file
            using (var stream = new MemoryStream())
            {
                await request.File.CopyToAsync(stream);
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(
                        new ExcelDataSetConfiguration()
                        {
                            UseColumnDataType = false,
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }

                        });

                    List<ImportCsvDto> dtos = new List<ImportCsvDto>();

                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        ImportCsvDto dto = new ImportCsvDto
                        {
                            MSSV = dataSet.Tables[0].Rows[i].ItemArray[0] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[0]) : "-1",
                            
                            Email= dataSet.Tables[0].Rows[i].ItemArray[1] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[1]) : "-1",

                            EmailConfirmed= dataSet.Tables[0].Rows[i].ItemArray[2] != null ? Convert.ToBoolean(dataSet.Tables[0].Rows[i].ItemArray[2]) : false,

                            PhoneNumber=dataSet.Tables[0].Rows[i].ItemArray[3] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[3]) : "-1",

                            HoTen= dataSet.Tables[0].Rows[i].ItemArray[4] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[4]) : "-1",

                            IsConfirmed= dataSet.Tables[0].Rows[i].ItemArray[5] != null ? Convert.ToBoolean(dataSet.Tables[0].Rows[i].ItemArray[5]) : false,

                            TrangThaiThucTap= dataSet.Tables[0].Rows[i].ItemArray[6] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[6]) : "-1",

                            NgaySinh= dataSet.Tables[0].Rows[i].ItemArray[7] != null ? Convert.ToDateTime(dataSet.Tables[0].Rows[i].ItemArray[7]) : null,

                            GioiTinh=dataSet.Tables[0].Rows[i].ItemArray[8] != null ? Convert.ToBoolean(dataSet.Tables[0].Rows[i].ItemArray[8]) : false,

                            EmailTruong= dataSet.Tables[0].Rows[i].ItemArray[9] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[9]) : "-1",

                            SdtNguoiThan= dataSet.Tables[0].Rows[i].ItemArray[10] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[10]) : "-1",

                            DiaChi= dataSet.Tables[0].Rows[i].ItemArray[11] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[11]) : "-1",

                            GPA = dataSet.Tables[0].Rows[i].ItemArray[12] != null ? Convert.ToDecimal(dataSet.Tables[0].Rows[i].ItemArray[12]) : 0,

                            TrinhDoTiengAnh= dataSet.Tables[0].Rows[i].ItemArray[13] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[13]) : "-1",

                            LinkFacebook= dataSet.Tables[0].Rows[i].ItemArray[14] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[14]) : "-1",

                            LinkCv= dataSet.Tables[0].Rows[i].ItemArray[15] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[15]) : "-1",

                            NganhHoc= dataSet.Tables[0].Rows[i].ItemArray[16] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[16]) : "-1",

                            TrangThai = dataSet.Tables[0].Rows[i].ItemArray[17] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[17]) : "-1",

                            Round= dataSet.Tables[0].Rows[i].ItemArray[18] != null ? Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[18]) : 0,

                            ViTriMongMuon= dataSet.Tables[0].Rows[i].ItemArray[19] != null ? Convert.ToString(dataSet.Tables[0].Rows[i].ItemArray[19]) : "-1",

                            IdTruong= dataSet.Tables[0].Rows[i].ItemArray[20] != null ? Convert.ToInt32(dataSet.Tables[0].Rows[i].ItemArray[20]) : 0,
                        };
                        dtos.Add(dto);
                    }
                    foreach (ImportCsvDto dto in dtos)
                    {
                        // Save InternInfo first
                        var internInfo = _mapper.Map<InternInfo>(dto);
                        internInfo.EmailCaNhan = dto.Email;
                        internInfo.Sdt = dto.PhoneNumber;
                        internInfo.CreatedBy = "System";
                        internInfo.LastUpdatedBy = "System";
                        internInfo.CreatedTime = DateTimeOffset.Now;

                        var internResult = await _unitOfWork.InternInfoRepository.AddAsync(internInfo);
                        await _unitOfWork.SaveChangeAsync();

                        // Save AspNetUser
                        var user = _mapper.Map<AspNetUser>(dto);
                        user.UserName = user.Email;
                        user.InternInfoId = internInfo.Id;
                        user.HoVaTen = internInfo.HoTen;
                        user.CreatedTime = DateTimeOffset.Now;
                        //Generate random password for user
                        var randomPassword = PasswordGenerator.Generate(8);

                        var result = await _userManager.CreateAsync(user, randomPassword);

                        // Check if user add successfully
                        if (!result.Succeeded)
                        {
                            var errors = result.Errors.Select(e => $"{e.Code}: {e.Description}");
                            var errorMessage = string.Join("; ", errors);
                            // Rollback InternInfo creation
                            _unitOfWork.InternInfoRepository.Remove(internInfo);
                            await _unitOfWork.SaveChangeAsync();
                            throw new Exception($"Failed to create user: {errorMessage}");
                            response.IsSuccess = false;
                            response.Message = "fail";
                            return response;
                        }
                        else
                        {
                            // Retrieve role name from appsettings
                            var internRole = _configuration["RoleSettings:Intern"];
                            // role exists in the database
                            var roleExists = await _roleManager.RoleExistsAsync(internRole);
                            if (!roleExists)
                            {
                                throw new Exception($"Role '{internRole}' does not exist in the database");
                            }
                            await _userManager.AddToRoleAsync(user, internRole);

                            internInfo.UserId = user.Id; // Update internInfo with the user ID
                            await _unitOfWork.SaveChangeAsync(); // Save the changes to internInfo

                            // Send email to user
                            var emailBody = $"Your account has been created successfully. \nUsername: {user.UserName}\nPassword: {randomPassword}";
                            var emailSent = await _emailService.SendEmailAsync(new List<string> { user.Email }, "Account Creation Notification", emailBody);

                            if (!emailSent)
                            {
                                throw new Exception($"Failed to send email notification to {user.Email}");
                            }
                        }
                    }
                }
            }
            //Read csv file - End
            return response;
        }
    }
}
