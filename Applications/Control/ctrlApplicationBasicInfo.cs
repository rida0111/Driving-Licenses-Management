using Businesses_Access_Layer;
using DVLD2.People;
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
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private static clsLDLApplication _clsLDLApplication;

        public clsLDLApplication LDLApplicationInfo { get { return _clsLDLApplication; } }

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }


        private void _FillApplicationInfo()
        {
            lbAppID.Text = _clsLDLApplication.ApplicationInfo.ApplicationID.ToString();

            lbStatus.Text = _clsLDLApplication.ApplicationInfo.StatusName;

            lbFees.Text = _clsLDLApplication.ApplicationInfo.PaidFess.ToString();

            lbApplicationTypeName.Text = _clsLDLApplication.ApplicationInfo.ApplicationsTypeInfo.Title;

            lbApplicant.Text = _clsLDLApplication.ApplicationInfo.PersonInfo.FullName;

            lbDate.Text = _clsLDLApplication.ApplicationInfo.ApplicationDate.ToShortDateString();

            lbStatusDate.Text = _clsLDLApplication.ApplicationInfo.LastStatusDate.ToShortDateString();

            lbCreatedBy.Text = _clsLDLApplication.ApplicationInfo.UserInfo.UserName;
        }

        private void _ResetApplicationInfo()
        {
            lbAppID.Text = "[???]";

            lbStatus.Text = "[???]";

            lbFees.Text = "[$$$$]";

            lbApplicationTypeName.Text = "[???]";

            lbApplicant.Text = "[???]";

            lbDate.Text = "[???]";

            lbStatusDate.Text = "[???]";

            lbCreatedBy.Text = "[???]";

            linklPersonInfo.Enabled = false;
        }

        public void LoadAppLicationInfo(int LDLApplicationID)
        {
            _clsLDLApplication = clsLDLApplication.FindByID(LDLApplicationID);

            if (_clsLDLApplication != null)
                _FillApplicationInfo();
            else
                _ResetApplicationInfo();
        }

        private void linklPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = _clsLDLApplication.ApplicationInfo.PersonId;

           frmPersonDetails frm = new frmPersonDetails(PersonID);

            frm.ShowDialog();
        }
    }
}
