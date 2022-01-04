using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.Companies
{
    [Service]
    public class GetAllCompanies
    {
        private IRepositoryWrapper _repoWrapper;
        public GetAllCompanies(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class CompaniesResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public virtual IList<GetAllCompaniesFinancialYears> FinancialYears { get; set; }
            public virtual IList<GetAllCompaniesProductCategories> ProductCategories { get; set; }
            public virtual IList<GetAllCompaniesLedgerCategories> LedgerCategories { get; set; }
        }
        public class GetAllCompaniesFinancialYears
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }
            public bool IsActive { get; set; }
            public bool? IsLocked { get; set; }          
        }
        public class GetAllCompaniesProductCategories
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
        public class GetAllCompaniesLedgerCategories
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
        public async Task<IEnumerable<CompaniesResponse>> Do()
        {

            var list = await _repoWrapper.CompanyRepo.GetAllCompanies();
            //  var list = _companyRepository.GetCompanies(pagingParameters).GetAwaiter().GetResult();
            var companyList = new List<CompaniesResponse>();
            
            foreach (var company in list)
            {
                CompaniesResponse companiesResponse = new CompaniesResponse();
                companiesResponse.Id  = company.Id;
                companiesResponse.Name = company.Name;
                var financialYearList = new List<GetAllCompaniesFinancialYears>();
                foreach (var fyr in company.FinancialYears)
                {
                    GetAllCompaniesFinancialYears financialYear = new GetAllCompaniesFinancialYears();
                    financialYear.Id = fyr.Id;
                    financialYear.Name = fyr.Name;
                    financialYearList.Add(financialYear);
                }
                companiesResponse.FinancialYears = financialYearList;
                var ProductCategoriesList = new List<GetAllCompaniesProductCategories>();
                foreach (var fyr in company.ProductCategories)
                {
                    GetAllCompaniesProductCategories productCategories = new GetAllCompaniesProductCategories();
                    productCategories.Id = fyr.Id;
                    productCategories.Name = fyr.Name;
                    ProductCategoriesList.Add(productCategories);
                }
                companiesResponse.ProductCategories = ProductCategoriesList;
                var ledgerCategoriesList = new List<GetAllCompaniesLedgerCategories>();
                foreach (var fyr in company.LedgerCategories)
                {
                    GetAllCompaniesLedgerCategories ledgerCategory = new GetAllCompaniesLedgerCategories();
                    ledgerCategory.Id = fyr.Id;
                    ledgerCategory.Name = fyr.Name;
                    ledgerCategoriesList.Add(ledgerCategory);
                }
                companiesResponse.LedgerCategories = ledgerCategoriesList;
                companyList.Add(companiesResponse);
            }
          
            return companyList;
        }
    }
}
