using Businesses_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Control
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
      
        private  clsLocalDrivingLicenseApplication _clsLDLApplication;

        public clsLocalDrivingLicenseApplication LDLApplicationInfo { get { return _clsLDLApplication; } }

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private void _FillApplicationInfo()
        {
            
            lblApplicationID.Text = _clsLDLApplication.ApplicationInfo.ApplicationID.ToString();

            lblStatus.Text = _clsLDLApplication.ApplicationInfo.StatusName;

            lblFees.Text = _clsLDLApplication.ApplicationInfo.PaidFess.ToString();

            lblApplicationTypeName.Text = _clsLDLApplication.ApplicationInfo.ApplicationsTypeInfo.Title;

            lblApplicant.Text = _clsLDLApplication.ApplicationInfo.PersonInfo.FullName;

            lblDate.Text = _clsLDLApplication.ApplicationInfo.ApplicationDate.ToShortDateString();

            lblStatusDate.Text = _clsLDLApplication.ApplicationInfo.LastStatusDate.ToShortDateString();

            lblCreatedBy.Text = _clsLDLApplication.ApplicationInfo.UserInfo.UserName;
        }

        public void ResetApplicationInfo()
        {
            
            lblApplicationID.Text = "[???]";

            lblStatus.Text = "[???]";

            lblFees.Text = "[$$$$]";

            lblApplicationTypeName.Text = "[???]";

            lblApplicant.Text = "[???]";

            lblDate.Text = "[???]";

            lblStatusDate.Text = "[???]";

            lblCreatedBy.Text = "[???]";

            llPersonInfo.Enabled = false;
        }

        public void LoadAppLicationInfo(int LDLApplicationID)
        {
            _clsLDLApplication = clsLocalDrivingLicenseApplication.FindByID(LDLApplicationID);

            if (_clsLDLApplication != null)
            {
                _FillApplicationInfo();
                return;
            }

            ResetApplicationInfo();
        }

        private void llPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = _clsLDLApplication.ApplicationInfo.PersonID;

            frmPersonDetails frm = new frmPersonDetails(PersonID);

            frm.ShowDialog();
        }

      

    }
}
