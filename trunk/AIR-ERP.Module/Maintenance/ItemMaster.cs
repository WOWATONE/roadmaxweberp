using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{
    [DefaultClassOptions]
    [ObjectCaptionFormat("{0:Description}")]
    public class ItemMaster : BaseObject
    {
        public ItemMaster(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private Company company;
        [RuleRequiredField("RuleRequiredField for ItemMaster.Company", DefaultContexts.Save)]
        public Company Company
        {
            get { return company; }
            set
            {
                SetPropertyValue("Company", ref company, value);
            }
        }

        private VatCategory vatCategory;
        [RuleRequiredField("RuleRequiredField for ItemMaster.VatCategory", DefaultContexts.Save)]
        public VatCategory VatCategory
        {
            get { return vatCategory; }
            set { SetPropertyValue("VatCategory", ref vatCategory, value); }
        }

        private ProductGroup productGroup;
        [DataSourceCriteria("IsActive = True")]
        public ProductGroup ProductGroup
        {
            get { return productGroup; }
            set
            {
                SetPropertyValue("ProductGroup", ref productGroup, value);
            }
        }

        private ItemType itemType;
        [DataSourceCriteria("IsActive = True")]
        public ItemType ItemType
        {
            get { return itemType; }
            set
            {
                SetPropertyValue("ItemType", ref itemType, value);
            }
        }

        private UnitOfMeasure unitOfMeasure;
        [RuleRequiredField("RuleRequiredField for ItemMaster.UnitOfMeasure", DefaultContexts.Save)]
        [DataSourceCriteria("IsActive = True")]
        public UnitOfMeasure UnitOfMeasure
        {
            get { return unitOfMeasure; }
            set
            {
                SetPropertyValue("UnitOfMeasure", ref unitOfMeasure, value);
            }
        }

        private String description;
        [RuleRequiredField("RuleRequiredField for ItemMaster.Description", DefaultContexts.Save)]
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

        private Boolean isSerialized;
        public Boolean IsSerialized
        {
            get
            {
                return isSerialized;
            }
            set
            {
                SetPropertyValue("IsSerialized", ref isSerialized, value);
            }
        }

        private Decimal lastCost;
        public Decimal LastCost
        {
            get
            {
                return lastCost;
            }
            set
            {
                SetPropertyValue("LastCost", ref lastCost, value);
            }
        }

        private Decimal lastPrice;
        public Decimal LastPrice
        {
            get
            {
                return lastPrice;
            }
            set
            {
                SetPropertyValue("LastPrice", ref lastPrice, value);
            }
        }

        private String suppliersDescription;
        [RuleRequiredField("RuleRequiredField for SuppliersDescription", DefaultContexts.Save)]
        public String SuppliersDescription
        {
            get
            {
                return suppliersDescription;
            }
            set
            {
                SetPropertyValue("SuppliersDescription", ref suppliersDescription, value);
            }
        }

        private String sku;
        public String Sku
        {
            get
            {
                return sku;
            }
            set
            {
                SetPropertyValue("Sku", ref sku, value);
            }
        }

        private String suppliersSku;
        public String SuppliersSku
        {
            get
            {
                return suppliersSku;
            }
            set
            {
                SetPropertyValue("SuppliersSku", ref suppliersSku, value);
            }
        }

        private Decimal reorderPoint;
        public Decimal ReorderPoint
        {
            get
            {
                return reorderPoint;
            }
            set
            {
                SetPropertyValue("ReorderPoint", ref reorderPoint, value);
            }
        }

        private Decimal reorderQty;
        public Decimal ReorderQty
        {
            get
            {
                return reorderQty;
            }
            set
            {
                SetPropertyValue("ReorderQty", ref reorderQty, value);
            }
        }

        [Association("ItemMaster-Conversion"), Aggregated]
        public XPCollection<ItemConversion> Conversion
        {
            get
            {
                return GetCollection<ItemConversion>("Conversion");
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
