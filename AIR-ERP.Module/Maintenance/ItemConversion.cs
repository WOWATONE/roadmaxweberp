using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{
    [DefaultClassOptions]
    public class ItemConversion : BaseObject
    {
        public ItemConversion(Session session) : base(session) { }

        private UnitOfMeasure unitOfMeasureFrom;
        [RuleRequiredField("RuleRequiredField for UnitOfMeasureFrom", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public UnitOfMeasure UnitOfMeasureFrom
        {
            get { return unitOfMeasureFrom; }
            set
            {
                SetPropertyValue("UnitOfMeasureFrom", ref unitOfMeasureFrom, value);
            }
        }

        private UnitOfMeasure unitOfMeasureTo;
        [RuleRequiredField("RuleRequiredField for UnitOfMeasureTo", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public UnitOfMeasure UnitOfMeasureTo
        {
            get { return unitOfMeasureTo; }
            set
            {
                SetPropertyValue("UnitOfMeasureTo", ref unitOfMeasureTo, value);
            }
        }

        private Decimal qtyFrom;
        public Decimal QtyFrom
        {
            get
            {
                return qtyFrom;
            }
            set
            {
                SetPropertyValue("QtyFrom", ref qtyFrom, value);
            }
        }

        private Decimal qtyTo;
        public Decimal QtyTo
        {
            get
            {
                return qtyTo;
            }
            set
            {
                SetPropertyValue("QtyTo", ref qtyTo, value);
            }
        }

        private Decimal cost;
        public Decimal Cost
        {
            get
            {
                return cost;
            }
            set
            {
                SetPropertyValue("Cost", ref cost, value);
            }
        }

        private Boolean isDefault;
        public Boolean IsDefault
        {
            get
            {
                return isDefault;
            }
            set
            {
                SetPropertyValue("IsDefault", ref isDefault, value);
            }
        }

        private ItemMaster _item;
        [Association("ItemMaster-Conversion")]
        public ItemMaster ItemMaster
        {
            get { return _item; }
            set { SetPropertyValue("ItemMaster", ref _item, value); }
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
