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

namespace DVLD.User.Control
{
    public partial class ctrlUserCard : UserControl
    {
        private clsUser _User;
        public clsUser UserInfo { get { return _User; } }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {

             _User = clsUser.FindByID(UserID);

            if (_User == null)
            {
                MessageBox.Show("No User With this Id =" + UserID, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblUserID.Text = _User.UserID.ToString();

            lblUserName.Text = _User.UserName;

            lbIlsActive.Text = (_User.IsActive) ? "Yes" : "No";


            ctrlPersonCard1.LoadPersonInfoByID(_User.PersonId);
        }

    }
}
