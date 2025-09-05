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

namespace DVLD.User
{
    public partial class frmManageUsers : Form
    {

        private DataTable _dtUsers;

        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAddUser_Click(object sender, EventArgs e)
        {

            frmAddEditUser frm = new frmAddEditUser();

            frm.ShowDialog();

            frmManageUsers_Load(null, null);
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {

            _dtUsers = clsUser.GetAllUsers();

            dgvUsers.DataSource = _dtUsers;

            lblRecordsNumber.Text = dgvUsers.RowCount.ToString();

            if (dgvUsers.Rows.Count > 0)
            {

                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 120;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 120;

                dgvUsers.Columns[2].HeaderText = "FullName";
                dgvUsers.Columns[2].Width = 350;

                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 140;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 100;
            }

            cbFilterby.SelectedIndex = 0;
        }

        private void cbFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilterby.Text != "None");
            cbIsActive.Visible = (cbFilterby.Text == "Is Active");

            if (cbIsActive.Visible)
            {
                txtFilter.Visible = false;
                cbIsActive.SelectedIndex = 0;
                return;
            }

            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvUsers.RowCount.ToString();
            }

            txtFilter.Text = "";
            txtFilter.Focus();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Value = "";

            switch (cbIsActive.Text)
            {

                case "Yes":
                    Value = "1";
                    break;
                case "No":
                    Value = "0";
                    break;
                default:
                    Value = "";
                    break;
            }

            if (Value == "")
                _dtUsers.DefaultView.RowFilter = "";
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", Value);
            
            lblRecordsNumber.Text = dgvUsers.RowCount.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string ColumnName = "";

            switch (cbFilterby.Text)
            {

                case "Person ID":
                    ColumnName = "PersonID";
                    break;
                case "User ID":
                    ColumnName = "UserID";
                    break;
                case "User Name":
                    ColumnName = "UserName";
                    break;
                case "Full Name":
                    ColumnName = "FullName";
                    break;
                default:
                    ColumnName = "None";
                    break;
            }
          
            if (txtFilter.Text == "" || ColumnName == "None")
                _dtUsers.DefaultView.RowFilter = "";
            else if (ColumnName == "PersonID" || ColumnName == "UserID")
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, txtFilter.Text.Trim());
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", ColumnName, txtFilter.Text.Trim());

            lblRecordsNumber.Text = dgvUsers.RowCount.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterby.Text == "User ID" || cbFilterby.Text == "Person ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.SelectedCells[0].Value);

            frm.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();

            frm.ShowDialog();

            frmManageUsers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser((int)dgvUsers.SelectedCells[0].Value);

            frm.ShowDialog();

            frmManageUsers_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.SelectedCells[0].Value;

            if (clsUser.DeleteUserInfo(UserID))
            {
                MessageBox.Show("User has been deleted Successfully", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmManageUsers_Load(null, null);
            }

            else
                MessageBox.Show("User Is not deleted due to data connected to it", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {       
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.SelectedCells[0].Value);

            frm.ShowDialog();

            frmManageUsers_Load(null, null);
        }

    }
}
