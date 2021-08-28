using Aow.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TreeUtility;

namespace AowCore.Domain
{
    public class Ledger : AuditableEntity<Guid>, ITreeNode<Ledger>
    {
        private Guid? _parentCategoryId;

        [Display(Name = "Parent Category")]
        public Guid? ParentCategoryId
        {
            get { return _parentCategoryId; }
            set
            {
                if (Id == value)
                    throw new InvalidOperationException("A category cannot have itself as its parent.");

                _parentCategoryId = value;
            }
        }

        public virtual Ledger Parent { get; set; }
        public IList<Ledger> Children { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? OpeningBalance { get; set; }
        public string OpeningBalanceCrDr { get; set; }


        public string PrintName { get; set; }
        public string RegTaxNumber { get; set; }      
        public string PANNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string LandMark { get; set; }       
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }


        public string CompanyNameShipping { get; set; }   
        public string ShippingAddressLine1 { get; set; }
        public string ShippingAddressLine2 { get; set; }        
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AlternateContactNumber { get; set; }

       
        public string AccountType { get; set; }    

        public Guid LedgerCategoryId { get; set; }
        public virtual LedgerCategory LedgerCategory { get; set; }
       

        public Guid? UserId { get; set; }
        public bool? Status { get; set; }
     
    }
}
