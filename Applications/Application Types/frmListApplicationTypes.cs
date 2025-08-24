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

namespace DVLD2.Applications.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {

        private static DataTable _dtApplicationTypes = new DataTable();

        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtApplicationTypes = clsApplicationType.GetAllApplicationTypes();

            dgvApplicationTypes.DataSource = _dtApplicationTypes;


            if (dgvApplicationTypes.RowCount > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 70;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 450;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 130;
            }

            lbRecordsNumber.Text = dgvApplicationTypes.RowCount.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte ApplicationTypeID = Convert.ToByte(dgvApplicationTypes.SelectedCells[0].Value);

            frmEditApplicationType frm = new frmEditApplicationType((clsApplication.enApplicationType)ApplicationTypeID);

            frm.ShowDialog();

            frmListApplicationTypes_Load(null, null);
        }

    }
}
