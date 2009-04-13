using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{
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
}
