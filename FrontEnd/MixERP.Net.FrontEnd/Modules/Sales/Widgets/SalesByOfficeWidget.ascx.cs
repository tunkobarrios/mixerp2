﻿/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.FrontEnd.Base;
using System;
using MixERP.Net.Common.Domains;

namespace MixERP.Net.Core.Modules.Sales.Widgets
{
    public partial class SalesByOfficeWidget : MixERPWidget
    {
        public override void OnControlLoad(object sender, EventArgs e)
        {
            this.TitleLiteral.Text = Resources.Titles.SalesByOfficeInThousands;

            this.SalesByOfficeGridView.Attributes.Add("style", "display:none;");
            this.SalesByOfficeGridView.DataSource = Data.Reports.SalesByMonth.GetSalesByOffice();
            this.SalesByOfficeGridView.DataBind();

            base.OnControlLoad(sender, e);
        }

        public override AccessLevel AccessLevel
        {
            get { return AccessLevel.PolicyBased; }
        }
    }
}