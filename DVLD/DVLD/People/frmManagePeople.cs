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

namespace DVLD.People
{
    public partial class frmManagePeople : Form
    {

        private DataTable _dtPeople;
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string ColumnName = "";

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    ColumnName = "PersonID";
                    break;
                case "National No.":
                    ColumnName = "NationalNo";
                    break;
                default:
                    ColumnName = cbFilterBy.Text;
                    break;
            }

            if (txtFilter.Text == "" || ColumnName == "None")
                _dtPeople.DefaultView.RowFilter = "";

            else if (ColumnName == "PersonID")
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] ={1}", ColumnName, txtFilter.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", ColumnName, txtFilter.Text.Trim());


            lbRecordsNumber.Text = dgvPeople.Rows.Count.ToString();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _dtPeople = clsPerson.GetAllPerson();

            dgvPeople.DataSource = _dtPeople;

            lbRecordsNumber.Text = dgvPeople.RowCount.ToString();

            cbFilterBy.SelectedIndex = 0;

            if (dgvPeople.RowCount > 0)
            {
                dgvPeople.Columns[0].HeaderText = "PersonID";
                dgvPeople.Columns[0].Width = 100;

                dgvPeople.Columns[1].HeaderText = "NationalNo";
                dgvPeople.Columns[1].Width = 100;

                dgvPeople.Columns[2].HeaderText = "FirstName";
                dgvPeople.Columns[2].Width = 100;

                dgvPeople.Columns[3].HeaderText = "SecondName";
                dgvPeople.Columns[3].Width = 100;

                dgvPeople.Columns[4].HeaderText = "ThirdName";
                dgvPeople.Columns[4].Width = 100;

                dgvPeople.Columns[5].HeaderText = "LastName";
                dgvPeople.Columns[5].Width = 100;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 70;

                dgvPeople.Columns[7].HeaderText = "DateOfBirth";
                dgvPeople.Columns[7].Width = 150;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 100;

                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 140;

                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 160;
            }

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilterBy.Text != "None");

            txtFilter.Text = "";

            txtFilter.Focus();
        }

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.SelectedCells[0].Value;

            frmPersonDetails frm = new frmPersonDetails(PersonID);

            frm.ShowDialog();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddorEditPerson frm = new frmAddorEditPerson();

            frm.ShowDialog();

            frmManagePeople_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt16(dgvPeople.SelectedCells[0].Value);

            frmPersonDetails frm = new frmPersonDetails(PersonID);

            frm.ShowDialog();

            frmManagePeople_Load(null, null);
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddorEditPerson frm = new frmAddorEditPerson();

            frm.ShowDialog();

            frmManagePeople_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt16(dgvPeople.SelectedCells[0].Value);

            frmAddorEditPerson frm = new frmAddorEditPerson(PersonID);

            frm.ShowDialog();

            frmManagePeople_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int PersonID = (int)dgvPeople.SelectedCells[0].Value;

            if (MessageBox.Show($"Are you Sure you want to delete Person [{PersonID}]", "Comfirm Delete",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            if (clsPerson.DeletePerson(PersonID))
            {
                MessageBox.Show("Person Delete Successfully", "Successful", MessageBoxButtons.OK
                    , MessageBoxIcon.Information);

                frmManagePeople_Load(null, null);
            }

            else
            {
                MessageBox.Show("Person was Not Deleted Beacause it has Data Linked to it", "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

     
    }
}
