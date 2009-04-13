using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{

    [DefaultClassOptions]
    [ObjectCaptionFormat("{0:WarehouseName}")]
    public class Warehouse : BaseObject
    {
        public Warehouse(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private Company company;
        [RuleRequiredField("RuleRequiredField for Warehouse.Company", DefaultContexts.Save)]
        public Company Company
        {
            get { return company; }
            set { SetPropertyValue("Company", ref company, value); }
        }

        private String acronym;
        public string Acronym
        {
            get
            {
                return acronym;
            }
            set
            {
                SetPropertyValue("Acronym", ref acronym, value);
            }
        }
        private String warehousename;
        [RuleRequiredField("RuleRequiredField for WarehouseName", DefaultContexts.Save)]
        public String WarehouseName
        {
            get
            {
                return warehousename;
            }
            set
            {
                SetPropertyValue("WarehouseName", ref warehousename, value);
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
