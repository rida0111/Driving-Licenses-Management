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

namespace DVLD2.User.Control
{
    public partial class ctrlUserCard : UserControl
    {
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
         
            clsUser User = clsUser.FindByID(UserID);

            if (User == null)
            {
                MessageBox.Show("No User With this Id =" + UserID, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lbUserId.Text = User.UserId.ToString();

            lbUserName.Text = User.UserName;

            lbIsActive.Text = (User.IsActive) ? "Yes" : "No";


            ctrlPersonCard1.LoadPersonInfoByID(User.PersonId);
        }

    }
}
