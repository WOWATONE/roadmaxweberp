using System;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
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
