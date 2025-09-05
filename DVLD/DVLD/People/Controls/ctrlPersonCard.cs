using Businesses_Access_Layer;
using DVLD.People;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlPersonCard : UserControl
    {

        private  clsPerson _clsPerson;

        private int _PersonID = -1;
        public int PersonID { get { return _PersonID; } }
        public clsPerson PersonInfo { get { return _clsPerson; } }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoadPersonInfoByID(int personID)
        {
            _clsPerson = clsPerson.FindById(personID);

            if(_clsPerson == null) 
            {
                MessageBox.Show("No Person with this ID = " + personID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetPersonInfo();
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfoByNationalNo(string NationalNo)
        {

            _clsPerson = clsPerson.FindByNationalNo(NationalNo);
          
            if (_clsPerson == null)
            {
                MessageBox.Show("No Person with this NationalNumber = " + NationalNo, "Not Found"
                  , MessageBoxButtons.OK, MessageBoxIcon.Error);

                _ResetPersonInfo();
                return;
            }

            _FillPersonInfo();
        }

        private void _ResetPersonInfo()
        {

            lblPersonId.Text = "[???]";
            lblDateofBirth.Text = "[???]";
            lblName.Text = "[???]";

            lblNationalNo.Text = "[???]";
            lblEmail.Text = "[???]";
            lblAddress.Text = "[???]";

            lblPhone.Text = "[???]";
            lblGendor.Text = "[???]";

            llEditPersoninfo.Enabled = false;

             pbPerson.Image = Resources.Male_512;

            pbGendor.Image = Resources.Man_32;
        }

        private void HandleImage()
        {

            pbGendor.Image = (_clsPerson.Gendor == 0) ? Resources.Man_32 : Resources.Woman_32;

            
            if (string.IsNullOrEmpty(_clsPerson.ImagePath))
                pbPerson.Image = (_clsPerson.Gendor == 0) ? Resources.Male_512 : Resources.Female_512;
            else
            {
                if (File.Exists(_clsPerson.ImagePath))
                    pbPerson.ImageLocation = _clsPerson.ImagePath;
                else
                    MessageBox.Show("Could not found this image: =" + _clsPerson.ImagePath);
            }

        }

        private void _FillPersonInfo()
        {

            _PersonID = _clsPerson.PersonID;

            llEditPersoninfo.Enabled = true;

            lblPersonId.Text = _clsPerson.PersonID.ToString();

            lblDateofBirth.Text = _clsPerson.DateofBirth.ToShortDateString();

            lblName.Text = _clsPerson.FullName;

            lblNationalNo.Text = _clsPerson.NationalNo;

            lblEmail.Text = _clsPerson.Email;

            lblAddress.Text = _clsPerson.Address;

            lblPhone.Text = _clsPerson.Phone;

            lblCountry.Text = _clsPerson.CountryName;

            lblGendor.Text = (_clsPerson.Gendor == 0) ? "Male" : "Female";

            HandleImage();
        }

        private void llEditPersoninfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddorEditPerson  frm = new frmAddorEditPerson(_PersonID);          
            frm.ShowDialog();

            LoadPersonInfoByID(PersonID);
        }

    }
}
