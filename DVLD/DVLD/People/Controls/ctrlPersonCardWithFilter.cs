using Businesses_Access_Layer;
using DVLD.People;
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
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        public int PersonID { get { return ctrlPersonCard1.PersonID; } }

        public clsPerson PersonInfo { get { return ctrlPersonCard1.PersonInfo; } }

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


        public event Action<int> OnFilterComplete;

        protected virtual void FilterComplete(int PersonID)
        {

            Action<int> handler = OnFilterComplete;

            if (handler != null)
            {
                handler(PersonID);
            }

        }
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }
        public void LoadPersonInfo(int PersonID)
        {

            clsPerson _Person = clsPerson.FindById(PersonID);

            if (_Person == null)
                cbFilterby.SelectedIndex = 1;
            else
            {
                cbFilterby.Text = "PersonID";
                txtFilter.Text = PersonID.ToString();
            }

            ctrlPersonCard1.LoadPersonInfoByID(PersonID);

            if (OnFilterComplete != null)
                FilterComplete(ctrlPersonCard1.PersonID);
        }
        private void BackOfPersonID(int PersonId)
        {
            LoadPersonInfo(PersonId);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error"
                  , "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (cbFilterby.Text == "PersonID")            
                ctrlPersonCard1.LoadPersonInfoByID(int.Parse(txtFilter.Text.Trim()));          
            else
                ctrlPersonCard1.LoadPersonInfoByNationalNo(txtFilter.Text.Trim());


            if (OnFilterComplete != null)
                FilterComplete(ctrlPersonCard1.PersonID);

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddorEditPerson AddNewPerson = new frmAddorEditPerson();

            AddNewPerson.BackPersonID += BackOfPersonID;

            AddNewPerson.ShowDialog();
        }
     
        private void ctrlFilterPersonInfo_Load(object sender, EventArgs e)
        {
            cbFilterby.SelectedIndex = 1;
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Text = string.Empty;

            txtFilter.Focus();
        }
        private void tbFilter_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilter, "Text Box is Empty!");
            }
            else
                errorProvider1.SetError(txtFilter, "");
        }

        public void FilterFocus()
        {
            txtFilter.Focus();
        }
  
        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterby.Text == "PersonID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }
        }


    }

}
