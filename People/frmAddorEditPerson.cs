using Businesses_Access_Layer;
using DVLD2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.People
{
    public partial class frmAddorEditPerson : Form
    {

        private static int _PersonID;

        private static string _SourceFile = "";


        private clsPerson _clsPerson = new clsPerson();


        public delegate void DataBackEvent(int PersonID);

        public event DataBackEvent BackPersonID;

        public delegate void DataBack(Image image);

        public event DataBack BackImagePath;

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

        private void FillingcbCountry()
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

            if (_People != null)
            {

                if (_Mode == enMode.AddNew)
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtNationalNo, "National number is used for another person !");
                }

                if (_Mode == enMode.Update)
                {

                    if (_clsPerson.NationalNo == txtNationalNo.Text.Trim())
                        errorProvider1.SetError(txtNationalNo, "");
                    else
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtNationalNo, "National number is used for another person !");
                    }

                }

            }
            else
                errorProvider1.SetError(txtNationalNo, "");


        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
                return;


            Regex emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);


            if (emailRegex.IsMatch(txtEmail.Text))
            {
                e.Cancel = false;

                errorProvider1.SetError(txtEmail, "");
            }
            else
                errorProvider1.SetError(txtEmail, "Invalid Email Address format !");
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

        private void UpdatePersonInfo()
        {

            _clsPerson = clsPerson.FindById(_PersonID);

            if (_clsPerson == null)
                return;
            
            lbPersonId.Text = _PersonID.ToString();

            txtAddress.Text = _clsPerson.Address;

            txtFirstName.Text = _clsPerson.FirstName;

            txtThirdName.Text = _clsPerson.ThirdName;

            txtSecondName.Text = _clsPerson.SecondName;

            txtLastName.Text = _clsPerson.LastName;

            txtEmail.Text = _clsPerson.Email;

            txtPhone.Text = _clsPerson.Phone;

            txtNationalNo.Text = _clsPerson.NationalNo;

            dateTimePicker1.Value = _clsPerson.DateofBirth;

            rbMale.Checked = (_clsPerson.Gendor == 0) ? true : false;

            rbFemale.Checked = (_clsPerson.Gendor == 1) ? true : false;

            if (!(string.IsNullOrEmpty(_clsPerson.ImagePath)))
            {
                linkRemove.Visible = true;
                 pbPersonImage.ImageLocation = _clsPerson.ImagePath;
            }

        }

        private void HandleImage()
        {

            if (linkRemove.Visible == false)
            {
                _DeleteFilefromFolder(_clsPerson.ImagePath);

                _clsPerson.ImagePath = DBNull.Value.ToString();
            }

            else if (!string.IsNullOrEmpty(_SourceFile))
            {
                _DeleteFilefromFolder(_clsPerson.ImagePath);

                _clsPerson.ImagePath = GenerateGuid();
                _SourceFile = "";
            }

        }

        private void frmAddorEditPerson_Load(object sender, EventArgs e)
        {

            FillingcbCountry();

            if (_Mode == enMode.AddNew)
            {
                lbtext.Text = "Add New Person";
                this.Text = "Add New Person";

                cbCountries.SelectedIndex = cbCountries.FindString("Morocco");

                dateTimePicker1.MinDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);

                dateTimePicker1.Value = dateTimePicker1.MinDate;
            }

            if (_Mode == enMode.Update)
            {
                lbtext.Text = "Update Person Info";
                this.Text = "Update Person Info";

                UpdatePersonInfo();
                cbCountries.SelectedIndex = cbCountries.FindString(clsCountry.FindByID(_clsPerson.NationalCountryId).CountryName);
               
            }

        }
        private string GenerateGuid()
        {

            string DestFolder = @"C:\DVLD-People-Images\";

            string newFileName = DestFolder + Guid.NewGuid().ToString() + ".png";

            string newFilePath = Path.Combine(DestFolder, newFileName);

            File.Copy(_SourceFile, newFilePath, true);


            return newFileName;

        }
        private bool _DeleteFilefromFolder(string ImagePath)
        {
           
            bool isDelete = false;


            if (string.IsNullOrEmpty(ImagePath))
                return isDelete;


            string[] files = Directory.GetFiles(@"C:\DVLD-People-Images", "*", SearchOption.AllDirectories);

            foreach (string file in files)
            {

                if (File.Exists(ImagePath))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();

                    File.Delete(file);
                    isDelete = true;

                    break;
                }

            }

            return isDelete;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valide!, put the mouse over the red icon(s) to see the error"
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

            _clsPerson.DateofBirth = dateTimePicker1.Value;

            _clsPerson.NationalCountryId = clsCountry.FindByName(cbCountries.SelectedItem.ToString()).CountryID;


            if (rbMale.Checked)
                _clsPerson.Gendor = 0;
            else
                _clsPerson.Gendor = 1;

            HandleImage();


            if (_clsPerson.Save())
            {

                if (_Mode == enMode.Update)
                    MessageBox.Show("Data Updated Succesfully !", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (_Mode == enMode.AddNew)
                {
                    _PersonID = _clsPerson.PersonID;

                    lbtext.Text = "Update Person";

                    lbPersonId.Text = _PersonID.ToString();
                    _Mode = enMode.Update;

                    MessageBox.Show("Data Saved Succesfully !", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                BackPersonID?.Invoke(_PersonID);
            }
            else
                MessageBox.Show("Fail to Save Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void linkSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select a file";

            openFileDialog.Filter = "All files (*.*)|*.*";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _SourceFile = openFileDialog.FileName;

                pbPersonImage.ImageLocation = _SourceFile;

                linkRemove.Visible = true;
            }

             ResetImage(pbPersonImage.Image);
        }
       
        private void ResetImage(Image image)
        {
            BackImagePath?.Invoke(image);
        }

        private void linkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            ResetImage(pbPersonImage.Image);

            linkRemove.Visible = false;
        }


    }


}
