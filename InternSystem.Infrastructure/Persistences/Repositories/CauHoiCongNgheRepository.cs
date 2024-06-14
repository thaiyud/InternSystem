﻿using InternSystem.Application.Common.Persistences.IRepositories;
using InternSystem.Domain.Entities;
using InternSystem.Infrastructure.Persistences.DBContext;
using InternSystem.Infrastructure.Persistences.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternSystem.Infrastructure.Persistences.Repositories
{
    public class CauHoiCongNgheRepository : BaseRepository<CauHoiCongNghe>, ICauHoiCongNgheRepository
    {
        private readonly ApplicationDbContext _context;
        public CauHoiCongNgheRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
