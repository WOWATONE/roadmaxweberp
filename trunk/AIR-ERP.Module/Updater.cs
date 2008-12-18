using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;

namespace AIR_ERP.Module
{
    public class Updater : ModuleUpdater
    {
        public Updater(Session session, Version currentDBVersion) : base(session, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();
            Employee userAdmin = Session.FindObject<Employee>(new BinaryOperator("UserName", "Admin"));
            if (new XPCollection<Company>(Session).Count == 0)
            {
                Role administratorRole;
                administratorRole = new Role(Session);
                administratorRole.Name = "Administrator role";
                administratorRole.AddPermission(new EditModelPermission());
                administratorRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.AllAccess));
                administratorRole.Save();

                Role userRole;
                userRole = new Role(Session);
                userRole.Name = "User";
                userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.Navigate));
                userRole.AddPermission(new ObjectAccessPermission(typeof(object), ObjectAccess.Read));
                userRole.Save();

                userAdmin = new Employee(Session);
                userAdmin.UserName = "Admin";
                userAdmin.FirstName = "Admin";
                userAdmin.LastName = "";
                userAdmin.Roles.Add(administratorRole);
                userAdmin.Save();

                Branch branch1 = new Branch(Session);
                branch1.Acronym = "WTPR";
                branch1.Branchname = "Roadmax Head Office";


                Company company1 = new Company(Session);
                company1.CompanyName = "Roadmax Marketing Inc.";
                company1.Acronym = "WTPR";
                company1.Employees.Add(userAdmin);
                company1.Branches.Add(branch1);
                company1.Save();

            }            
        }
    }
}
