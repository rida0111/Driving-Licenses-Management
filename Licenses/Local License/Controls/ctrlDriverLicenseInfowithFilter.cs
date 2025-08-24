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

namespace DVLD2
{
    public partial class ctrlDriverLicenseInfowithFilter : UserControl
    {
        private bool _Filter = true;

        public clsLocalLicenses LicenseInfo { get { return ctrlDriverLicenseInfo1.LicenseInfo; } }

        public bool Filter
        {
            get { return _Filter; }
            set
            {
                _Filter = value;

                gbFilter.Enabled = _Filter;
            }
        }

        public event Action<int> OnLicenseComplete;

        protected virtual void LicenseComplete(int LicenseID)
        {

            Action<int> handler = OnLicenseComplete;

            if (handler != null)
            {
                handler(LicenseID);
            }

        }

        public ctrlDriverLicenseInfowithFilter()
        {
            InitializeComponent();
        }

        public void LoadLicenseID(int LicenseID)
        {

            tbLicenseID.Text = LicenseID.ToString();

            ctrlDriverLicenseInfo1.LoadDriverLicenseInfo(LicenseID);


            if (OnLicenseComplete != null)
            {
                LicenseComplete(LicenseID);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error"
                  , "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            int LicenseID = Convert.ToInt32(tbLicenseID.Text.Trim());

            ctrlDriverLicenseInfo1.LoadDriverLicenseInfo(LicenseID);

            if (OnLicenseComplete != null)
            {
                LicenseComplete(LicenseID);
            }
        }

        private void tbLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void tbLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbLicenseID.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(tbLicenseID, "Text Box is Empty!");
            }
            else
                errorProvider1.SetError(tbLicenseID, "");
        }

    }
}
