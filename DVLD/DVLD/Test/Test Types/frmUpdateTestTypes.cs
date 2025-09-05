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
using static Businesses_Access_Layer.clsTestType;

namespace DVLD
{
    public partial class frmUpdateTestTypes : Form
    {
        private  clsTestType _TestType;

        private  int _TestTypeID;

        public frmUpdateTestTypes(int TestTypeID)
        {
            InitializeComponent();

            _TestTypeID = TestTypeID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error"
                  , "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.TestTitle = txtTestTitle.Text.Trim();

            _TestType.TestDescription = txtDescription.Text.Trim();

            _TestType.TestFees = Convert.ToSingle(txtFees.Text.Trim());

            if (_TestType.Save())
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data is not Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.FindByTestID((clsTestType.enTestType)_TestTypeID);

            lblTestTypeID.Text = _TestTypeID.ToString();

            if (_TestType == null)
                return;

            txtTestTitle.Text = _TestType.TestTitle;
            txtDescription.Text = _TestType.TestDescription;
            txtFees.Text = _TestType.TestFees.ToString();
        }

        private void TextBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;

            if (string.IsNullOrEmpty(Temp.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "Text box is Empty!");
                return;
            }
            else
                errorProvider1.SetError(Temp, null);

        }

        private void tbFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Text box is Empty!");
                return;
            }
            else
                errorProvider1.SetError(txtFees, null);

            
            if(!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number!");
                return;
            }
            else
                errorProvider1.SetError(txtFees, null);
        }

    }
}
