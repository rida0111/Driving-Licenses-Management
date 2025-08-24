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

namespace DVLD2.Licenses.International_Licenses.Control
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {

        private static clsPerson _Person;

        private static clsInternationalLicense _clsInterLicense;

        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void HandleImage()
        {
            
           pbGendor.Image = (_Person.Gendor == 0) ? Resources.Man_32 : Resources.Woman_32;

            if (string.IsNullOrEmpty(_Person.ImagePath))
                pbInterlicense.Image = (_Person.Gendor == 0) ? Resources.Male_512 : Resources.Female_512;
            else
                pbInterlicense.Image = Image.FromFile(_Person.ImagePath);
            
        }
        public void LoadInternationalLicenseInfo(int InternationalLicenseID)
        {

            _clsInterLicense = clsInternationalLicense.FindByInterID(InternationalLicenseID);

            if (_clsInterLicense != null)
                _FillInternationalLicenseInfo();
            else
                _RestInternationalLicenseInfo();
        }
        private void _FillInternationalLicenseInfo()
        {

            _Person = _clsInterLicense.ApplicationInfo.PersonInfo;

            lbgendor.Text = _Person.Gendor == 0 ? "Male" : "Female";

            lbName.Text = _Person.FullName;

            lbIntelicenseID.Text = _clsInterLicense.InterLicenseID.ToString();

            lbLocalLicenseID.Text = _clsInterLicense.LocalLicenseID.ToString();

            lbNationalNo.Text = _Person.NationalNo.ToString();

            lbIssueDate.Text = _clsInterLicense.IssueDate.ToShortDateString();

            lbAppID.Text = _clsInterLicense.ApplicationID.ToString();

            lbIsActive.Text = _clsInterLicense.IsActive ? "Yes" : "No";

            lbBirthDate.Text = _Person.DateofBirth.ToShortDateString();

            lbDriverID.Text = _clsInterLicense.DriverID.ToString();

            lbExDate.Text = _clsInterLicense.ExpirationDate.ToShortDateString();

            HandleImage();
        }
        private void _RestInternationalLicenseInfo()
        {

            lbgendor.Text = "[???]";

            lbName.Text = "[???]";

            lbIntelicenseID.Text = "[???]";

            lbLocalLicenseID.Text = "[???]";

            lbNationalNo.Text = "[???]";

            lbIssueDate.Text = "[???]";

            lbAppID.Text = "[???]";

            lbIsActive.Text = "[???]";

            lbBirthDate.Text = "[???]";

            lbDriverID.Text = "[???]";

            lbExDate.Text = "[???]";

            pbInterlicense.Image = Resources.Male_512;

            pbGendor.Image = Resources.Man_32;
        }


    }
}
