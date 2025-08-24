using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.People
{
    public partial class frmPersonDetails : Form
    {

        private static int _PersonID;

        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPersonDetails_Load(object sender, EventArgs e)
        {
            ctrlPersonCard1.LoadPersonInfoByID(_PersonID);
        }

    }
}
