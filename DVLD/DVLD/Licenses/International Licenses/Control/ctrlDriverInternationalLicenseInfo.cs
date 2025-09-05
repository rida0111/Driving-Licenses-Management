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
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {

        private  clsInternationalLicense _clsInterLicense;

        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void HandleImage()
        {

            pbGendor.Image = (_clsInterLicense.DriverInfo.PersonInfo.Gendor == 0) ? Resources.Man_32 : Resources.Woman_32;

            if (string.IsNullOrEmpty(_clsInterLicense.DriverInfo.PersonInfo.ImagePath))
                pbInterlicense.Image = (_clsInterLicense.DriverInfo.PersonInfo.Gendor == 0) ? Resources.Male_512 : Resources.Female_512;
            else
            {
                if (File.Exists(_clsInterLicense.DriverInfo.PersonInfo.ImagePath))
                    pbInterlicense.ImageLocation = _clsInterLicense.DriverInfo.PersonInfo.ImagePath;
                else
                    MessageBox.Show("Could not found this image: =" + _clsInterLicense.DriverInfo.PersonInfo.ImagePath);

            }
            
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
            lblgendor.Text = _clsInterLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";

            lbllName.Text = _clsInterLicense.DriverInfo.PersonInfo.FullName;

            lblInternationallicenseID.Text = _clsInterLicense.InterLicenseID.ToString();

            lblLocalLicenseID.Text = _clsInterLicense.LocalLicenseID.ToString();

            lblNationalNo.Text = _clsInterLicense.DriverInfo.PersonInfo.NationalNo.ToString();

            lblIssueDate.Text = _clsInterLicense.IssueDate.ToShortDateString();

            lblApplicationID.Text = _clsInterLicense.ApplicationID.ToString();

            lblIsActive.Text = _clsInterLicense.IsActive ? "Yes" : "No";

            lblBirthDate.Text = _clsInterLicense.DriverInfo.PersonInfo.DateofBirth.ToShortDateString();

            lblDriverID.Text = _clsInterLicense.DriverID.ToString();

            lbExpirationDate.Text = _clsInterLicense.ExpirationDate.ToShortDateString();

            HandleImage();
        }
        private void _RestInternationalLicenseInfo()
        {

            lblgendor.Text = "[???]";

            lbllName.Text = "[???]";

            lblInternationallicenseID.Text = "[???]";

            lblLocalLicenseID.Text = "[???]";

            lblNationalNo.Text = "[???]";

            lblIssueDate.Text = "[???]";

            lblApplicationID.Text = "[???]";

            lblIsActive.Text = "[???]";

            lblBirthDate.Text = "[???]";

            lblDriverID.Text = "[???]";

            lbExpirationDate.Text = "[???]";

            pbInterlicense.Image = Resources.Male_512;

            pbGendor.Image = Resources.Man_32;
        }

    }
}
