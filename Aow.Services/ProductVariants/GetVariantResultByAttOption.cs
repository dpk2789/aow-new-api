using Aow.Infrastructure.IRepositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.ProductVariants
{
    [Service]
    public class GetVariantResultByAttOption
    {
        private IRepositoryWrapper _repoWrapper;
        public GetVariantResultByAttOption(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class GetVariantResultByAttOptionResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid CompanyId { get; set; }
            public Guid? ParentCategoryId { get; set; }
        }

        public class GetVariantResultResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }      
        }

        public async Task<IEnumerable<GetVariantResultResponse>> Do(string data)
        {
            var deserialiseList = JsonConvert.DeserializeObject<List<GetVariantResultByAttOptionResponse>>(data);
            var varients = new List<GetVariantResultResponse>();
            foreach (var item in deserialiseList)
            {                
                var optionVariants = await _repoWrapper.ProductVariantAndOptionRepo.GetVarientsWithOptionsByOption(item.Id);
                foreach (var optionVariant in optionVariants)
                {
                    GetVariantResultResponse getVariantResultResponse = new GetVariantResultResponse();
                    getVariantResultResponse.Id = optionVariant.ProductVariant.Id;
                    getVariantResultResponse.Name = optionVariant.ProductVariant.Name;
                    varients.Add(getVariantResultResponse);
                }                   
            }

            return varients;
        }
    }
}
