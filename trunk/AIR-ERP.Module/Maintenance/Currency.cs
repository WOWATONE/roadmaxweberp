using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace AIR_ERP.Module
{
    //Currency 
    [DefaultClassOptions]
    [RuleCriteria("", DefaultContexts.Save, "CurrencyRate != 0", "Currency Rate shouldn't be 0", SkipNullOrEmptyValues = false)]
    [ObjectCaptionFormat("{0:CurrencyName}")]
    public class Currency : BaseObject
    {
        public Currency(Session session) : base(session) { }

        private String currencyname;
        [RuleRequiredField("RuleRequiredField for Currency Name", DefaultContexts.Save)]
        public string CurrencyName
        {
            get
            {
                return currencyname;
            }
            set
            {
                SetPropertyValue("CurrencyName", ref currencyname, value);
            }
        }
        private Decimal currencyrate;
        public Decimal CurrencyRate
        {
            get
            {
                return currencyrate;
            }
            set
            {
                SetPropertyValue("CurrencyRate", ref currencyrate, value);
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
