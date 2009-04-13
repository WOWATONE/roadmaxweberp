using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{
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
}
