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
    [RuleCriteria("RuleARVOtherQuantity", DefaultContexts.Save, "Quantity != 0", "Quantity shouldn't be 0", SkipNullOrEmptyValues = false)]
    [RuleCriteria("RuleARVOtherPrice", DefaultContexts.Save, "Price != 0", "Price shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class ARVOther : BaseObject
    {
        public ARVOther(Session session) : base(session) { }
        public ARVOther()
        {
            
        }
                                                            

        private ChartOfAccounts accountId;
        [RuleRequiredField("RuleRequiredField for ARVOther.AccountId", DefaultContexts.Save)]
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
                if (AoH != null && !IsLoading)
                {
                    AoH.UpdateTotal();
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
                if (AoH != null && !IsLoading)
                {
                    AoH.UpdateTotal();
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

        private ARVHeader AoH;
        [Association("ARVHeader-ARVOthers")]
        public ARVHeader ARVHeader
        {
            get { return AoH; }
            set { SetPropertyValue("ARVHeader", ref AoH, value); }
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
