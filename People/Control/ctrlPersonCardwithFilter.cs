using Businesses_Access_Layer;
using DVLD2.People;
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
    public partial class ctrlPersonCardwithFilter : UserControl
    {
   
        public   int PersonID  {  get { return ctrlPersonCard1.PersonID; } }

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

        public ctrlPersonCardwithFilter()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {

            clsPerson _Person = clsPerson.FindById(PersonID);

            if (_Person == null)
                cbFilter.SelectedIndex = 1;
            else
            {
                cbFilter.Text = "PersonID";
                tbFilter.Text = PersonID.ToString();
            }

            ctrlPersonCard1.LoadPersonInfoByID(PersonID);


            if (OnFilterComplete != null)
                FilterComplete(ctrlPersonCard1.PersonID);
        }
        private void BackOfPersonID(int PersonId)
        {
            LoadPersonInfo(PersonId);
        }
        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "PersonID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }
        private void tbFilter_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbFilter.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(tbFilter, "Text Box is Empty!");
            }
            else
                errorProvider1.SetError(tbFilter, "");
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error"
                  , "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (cbFilter.Text == "PersonID")
            {
                int PersonID = Convert.ToInt32(tbFilter.Text.Trim());

                ctrlPersonCard1.LoadPersonInfoByID(PersonID);
            }
            else
                ctrlPersonCard1.LoadPersonInfoByNationalNo(tbFilter.Text.Trim());


            if (OnFilterComplete != null)
                FilterComplete(ctrlPersonCard1.PersonID);

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            frmAddorEditPerson frm = new frmAddorEditPerson();

            frm.BackPersonID += BackOfPersonID;

            frm.ShowDialog();
        }
        private void ctrlPersonCardwithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 1;
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilter.Text = string.Empty;

            tbFilter.Focus();
        }

      
    }
}
