using System;
using DevExpress.Persistent.Base;

namespace AIR_ERP.Module
{
    public class CurrentCompanyOidParameter : ReadOnlyParameter
    {
        public CurrentCompanyOidParameter()
            : base("CurrentCompanyOid", typeof(object))
        {
        }
        public CurrentCompanyOidParameter(string name, Type valueType)
            : base(name, valueType)
        {
            
        }
         
        public override object CurrentValue
        {
            get
            {
                return CurrentCompanyOid;
            }
        }
        public object CurrentCompanyOid { get; set; }
    }
}
