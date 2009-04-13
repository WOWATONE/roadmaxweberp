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
    [RuleCriteria("RuleSIOtherQuantity", DefaultContexts.Save, "Quantity != 0", "Quantity shouldn't be 0", SkipNullOrEmptyValues = false)]
    [RuleCriteria("RuleSIOtherPrice", DefaultContexts.Save, "Price != 0", "Price shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class SIOther : BaseObject
    {
        public SIOther(Session session) : base(session) { }
        public SIOther()
        {
            
        }
                                                            

        private ChartOfAccounts accountId;
        [RuleRequiredField("RuleRequiredField for SIOther.AccountId", DefaultContexts.Save)]
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
                if (SIoH != null && !IsLoading)
                {
                    SIoH.UpdateTotal();
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
                if (SIoH != null && !IsLoading)
                {
                    SIoH.UpdateTotal();
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

        private SIHeader SIoH;
        [Association("SIHeader-SIOthers")]
        public SIHeader SIHeader
        {
            get { return SIoH; }
            set { SetPropertyValue("SIHeader", ref SIoH, value); }
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
