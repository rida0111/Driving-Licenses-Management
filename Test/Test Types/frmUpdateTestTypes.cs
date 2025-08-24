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

namespace DVLD2.Test.Test_Types
{
    public partial class frmUpdateTestTypes : Form
    {
        private static clsTestType _TestType;

        private static byte _TestTypeID;

        public frmUpdateTestTypes(clsTestAppointment.enTestType TestType)
        {
            InitializeComponent();

            _TestTypeID = (byte)TestType;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _TestType.TestTitle = tbTestTitle.Text;

            _TestType.TestDescription = tbDescription.Text;

            _TestType.TestFees = Convert.ToInt32(tbfees.Text.Trim());

            if (_TestType.Save())
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data is not Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.FindByTestID(_TestTypeID);

            if (_TestType == null)
                return;

            lbTestTypeID.Text = _TestType.TestTypeID.ToString();

            tbTestTitle.Text = _TestType.TestTitle;

            tbDescription.Text = _TestType.TestDescription;

            tbfees.Text = _TestType.TestFees.ToString();
        }


    }
}
