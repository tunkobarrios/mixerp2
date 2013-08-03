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

namespace MixERP.Net.FrontEnd.Finance.Setup
{
    public partial class AgeingSlabs : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MixERP.Net.FrontEnd.UserControls.Forms.FormControl form = (MixERP.Net.FrontEnd.UserControls.Forms.FormControl)this.LoadControl("~/UserControls/Forms/FormControl.ascx");
            form.DenyAdd = false;
            form.DenyEdit = false;
            form.DenyDelete = false;
            form.Width = 1000;
            form.PageSize = 10;


            form.Text = Resources.Titles.AgeingSlabSetup;

            form.TableSchema = "core";
            form.Table = "ageing_slabs";
            form.ViewSchema = "core";
            form.View = "ageing_slabs";


            form.KeyColumn = "ageing_slab_id";

            form.DisplayFields = "";
            form.SelectedValues = "";

            FormPlaceHolder.Controls.Add(form);
        }
    }
}