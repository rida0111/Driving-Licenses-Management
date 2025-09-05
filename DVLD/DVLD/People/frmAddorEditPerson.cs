using Businesses_Access_Layer;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmAddorEditPerson : Form
    {

        private  int _PersonID;


        private clsPerson _clsPerson;

        public delegate void DataBackEvent(int PersonID);

        public event DataBackEvent BackPersonID;
   
        private enum enMode { AddNew = 0, Update = 1 }

        private enMode _Mode;

        public frmAddorEditPerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddorEditPerson(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
            _Mode = enMode.Update;
        }

        private void TxtBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox textbox = (TextBox)sender;

            if (string.IsNullOrEmpty(textbox.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(textbox, textbox.Tag + " is Empty !");
            }
            else
                errorProvider1.SetError(textbox, "");
        }

        private void FillcbCountry()
        {
            DataTable dt = clsCountry.GetAllCountries();

            foreach (DataRow row in dt.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number Is Empty !");
                return;
            }

            clsPerson _People = clsPerson.FindByNationalNo(txtNationalNo.Text.Trim());

            if (_clsPerson.NationalNo != txtNationalNo.Text.Trim() && _People != null)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National number is used for another person !");
            }
            else
                errorProvider1.SetError(txtNationalNo, "");
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
                return;

            if (!clsValidation.IsEmailValid(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address format !");
            }
            else
                errorProvider1.SetError(txtEmail, ""); 
        }
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Male_512;
        }
        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Resources.Female_512;
        }

        private void _FillPersonInfo()
        {

            _clsPerson = clsPerson.FindById(_PersonID);

            if (_clsPerson == null)
                return;

            lblPersonId.Text = _PersonID.ToString();

            txtAddress.Text = _clsPerson.Address;

            txtFirstName.Text = _clsPerson.FirstName;

            txtThirdName.Text = _clsPerson.ThirdName;

            txtSecondName.Text = _clsPerson.SecondName;

            txtLastName.Text = _clsPerson.LastName;

            txtEmail.Text = _clsPerson.Email;

            txtPhone.Text = _clsPerson.Phone;

            txtNationalNo.Text = _clsPerson.NationalNo;

            dtpDateofBirth.Value = _clsPerson.DateofBirth;

            rbMale.Checked = (_clsPerson.Gendor == 0) ? true : false;

            rbFemale.Checked = (_clsPerson.Gendor == 1) ? true : false;

            llRemove.Visible = !string.IsNullOrEmpty(_clsPerson.ImagePath);

            if (!(string.IsNullOrEmpty(_clsPerson.ImagePath)))
                pbPersonImage.ImageLocation = _clsPerson.ImagePath;
        }

        private void HandleImage()
        {
          
            if (pbPersonImage.ImageLocation != _clsPerson.ImagePath)
            {
                if (!string.IsNullOrEmpty(_clsPerson.ImagePath))
                {
                    File.Delete(_clsPerson.ImagePath);             
                }

                if (pbPersonImage.ImageLocation != null)
                {
                    string SourceFile = pbPersonImage.ImageLocation.ToString();
                    pbPersonImage.ImageLocation = GenerateGuid(SourceFile);
                }    
                   
            }
        }

        private void frmAddorEditPerson_Load(object sender, EventArgs e)
        {
            FillcbCountry();

            if (_Mode == enMode.AddNew)
            {
                lbtext.Text = "Add New Person";
                this.Text = "Add New Person";

                _clsPerson = new clsPerson();

                cbCountries.SelectedIndex = cbCountries.FindString("Morocco");

                dtpDateofBirth.MaxDate = DateTime.Now.AddYears(-18);
              
                dtpDateofBirth.MinDate = DateTime.Now.AddYears(-100);

                dtpDateofBirth.Value = dtpDateofBirth.MaxDate;
            }

            if (_Mode == enMode.Update)
            {
                lbtext.Text = "Update Person Info";
                this.Text = "Update Person Info";

                _FillPersonInfo();
                cbCountries.SelectedIndex = cbCountries.FindString(clsCountry.FindByID(_clsPerson.NationalCountryId).CountryName);
            }

        }
        private string GenerateGuid(string sourceFile)
        {

            string FolderPath = @"C:\DVLD-People-Images\";
            
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);                   
            }

            FileInfo fi = new FileInfo(sourceFile);
            string extn = fi.Extension;

            string newFileName = FolderPath + Guid.NewGuid().ToString() + extn;

            string newFilePath = Path.Combine(FolderPath, newFileName);

            File.Copy(pbPersonImage.ImageLocation, newFilePath, true);

            return newFileName;
        }
     
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error"
                    , "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _clsPerson.Address = txtAddress.Text.Trim();

            _clsPerson.FirstName = txtFirstName.Text.Trim();

            _clsPerson.LastName = txtLastName.Text.Trim();

            _clsPerson.SecondName = txtSecondName.Text.Trim();

            _clsPerson.ThirdName = txtThirdName.Text.Trim();

            _clsPerson.Email = txtEmail.Text.Trim();

            _clsPerson.Phone = txtPhone.Text.Trim();

            _clsPerson.NationalNo = txtNationalNo.Text.Trim();

            _clsPerson.DateofBirth = dtpDateofBirth.Value;

            _clsPerson.NationalCountryId = clsCountry.FindByName(cbCountries.SelectedItem.ToString()).CountryID;


            if (rbMale.Checked)
                _clsPerson.Gendor = 0;
            else
                _clsPerson.Gendor = 1;

            HandleImage();

            _clsPerson.ImagePath = pbPersonImage.ImageLocation;

            if (_clsPerson.Save())
            {
                _PersonID = _clsPerson.PersonID;

                lbtext.Text = "Update Person";

                lblPersonId.Text = _PersonID.ToString();

                _Mode = enMode.Update;

                MessageBox.Show("Data Saved Successfully !", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                BackPersonID?.Invoke(_PersonID);
            }
            else
                MessageBox.Show("Fail to Save Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select a file";

            openFileDialog.Filter = "All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {               
                pbPersonImage.ImageLocation = openFileDialog.FileName;
                llRemove.Visible = true;
            }
        }
        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;
    
            llRemove.Visible = false;
        }

     
    }
}
