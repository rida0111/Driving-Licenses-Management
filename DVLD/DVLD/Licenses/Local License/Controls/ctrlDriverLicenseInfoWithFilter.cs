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
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        private  int _LicenseID { get { return ctrDriverLicenseInfo1.LicenseID; } }  

        public clsLocalLicenses LicenseInfo { get { return ctrDriverLicenseInfo1.LicenseInfo; } }

        private bool _Filter = true;

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

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        protected virtual void OnLicenseSelected(int LicenseID)
        {

            Action<int> handler = OnLicenseComplete;

            if (handler != null)
            {
                handler(LicenseID);
            }
        }

        public void LoadLicenseInfo(int LicenseID)
        {

            txtLicenseID.Text = LicenseID.ToString();

            ctrDriverLicenseInfo1.LoadDriverLicenseInfo(LicenseID);

            if (OnLicenseComplete != null && gbFilter.Enabled)
            {
                OnLicenseSelected(this._LicenseID);
            }
        }

      
        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error"
                  , "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            int LicenseID = int.Parse(txtLicenseID.Text.Trim());

            LoadLicenseInfo(LicenseID);
        }

        private void tbLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if(e.KeyChar ==(char)13)
            {
                btnSearch.PerformClick();
            }
        }

        private void tbLicenseID_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtLicenseID, "Text Box is Empty!");
            }
            else
                errorProvider1.SetError(txtLicenseID, "");

        }


        public void FilterFocus()
        {
            txtLicenseID.Focus();
        }
    
    }
}
