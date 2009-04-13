using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{
    [DefaultClassOptions]
    [ObjectCaptionFormat("{0:TermDescription}")]
    public class Term : BaseObject
    {
        public Term(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            isActive = true;
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
        private String termDescription;
        [RuleRequiredField("RuleRequiredField for TermDescription", DefaultContexts.Save)]
        public String TermDescription
        {
            get
            {
                return termDescription;
            }
            set
            {
                SetPropertyValue("TermDescription", ref termDescription, value);
            }
        }

        private Decimal numOfDays;
        public Decimal NumOfDays
        {
            get
            {
                return numOfDays;
            }
            set
            {
                SetPropertyValue("NumOfDays", ref numOfDays, value);
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
