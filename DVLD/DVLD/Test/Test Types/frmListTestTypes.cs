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

namespace DVLD
{
    public partial class frmListTestTypes : Form
    {
        private DataTable _dtListTestTypes;

        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtListTestTypes = clsTestType.GetAllTestTypes();

            dgvTestTypes.DataSource = _dtListTestTypes;


            if (dgvTestTypes.RowCount > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 120;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 180;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 450;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 100;
            }

            lblRecordsNumber.Text = dgvTestTypes.RowCount.ToString();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmUpdateTestTypes frm = new frmUpdateTestTypes((int)dgvTestTypes.SelectedCells[0].Value);

            frm.ShowDialog();

            frmListTestTypes_Load(null, null);
        }

       
    }
}
