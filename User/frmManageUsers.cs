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

namespace DVLD2.User
{


    public partial class frmManageUsers : Form
    {

        private static DataTable _dtUsers = new DataTable();

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

            lbRecordsNumber.Text = dgvUsers.RowCount.ToString();

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

            cbFilter.SelectedIndex = 0;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIsActive.Visible = (cbFilter.Text == "IsActive");

            TxtFilter.Visible = (cbFilter.Text != "None" && cbFilter.Text != "IsActive");

            if (cbIsActive.Visible)
            {
                cbIsActive.SelectedIndex = 0;
                return;
            }

            _dtUsers.DefaultView.RowFilter = "";

            lbRecordsNumber.Text = dgvUsers.RowCount.ToString();

            TxtFilter.Text = "";

            TxtFilter.Focus();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ColumnName = "";

            switch (cbIsActive.Text)
            {
                case "All":
                    ColumnName = "All";
                    break;
                case "Yes":
                    ColumnName = "1";
                    break;
                case "No":
                    ColumnName = "0";
                    break;
                default:
                    ColumnName = "";
                    break;

            }

            if (ColumnName == "All")
                _dtUsers.DefaultView.RowFilter = "";
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", ColumnName);


            lbRecordsNumber.Text = dgvUsers.RowCount.ToString();
        }

        private void TxtFilter_TextChanged(object sender, EventArgs e)
        {
            string ColumnName = "";

            switch (cbFilter.Text)
            {

                case "Person ID":
                    ColumnName = "PersonID";
                    break;
                case "User ID":
                    ColumnName = "UserID";
                    break;
                case "UserName":
                    ColumnName = "UserName";
                    break;
                case "FullName":
                    ColumnName = "FullName";
                    break;
                case "IsActive":
                    ColumnName = "IsActive";
                    break;
                default:
                    ColumnName = "None";
                    break;
            }


            if (TxtFilter.Text == "" || ColumnName == "None" || ColumnName == "IsActive")
                _dtUsers.DefaultView.RowFilter = "";
            else if (ColumnName == "PersonID" || ColumnName == "UserID")
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, TxtFilter.Text.Trim());
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", ColumnName, TxtFilter.Text.Trim());

            lbRecordsNumber.Text = dgvUsers.RowCount.ToString();
        }

        private void TxtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilter.Text == "User ID" || cbFilter.Text == "Person ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.SelectedCells[0].Value;

            frmUserInfo frm = new frmUserInfo(UserID);

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
            int UserID = (int)dgvUsers.SelectedCells[0].Value;

            frmAddEditUser frm = new frmAddEditUser(UserID);

            frm.ShowDialog();

            frmManageUsers_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.SelectedCells[0].Value;

            if (clsUser.DeleteUserInfo(UserID))
            {
                MessageBox.Show("User has been deleted Successfuly", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmManageUsers_Load(null, null);
            }

            else         
                MessageBox.Show("User Is not deleted due to data connected to it", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.SelectedCells[0].Value;

            frmChangePassword frm = new frmChangePassword(UserID);

            frm.ShowDialog();

            frmManageUsers_Load(null, null);
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This property is not implemented yet !", "Not Implemented"
               , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This property is not implemented yet !", "Not Implemented"
               , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }


}
