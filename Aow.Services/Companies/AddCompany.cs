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
                        new Aow.Infrastructure.Domain.LedgerCategory {Id=Guid.NewGuid(),   Name = "Sales Account",ParentCategoryId=incomeId,CompanyId=companyId,Type="Master"} ,
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
                          new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Purchase Account",LedgerCategoryId=currentAssetId,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                           new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Freight & Dilevery",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                            new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Job Work",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                        new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Interest Paid",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                      new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Depreciation Expense",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                        new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Mobile & Internet Recharge",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                         new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Salary & Wages",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                          new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Office Expense",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                            new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Repair & Maintainance",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                              new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Bank Service Charges",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                            new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Advertising & Promotion",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                             new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Travel Expense",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                                new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Charity & Donations",LedgerCategoryId=inDirectExpense,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                                new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Profit & Loss",LedgerCategoryId=profitLossId,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                                   new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Cash",LedgerCategoryId=cashInHand,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,
                                    new Aow.Infrastructure.Domain.Ledger {Id=Guid.NewGuid(),   Name = "Stock In Hand",LedgerCategoryId=stockInHand,CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now,CreatedBy=userName} ,

                };

            ledgers.ForEach(l => _repoWrapper.LedgerRepositoryRepo.Create(l));           

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
