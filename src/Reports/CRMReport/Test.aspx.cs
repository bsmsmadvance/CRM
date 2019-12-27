using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMReport.Extensions;

namespace CRMReport
{
    public partial class Test : System.Web.UI.Page 
    {
        public string dbConnection { get; set; }
        public string dbUsername { get; set; }
        public string dbPassword { get; set; }
        public string reportName { get; set; }
        public string groupName { get; set; }
        public string fullReportPath { get; set; }
        public DateTime todayDate { get; set; }
        public string downloadAs { get; set; }
        public DataTable dt { get; set; }

        DBConnection connection = new DBConnection();
        SqlConnection con = new SqlConnection();
        ReportDocument crystalReport = new ReportDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Default Value
            dbConnection = connection.dbConnection;
            dbUsername = connection.dbUsername;
            dbPassword = connection.dbPassword;
            con.ConnectionString = dbConnection;

            //Parameter
            reportName = Request.QueryString["ReportName"];
            groupName = Request.QueryString["Group"];
            downloadAs = Request.QueryString["DownloadAs"];

            if(downloadAs == null)
            {
                downloadAs = string.Empty;
            }
            
            try
            {
                fullReportPath = "/Reports/" + reportName + ".rpt";

                SqlCommand cmd = new SqlCommand("RPT." + reportName, con);
                cmd.Parameters.AddWithValue("@alias", "KBank");
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);

                //For Exportation
                crystalReport.Load(Server.MapPath(fullReportPath));
                crystalReport.SetDataSource(dt);
                CrystalReportViewer1.ReportSource = crystalReport;
                crystalReport.SetDatabaseLogon(dbUsername, dbPassword);

                if (!string.IsNullOrEmpty(reportName) || !string.IsNullOrEmpty(groupName))
                {
                    if (downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        exportAsPDF();
                    }
                    else if (downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        exportAsExcel();
                    }
                    else
                    {
                        standardView();
                    }

                }
            }
            catch (Exception custom)
            {
                throw custom;
            }
        }

        public void exportAsPDF()
        {
            crystalReport.ExportToHttpResponse
            (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, reportName);
        }

        public void exportAsExcel()
        {
            crystalReport.ExportToHttpResponse
            (CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Response, true, reportName);
        }

        public void standardView()
        {
            //this.CrystalReportViewer1.ReportSource = crystalReport;
            this.CrystalReportViewer1.DocumentView = CrystalDecisions.Shared.DocumentViewType.WebLayout;
        }

        protected void PDF_Click(object sender, ImageClickEventArgs e)
        {
            exportAsPDF();
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            exportAsExcel();
        }

    }
}