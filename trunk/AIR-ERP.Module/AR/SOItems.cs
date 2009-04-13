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
    [ObjectCaptionFormat("{0:SOHeader.DocNum}")]
    [RuleCriteria("RuleSOItemsQuantity", DefaultContexts.Save, "Quantity != 0", "Quantity shouldn't be 0", SkipNullOrEmptyValues = false)]
    [RuleCriteria("RuleSOItemsPrice", DefaultContexts.Save, "Price != 0", "Price shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class SOItems : BaseObject
    {
        public SOItems(Session session) : base(session) { }
        public SOItems()
        {
            
        }
                                                           

        private ItemMaster itemMaster;
        [RuleRequiredField("RuleRequiredField for SOItems.ItemMaster", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        [ImmediatePostData]
        public ItemMaster ItemMaster
        {
            get { return itemMaster; }
            set
            {
                SetPropertyValue("ItemMaster", ref itemMaster, value);
                if (value != null && !IsLoading)
                {
                    UnitOfMeasure = value.UnitOfMeasure;
                    Price = value.LastPrice;
                    Cost = value.LastCost;
                }
            }
        }

        private UnitOfMeasure unitOfMeasure;
        [RuleRequiredField("RuleRequiredField for SOItems.UnitOfMeasure", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public UnitOfMeasure UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set
            {
                SetPropertyValue("UnitOfMeasure", ref unitOfMeasure, value);
            }
        }

        private Warehouse warehouse;
        [RuleRequiredField("RuleRequiredField for SOItems.Warehouse", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public Warehouse Warehouse
        {
            get { return warehouse; }
            set
            {
                SetPropertyValue("Warehouse", ref warehouse, value);
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

        private Decimal discount;
        [ImmediatePostData]
        public Decimal Discount
        {
            get
            {
                return discount;
            }
            set
            {
                SetPropertyValue("Discount", ref discount, value);
                if (SoH != null && !IsLoading)
                {
                    SoH.UpdateTotal();
                }
            }
        }

        private Decimal discountRate;
        [ImmediatePostData]
        public Decimal DiscountRate
        {
            get
            {
                return discountRate;
            }
            set
            {
                SetPropertyValue("DiscountRate", ref discountRate, value);
            }
        }

        private SOHeader SoH;
        [Association("SOHeader-SOItem")]
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
