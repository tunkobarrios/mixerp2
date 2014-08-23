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
using MixERP.Net.Common;
using MixERP.Net.WebControls.ScrudFactory.Resources;

namespace MixERP.Net.WebControls.ScrudFactory
{
    public partial class ScrudForm
    {
        private void DisplaySuccess()
        {
            this.messageLabel.CssClass = this.GetSuccessCssClass();
            this.messageLabel.Text = ScrudResource.TaskCompletedSuccessfully;

            this.gridPanel.Attributes["style"] = "display:block;";
            this.formPanel.Attributes["style"] = "display:none;";
            PageUtility.ExecuteJavaScript("resetForm", "$('#form1').each(function(){this.reset();});", this.Page);
        }
    }
}