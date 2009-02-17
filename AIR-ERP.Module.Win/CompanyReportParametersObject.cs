using System;

using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;

namespace AIR_ERP.Module.Win
{
    [NonPersistent]
    public class CompanyReportParametersObject : ReportParametersObjectBase
    {
        public CompanyReportParametersObject(Session session) : base(session) { }

        public override CriteriaOperator GetCriteria()
        {
            return CriteriaWrapper.ParseCriteriaWithReadOnlyParameters(
            "[Oid] = '@CurrentCompanyOid'", ReportDataType);

        }
        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }
    }

}
