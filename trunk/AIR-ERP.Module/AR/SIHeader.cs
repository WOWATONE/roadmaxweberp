using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;

namespace AIR_ERP.Module
{
	[DefaultClassOptions]
	[ObjectCaptionFormat("{0:DocNum}{0:BusinessPartner}")]
    [NavigationItem("Accounts Receivable"), System.ComponentModel.DisplayName("Sales Invoice")]
    [RuleCriteria("RuleSIHeaderDocNum", DefaultContexts.Save, "DocNum != 0", "Document Number shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class SIHeader : BaseObject
    {
        public SIHeader(Session session) : base(session) { }
        public SIHeader()
        {
            
        }
                                                            
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
            docDate = DateTime.Now;
            company = Session.FindObject<Company>(CriteriaOperator.Parse("Employees[Oid = ?]", SecuritySystem.CurrentUserId));

        }

        private Currency currency;
        [RuleRequiredField("RuleRequiredField for SIHeader.Currency", DefaultContexts.Save)]
        public Currency Currency
        {
            get { return currency; }
            set { SetPropertyValue("Currency", ref currency, value); }
        }

        private Company company;
        public Company Company
        {
            get { return company; }
            set
            {
                SetPropertyValue("Company", ref company, value);
                branch = null;

            }
        }

        private Branch branch;
        public Branch Branch
        {
            get { return branch; }
            set { SetPropertyValue("Branch", ref branch, value); }
        }

        private VatCategory vatCategory;
        [RuleRequiredField("RuleRequiredField for SIHeader.VatCategory", DefaultContexts.Save)]
        public VatCategory VatCategory
        {
            get { return vatCategory; }
            set { SetPropertyValue("VatCategory", ref vatCategory, value); }
        }

        private Term term;
        [RuleRequiredField("RuleRequiredField for SIHeader.Term", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public Term Term
        {
            get { return term; }
            set { SetPropertyValue("Term", ref term, value); }
        }

        private BusinessPartner businessPartner;
        [RuleRequiredField("RuleRequiredField for SIHeader.BusinessPartner", DefaultContexts.Save)]
        public BusinessPartner BusinessPartner
        {
            get { return businessPartner; }
            set
            {
                SetPropertyValue("BusinessPartner", ref businessPartner, value);
                if (value != null && !IsLoading)
                {
                    VatCategory = value.VatCategory;
                    Term = value.Term;
                    Currency = value.Currency;
                }
            }
        }

        private Decimal docNum;
        public Decimal DocNum
        {
            get { return docNum; }
            set { SetPropertyValue("DocNum", ref docNum, value); }
        }

        private DateTime docDate;
        public DateTime DocDate
        {
            get
            {
                return docDate;
            }
            set
            {
                SetPropertyValue("DocDate", ref docDate, value);
            }
        }

        private String remarks;
        public string Remarks
        {
            get
            {
                return remarks;
            }
            set
            {
                SetPropertyValue("Remarks", ref remarks, value);
            }
        }

        private Boolean isActive;
        public Boolean IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                SetPropertyValue("IsActive", ref isActive, value);
            }
        }

        [Persistent("TotalAmount")]
        private decimal _TotalAmount;
        [PersistentAlias("SIItem[].Sum(Price * Quantity)")]
        public decimal TotalAmount
        {
            get
            {
                return _TotalAmount;
            }
        }

        public void UpdateTotal()
        {
            _TotalAmount = 0m;
            //foreach (SIItems SIItem in SIItem)
            //{
            //    _TotalAmount += SIItem.Price * SIItem.Quantity - SIItem.Discount;
            //}
            foreach (SIOther SIOther in SIOthers)
            {
                _TotalAmount += SIOther.Price * SIOther.Quantity;
            }
            OnChanged("TotalAmount");
        }

        protected override XPCollection<T> CreateCollection<T>(DevExpress.Xpo.Metadata.XPMemberInfo property)
        {
            XPCollection<T> coll = base.CreateCollection<T>(property);
            //if (property.Name == "SIItem" || property.Name == "SIOthers")
            if (property.Name == "SIOthers")
            {
                coll.CollectionChanged += coll_CollectionChanged;
            }
            return coll;
        }
        void coll_CollectionChanged(object sender, XPCollectionChangedEventArgs e)
        {
            UpdateTotal();
        }

        //[Association("SIHeader-SIItem"), Aggregated]
        //public XPCollection<SIItems> SIItem
        //{
        //    get
        //    {
        //        return GetCollection<SIItems>("SIItem");
        //    }
        //}

        [Association("SIHeader-SIOthers"), Aggregated]
        public XPCollection<SIOther> SIOthers
        {
            get
            {
                return GetCollection<SIOther>("SIOthers");
            }
        }

        private XPCollection<AuditDataItemPersistent> auditTrail;
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null)
                {
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return auditTrail;
            }
        }
    }
}
