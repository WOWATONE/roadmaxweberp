using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Reports;
using DevExpress.Data.Filtering;
using DevExpress.XtraReports.UI;

namespace AIR_ERP.Module.Win
{
    public partial class PrintOutViewController : ViewController
    {
        public PrintOutViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void printOut_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            String currentOid = "";
            ReportData reportdata = ObjectSpace.FindObject<ReportData>(new BinaryOperator("Name", String.Format("{0}PrintOut", View.ObjectType.Name)));
            XtraReport xr = reportdata.LoadXtraReport(ObjectSpace);
            currentOid = (((DetailView)View).CurrentObject.ToString().Remove(0,
                (((DetailView)View).CurrentObject.ToString().Length) - 38));
            currentOid = currentOid.Replace("(", "{");
            currentOid = currentOid.Replace(")", "}");
            xr.FilterString = String.Format("[Oid] = {0}", currentOid);
            xr.ShowPreviewDialog();
        }
    }
}
