using Businesses_Access_Layer;
using DVLD2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private static clsLocalLicenses _License;

        private static clsPerson _Person;
        public clsLocalLicenses LicenseInfo { get { return _License; } }

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadDriverLicenseInfo(int licenseID)
        {
            _License = clsLocalLicenses.FindByID(licenseID);

            if (_License != null)
                _FillDriverLicenseInfo();
            else
            {
                MessageBox.Show($"No license with this ID = {licenseID}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetDriverLicenseInfo();
            }

        }

        private void HandleImage()
        {
            
            pbGendor.Image = (_Person.Gendor == 0) ? Resources.Man_32 : Resources.Woman_32;

            if (string.IsNullOrEmpty(_Person.ImagePath))
                pbDriver.Image = (_Person.Gendor == 0) ? Resources.Male_512 : Resources.Female_512;       
            else
                pbDriver.Image = Image.FromFile(_Person.ImagePath);
            
        }

        private void _FillDriverLicenseInfo()
        {

            _Person = _License.ApplicationInfo.PersonInfo;

            lbClassName.Text = _License.LicenseClassInfo.ClassName;

            lbNationalNo.Text = _Person.NationalNo;

            lbName.Text = _Person.FullName;

            lbLicenseID.Text = _License.LicenseId.ToString();

            lbGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";

            lbIssueDate.Text = _License.IssueDate.ToShortDateString();

            lbNotes.Text = (string.IsNullOrEmpty(_License.Notes)) ? "No Notes" : _License.Notes;

            lbIsActive.Text = _License.IsActive ? "Yes" : "No";

            lbDateBirth.Text = _Person.DateofBirth.ToShortDateString();

            lbDriverID.Text = _License.DriverId.ToString();

            lbExDate.Text = _License.ExpirationDate.ToShortDateString();

            lbDetained.Text = clsDetainLicense.IsLicenseDetained(_License.LicenseId) ? "Yes" : "No";

            lbIssueReason.Text = _License.IssueReasonName;

            HandleImage();
        }

        private void _ResetDriverLicenseInfo()
        {
            lbClassName.Text = "[????]";
            lbNationalNo.Text = "[????]";
            lbName.Text = "[????]";

            lbLicenseID.Text = "[????]";
            lbGendor.Text = "[????]";
            lbIssueDate.Text = "[????]";

            lbNotes.Text = "[????]";
            lbIsActive.Text = "[????]";
            lbDateBirth.Text = "[????]";

            lbDriverID.Text = "[????]";
            lbExDate.Text = "[????]";
            lbDetained.Text = "[????]";

            lbIssueReason.Text = "[????]";
          
             pbDriver.Image = Resources.Male_512;
             pbGendor.Image = Resources.Man_32;  

        }

    }
}
