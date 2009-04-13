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
    [RuleCriteria("RuleDRItemsQuantity", DefaultContexts.Save, "Quantity != 0", "Quantity shouldn't be 0", SkipNullOrEmptyValues = false)]
    [RuleCriteria("RuleDRItemsPrice", DefaultContexts.Save, "Price != 0", "Price shouldn't be 0", SkipNullOrEmptyValues = false)]
    public class DRItems : BaseObject
    {
        public DRItems(Session session) : base(session) { }
        public DRItems()
        {
            
        }
                                                           

        private ItemMaster itemMaster;
        [RuleRequiredField("RuleRequiredField for DRItems.ItemMaster", DefaultContexts.Save)]
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
        [RuleRequiredField("RuleRequiredField for DRItems.UnitOfMeasure", DefaultContexts.Save)]
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
        [RuleRequiredField("RuleRequiredField for DRItems.Warehouse", DefaultContexts.Save)]
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

        private Decimal discount;
        public Decimal Discount
        {
            get
            {
                return discount;
            }
            set
            {
                SetPropertyValue("Discount", ref discount, value);
                if (DoH != null && !IsLoading)
                {
                    DoH.UpdateTotal();
                }
            }
        }

        private Decimal discountRate;
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

        private SOItems soItems;
        [ImmediatePostData]
        [System.ComponentModel.Browsable(false)]
        public SOItems SoItems
        {
            get { return soItems; }
            set
            {
                SetPropertyValue("SoItems", ref soItems, value);
                if (value != null && !IsLoading)
                {
                    ItemMaster = soItems.ItemMaster;
                    Quantity = soItems.Quantity;
                    Price = soItems.Price;
                    Cost = soItems.Cost;
                    Discount = soItems.Discount;
                    DiscountRate = soItems.DiscountRate;
                    UnitOfMeasure = soItems.UnitOfMeasure;
                    Warehouse = soItems.Warehouse;
                }
            }
        }

        private DRHeader DoH;
        [Association("DRHeader-DRItem")]
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
