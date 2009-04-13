using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{
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
}
