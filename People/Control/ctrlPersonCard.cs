using Businesses_Access_Layer;
using DVLD2.People;
using DVLD2.Properties;
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
    public partial class ctrlPersonCard : UserControl
    {
       
        private static clsPerson _clsPerson;

        private static int _PersonID = -1;

        public int PersonID { get { return _PersonID; } }

        public clsPerson PersonInfo { get { return _clsPerson; } }


        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoadPersonInfoByID(int personID)
        {

            _clsPerson = clsPerson.FindById(personID);

            if (_clsPerson != null)
                _FillPersonInfo();
            else
            {
                MessageBox.Show("No Person with this ID = " + personID, "Not Found"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ResetPersonInfo();
            }

        }

        public void LoadPersonInfoByNationalNo(string NationalNo)
        {

            _clsPerson = clsPerson.FindByNationalNo(NationalNo);

            if (_clsPerson != null)
                _FillPersonInfo();
            else
            {
                MessageBox.Show("No Person with this NationalNumber = " + NationalNo, "Not Found"
                  , MessageBoxButtons.OK, MessageBoxIcon.Error);

                _ResetPersonInfo();
            }

        }

        private void _ResetPersonInfo()
        {
            _PersonID = 0;
            lPersonId.Text = "[???]";
            lDateofBirth.Text = "[???]";
            lName.Text = "[???]";

            lNationalNo.Text = "[???]";
            lEmail.Text = "[???]";
            lAddress.Text = "[???]";

            lPhone.Text = "[???]";
            lGendor.Text = "[???]";

            linklEditPersoninfo.Enabled = false;
         
            pbPerson.Image = Resources.Male_512;

             pbGendor.Image = Resources.Man_32;
        }

        private void HandleImage()
        {
            
            pbGendor.Image = (_clsPerson.Gendor == 0) ? Resources.Man_32 : Resources.Woman_32;

            if (string.IsNullOrEmpty(_clsPerson.ImagePath))
                pbPerson.Image = (_clsPerson.Gendor == 0) ? Resources.Male_512 : Resources.Female_512;
            else
                pbPerson.Image = Image.FromFile(_clsPerson.ImagePath);
        }

        private void _FillPersonInfo()
        {

            _PersonID = _clsPerson.PersonID;

            linklEditPersoninfo.Enabled = true;

            lPersonId.Text = _clsPerson.PersonID.ToString();

            lDateofBirth.Text = _clsPerson.DateofBirth.ToShortDateString();

            lName.Text = _clsPerson.FullName;

            lNationalNo.Text = _clsPerson.NationalNo;

            lEmail.Text = _clsPerson.Email;

            lAddress.Text = _clsPerson.Address;

            lPhone.Text = _clsPerson.Phone;

            lCountry.Text = _clsPerson.CountryName;

            lGendor.Text = (_clsPerson.Gendor == 0) ? "Male" : "Female";

            HandleImage();
        }

        private void Frm_ImagePath(Image image)
        {
             pbPerson.Image = image;
        }

        private void linklEditPersoninfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddorEditPerson frm = new frmAddorEditPerson(_PersonID);

            frm.BackImagePath += Frm_ImagePath;

            frm.ShowDialog();

            LoadPersonInfoByID(PersonID);
        }


    }

}
