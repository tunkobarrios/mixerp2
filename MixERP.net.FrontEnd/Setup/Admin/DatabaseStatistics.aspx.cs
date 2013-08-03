﻿/********************************************************************************
    Copyright (C) Binod Nepal, Planet Earth Solutions Pvt. Ltd., Kathmandu.
	Released under the terms of the GNU General Public License, GPL, 
	as published by the Free Software Foundation, either version 3 
	of the License, or (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
    See the License here <http://www.gnu.org/licenses/gpl-3.0.html>.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MixERP.Net.FrontEnd.Setup.Admin
{
    public partial class DatabaseStatistics : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void VacuumButton_Click(object sender, EventArgs e)
        {
            MixERP.Net.BusinessLayer.Helpers.Maintenance.Vacuum();
            this.DisplaySuccess();
        }

        protected void FullVacuumButton_Click(object sender, EventArgs e)
        {
            MixERP.Net.BusinessLayer.Helpers.Maintenance.VacuumFull();
            this.DisplaySuccess();
        }

        protected void AnalyzeButton_Click(object sender, EventArgs e)
        {
            MixERP.Net.BusinessLayer.Helpers.Maintenance.Analyze();
            this.DisplaySuccess();
        }

        private void DisplaySuccess()
        {
            MessageLiteral.Text = "<div class='success'>Task completed successfully.</div>";
        }
    }
}