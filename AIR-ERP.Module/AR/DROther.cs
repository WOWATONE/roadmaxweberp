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
    [RuleCriteria("RuleDROtherQuantity", DefaultContexts.Save, "Quantity != 0", "Quantity shouldn't be 0", SkipNullOrEmptyValues = false)]
    [RuleCriteria("RuleDROtherPrice", DefaultContexts.Save, "Price != 0", "Price shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class DROther : BaseObject
    {
        public DROther(Session session) : base(session) { }
        public DROther()
        {
            
        }
                                                           

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
                if (DoH != null && !IsLoading)
                {
                    DoH.UpdateTotal();
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
                if (DoH != null && !IsLoading)
                {
                    DoH.UpdateTotal();
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
}
