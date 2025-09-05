using Businesses_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Application_Types
{

    public partial class frmEditApplicationType : Form
    {
        private  clsApplicationType _clsApplicationType;

        private  int _ApplicationTypeID;

        public frmEditApplicationType(int ApplicationType)
        {
            InitializeComponent();

            _ApplicationTypeID = ApplicationType;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error"
                   , "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_clsApplicationType == null)
                return;

            _clsApplicationType.Title = txtTitle.Text.Trim();

            _clsApplicationType.Fees = Convert.ToSingle(txtFees.Text.Trim());


            if (_clsApplicationType.Save())
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data is not Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _clsApplicationType = clsApplicationType.Find((clsApplication.enApplicationType)_ApplicationTypeID);

            if (_clsApplicationType == null)
                return;

            lblApplicationID.Text = _clsApplicationType.ApplicationTypeID.ToString();

            txtTitle.Text = _clsApplicationType.Title;

            txtFees.Text = _clsApplicationType.Fees.ToString();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This Text Box is Empty!");
            }
            else
                errorProvider1.SetError(txtTitle, "");
        }

       

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "This Text Box is Empty!");
                return;
            }
            else
                errorProvider1.SetError(txtFees, null);


            if(!clsValidation.IsNumber(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
                errorProvider1.SetError(txtFees, null);
        }


    }

}
