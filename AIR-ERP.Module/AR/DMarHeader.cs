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
    [NavigationItem("Accounts Receivable"), System.ComponentModel.DisplayName("Debit Memo")]
    [RuleCriteria("RuleDMarHeaderDocNum", DefaultContexts.Save, "DocNum != 0", "Document Number shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class DMarHeader : BaseObject
    {
        public DMarHeader(Session session) : base(session) { }
        public DMarHeader()
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
        [RuleRequiredField("RuleRequiredField for DMarHeader.Currency", DefaultContexts.Save)]
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
        [RuleRequiredField("RuleRequiredField for DMarHeader.VatCategory", DefaultContexts.Save)]
        public VatCategory VatCategory
        {
            get { return vatCategory; }
            set { SetPropertyValue("VatCategory", ref vatCategory, value); }
        }

        private Term term;
        [RuleRequiredField("RuleRequiredField for DMarHeader.Term", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public Term Term
        {
            get { return term; }
            set { SetPropertyValue("Term", ref term, value); }
        }

        private BusinessPartner businessPartner;
        [RuleRequiredField("RuleRequiredField for DMarHeader.BusinessPartner", DefaultContexts.Save)]
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
        [PersistentAlias("DMarOther[].Sum(Price * Quantity)")]
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
            foreach (DMarOther DMarOther in DMarOthers)
            {
                _TotalAmount += DMarOther.Price * DMarOther.Quantity;
            }
            OnChanged("TotalAmount");
        }

        protected override XPCollection<T> CreateCollection<T>(DevExpress.Xpo.Metadata.XPMemberInfo property)
        {
            XPCollection<T> coll = base.CreateCollection<T>(property);
            if (property.Name == "DMarOthers")
            {
                coll.CollectionChanged += coll_CollectionChanged;
            }
            return coll;
        }
        void coll_CollectionChanged(object sender, XPCollectionChangedEventArgs e)
        {
            UpdateTotal();
        }

        [Association("DMarHeader-DMarOthers"), Aggregated]
        public XPCollection<DMarOther> DMarOthers
        {
            get
            {
                return GetCollection<DMarOther>("DMarOthers");
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
