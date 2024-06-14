﻿using AutoMapper;
using CsvHelper;
using InternSystem.Application.Common.EmailService;
using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Application.Features.User.Commands;
using InternSystem.Application.Features.User.Models;
using InternSystem.Application.Features.User.Utility;
using InternSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InternSystem.Application.Features.User.Handlers
{
    public class ImportCsvHandler : IRequestHandler<ImportCsvCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImportCsvHandler(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AspNetUser> userManager, 
            IEmailService emailService, RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailService = emailService;
            _roleManager = roleManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        //Save InternInfo first then save AspNetUser
        public async Task<Unit> Handle(ImportCsvCommand request, CancellationToken cancellationToken)
        {
            //Read csv file
            using (var reader = new StreamReader(request.File.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ImportCsvDto>().ToList();
                Console.WriteLine($"Number of records to process: {records.Count}");

                foreach (var record in records)
                {
                    // Save InternInfo first
                    var internInfo = _mapper.Map<InternInfo>(record);
                    internInfo.EmailCaNhan = record.Email;
                    internInfo.Sdt = record.PhoneNumber;
                    internInfo.CreatedBy = "System"; // placeholder, replace with actual user ID from token
                    internInfo.LastUpdatedBy = "System"; // placeholder, replace with actual user ID from token

                    var internResult = await _unitOfWork.InternInfoRepository.AddAsync(internInfo);
                    await _unitOfWork.SaveChangeAsync();

                    // Save AspNetUser
                    var user = _mapper.Map<AspNetUser>(record);
                    user.UserName = user.Email;
                    user.InternInfoId = internInfo.Id;
                    user.HoVaTen = internInfo.HoTen;
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

            return Unit.Value;
        }
        ////Save AspNetUser first then save InternInfo with UserID
        //public async Task<Unit> Handle(ImportCsvCommand request, CancellationToken cancellationToken)
        //{
        //    //Read csv file
        //    using (var reader = new StreamReader(request.File.OpenReadStream()))
        //    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //    {
        //        var records = csv.GetRecords<ImportCsvDto>().ToList();
        //        Console.WriteLine($"Number of records to process: {records.Count}");

        //        foreach (var record in records)
        //        {
        //            //Save AspNetUser
        //            var user = _mapper.Map<AspNetUser>(record);
        //            user.UserName = user.Email;
        //            var result = await _userManager.CreateAsync(user, record.PasswordHash);

        //            if (!result.Succeeded)
        //            {
        //                var errors = result.Errors.Select(e => $"{e.Code}: {e.Description}");
        //                var errorMessage = string.Join("; ", errors);
        //                throw new Exception($"Failed to create user: {errorMessage}");
        //            }

        //            //Save InternInfo
        //                var internInfo = _mapper.Map<InternInfo>(record);
        //                internInfo.UserId = user.Id;
        //                internInfo.EmailCaNhan = user.Email;
        //                internInfo.Sdt = user.PhoneNumber;
        //                internInfo.CreatedBy = user.Id; //place holder for user token
        //                internInfo.LastUpdatedBy = user.Id; //place holder for user token

        //                //var internInfo = _mapper.Map<InternInfo>(internInfoDto);

        //                var internResult = await _unitOfWork.InternInfoRepository.AddAsync(internInfo);
        //        }

        //        await _unitOfWork.SaveChangeAsync();
        //    }

        //    return Unit.Value;
        //}

    }
}