using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{
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
        [DataSourceCriteria("IsParent = True and IsActive = True")]
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
}
