﻿using Aow.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Aow.Infrastructure.IRepositories
{
    public interface IRepositoryWrapper
    {
        ICompanyRepository CompanyRepo { get; }
        IFinancialYearRepository FinancialYearRepo { get; }
        IUserCompanyRepository UserCompanyRepo { get; }
        public Task<int> SaveNew();
    }
}
