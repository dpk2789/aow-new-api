using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.Companies
{
    [Service]
    public class DeleteCompany
    {
        private IRepositoryWrapper _repoWrapper;

        public DeleteCompany(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class DeleteCompanyResponse
        {
            public string Message { get; set; }
            public bool Success { get; set; }
        }
        public async Task<DeleteCompanyResponse> Do(Guid id)
        {
            try
            {
                var company = _repoWrapper.CompanyRepo.GetCompany(id);
                if (company == null)
                {
                    return null;
                }
                _repoWrapper.CompanyRepo.Delete(company);
                int i = await _repoWrapper.SaveNew();
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
            catch (Exception ex)
            {
                return new DeleteCompanyResponse
                {
                    Message = ex.Message,
                    Success = false
                };
            }
           
        }
    }
}
