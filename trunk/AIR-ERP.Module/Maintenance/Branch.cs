using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{
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
}
