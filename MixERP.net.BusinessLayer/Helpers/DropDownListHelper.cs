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
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace MixERP.Net.BusinessLayer.Helpers
{
    public static class DropDownListHelper
    {
        public static void BindDropDownList(ListControl list, string schemaName, string tableName, string valueField, string displayField)
        {
            if(list == null)
            {
                return;
            }

            using(DataTable table = DatabaseLayer.Helpers.FormHelper.GetTable(schemaName, tableName))
            {
                table.Columns.Add("text_field", typeof(string), displayField);

                list.DataSource = table;
                list.DataValueField = valueField;
                list.DataTextField = "text_field";
                list.DataBind();
            }
        }

        public static void BindDropDownList(ListControl list, DataTable table, string valueField, string displayField)
        {
            if(list == null)
            {
                return;
            }

            if(table == null)
            {
                return;
            }

            table.Columns.Add("text_field", typeof(string), displayField);

            list.DataSource = table;
            list.DataValueField = valueField;
            list.DataTextField = "text_field";
            list.DataBind();
        }

        /// <summary>
        /// Selects the item in the list control that contains the specified value, if it exists.
        /// </summary>
        /// <param name="dropDownList"></param>
        /// <param name="selectedValue">The value of the item in the list control to select</param>
        /// <returns>Returns true if the value exists in the list control, false otherwise</returns>
        public static bool SetSelectedValue(ListControl dropDownList, String selectedValue)
        {
            if(dropDownList != null)
            {
                dropDownList.ClearSelection();

                ListItem selectedListItem = dropDownList.Items.FindByValue(selectedValue);

                if(selectedListItem != null)
                {
                    selectedListItem.Selected = true;
                    return true;
                }
            }

            return false;
        }
    }
}
