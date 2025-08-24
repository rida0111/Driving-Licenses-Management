using DVLD2.Applications.Application_Types;
using DVLD2.Applications.International_License;
using DVLD2.Applications.Local_License;
using DVLD2.Applications.Release_Application;
using DVLD2.Applications.Renew_application;
using DVLD2.Applications.Replacement_application;
using DVLD2.Driver;
using DVLD2.Licenses.DetainLicense;
using DVLD2.Login;
using DVLD2.People;
using DVLD2.Test.Test_Types;
using DVLD2.User;
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
    public partial class frmMain : Form
    {
        private static bool menuItemClicked = false;

        private frmLoginScreen _LoginScreen;

      
        public frmMain(frmLoginScreen frmLoginScreen)
        {
            InitializeComponent();

            this._LoginScreen = frmLoginScreen;
           _LoginScreen.Hide();
        }
    
        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers frm = new frmManageUsers();
            frm.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople frm = new frmManagePeople();
            frm.ShowDialog();
        }

        private void DriversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void ManageApplicationTypestoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();

            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddInternationalLicense frm = new frmAddInternationalLicense();
            frm.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInterlicensesApplication frm = new frmListInterlicensesApplication();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApp frm = new frmAddUpdateLocalDrivingLicenseApp();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApp frm = new frmListLocalDrivingLicenseApp();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            int UserID = clsUserInfo.CurrentUser.UserId;

            frmUserInfo frm = new frmUserInfo(UserID);

            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = clsUserInfo.CurrentUser.UserId;

            frmChangePassword frm = new frmChangePassword(UserID);

            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuItemClicked = true;

            _LoginScreen.Show();
            this.Close();
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApp frm = new frmListLocalDrivingLicenseApp();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicense frm = new frmRenewLocalDrivingLicense();
            frm.ShowDialog();
        }

        private void replacmentForLostOrDamegedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementLicense frm = new frmReplacementLicense();
            frm.ShowDialog();
        }

        private void realeseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frm = new frmReleaseDetainLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frm= new frmReleaseDetainLicense();
            frm.ShowDialog();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!menuItemClicked)
                Application.Exit();
            else
                menuItemClicked = false;
        }
    }


}
