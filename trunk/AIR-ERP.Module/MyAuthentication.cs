using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using DevExpress.ExpressApp.Security;

namespace AIR_ERP.Module
{
    public class MyAuthentication : AuthenticationBase
    {
        private MyLogonParameters logonParameters;

        public MyAuthentication()
        {
            logonParameters = new MyLogonParameters();
        }
        public override void ClearSecuredLogonParameters()
        {
            logonParameters.Password = "";
            base.ClearSecuredLogonParameters();
        }
        public override object Authenticate(DevExpress.ExpressApp.ObjectSpace objectSpace)
        {
            if (logonParameters.Employee == null)
            {
                throw new ArgumentNullException("User");
            }
            if (!logonParameters.Employee.ComparePassword(logonParameters.Password))
            {
                throw new AuthenticationException(logonParameters.Employee.UserName, "Wrong password");
            }
            return objectSpace.GetObject(logonParameters.Employee);
        }
        public override IList<Type> GetBusinessClasses()
        {
            return new Type[] { typeof(MyLogonParameters) };
        }
        public override bool AskLogonParametersViaUI
        {
            get
            {
                return true;
            }
        }
        public override object LogonParameters
        {
            get { return logonParameters; }
        }

        public override bool IsLogoffEnabled
        {
            get { return true; }
        }
    }
}
