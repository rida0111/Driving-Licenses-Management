using Businesses_Access_Layer;
using DVLD2.Licenses.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Licenses
{
    public partial class frmShowLicenseHistory : Form
    {
        private static int _DriverID;
        public frmShowLicenseHistory(int DriverID)
        {
            InitializeComponent();
            _DriverID = DriverID;
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            int PersonID = clsDriver.FindByID(_DriverID).PersonID;

            ctrlPersonCardwithFilter1.Filter = false;

            ctrlPersonCardwithFilter1.LoadPersonInfo(PersonID);

            ctrlDriverLicenses1.LoadDriverLicenses(_DriverID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
