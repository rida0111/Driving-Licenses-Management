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

namespace DVLD2.Licenses.Local_License
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private static int _LDLApplicationID;

        public frmIssueDriverLicenseFirstTime(int LDLAppID)
        {
            InitializeComponent();
            _LDLApplicationID = LDLAppID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = clsLocalLicenses.AddNewLocalLicense(_LDLApplicationID, tbNote.Text.Trim());

            if (LicenseID != -1)
                MessageBox.Show($"License Issued Successfully With License ID ={LicenseID}",
                                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"License was Not Added !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Close();
        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadLicenseAppInfo(_LDLApplicationID);
        }
    }
}
