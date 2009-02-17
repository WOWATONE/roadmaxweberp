
using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;

namespace AIR_ERP.Module
{
    //Currency 
    [DefaultClassOptions]
    [RuleCriteria("", DefaultContexts.Save, "CurrencyRate != 0", "Currency Rate shouldn't be 0", SkipNullOrEmptyValues = false)]
    [ObjectCaptionFormat("{0:CurrencyName}")]
    public class Currency : BaseObject
    {
        public Currency(Session session) : base(session) { }

        private String currencyname;
        [RuleRequiredField("RuleRequiredField for Currency Name", DefaultContexts.Save)]
        public string CurrencyName
        {
            get
            {
                return currencyname;
            }
            set
            {
                SetPropertyValue("CurrencyName", ref currencyname, value);
            }
        }
        private Decimal currencyrate;
        public Decimal CurrencyRate
        {
            get
            {
                return currencyrate;
            }
            set
            {
                SetPropertyValue("CurrencyRate", ref currencyrate, value);
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
    //Company
    [DefaultClassOptions]
    [ObjectCaptionFormat("{0:CompanyName}")]
    public class Company : BaseObject
    {
        public Company(Session session) : base(session) { }
        private Currency currency;
        [RuleRequiredField("RuleRequiredField for Company.Currency", DefaultContexts.Save)]
        public Currency Currency
        {
            get { return currency; }
            set { SetPropertyValue("Currency", ref currency, value); }
        }

        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }

        private String companyname;
        [RuleRequiredField("RuleRequiredField for CompanyName", DefaultContexts.Save)]
        public String CompanyName
        {
            get
            {
                return companyname;
            }
            set
            {
                SetPropertyValue("CompanyName", ref companyname, value);
            }
        }

        private String companyaddress;
        [RuleRequiredField("RuleRequiredField for CompanyAddress", DefaultContexts.Save)]
        public String CompanyAddress
        {
            get
            {
                return companyaddress;
            }
            set
            {
                SetPropertyValue("CompanyAddress", ref companyaddress, value);
            }
        }

        private String companyemail;
        public String CompanyEmail
        {
            get
            {
                return companyemail;
            }
            set
            {
                SetPropertyValue("CompanyEmail", ref companyemail, value);
            }
        }

        private String faxnumber;
        public String FaxNumber
        {
            get
            {
                return faxnumber;
            }
            set
            {
                SetPropertyValue("FaxNumber", ref faxnumber, value);
            }
        }

        private String telnumber;
        public String TelNumber
        {
            get
            {
                return telnumber;
            }
            set
            {
                SetPropertyValue("TelNumber", ref telnumber, value);
            }
        }
        [Association("Company-Branches"), Aggregated]
        public XPCollection<Branch> Branches
        {
            get
            {
                return GetCollection<Branch>("Branches");
            }
        }

        [Association("Company-Employees", typeof(Employee))]
        public XPCollection Employees
        {
            get { return GetCollection("Employees"); }
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
    //Branch
    [DefaultClassOptions]
    [ObjectCaptionFormat("{0:Branchname}")]
    public class Branch : BaseObject
    {
        public Branch(Session session) : base(session) { }

        private Currency currency;
        [RuleRequiredField("RuleRequiredField for Branch.Currency", DefaultContexts.Save)]
        public Currency Currency
        {
            get { return currency; }
            set { SetPropertyValue("Currency", ref currency, value); }
        }

        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }
        private String branchname;
        [RuleRequiredField("RuleRequiredField for Branchname", DefaultContexts.Save)]
        public String Branchname
        {
            get
            {
                return branchname;
            }
            set
            {
                SetPropertyValue("Branchname", ref branchname, value);
            }
        }
        private String branchaddress;
        [RuleRequiredField("RuleRequiredField for Branchaddress", DefaultContexts.Save)]
        public String Branchaddress
        {
            get
            {
                return branchaddress;
            }
            set
            {
                SetPropertyValue("Branchaddress", ref branchaddress, value);
            }
        }
        private String branchemail;
        public String BranchEmail
        {
            get
            {
                return branchemail;
            }
            set
            {
                SetPropertyValue("BranchEmail", ref branchemail, value);
            }
        }
        private String faxnumber;
        public String FaxNumber
        {
            get
            {
                return faxnumber;
            }
            set
            {
                SetPropertyValue("FaxNumber", ref faxnumber, value);
            }
        }
        private String telnumber;
        public String TelNumber
        {
            get
            {
                return telnumber;
            }
            set
            {
                SetPropertyValue("TelNumber", ref telnumber, value);
            }
        }
        private Company _comp;
        [Association("Company-Branches")]
        public Company Company
        {
            get { return _comp; }
            set { SetPropertyValue("Company", ref _comp, value); }
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
    //Employee
    [DefaultClassOptions(), System.ComponentModel.DefaultProperty("UserName")]
    public class Employee : User
    {
        private Company company;

        public Employee(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            company = Session.FindObject<Company>(CriteriaOperator.Parse("Employees[Oid = ?]", SecuritySystem.CurrentUserId));
            Role roleUser;
            roleUser = Session.FindObject<Role>(new BinaryOperator("Name", "User"));
            base.Roles.Add(roleUser);
        }
        
        [Association("Company-Employees", typeof(Company))]
        public Company Company
        {
            get { return company; }
            set
            {
                SetPropertyValue("Company", ref company, value);
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

    //Account Type
    [DefaultClassOptions]
    [ObjectCaptionFormat("{0:Description}")]
    public class AccountType : BaseObject
    {
        public AccountType(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }
        private String description;
        [RuleRequiredField("RuleRequiredField for AccountType.Description", DefaultContexts.Save)]
        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                SetPropertyValue("Description", ref description, value);
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

    //Bank FM
    [DefaultClassOptions]
    [ObjectCaptionFormat("{0:BankName}")]
    public class Bank : BaseObject
    {
        public Bank(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }
        private String bankName;
        [RuleRequiredField("RuleRequiredField for BankName", DefaultContexts.Save)]
        public String BankName
        {
            get
            {
                return bankName;
            }
            set
            {
                SetPropertyValue("BankName", ref bankName, value);
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

    //Bank Account
    [DefaultClassOptions]
    [ObjectCaptionFormat("{0:AccountNumber}")]
    public class BankAccount : BaseObject
    {
        public BankAccount(Session session) : base(session) { }

        private Company company;
        [RuleRequiredField("RuleRequiredField for BankAccount.Company", DefaultContexts.Save)]
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
        [RuleRequiredField("RuleRequiredField for BankAccount.Branch", DefaultContexts.Save)]
        public Branch Branch
        {
            get { return branch; }
            set { SetPropertyValue("Branch", ref branch, value); }
        }

        private Currency currency;
        [RuleRequiredField("RuleRequiredField for BankAccount.Currency", DefaultContexts.Save)]
        public Currency Currency
        {
            get { return currency; }
            set { SetPropertyValue("Currency", ref currency, value); }
        }

        private Bank bank;
        [RuleRequiredField("RuleRequiredField for BankAccount.Bank", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public Bank Bank
        {
            get { return bank; }
            set { SetPropertyValue("Bank", ref bank, value); }
        }

        private String accountNumber;
        [RuleRequiredField("RuleRequiredField for AccountNumber", DefaultContexts.Save)]
        public String AccountNumber
        {
            get
            {
                return accountNumber;
            }
            set
            {
                SetPropertyValue("AccountNumber", ref accountNumber, value);
            }
        }

        private Decimal maintainingBalance;
        public Decimal MaintainingBalance
        {
            get
            {
                return maintainingBalance;
            }
            set
            {
                SetPropertyValue("MaintainingBalance", ref maintainingBalance, value);
            }
        }

        private Decimal beginingBalance;
        public Decimal BeginingBalance
        {
            get
            {
                return beginingBalance;
            }
            set
            {
                SetPropertyValue("BeginingBalance", ref beginingBalance, value);
            }
        }

        private DateTime beginingDate;
        public DateTime BeginingDate
        {
            get
            {
                return beginingDate;
            }
            set
            {
                SetPropertyValue("BeginingDate", ref beginingDate, value);
            }
        }

        private String bankAddress;
        public String BankAddress
        {
            get
            {
                return bankAddress;
            }
            set
            {
                SetPropertyValue("BankAddress", ref bankAddress, value);
            }
        }

        private String remarks;
        public String Remarks
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
    [ObjectCaptionFormat("{0:Description}")]
    public class VatCategory : BaseObject
    {
        public VatCategory(Session session) : base(session) { }
        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }
        private String description;
        [RuleRequiredField("RuleRequiredField for VatCategory.Description", DefaultContexts.Save)]
        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                SetPropertyValue("Description", ref description, value);
            }
        }
        private Decimal rate;
        public Decimal Rate
        {
            get
            {
                return rate;
            }
            set
            {
                SetPropertyValue("Rate", ref rate, value);
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

    //Business Partner's Type
    [DefaultClassOptions]
    [ObjectCaptionFormat("{0:Description}")]
    public class BusinessPartnerType : BaseObject
    {
        public BusinessPartnerType(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private String description;
        [RuleRequiredField("RuleRequiredField for BusinessPartnerType.Description", DefaultContexts.Save)]
        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                SetPropertyValue("Description", ref description, value);
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
    [ObjectCaptionFormat("{0:BusinessPartnerName}")]
    public class BusinessPartner : BaseObject
    {
        public BusinessPartner(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private Company company;
        [RuleRequiredField("RuleRequiredField for BusinessPartner.Company", DefaultContexts.Save)]
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
        [RuleRequiredField("RuleRequiredField for BusinessPartner.Branch", DefaultContexts.Save)]
        public Branch Branch
        {
            get { return branch; }
            set { SetPropertyValue("Branch", ref branch, value); }
        }

        private VatCategory vatCategory;
        [RuleRequiredField("RuleRequiredField for BusinessPartner.VatCategory", DefaultContexts.Save)]
        public VatCategory VatCategory
        {
            get { return vatCategory; }
            set { SetPropertyValue("VatCategory", ref vatCategory, value); }
        }

        private Term term;
        [RuleRequiredField("RuleRequiredField for BusinessPartner.Term", DefaultContexts.Save)]
        //[DataSourceCriteria("IsActive = True")]
        public Term Term
        {
            get { return term; }
            set { SetPropertyValue("Term", ref term, value); }
        }

        private Currency currency;
        public Currency Currency
        {
            get { return currency; }
            set { SetPropertyValue("Currency", ref currency, value); }
        }

        private BusinessPartnerType businessPartnerType;
        [RuleRequiredField("RuleRequiredField for BusinessPartner.BusinessPartnerType", DefaultContexts.Save)]
        //[DataSourceCriteria("IsActive = True")]
        public BusinessPartnerType BusinessPartnerType
        {
            get { return businessPartnerType; }
            set { SetPropertyValue("BusinessPartnerType", ref businessPartnerType, value); }
        }

        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }

        private String businessPartnerName;
        [RuleRequiredField("RuleRequiredField for BusinessPartnerName", DefaultContexts.Save)]
        public String BusinessPartnerName
        {
            get
            {
                return businessPartnerName;
            }
            set
            {
                SetPropertyValue("BusinessPartnerName", ref businessPartnerName, value);
            }
        }

        private Decimal creditLimit;
        public Decimal CreditLimit
        {
            get
            {
                return creditLimit;
            }
            set
            {
                SetPropertyValue("CreditLimit", ref creditLimit, value);
            }
        }

        private DateTime dateStarted;
        public DateTime DateStarted
        {
            get
            {
                return dateStarted;
            }
            set
            {
                SetPropertyValue("DateStarted", ref dateStarted, value);
            }
        }

        private String faxnumber;
        public String FaxNumber
        {
            get
            {
                return faxnumber;
            }
            set
            {
                SetPropertyValue("FaxNumber", ref faxnumber, value);
            }
        }
        private String telnumber;
        public String TelNumber
        {
            get
            {
                return telnumber;
            }
            set
            {
                SetPropertyValue("TelNumber", ref telnumber, value);
            }
        }

        private String email;
        public String Email
        {
            get
            {
                return email;
            }
            set
            {
                SetPropertyValue("Email", ref email, value);
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
    [ObjectCaptionFormat("{0:TermDescription}")]
    public class Term : BaseObject
    {
        public Term(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }
        private String termDescription;
        [RuleRequiredField("RuleRequiredField for TermDescription", DefaultContexts.Save)]
        public String TermDescription
        {
            get
            {
                return termDescription;
            }
            set
            {
                SetPropertyValue("TermDescription", ref termDescription, value);
            }
        }

        private Decimal numOfDays;
        public Decimal NumOfDays
        {
            get
            {
                return numOfDays;
            }
            set
            {
                SetPropertyValue("NumOfDays", ref numOfDays, value);
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
    [ObjectCaptionFormat("{0:Description}")]
    public class ChartOfAccounts : BaseObject
    {
        public ChartOfAccounts(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private Company company;
        [RuleRequiredField("RuleRequiredField for ChartOfAccounts.Company", DefaultContexts.Save)]
        public Company Company
        {
            get { return company; }
            set { SetPropertyValue("Company", ref company, value); }
        }

        private AccountType accountType;
        [RuleRequiredField("RuleRequiredField for ChartOfAccounts.AccountType", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public AccountType AccountType
        {
            get { return accountType; }
            set { SetPropertyValue("AccountType", ref accountType, value); }
        }

        private String accountId;
        public string AccountId
        {
            get
            {
                return accountId;
            }
            set
            {
                SetPropertyValue("AccountId", ref accountId, value);
            }
        }
        private String description;
        [RuleRequiredField("RuleRequiredField for ChartOfAccounts.Description", DefaultContexts.Save)]
        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                SetPropertyValue("Description", ref description, value);
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

        private Boolean isParent;
        public Boolean IsParent
        {
            get
            {
                return isParent;
            }
            set
            {
                SetPropertyValue("IsParent", ref isParent, value);
            }
        }

        private Decimal sortingSequence;
        public Decimal SortingSequence
        {
            get
            {
                return sortingSequence;
            }
            set
            {
                SetPropertyValue("SortingSequence", ref sortingSequence, value);
            }
        }

        private ChartOfAccounts parentAccountId;
        public ChartOfAccounts ParentAccountId
        {
            get { return parentAccountId; }
            set { SetPropertyValue("ParentAccountId", ref parentAccountId, value); }
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
    [ObjectCaptionFormat("{0:Description}")]
    public class ItemType : BaseObject
    {
        public ItemType(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }
        private String description;
        [RuleRequiredField("RuleRequiredField for ItemType.Description", DefaultContexts.Save)]
        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                SetPropertyValue("Description", ref description, value);
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
    [ObjectCaptionFormat("{0:Description}")]
    public class ProductGroup : BaseObject
    {
        public ProductGroup(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }
        private String description;
        [RuleRequiredField("RuleRequiredField for ProductGroup.Description", DefaultContexts.Save)]
        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                SetPropertyValue("Description", ref description, value);
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
    [ObjectCaptionFormat("{0:Description}")]
    public class UnitOfMeasure : BaseObject
    {
        public UnitOfMeasure(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }
        private String description;
        [RuleRequiredField("RuleRequiredField for UnitOfMeasure.Description", DefaultContexts.Save)]
        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                SetPropertyValue("Description", ref description, value);
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
    [ObjectCaptionFormat("{0:Description}")]
    public class ItemMaster : BaseObject
    {
        public ItemMaster(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private Company company;
        [RuleRequiredField("RuleRequiredField for ItemMaster.Company", DefaultContexts.Save)]
        public Company Company
        {
            get { return company; }
            set
            {
                SetPropertyValue("Company", ref company, value);
            }
        }

        private VatCategory vatCategory;
        [RuleRequiredField("RuleRequiredField for ItemMaster.VatCategory", DefaultContexts.Save)]
        public VatCategory VatCategory
        {
            get { return vatCategory; }
            set { SetPropertyValue("VatCategory", ref vatCategory, value); }
        }

        private ProductGroup productGroup;
        [DataSourceCriteria("IsActive = True")]
        public ProductGroup ProductGroup
        {
            get { return productGroup; }
            set
            {
                SetPropertyValue("ProductGroup", ref productGroup, value);
            }
        }

        private ItemType itemType;
        [DataSourceCriteria("IsActive = True")]
        public ItemType ItemType
        {
            get { return itemType; }
            set
            {
                SetPropertyValue("ItemType", ref itemType, value);
            }
        }

        private UnitOfMeasure unitOfMeasure;
        [RuleRequiredField("RuleRequiredField for ItemMaster.UnitOfMeasure", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public UnitOfMeasure UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set
            {
                SetPropertyValue("UnitOfMeasure", ref unitOfMeasure, value);
            }
        }

        private String description;
        [RuleRequiredField("RuleRequiredField for ItemMaster.Description", DefaultContexts.Save)]
        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                SetPropertyValue("Description", ref description, value);
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

        private Boolean isSerialized;
        public Boolean IsSerialized
        {
            get
            {
                return isSerialized;
            }
            set
            {
                SetPropertyValue("IsSerialized", ref isSerialized, value);
            }
        }

        private Decimal lastCost;
        public Decimal LastCost
        {
            get
            {
                return lastCost;
            }
            set
            {
                SetPropertyValue("LastCost", ref lastCost, value);
            }
        }

        private Decimal lastPrice;
        public Decimal LastPrice
        {
            get
            {
                return lastPrice;
            }
            set
            {
                SetPropertyValue("LastPrice", ref lastPrice, value);
            }
        }

        private String suppliersDescription;
        [RuleRequiredField("RuleRequiredField for SuppliersDescription", DefaultContexts.Save)]
        public String SuppliersDescription
        {
            get
            {
                return suppliersDescription;
            }
            set
            {
                SetPropertyValue("SuppliersDescription", ref suppliersDescription, value);
            }
        }

        private String sku;
        public String Sku
        {
            get
            {
                return sku;
            }
            set
            {
                SetPropertyValue("Sku", ref sku, value);
            }
        }

        private String suppliersSku;
        public String SuppliersSku
        {
            get
            {
                return suppliersSku;
            }
            set
            {
                SetPropertyValue("SuppliersSku", ref suppliersSku, value);
            }
        }

        private Decimal reorderPoint;
        public Decimal ReorderPoint
        {
            get
            {
                return reorderPoint;
            }
            set
            {
                SetPropertyValue("ReorderPoint", ref reorderPoint, value);
            }
        }

        private Decimal reorderQty;
        public Decimal ReorderQty
        {
            get
            {
                return reorderQty;
            }
            set
            {
                SetPropertyValue("ReorderQty", ref reorderQty, value);
            }
        }

        [Association("ItemMaster-Conversion"), Aggregated]
        public XPCollection<ItemConversion> Conversion
        {
            get
            {
                return GetCollection<ItemConversion>("Conversion");
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
    public class ItemConversion : BaseObject
    {
        public ItemConversion(Session session) : base(session) { }

        private UnitOfMeasure unitOfMeasureFrom;
        [RuleRequiredField("RuleRequiredField for UnitOfMeasureFrom", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public UnitOfMeasure UnitOfMeasureFrom
        {
            get { return unitOfMeasureFrom; }
            set
            {
                SetPropertyValue("UnitOfMeasureFrom", ref unitOfMeasureFrom, value);
            }
        }

        private UnitOfMeasure unitOfMeasureTo;
        [RuleRequiredField("RuleRequiredField for UnitOfMeasureTo", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public UnitOfMeasure UnitOfMeasureTo
        {
            get { return unitOfMeasureTo; }
            set
            {
                SetPropertyValue("UnitOfMeasureTo", ref unitOfMeasureTo, value);
            }
        }

        private Decimal qtyFrom;
        public Decimal QtyFrom
        {
            get
            {
                return qtyFrom;
            }
            set
            {
                SetPropertyValue("QtyFrom", ref qtyFrom, value);
            }
        }

        private Decimal qtyTo;
        public Decimal QtyTo
        {
            get
            {
                return qtyTo;
            }
            set
            {
                SetPropertyValue("QtyTo", ref qtyTo, value);
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

        private Boolean isDefault;
        public Boolean IsDefault
        {
            get
            {
                return isDefault;
            }
            set
            {
                SetPropertyValue("IsDefault", ref isDefault, value);
            }
        }

        private ItemMaster _item;
        [Association("ItemMaster-Conversion")]
        public ItemMaster ItemMaster
        {
            get { return _item; }
            set { SetPropertyValue("ItemMaster", ref _item, value); }
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
    [ObjectCaptionFormat("{0:WarehouseName}")]
    public class Warehouse : BaseObject
    {
        public Warehouse(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private Company company;
        [RuleRequiredField("RuleRequiredField for Warehouse.Company", DefaultContexts.Save)]
        public Company Company
        {
            get { return company; }
            set { SetPropertyValue("Company", ref company, value); }
        }

        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }
        private String warehousename;
        [RuleRequiredField("RuleRequiredField for WarehouseName", DefaultContexts.Save)]
        public String WarehouseName
        {
            get
            {
                return warehousename;
            }
            set
            {
                SetPropertyValue("WarehouseName", ref warehousename, value);
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
