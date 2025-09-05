﻿using Businesses_Access_Layer;
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
    public partial class frmScheduleTest : Form
    {
        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID, int TestAppointmentID = -1)
        {
            InitializeComponent();

            ctrlScheduleTest1.TestType = TestTypeID;
            ctrlScheduleTest1.LoadScheduleTestInfo(LocalDrivingLicenseApplicationID, TestAppointmentID);
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
