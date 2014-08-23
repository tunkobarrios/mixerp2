﻿<%-- 
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
--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemSelector.aspx.cs" Inherits="MixERP.Net.FrontEnd.General.ItemSelector" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scrud Item Selector</title>

    <script src="//code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="/Scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Scripts/bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />

    <style type="text/css">
        html, body, form {
            height: 100%;
            background-color: white !important;
        }

        form {
            padding: 12px;
        }

        .grid td, .grid th {
            white-space: nowrap;
        }

        .filter {
            width: 172px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <mixerp:ScrudItemSelector runat="server"
            TopPanelCssClass="vpad16"
            TopPanelTableCssClass="valignmiddle"
            FilterDropDownListCssClass="form-control input-sm pad-right"
            FilterTextBoxCssClass="form-control input-sm"
            ButtonCssClass="btn btn-default btn-sm"
            GridViewCssClass="table table-bordered table-hover"
            GridViewPagerCssClass="gridpager"
            GridViewRowCssClass=""
            GridViewAlternateRowCssClass="alt" />
    </form>
</body>
</html>
