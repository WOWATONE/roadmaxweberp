using System;
using System.Configuration;
using System.Windows.Forms;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using AIR_ERP.Module;

namespace AIR_ERP.Win
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            AIR_ERPWindowsFormsApplication winApplication = new AIR_ERPWindowsFormsApplication();
            winApplication.Security = new SecurityComplex<User, Role>(new MyAuthentication());
            winApplication.CreateCustomLogonWindowObjectSpace += new EventHandler<CreateCustomLogonWindowObjectSpaceEventArgs>(application_CreateCustomLogonWindowObjectSpace);
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            try
            {
                winApplication.Setup();
                winApplication.Start();
            }
            catch (Exception e)
            {
                winApplication.HandleException(e);
            }
        }
        static void application_CreateCustomLogonWindowObjectSpace(object sender, CreateCustomLogonWindowObjectSpaceEventArgs e)
        {
            e.ObjectSpace = ((XafApplication)sender).CreateObjectSpace();
            ((MyLogonParameters)e.LogonParameters).ObjectSpace = e.ObjectSpace;
        }
    }
}
