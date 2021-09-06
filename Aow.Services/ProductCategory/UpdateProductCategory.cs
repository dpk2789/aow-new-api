using Aow.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace Aow.Services.ProductCategory
{
    public class UpdateProductCategory
    {
        private IRepositoryWrapper _repoWrapper;
        public UpdateProductCategory(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class UpdateProductCategoryRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public string CompanyId { get; set; }
        }
        public class UpdateProductCategoryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }

        public async Task<UpdateProductCategoryResponse> Do(UpdateProductCategoryRequest request)
        {
            try
            {
                var financialYear = _repoWrapper.FinancialYearRepo.GetFinancialYear(request.Id);
                if (financialYear == null)
                {
                    return null;
                }
                financialYear.Name = request.Name;
                financialYear.Start = Convert.ToDateTime(request.Start);
                DateTime dt = Convert.ToDateTime(request.End);              
                financialYear.End = dt;
                _repoWrapper.FinancialYearRepo.Update(financialYear);

                int i = await _repoWrapper.SaveNew();
                if (i <= 0)
                {
                    return new UpdateProductCategoryResponse
                    {
                        Name = request.Name,
                        Success = false
                    };
                }
                else
                {
                    return new UpdateProductCategoryResponse
                    {
                        Id = financialYear.Id,
                        Name = request.Name,
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new UpdateProductCategoryResponse
                {
                    Name = request.Name,
                    Success = false,
                    Description = ex.Message
                };
            }
        }
    }
}
