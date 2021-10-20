using Aow.Infrastructure.Domain;
using Aow.Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aow.Services.Companies
{
    [Service]
    public class AddCompany
    {
        private IRepositoryWrapper _repoWrapper;

        public AddCompany(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        public class CreateRequest
        {
            public string UserName { get; set; }
            public string TaxType { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
        public class CreateResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool Success { get; set; }
        }
        public async Task<CreateResponse> Do(CreateRequest request)
        {
            Guid companyId = Guid.NewGuid();
            var user = await _repoWrapper.UserRepo.GetUserByName(request.UserName);
            if (user == null)
            {
                return null;
            };
            var company = new Company
            {
                Id = companyId,
                Name = request.Name,
                TaxType = request.TaxType,
                StartDateUtc = DateTime.UtcNow,
                EndDateUtc = DateTime.UtcNow,
                Status = false,
                NoOfDays = 0
            };
            _repoWrapper.CompanyRepo.Create(company);
            var appUserCompany = new AppUserCompany
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId,
                ApplicationUserId = user.Id
            };
            _repoWrapper.UserCompanyRepo.Create(appUserCompany);

            Guid assetsId = Guid.NewGuid();
            Guid expensesId = Guid.NewGuid();
            Guid equityId = Guid.NewGuid();
            Guid incomeId = Guid.NewGuid();
            Guid liabilitiesId = Guid.NewGuid();
            Guid ownersDrawId = Guid.NewGuid();
            Guid profitLossId = Guid.NewGuid();

            Guid currentAssetId = Guid.NewGuid();
            Guid currentLiabilityId = Guid.NewGuid();
            Guid dutiesNTaxies = Guid.NewGuid();
            Guid directExpense = Guid.NewGuid();
            Guid inDirectExpense = Guid.NewGuid();
            Guid cashInHand = Guid.NewGuid();
            Guid stockInHand = Guid.NewGuid();


            var categories = new List<Aow.Infrastructure.Domain.LedgerCategory>
                {
                     //ledger parent category
                           new Aow.Infrastructure.Domain.LedgerCategory { Id = incomeId, Name = "Income",CompanyId=companyId,Type="Master"},
                       new Aow.Infrastructure.Domain.LedgerCategory { Id = expensesId, Name = "Expense",CompanyId=companyId,Type="Master"},
                       new Aow.Infrastructure.Domain.LedgerCategory { Id = equityId, Name = "Equity" ,CompanyId=companyId,Type="Master"},
                       new Aow.Infrastructure.Domain.LedgerCategory { Id = liabilitiesId, Name = "Liability",CompanyId=companyId,Type="Master"},
                         new Aow.Infrastructure.Domain.LedgerCategory {Id=assetsId,   Name = "Asset",CompanyId=companyId,Type="Master"} ,
                          new Aow.Infrastructure.Domain.LedgerCategory {Id=ownersDrawId,   Name = "Drawings",CompanyId=companyId,Type="Master"} ,
                           new Aow.Infrastructure.Domain.LedgerCategory {Id=profitLossId,   Name = "Profit & Loss",CompanyId=companyId,Type="Master"} ,


                        new Aow.Infrastructure.Domain.LedgerCategory { Id = Guid.NewGuid(), Name = "Revenue",ParentCategoryId= incomeId,CompanyId=companyId,Type="Master"},
                       // new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Sales Account",ParentCategoryId=incomeId,CompanyId=companyId,Type="Master"} ,
                        new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Debitors",ParentCategoryId=incomeId,CompanyId=companyId,Type="Master"} ,
                        new Aow.Infrastructure.Domain.LedgerCategory { Id = Guid.NewGuid(), Name = "Other Income",ParentCategoryId= incomeId,CompanyId=companyId,Type="Master"},
                        new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Account Recievable",ParentCategoryId=incomeId,CompanyId=companyId,Type="Master"} ,


                      new Aow.Infrastructure.Domain.LedgerCategory {Id=directExpense,   Name = "Direct Expense",ParentCategoryId=expensesId,CompanyId=companyId,Type="Master"} ,
                          new Aow.Infrastructure.Domain.LedgerCategory {Id=inDirectExpense,   Name = "InDirect Expense",ParentCategoryId=expensesId,CompanyId=companyId,Type="Master"} ,
                       new Aow.Infrastructure.Domain.LedgerCategory { Id = Guid.NewGuid(), Name = "Other Expense",ParentCategoryId= expensesId,CompanyId=companyId,Type="Master"},
                        //new LedgerCategory {Id=Guid.NewGuid(),   Name = "Purchase Account",ParentCategoryId=directExpense,CompanyId=Id,} ,
                        new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Creditors",ParentCategoryId=expensesId,CompanyId=companyId,Type="Master"} ,
                          new Aow.Infrastructure.Domain.LedgerCategory { Id = Guid.NewGuid(), Name = "Cost Of Good" ,ParentCategoryId=directExpense,CompanyId=companyId,Type="Master"},


                          new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Capital Account",ParentCategoryId=equityId,CompanyId=companyId,Type="Master"} ,
                          new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Owners Equity",ParentCategoryId=equityId,CompanyId=companyId,Type="Master"} ,
                           // new LedgerCategory {Id=Guid.NewGuid(),   Name = "Owners Opening Balance",ParentCategoryId=EquityId,CompanyId=Id},


                       new Aow.Infrastructure.Domain.LedgerCategory {Id=currentAssetId,   Name = "Current Assets",ParentCategoryId=assetsId,CompanyId=companyId,Type="Master"},
                     new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Fixed Assets",ParentCategoryId=assetsId,CompanyId=companyId,Type="Master"},
                      new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Bank Account",ParentCategoryId=currentAssetId,CompanyId=companyId,Type="Master"} ,
                         new Aow.Infrastructure.Domain.LedgerCategory {Id=cashInHand,   Name = "Cash In Hand"  ,ParentCategoryId=currentAssetId,CompanyId=companyId,Type="Master"},
                         new Aow.Infrastructure.Domain.LedgerCategory {Id=stockInHand,   Name = "Current Stock",ParentCategoryId=currentAssetId,CompanyId=companyId,Type="Master"},


                       new Aow.Infrastructure.Domain.LedgerCategory { Id = Guid.NewGuid(), Name = "Other Liability", ParentCategoryId = liabilitiesId ,CompanyId=companyId,Type="Master"},
                     new Aow.Infrastructure.Domain.LedgerCategory { Id = currentLiabilityId, Name = "Current Liability", ParentCategoryId = liabilitiesId ,CompanyId=companyId,Type="Master"} ,
                       new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Loans",ParentCategoryId=liabilitiesId,CompanyId=companyId,Type="Master"} ,
                             new Aow.Infrastructure.Domain.LedgerCategory {Id=dutiesNTaxies,   Name = "Duties N Taxes",ParentCategoryId=currentLiabilityId,CompanyId=companyId,Type="Master"} ,
                             new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Account Payable",ParentCategoryId=liabilitiesId,CompanyId=companyId,Type="Master"} ,

                      new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Owners Draw",ParentCategoryId=ownersDrawId,CompanyId=companyId,Type="Master"} ,
                };
            categories.ForEach(s => _repoWrapper.LedgerCategoryRepositoryRepo.Create(s));

            var ledgers = new List<Aow.Infrastructure.Domain.Ledger>
                {
                  new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Sales Account",LedgerCategoryId=currentAssetId,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                          new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Purchase Account",LedgerCategoryId=currentAssetId,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                           new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Freight & Dilevery",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                            new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Job Work",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                        new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Interest Paid",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                      new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Depreciation Expense",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                        new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Mobile & Internet Recharge",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                         new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Salary & Wages",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                          new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Office Expense",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                            new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Repair & Maintainance",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                              new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Bank Service Charges",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                            new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Advertising & Promotion",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                             new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Travel Expense",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                                new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Charity & Donations",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                                new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Profit & Loss",LedgerCategoryId=profitLossId,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                                   new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Cash",LedgerCategoryId=cashInHand,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                                    new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Stock In Hand",LedgerCategoryId=stockInHand,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,

                };

            ledgers.ForEach(l => _repoWrapper.LedgerRepositoryRepo.Create(l));


            Guid productTaxCategory = Guid.NewGuid();
            var sundryItemsCategory = new List<Aow.Infrastructure.Domain.ProductCategory>
                {
                          new Aow.Infrastructure.Domain.ProductCategory {Id=Guid.NewGuid(),   Name = "Discount",CompanyId=companyId,Type="Sundry Item"} ,
                          new Aow.Infrastructure.Domain.ProductCategory {Id=Guid.NewGuid(),   Name = "Round Off",CompanyId=companyId,Type="Sundry Item"},
                          new Aow.Infrastructure.Domain.ProductCategory {Id=productTaxCategory,   Name = "Taxation",CompanyId=companyId,Type="Sundry Item"},
                          new Aow.Infrastructure.Domain.ProductCategory {Id=Guid.NewGuid(),   Name = "Finished Goods",CompanyId=companyId,Type="Voucher Item"},
                           new Aow.Infrastructure.Domain.ProductCategory {Id=Guid.NewGuid(),   Name = "Raw Materials",CompanyId=companyId,Type="Voucher Item"}
                };
            sundryItemsCategory.ForEach(l => _repoWrapper.ProductCategoryRepo.Create(l));

            Guid iGst = Guid.NewGuid();
            Guid sGst = Guid.NewGuid();
            Guid cGst = Guid.NewGuid();
            Guid uGst = Guid.NewGuid();

            if (company.TaxType == "GST")
            {
                var gstledgers = new List<Aow.Infrastructure.Domain.Ledger>
                {
                          new Aow.Infrastructure.Domain.Ledger {Id=iGst,   Name = "IGST",LedgerCategoryId=dutiesNTaxies,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email },
                           new Aow.Infrastructure.Domain.Ledger {Id=sGst,   Name = "SGST",LedgerCategoryId=dutiesNTaxies,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                            new Aow.Infrastructure.Domain.Ledger {Id=cGst,   Name = "CGST",LedgerCategoryId=dutiesNTaxies,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                            new Aow.Infrastructure.Domain.Ledger {Id=uGst,   Name = "UGST",LedgerCategoryId=dutiesNTaxies,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                };
                gstledgers.ForEach(l => _repoWrapper.LedgerRepositoryRepo.Create(l));
                var sundryItems = new List<Product>
                {
                    new Product {Id=Guid.NewGuid(),   Name = "IGST 5 %",LedgerId=iGst,Percent="5",ProductCategoryId=productTaxCategory,ItemType="Sundry Item", CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email },
                          new Product {Id=Guid.NewGuid(),   Name = "IGST 12 %",Percent="12",LedgerId=iGst,ProductCategoryId=productTaxCategory,ItemType="Sundry Item",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email },
                          new Product {Id=Guid.NewGuid(),   Name = "IGST 18 %",Percent="18",LedgerId=iGst,ProductCategoryId=productTaxCategory,ItemType="Sundry Item",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email },
                          new Product {Id=Guid.NewGuid(),   Name = "IGST 28 %",Percent="28",LedgerId=iGst,ProductCategoryId=productTaxCategory,ItemType="Sundry Item",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email },
                           new Product {Id=Guid.NewGuid(),   Name = "SGST 2.5 %",Percent="2.5",LedgerId=sGst,ProductCategoryId=productTaxCategory,ItemType="Sundry Item",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                           new Product {Id=Guid.NewGuid(),   Name = "SGST 6 %",Percent="6",LedgerId=sGst,ProductCategoryId=productTaxCategory,ItemType="Sundry Item",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                            new Product {Id=Guid.NewGuid(),   Name = "SGST 9 %",Percent="9",LedgerId=sGst,ProductCategoryId=productTaxCategory,ItemType="Sundry Item",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                              new Product {Id=Guid.NewGuid(),   Name = "SGST 14 %",Percent="14",LedgerId=sGst,ProductCategoryId=productTaxCategory,ItemType="Sundry Item",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                            new Product {Id=Guid.NewGuid(),   Name = "CGST 2.5 %",Percent="2.5",LedgerId=cGst,ProductCategoryId=productTaxCategory,ItemType="Sundry Item",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                            new Product {Id=Guid.NewGuid(),   Name = "CGST 6 %",Percent="6",LedgerId=cGst,ProductCategoryId=productTaxCategory,ItemType="Sundry Item",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                            new Product {Id=Guid.NewGuid(),   Name = "CGST 9 %",Percent="9",LedgerId=cGst,ProductCategoryId=productTaxCategory,ItemType="Sundry Item",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                             new Product {Id=Guid.NewGuid(),   Name = "CGST 14 %",Percent="14",LedgerId=cGst,ProductCategoryId=productTaxCategory,ItemType="Sundry Item",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=user.Email} ,
                };
                sundryItems.ForEach(l => _repoWrapper.ProductRepo.Create(l));
            }

            int i = await _repoWrapper.SaveNew();

            if (i <= 0)
            {
                return new CreateResponse
                {
                    Name = company.Name,
                    Success = false
                };
            }
            else
            {
                return new CreateResponse
                {
                    Id = company.Id,
                    Name = company.Name,
                    Success = true
                };
            }

        }
    }
}
