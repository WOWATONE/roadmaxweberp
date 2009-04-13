using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{
    //Business Partner's Type
    [DefaultClassOptions]
    [ObjectCaptionFormat("{0:Description}")]
    public class BusinessPartnerType : BaseObject
    {
        public BusinessPartnerType(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
        }

        private String description;
        [RuleRequiredField("RuleRequiredField for BusinessPartnerType.Description", DefaultContexts.Save)]
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
