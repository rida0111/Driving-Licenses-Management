using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Licenses.International_Licenses
{
    public partial class frmShowInterLicenseInfo : Form
    {
        private static int _InterLicenseID;
        public frmShowInterLicenseInfo(int InterLicenseID)
        {
            InitializeComponent();
            _InterLicenseID = InterLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void frmShowInternationalInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverInterLicenseInfo1.LoadInternationalLicenseInfo(_InterLicenseID);
        }

    }
}
