﻿/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MixERP.Net.BusinessLayer.Helpers;
using MixERP.Net.Common.Helpers;
using MixERP.Net.WebControls.ScrudFactory;

namespace MixERP.Net.FrontEnd.Items.Setup
{
    public partial class SellingPrices : MixERP.Net.BusinessLayer.BasePageClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScrudForm scrud = new ScrudForm();

            scrud.KeyColumn = "item_selling_price_id";
            scrud.TableSchema = "core";
            scrud.Table = "item_selling_prices";
            scrud.ViewSchema = "core";
            scrud.View = "item_selling_price_view";

            scrud.DisplayFields = this.GetDisplayFields();
            scrud.DisplayViews = this.GetDisplayViews();

            scrud.Text = Resources.Titles.ItemSellingPrices;

            ToolkitScriptManager1.NamingContainer.Controls.Add(scrud);
        }

        private string GetDisplayFields()
        {
            List<string> displayFields = new List<string>();
            ScrudHelper.AddDisplayField(displayFields, "core.items.item_id", ConfigurationHelper.GetDbParameter("ItemDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.party_types.party_type_id", ConfigurationHelper.GetDbParameter("PartyTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.price_types.price_type_id", ConfigurationHelper.GetDbParameter("PriceTypeDisplayField"));
            ScrudHelper.AddDisplayField(displayFields, "core.units.unit_id", ConfigurationHelper.GetDbParameter("UnitDisplayField"));
            return string.Join(",", displayFields);
        }

        private string GetDisplayViews()
        {
            List<string> displayViews = new List<string>();
            ScrudHelper.AddDisplayView(displayViews, "core.items.item_id", "core.item_view");
            ScrudHelper.AddDisplayView(displayViews, "core.party_types.party_type_id", "core.party_types");
            ScrudHelper.AddDisplayView(displayViews, "core.price_types.price_type_id", "core.price_types");
            ScrudHelper.AddDisplayView(displayViews, "core.units.unit_id", "core.unit_view");
            return string.Join(",", displayViews);
        }

    }
}