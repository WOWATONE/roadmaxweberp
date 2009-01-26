using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace AIR_ERP.Module
{
    [DefaultClassOptions]
    [DefaultProperty("DocNum")]
    [ObjectCaptionFormat("{0:DocNum}{0:BusinessPartner}")]
    [NavigationItem("Accounts Receivable")]
    [RuleCriteria("RuleSOHeaderDocNum", DefaultContexts.Save, "DocNum != 0", "Document Number shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class SOHeader : BaseObject
    {
        public SOHeader(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
            docDate = DateTime.Now;
            company = Session.FindObject<Company>(CriteriaOperator.Parse("Employees[Oid = ?]", SecuritySystem.CurrentUserId));

        }

        private Currency currency;
        [RuleRequiredField("RuleRequiredField for SOHeader.Currency", DefaultContexts.Save)]
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
        [RuleRequiredField("RuleRequiredField for SOHeader.VatCategory", DefaultContexts.Save)]
        public VatCategory VatCategory
        {
            get { return vatCategory; }
            set { SetPropertyValue("VatCategory", ref vatCategory, value); }
        }

        private Term term;
        [RuleRequiredField("RuleRequiredField for SOHeader.Term", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public Term Term
        {
            get { return term; }
            set { SetPropertyValue("Term", ref term, value); }
        }

        private BusinessPartner businessPartner;
        [RuleRequiredField("RuleRequiredField for SOHeader.BusinessPartner", DefaultContexts.Save)]
        public BusinessPartner BusinessPartner
        {
            get { return businessPartner; }
            set
            {
                SetPropertyValue("BusinessPartner", ref businessPartner, value);

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

        [Association("SOHeader-SOItem"), Aggregated]
        public XPCollection<SOItems> SOItem
        {
            get
            {
                return GetCollection<SOItems>("SOItem");
            }
        }

        [Association("SOHeader-SOOthers"), Aggregated]
        public XPCollection<SOOther> SOOthers
        {
            get
            {
                return GetCollection<SOOther>("SOOthers");
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

    [DefaultClassOptions]
    [NavigationItem(false)]
    [RuleCriteria("RuleSOItemsQuantity", DefaultContexts.Save, "Quantity != 0", "Quantity shouldn't be 0", SkipNullOrEmptyValues = false)]
    [RuleCriteria("RuleSOItemsPrice", DefaultContexts.Save, "Price != 0", "Price shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class SOItems : BaseObject
    {
        public SOItems(Session session) : base(session) { }

        private ItemMaster itemMaster;
        [RuleRequiredField("RuleRequiredField for SOItems.ItemMaster", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public ItemMaster ItemMaster
        {
            get { return itemMaster; }
            set
            {
                SetPropertyValue("ItemMaster", ref itemMaster, value);
            }
        }

        private UnitOfMeasure unitOfMeasure;
        [RuleRequiredField("RuleRequiredField for SOItems.UnitOfMeasure", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public UnitOfMeasure UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set
            {
                SetPropertyValue("UnitOfMeasure", ref unitOfMeasure, value);
            }
        }

        private Warehouse warehouse;
        [RuleRequiredField("RuleRequiredField for SOItems.Warehouse", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public Warehouse Warehouse
        {
            get { return warehouse; }
            set
            {
                SetPropertyValue("Warehouse", ref warehouse, value);
            }
        }

        private Decimal cost;
        public Decimal Cost
        {
            get
            {
                return cost;
            }
            set
            {
                SetPropertyValue("Cost", ref cost, value);
            }
        }

        private Decimal price;
        public Decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                SetPropertyValue("Price", ref price, value);
            }
        }

        private Decimal quantity;
        public Decimal Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                SetPropertyValue("Quantity", ref quantity, value);
            }
        }

        private Decimal discount;
        public Decimal Discount
        {
            get
            {
                return discount;
            }
            set
            {
                SetPropertyValue("Discount", ref discount, value);
            }
        }

        private Decimal discountRate;
        public Decimal DiscountRate
        {
            get
            {
                return discountRate;
            }
            set
            {
                SetPropertyValue("DiscountRate", ref discountRate, value);
            }
        }

        private SOHeader SoH;
        [Association("SOHeader-SOItem")]
        public SOHeader SOHeader
        {
            get { return SoH; }
            set { SetPropertyValue("SOHeader", ref SoH, value); }
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

    [DefaultClassOptions]
    [NavigationItem(false)]
    [RuleCriteria("RuleSOOtherQuantity", DefaultContexts.Save, "Quantity != 0", "Quantity shouldn't be 0", SkipNullOrEmptyValues = false)]
    [RuleCriteria("RuleSOOtherPrice", DefaultContexts.Save, "Price != 0", "Price shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class SOOther : BaseObject
    {
        public SOOther(Session session) : base(session) { }

        private ChartOfAccounts accountId;
        [RuleRequiredField("RuleRequiredField for SOOther.AccountId", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public ChartOfAccounts AccountId
        {
            get { return accountId; }
            set
            {
                SetPropertyValue("AccountId", ref accountId, value);
            }
        }

        private Decimal price;
        public Decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                SetPropertyValue("Price", ref price, value);
            }
        }

        private Decimal quantity;
        public Decimal Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                SetPropertyValue("Quantity", ref quantity, value);
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

        private SOHeader SoH;
        [Association("SOHeader-SOOthers")]
        public SOHeader SOHeader
        {
            get { return SoH; }
            set { SetPropertyValue("SOHeader", ref SoH, value); }
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

    public class CurrentCompanyOidParameter : ReadOnlyParameter
    {
        public CurrentCompanyOidParameter()
            : base("CurrentCompanyOid", typeof(object))
        {
        }
        public override object CurrentValue
        {
            get
            {
                return CurrentCompanyOid;
            }
        }
        private object currentCompanyOid;
        public object CurrentCompanyOid
        {
            get
            {
                return currentCompanyOid;
            }
            set
            {
                currentCompanyOid = value;
            }
        }
    }

    [DefaultClassOptions]
    [DefaultProperty("DocNum")]
    [ObjectCaptionFormat("{0:DocNum}{0:BusinessPartner}")]
    [NavigationItem("Accounts Receivable")]
    [RuleCriteria("RuleDRHeaderDocNum", DefaultContexts.Save, "DocNum != 0", "Document Number shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class DRHeader : BaseObject
    {
        public DRHeader(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
            docDate = DateTime.Now;
            company = Session.FindObject<Company>(CriteriaOperator.Parse("Employees[Oid = ?]", SecuritySystem.CurrentUserId));

        }

        private Currency currency;
        [RuleRequiredField("RuleRequiredField for DRHeader.Currency", DefaultContexts.Save)]
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
        [RuleRequiredField("RuleRequiredField for DRHeader.VatCategory", DefaultContexts.Save)]
        public VatCategory VatCategory
        {
            get { return vatCategory; }
            set { SetPropertyValue("VatCategory", ref vatCategory, value); }
        }

        private Term term;
        [RuleRequiredField("RuleRequiredField for DRHeader.Term", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public Term Term
        {
            get { return term; }
            set { SetPropertyValue("Term", ref term, value); }
        }

        private BusinessPartner businessPartner;
        [RuleRequiredField("RuleRequiredField for DRHeader.BusinessPartner", DefaultContexts.Save)]
        public BusinessPartner BusinessPartner
        {
            get { return businessPartner; }
            set
            {
                SetPropertyValue("BusinessPartner", ref businessPartner, value);

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

        [Association("DRHeader-DRItem"), Aggregated]
        public XPCollection<DRItems> DRItem
        {
            get
            {
                return GetCollection<DRItems>("DRItem");
            }
        }

        [Association("DRHeader-DROthers"), Aggregated]
        public XPCollection<DROther> DROthers
        {
            get
            {
                return GetCollection<DROther>("DROthers");
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

    [DefaultClassOptions]
    [NavigationItem(false)]
    [RuleCriteria("RuleDRItemsQuantity", DefaultContexts.Save, "Quantity != 0", "Quantity shouldn't be 0", SkipNullOrEmptyValues = false)]
    [RuleCriteria("RuleDRItemsPrice", DefaultContexts.Save, "Price != 0", "Price shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class DRItems : BaseObject
    {
        public DRItems(Session session) : base(session) { }

        private ItemMaster itemMaster;
        [RuleRequiredField("RuleRequiredField for DRItems.ItemMaster", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public ItemMaster ItemMaster
        {
            get { return itemMaster; }
            set
            {
                SetPropertyValue("ItemMaster", ref itemMaster, value);
            }
        }

        private UnitOfMeasure unitOfMeasure;
        [RuleRequiredField("RuleRequiredField for DRItems.UnitOfMeasure", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public UnitOfMeasure UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set
            {
                SetPropertyValue("UnitOfMeasure", ref unitOfMeasure, value);
            }
        }

        private Warehouse warehouse;
        [RuleRequiredField("RuleRequiredField for DRItems.Warehouse", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public Warehouse Warehouse
        {
            get { return warehouse; }
            set
            {
                SetPropertyValue("Warehouse", ref warehouse, value);
            }
        }

        private Decimal cost;
        public Decimal Cost
        {
            get
            {
                return cost;
            }
            set
            {
                SetPropertyValue("Cost", ref cost, value);
            }
        }

        private Decimal price;
        public Decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                SetPropertyValue("Price", ref price, value);
            }
        }

        private Decimal quantity;
        public Decimal Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                SetPropertyValue("Quantity", ref quantity, value);
            }
        }

        private Decimal discount;
        public Decimal Discount
        {
            get
            {
                return discount;
            }
            set
            {
                SetPropertyValue("Discount", ref discount, value);
            }
        }

        private Decimal discountRate;
        public Decimal DiscountRate
        {
            get
            {
                return discountRate;
            }
            set
            {
                SetPropertyValue("DiscountRate", ref discountRate, value);
            }
        }

        private DRHeader DoH;
        [Association("DRHeader-DRItem")]
        public DRHeader DRHeader
        {
            get { return DoH; }
            set { SetPropertyValue("DRHeader", ref DoH, value); }
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

    [DefaultClassOptions]
    [NavigationItem(false)]
    [RuleCriteria("RuleDROtherQuantity", DefaultContexts.Save, "Quantity != 0", "Quantity shouldn't be 0", SkipNullOrEmptyValues = false)]
    [RuleCriteria("RuleDROtherPrice", DefaultContexts.Save, "Price != 0", "Price shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class DROther : BaseObject
    {
        public DROther(Session session) : base(session) { }

        private ChartOfAccounts accountId;
        [RuleRequiredField("RuleRequiredField for DROther.AccountId", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public ChartOfAccounts AccountId
        {
            get { return accountId; }
            set
            {
                SetPropertyValue("AccountId", ref accountId, value);
            }
        }

        private Decimal price;
        public Decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                SetPropertyValue("Price", ref price, value);
            }
        }

        private Decimal quantity;
        public Decimal Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                SetPropertyValue("Quantity", ref quantity, value);
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

        private DRHeader DoH;
        [Association("DRHeader-DROthers")]
        public DRHeader DRHeader
        {
            get { return DoH; }
            set { SetPropertyValue("DRHeader", ref DoH, value); }
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

    [DefaultClassOptions]
    [NavigationItem("Accounts Receivable")]
    [DefaultProperty("DocNum")]
    [ObjectCaptionFormat("{0:DocNum}{0:BusinessPartner}")]
    [RuleCriteria("RuleARVHeaderDocNum", DefaultContexts.Save, "DocNum != 0", "Document Number shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class ARVHeader : BaseObject
    {
        public ARVHeader(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
            docDate = DateTime.Now;
            company = Session.FindObject<Company>(CriteriaOperator.Parse("Employees[Oid = ?]", SecuritySystem.CurrentUserId));

        }

        private Currency currency;
        [RuleRequiredField("RuleRequiredField for ARVHeader.Currency", DefaultContexts.Save)]
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
        [RuleRequiredField("RuleRequiredField for ARVHeader.VatCategory", DefaultContexts.Save)]
        public VatCategory VatCategory
        {
            get { return vatCategory; }
            set { SetPropertyValue("VatCategory", ref vatCategory, value); }
        }

        private Term term;
        [RuleRequiredField("RuleRequiredField for ARVHeader.Term", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public Term Term
        {
            get { return term; }
            set { SetPropertyValue("Term", ref term, value); }
        }

        private BusinessPartner businessPartner;
        [RuleRequiredField("RuleRequiredField for ARVHeader.BusinessPartner", DefaultContexts.Save)]
        public BusinessPartner BusinessPartner
        {
            get { return businessPartner; }
            set
            {
                SetPropertyValue("BusinessPartner", ref businessPartner, value);

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

        //[Association("ARVHeader-ARVOthers"), Aggregated]
        //public XPCollection<ARVOther> ARVOthers
        //{
        //    get
        //    {
        //        return GetCollection<ARVOther>("ARVOthers");
        //    }
        //}

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