using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_License
{
    public partial class frmLocalDrivingLicenseDetails : Form
    {
        public frmLocalDrivingLicenseDetails(int LDLApplicationID)
        {
            InitializeComponent();

            ctrlDrivingLicenseApplicationInfo1.LoadLicenseInfo(LDLApplicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
