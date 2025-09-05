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

namespace DVLD
{
    public partial class frmShowLicenseHistory : Form
    {

        private int _PersonID = -1;

        public frmShowLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLicenseHistory_Load(object sender, EventArgs e)
        {
           
            ctrlPersonCardWithFilter1.Filter = false;

            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);

            ctrlDriverLicenses1.LoadDriverLicenses(_PersonID);
        }

    }

}
