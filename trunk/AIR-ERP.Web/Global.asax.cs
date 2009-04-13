using System;
using System.Configuration;
using System.Web;

using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using AIR_ERP.Module;

namespace AIR_ERP.Web
{
    public class Global : HttpApplication
    {
        private void Instance_CreateCustomLogonWindowObjectSpace(object sender, CreateCustomLogonWindowObjectSpaceEventArgs e)
        {
            e.ObjectSpace = ((XafApplication)sender).CreateObjectSpace();
            ((MyLogonParameters)e.LogonParameters).ObjectSpace = e.ObjectSpace;
        }
        public Global()
        {
            InitializeComponent();
        }
        protected void Application_Start(Object sender, EventArgs e)
        {
            WebApplication.OldStyleLayout = false;
        }
        protected void Session_Start(Object sender, EventArgs e)
        {
            WebApplication.SetInstance(Session, new AIR_ERPAspNetApplication());
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            WebApplication.Instance.Security = new SecurityComplex<User, Role>(new MyAuthentication());
            WebApplication.Instance.CreateCustomLogonWindowObjectSpace += Instance_CreateCustomLogonWindowObjectSpace;
            WebApplication.Instance.Setup();
            WebApplication.Instance.Start();
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            string filePath = HttpContext.Current.Request.PhysicalPath;
            if (!string.IsNullOrEmpty(filePath)
                && (filePath.IndexOf("Images") >= 0) && !System.IO.File.Exists(filePath))
            {
                HttpContext.Current.Response.End();
            }
            //if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("default.aspx"))
            //{
            //    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.ApplicationPath.EndsWith("\\") ?
            //        HttpContext.Current.Request.ApplicationPath : HttpContext.Current.Request.ApplicationPath + "\\"
            //        + "DefaultVertical.aspx" + HttpContext.Current.Request.Url.Query);
            //}
        }
        protected void Application_EndRequest(Object sender, EventArgs e)
        {
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            ErrorHandling.Instance.ProcessApplicationError();
        }
        protected void Session_End(Object sender, EventArgs e)
        {
            WebApplication.DisposeInstance(Session);
        }
        protected void Application_End(Object sender, EventArgs e)
        {
        }
        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}
