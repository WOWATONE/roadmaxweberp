using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{

    [DefaultClassOptions]
    [NavigationItem(false)]
    [CreatableItem(false)]
    [RuleCriteria("RuleDMarOtherQuantity", DefaultContexts.Save, "Quantity != 0", "Quantity shouldn't be 0", SkipNullOrEmptyValues = false)]
    [RuleCriteria("RuleDMarOtherPrice", DefaultContexts.Save, "Price != 0", "Price shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class DMarOther : BaseObject
    {
        public DMarOther(Session session) : base(session) { }
        public DMarOther()
        {
            
        }
                                                            

        private ChartOfAccounts accountId;
        [RuleRequiredField("RuleRequiredField for DMarOther.AccountId", DefaultContexts.Save)]
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
                if (DMarOh != null && !IsLoading)
                {
                    DMarOh.UpdateTotal();
                }
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
                if (DMarOh != null && !IsLoading)
                {
                    DMarOh.UpdateTotal();
                }
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

        private DMarHeader DMarOh;
        [Association("DMarHeader-DMarOthers")]
        public DMarHeader DMarHeader
        {
            get { return DMarOh; }
            set { SetPropertyValue("DMarHeader", ref DMarOh, value); }
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
