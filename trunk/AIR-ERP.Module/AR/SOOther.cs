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
    [RuleCriteria("RuleSOOtherQuantity", DefaultContexts.Save, "Quantity != 0", "Quantity shouldn't be 0", SkipNullOrEmptyValues = false)]
    [RuleCriteria("RuleSOOtherPrice", DefaultContexts.Save, "Price != 0", "Price shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class SOOther : BaseObject
    {
        public SOOther(Session session) : base(session) { }
        public SOOther()
        {
            
        }
                                                           

        private ChartOfAccounts accountId;
        [RuleRequiredField("RuleRequiredField for SOOther.AccountId", DefaultContexts.Save)]
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
        [ImmediatePostData]
        public Decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                SetPropertyValue("Price", ref price, value);
                if (SoH != null && !IsLoading)
                {
                    SoH.UpdateTotal();
                }
            }
        }

        private Decimal quantity;
        [ImmediatePostData]
        public Decimal Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                SetPropertyValue("Quantity", ref quantity, value);
                if (SoH != null && !IsLoading)
                {
                    SoH.UpdateTotal();
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

        private SOHeader SoH;
        [Association("SOHeader-SOOthers")]
        public SOHeader SOHeader
        {
            get { return SoH; }
            set { SetPropertyValue("SOHeader", ref SoH, value); }
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
