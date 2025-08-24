using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Applications.Local_License
{
    public partial class frmLocalDrivingLicenseDetails : Form
    {
        private static int _LDLApplicationID;
        public frmLocalDrivingLicenseDetails(int LDLApplicationID)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLocalDrivingLicenseDetails_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadLicenseAppInfo(_LDLApplicationID);
        }
    }
}
