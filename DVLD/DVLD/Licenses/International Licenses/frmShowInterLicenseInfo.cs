using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmShowInterLicenseInfo : Form
    {
        public frmShowInterLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();

            ctrlDriverInternationalLicenseInfo1.LoadInternationalLicenseInfo(InternationalLicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
