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

namespace DVLD2.Applications.Application_Types
{
    public partial class frmEditApplicationType : Form
    {
        private static clsApplicationType _clsApplicationType;

        private static byte _ApplicationTypeID;

        public frmEditApplicationType(clsApplication.enApplicationType ApplicationTypes)
        {
            InitializeComponent();
            _ApplicationTypeID = (byte)ApplicationTypes;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_clsApplicationType == null)
                return;

            _clsApplicationType.Title = tbTitle.Text;

            _clsApplicationType.Fees = Convert.ToInt32(tbFees.Text.Trim());

            _clsApplicationType.ApplicationTypeID = _ApplicationTypeID;


            if (_clsApplicationType.Save())
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data is not Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _clsApplicationType = clsApplicationType.FindByID(_ApplicationTypeID);

            if (_clsApplicationType == null)
                return;

            lbAppID.Text = _clsApplicationType.ApplicationTypeID.ToString();

            tbTitle.Text = _clsApplicationType.Title;

            tbFees.Text = _clsApplicationType.Fees.ToString();
        }


    }


}
