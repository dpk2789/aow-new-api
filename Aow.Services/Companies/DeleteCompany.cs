using Aow.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Companies
{
    [Service]
    public class DeleteCompany
    {
        private ICompanyRepository _companyRepository;

        public DeleteCompany(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public class DeleteCompanyResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }
        public async Task<DeleteCompanyResponse> Do(Guid id)
        {
            var company = _companyRepository.GetCompany(id);
            if (company == null)
            {
                return null;
            }
            _companyRepository.Delete(company);
            int i = await _companyRepository.Save();
            if (i <= 0)
            {
                return new DeleteCompanyResponse
                {
                    Message = "Error Deleting",
                    Success = false
                };
            }
            else
            {
                return new DeleteCompanyResponse
                {
                    Message = "Company Deleted",
                    Success = true
                };
            }
        }
    }
}
