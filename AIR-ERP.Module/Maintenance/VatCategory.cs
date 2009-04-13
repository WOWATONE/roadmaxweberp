using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{
    [DefaultClassOptions]
    [ObjectCaptionFormat("{0:Description}")]
    public class VatCategory : BaseObject
    {
        public VatCategory(Session session) : base(session) { }
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
        private String description;
        [RuleRequiredField("RuleRequiredField for VatCategory.Description", DefaultContexts.Save)]
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
        private Decimal rate;
        public Decimal Rate
        {
            get
            {
                return rate;
            }
            set
            {
                SetPropertyValue("Rate", ref rate, value);
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
