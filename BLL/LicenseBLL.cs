using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LicenseBLL
    {
        #region Properties
        private static LicenseBLL instance;

        public static LicenseBLL Instance
        {
            get
            {
                if (instance == null) instance = new LicenseBLL();
                return instance;
            }
        }
        #endregion

        public void AssignLicensesToCombobox(Guna2ComboBox cbo)
        {
            List<License> licenses = LicenseDAL.Instance.GetAllLicenses();
            this.AddTeachersToCombobox(cbo, licenses);
        }

        private void AddTeachersToCombobox(Guna2ComboBox cbo, List<License> licenses)
        {
            License license = new License();
            license.LicenseName = "Select License";
            licenses.Insert(0, license);

            cbo.DataSource = licenses;
            cbo.ValueMember = "LicenseID";
            cbo.DisplayMember = "LicenseName";
        }
    }
}
