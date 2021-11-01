using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using Aow.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeUtility;

namespace Aow.Infrastructure.Repositories
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AowContext repositoryContext)
          : base(repositoryContext)
        {
        }

        public ProductCategory GetProductCategory(Guid Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        //public Task<PagedList<ProductCategory>> GetProductCategories(PagingParameters ownerParameters, Guid cmpId)
        //{
        //    return Task.FromResult(PagedList<ProductCategory>.ToPagedList(FindAll().Include(x => x.Parent).Where(x => x.CompanyId == cmpId).OrderBy(on => on.Name),
        //                            ownerParameters.PageNumber, ownerParameters.PageSize));
        //}

        public Task<PagedList<ProductCategory>> GetProductCategories(PagingParameters ownerParameters, Guid cmpId)
        {
            var categories = new List<ProductCategory>();
            var sourceCategories = FindAll().Where(x => x.CompanyId == cmpId).OrderBy(on => on.Name);
            foreach (var sourceCategory in sourceCategories)
            {
                var c = new ProductCategory();
                c.Id = sourceCategory.Id;
                c.Name = sourceCategory.Name;
                c.Type = sourceCategory.Type;
                if (sourceCategory.ParentCategoryId != null)
                {
                    c.Parent = new ProductCategory();
                    c.ParentCategoryId = sourceCategory.ParentCategoryId;
                }
                categories.Add(c);
            }
            //IList<ProductCategory> listOfNodes = categories;
            //IList<ProductCategory> topLevelCategories = TreeHelper.ConvertToForest(listOfNodes);
            return Task.FromResult(PagedList<ProductCategory>.ToPagedList(categories.AsQueryable(), ownerParameters.PageNumber, ownerParameters.PageSize));

        }

    }
}
