using Businesses_Access_Layer;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        public int LicenseID = -1;

        private clsLocalLicenses _License;

        private  clsPerson _Person;
        public clsLocalLicenses LicenseInfo { get { return _License; } }

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

   
        public void LoadDriverLicenseInfo(int licenseID)
        {
            _License = clsLocalLicenses.FindByID(licenseID);

            if (_License == null)
            {
                MessageBox.Show($"No license with this ID = {licenseID}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LicenseID = -1;
                _ResetDriverLicenseInfo();
                return;
            }

            _FillDriverLicenseInfo();
        }

        private void _LoadPersonImage()
        {
            
            pbGendor.Image = (_Person.Gendor == 0) ? Resources.Man_32 : Resources.Woman_32;

            if (string.IsNullOrEmpty(_Person.ImagePath))
                pbDriver.Image = (_Person.Gendor == 0) ? Resources.Male_512 : Resources.Female_512;       
            else
            {
                if (File.Exists(_Person.ImagePath))
                    pbDriver.ImageLocation = _Person.ImagePath;
                else
                    MessageBox.Show("Could not found this image: =" + _Person.ImagePath);
            }
                     
        }

        private void _FillDriverLicenseInfo()
        {
            LicenseID = _License.LicenseId;

            _Person = _License.ApplicationInfo.PersonInfo;

            lblLicenseClassName.Text = _License.LicenseClassInfo.ClassName;

            lblNationalNo.Text = _Person.NationalNo;

            lblDriverName.Text = _Person.FullName;

            lblLicenseID.Text = _License.LicenseId.ToString();

            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";

            lblIssueDate.Text = _License.IssueDate.ToShortDateString();

            lblNotes.Text = string.IsNullOrEmpty(_License.Notes) ? "No Notes" : _License.Notes;

            lbIlsActive.Text = _License.IsActive ? "Yes" : "No";

            lblDateBirth.Text = _Person.DateofBirth.ToShortDateString();

            lblDriverID.Text = _License.DriverId.ToString();

            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();

            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";

            lblIssueReason.Text = _License.IssueReasonName;

            _LoadPersonImage();
        }

        private void _ResetDriverLicenseInfo()
        {
            lblLicenseClassName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblDriverName.Text = "[????]";

            lblLicenseID.Text = "[????]";
            lblGendor.Text = "[????]";
            lblIssueDate.Text = "[????]";

            lblNotes.Text = "[????]";
            lbIlsActive.Text = "[????]";
            lblDateBirth.Text = "[????]";

            lblDriverID.Text = "[????]";
            lblExpirationDate.Text = "[????]";
            lblIsDetained.Text = "[????]";

            lblIssueReason.Text = "[????]";

            pbDriver.Image = Resources.Male_512;

             pbGendor.Image = Resources.Man_32;  
        }


    }
}
