using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;

namespace AIR_ERP.Module
{
    //Employee
    [DefaultClassOptions(), System.ComponentModel.DefaultProperty("UserName")]
    public class Employee : User
    {
        private Company company;

        public Employee(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            company = Session.FindObject<Company>(CriteriaOperator.Parse("Employees[Oid = ?]", SecuritySystem.CurrentUserId));
            Role roleUser;
            roleUser = Session.FindObject<Role>(new BinaryOperator("Name", "User"));
            base.Roles.Add(roleUser);
        }

        [Association("Company-Employees", typeof(Company))]
        public Company Company
        {
            get { return company; }
            set
            {
                SetPropertyValue("Company", ref company, value);
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
