﻿/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using MixERP.Net.BusinessLayer.Helpers;

namespace MixERP.Net.FrontEnd.UserControls
{
    public partial class ReportControl : System.Web.UI.UserControl
    {
        public string ReportPath { get; set; }
        public bool AutoInitialize { get; set; }

        public Collection<Collection<KeyValuePair<string, string>>> Parameters { get; set; }
        private string path { get; set; }

        private bool IsValid()
        {
            if(string.IsNullOrWhiteSpace(this.ReportPath))
            {
                return false;
            }

            this.path = Server.MapPath(this.ReportPath);

            if(!System.IO.File.Exists(this.path))
            {
                return false;
            }

            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.AutoInitialize)
            {
                this.InitializeReport();
            }
        }

        public void InitializeReport()
        {
            if(!this.IsValid())
            {
                ReportTitleLiteral.Text = Resources.Titles.ReportNotFound;
                ReportTitleHidden.Value = ReportTitleLiteral.Text;
                TopSectionLiteral.Text = string.Format(MixERP.Net.BusinessLayer.Helpers.SessionHelper.Culture(), Resources.Warnings.InvalidLocation, this.path);
                return;
            }

            this.SetRunningTotalFields();
            this.SetDataSources();
            this.SetTitle();
            this.SetTopSection();
            this.SetBodySection();
            this.SetGridViews();
            this.SetBottomSection();
        }

        private System.Collections.ObjectModel.Collection<int> RunningTotalTextColumnIndices { get; set; }
        private System.Collections.ObjectModel.Collection<string> RunningTotalFieldIndices { get; set; }
        private void SetRunningTotalFields()
        {
            XmlNodeList dataSourceList = XmlHelper.GetNodes(path, "//DataSource");
            int runningTotalTextColumnIndex = 0;
            string runningTotalFieldIndices = string.Empty;

            this.RunningTotalFieldIndices = new System.Collections.ObjectModel.Collection<string>();
            this.RunningTotalTextColumnIndices = new System.Collections.ObjectModel.Collection<int>();

            foreach(XmlNode dataSource in dataSourceList)
            {
                runningTotalTextColumnIndex = 0;
                runningTotalFieldIndices = string.Empty;

                foreach(XmlNode node in dataSource.ChildNodes)
                {
                    if(node.Name.Equals("RunningTotalTextColumnIndex"))
                    {
                        runningTotalTextColumnIndex = Pes.Utility.Conversion.TryCastInteger(node.InnerText);
                    }

                    if(node.Name.Equals("RunningTotalFieldIndices"))
                    {
                        runningTotalFieldIndices = node.InnerText;
                    }
                }

                this.RunningTotalTextColumnIndices.Add(runningTotalTextColumnIndex);
                this.RunningTotalFieldIndices.Add(runningTotalFieldIndices);
            }

        }

        private System.Collections.ObjectModel.Collection<System.Data.DataTable> DataSources { get; set; }
        private void SetDataSources()
        {
            System.Xml.XmlNodeList dataSources = XmlHelper.GetNodes(path, "//DataSource");
            int index = 0;
            this.DataSources = new System.Collections.ObjectModel.Collection<System.Data.DataTable>();

            foreach(System.Xml.XmlNode datasource in dataSources)
            {
                foreach(System.Xml.XmlNode c in datasource.ChildNodes)
                {
                    if(c.Name.Equals("Query"))
                    {
                        index++;
                        string sql = c.InnerText;
                        Collection<KeyValuePair<string, string>> parameters = new Collection<KeyValuePair<string, string>>();

                        if(this.Parameters != null)
                        {
                            parameters = this.Parameters[index - 1];

                        }

                        using(System.Data.DataTable table = MixERP.Net.BusinessLayer.Helpers.ReportHelper.GetDataTable(sql, parameters))
                        {
                            this.DataSources.Add(table);
                        }
                    }

                    if(c.Name.Equals("RunningTotalTextColumnIndex"))
                    {
                        this.RunningTotalTextColumnIndices.Add(Pes.Utility.Conversion.TryCastInteger(c.InnerText));
                    }

                    if(c.Name.Equals("RunningTotalFieldIndices"))
                    {
                        this.RunningTotalFieldIndices.Add(c.InnerText);
                    }
                }
            }
        }

        private void SetTitle()
        {
            string title = XmlHelper.GetNodeText(path, "/PesReport/Title");
            ReportTitleLiteral.Text = MixERP.Net.BusinessLayer.Helpers.ReportHelper.Parse(title);
            ReportTitleHidden.Value = ReportTitleLiteral.Text;

            if(!string.IsNullOrWhiteSpace(ReportTitleLiteral.Text))
            {
                this.Page.Title = ReportTitleLiteral.Text;
            }
        }

        private void SetTopSection()
        {
            string topSection = XmlHelper.GetNodeText(path, "/PesReport/TopSection");
            topSection = MixERP.Net.BusinessLayer.Helpers.ReportHelper.Parse(topSection);
            topSection = ParseDataSource(topSection, this.DataSources);
            TopSectionLiteral.Text = topSection;
        }

        private void SetBodySection()
        {
            string bodySection = XmlHelper.GetNodeText(path, "/PesReport/Body/Content");
            bodySection = MixERP.Net.BusinessLayer.Helpers.ReportHelper.Parse(bodySection);
            bodySection = ParseDataSource(bodySection, this.DataSources);
            ContentLiteral.Text = bodySection;
        }

        private void SetGridViews()
        {
            XmlNodeList gridViewDataSource = XmlHelper.GetNodes(path, "//GridViewDataSource");
            string gridSection = string.Empty;

            foreach(XmlNode node in gridViewDataSource)
            {
                if(!string.IsNullOrWhiteSpace(node.InnerText))
                {
                    gridSection += node.InnerText.Trim() + ",";
                }
            }

            this.LoadGrid(string.Concat(gridSection));
        }

        private void LoadGrid(string indices)
        {
            foreach(string data in indices.Split(','))
            {
                string ds = data.Trim();

                if(!string.IsNullOrWhiteSpace(ds))
                {

                    if(!ds.Contains(' '))
                    {
                        int index = Pes.Utility.Conversion.TryCastInteger(ds);

                        GridView grid = new GridView();
                        grid.EnableTheming = false;

                        grid.ID = "GridView" + ds;
                        grid.CssClass = "report";

                        grid.Width = Unit.Percentage(100);
                        grid.GridLines = GridLines.Both;
                        grid.RowDataBound += GridView_RowDataBound;
                        grid.DataBound += GridView_DataBound;
                        BodyPlaceHolder.Controls.Add(grid);

                        grid.DataSource = this.DataSources[index];
                        grid.DataBind();
                    }
                }
            }

        }

        private void SetBottomSection()
        {
            string bottomSection = XmlHelper.GetNodeText(path, "/PesReport/BottomSection");
            bottomSection = MixERP.Net.BusinessLayer.Helpers.ReportHelper.Parse(bottomSection);
            bottomSection = ParseDataSource(bottomSection, this.DataSources);
            BottomSectionLiteral.Text = bottomSection;
        }

        void GridView_DataBound(object sender, EventArgs e)
        {
            GridView grid = (GridView)sender;

            int arg = Pes.Utility.Conversion.TryCastInteger(grid.ID.Replace("GridView", ""));

            if(string.IsNullOrWhiteSpace(this.RunningTotalFieldIndices[arg]))
            {
                return;
            }

            if(grid.FooterRow == null)
            {
                return;
            }

            grid.FooterRow.Visible = true;

            for(int i = 0; i < RunningTotalTextColumnIndices[arg]; i++)
            {
                grid.FooterRow.Cells[i].Visible = false;
            }

            grid.FooterRow.Cells[RunningTotalTextColumnIndices[arg]].ColumnSpan = RunningTotalTextColumnIndices[arg] + 1;
            grid.FooterRow.Cells[RunningTotalTextColumnIndices[arg]].Text = Resources.Titles.Total;
            grid.FooterRow.Cells[RunningTotalTextColumnIndices[arg]].Style.Add("text-align", "right");
            grid.FooterRow.Cells[RunningTotalTextColumnIndices[arg]].Font.Bold = true;

            foreach(string field in this.RunningTotalFieldIndices[arg].Split(','))
            {
                int index = Pes.Utility.Conversion.TryCastInteger(field.Trim());

                decimal total = 0;

                if(index > 0)
                {
                    foreach(GridViewRow row in grid.Rows)
                    {
                        if(row.RowType == DataControlRowType.DataRow)
                        {
                            total += Pes.Utility.Conversion.TryCastDecimal(row.Cells[index].Text);
                        }
                    }

                    grid.FooterRow.Cells[index].Text = total.ToString(MixERP.Net.BusinessLayer.Helpers.SessionHelper.Culture());
                    grid.FooterRow.Cells[index].Font.Bold = true;
                }
            }
        }

        void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.Header)
            {
                for(int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string cellText = e.Row.Cells[i].Text;

                    cellText = Pes.Utility.Helpers.LocalizationHelper.GetResourceString("FormResource", cellText, false);
                    e.Row.Cells[i].Text = cellText;
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Left;
                }
            }
        }

        public static string ParseDataSource(string expression, System.Collections.ObjectModel.Collection<System.Data.DataTable> table)
        {
            foreach(var match in Regex.Matches(expression, "{.*?}"))
            {
                string word = match.ToString();

                if(word.StartsWith("{DataSource", StringComparison.OrdinalIgnoreCase))
                {

                    int index = Pes.Utility.Conversion.TryCastInteger(word.Split('.').First().Replace("{DataSource[", "").Replace("]", ""));
                    string column = word.Split('.').Last().Replace("}", "");

                    if(table[index] != null)
                    {
                        if(table[index].Rows.Count > 0)
                        {
                            if(table[index].Columns.Contains(column))
                            {
                                expression = expression.Replace(word, table[index].Rows[0][column].ToString());
                            }
                        }
                    }
                }
            }

            return expression;
        }

        protected void ExcelImageButton_Click(object sender, ImageClickEventArgs e)
        {
            string html = ReportHidden.Value;
            if(!string.IsNullOrWhiteSpace(html))
            {
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment; filename=" + ReportTitleHidden.Value + ".xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.Write(html);
                Response.Flush();
                Response.Close();
            }
        }

        protected void WordImageButton_Click(object sender, ImageClickEventArgs e)
        {
            string html = ReportHidden.Value;
            if(!string.IsNullOrWhiteSpace(html))
            {
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment; filename=" + ReportTitleHidden.Value + ".doc");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-word";
                Response.Write(html);
                Response.Flush();
                Response.Close();
            }
        }


    }

}

