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
using System.IO;
using CRMReport.Reports.AR;

namespace CRMReport.Reports.AR
{
    public partial class RP_AR_0081 : System.Web.UI.Page
    {
        //Parameter
        ARParams ARP = new ARParams();
        DBConnection connection = new DBConnection();
        ReportDocument crystalReport = new ReportDocument();
        SharedMethod SM = new SharedMethod();

        public string reportName { get; set; }
        public string dbConnection { get; set; }
        public string dbUsername { get; set; }
        public string dbPassword { get; set; }
        public string fullReportPath { get; set; }
        public string downloadAs { get; set; }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter dtAdapter = new SqlDataAdapter();

        protected void Page_Load(object sender, EventArgs e)
        {
            ARP.dbConnection = connection.dbConnection;
            ARP.dbUsername = connection.dbUsername;
            ARP.dbPassword = connection.dbPassword;
            con.ConnectionString = dbConnection;

            //Parameter
            ARP.reportName = Request.QueryString["ReportName"];
            ARP.downloadAs = Request.QueryString["DownloadAs"];
            ARP.CompanyID = Request.QueryString["CompanyID"] != null ? Request.QueryString["CompanyID"] : ""; 
            ARP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : ""; 
            ARP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : ""; 
            ARP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : ""; 
            ARP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : ""; 

            //Convert DateTime
            if (!string.IsNullOrEmpty(ARP.DateStart))
            {
                ARP.actualDS = new DateTime(long.Parse(ARP.DateStart));
            }
            else
            {
                ARP.actualDS = new DateTime(1800, 01, 01, 00, 00, 00);
            }
            if (!string.IsNullOrEmpty(ARP.DateEnd))
            {
                ARP.actualDE = new DateTime(long.Parse(ARP.DateEnd));
            }
            else
            {
                ARP.actualDE = new DateTime(7000, 01, 01, 00, 00, 00);
            }

            if (ARP.downloadAs == null)
            {
                ARP.downloadAs = string.Empty;
            }

            try
            {
                reportName = "RP_AR_008";
                fullReportPath = "/Reports/AR/" + reportName + ".rpt";

                string reportPath = ConfigurationManager.AppSettings["ReportPath"];
                crystalReport.Load(Server.MapPath(fullReportPath));
                //crystalReport.Load("C:\\inetpub\\wwwroot\\crmreport\\Reports\\AR\\RP_AR_004\\RP_AR_008.rpt");

                cmd = new SqlCommand("AP2SP_RP_AR_008", con);
                cmd.Parameters.AddWithValue("@CompanyID", ARP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductID", ARP.ProductID);
                cmd.Parameters.AddWithValue("@DateStart", ARP.DateStart);
                cmd.Parameters.AddWithValue("@DateEnd", ARP.DateEnd);
                cmd.Parameters.AddWithValue("@Username", ARP.UserName);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);
                
                crystalReport.SetDataSource(ds.Tables[0]);
                CRS.ReportDocument.SetParameterValue("@CompanyID", ARP.CompanyID);
                CRS.ReportDocument.SetParameterValue("@ProductID", ARP.ProductID);
                CRS.ReportDocument.SetParameterValue("@DateStart", ARP.actualDS);
                CRS.ReportDocument.SetParameterValue("@DateEnd", ARP.actualDE);
                CRS.ReportDocument.SetParameterValue("@Username", ARP.UserName);

                CRV.ReportSource = crystalReport;
                CRV.Visible = true;

                CRS.ReportDocument.SetDatabaseLogon(ARP.dbUsername, ARP.dbPassword);

                if (!string.IsNullOrEmpty(ARP.reportName))
                {
                    if (ARP.downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        SM.exportAsPDF(crystalReport, Response, ARP.reportName);
                    }
                    else if (ARP.downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        SM.exportAsExcel(crystalReport, Response, ARP.reportName);
                    }
                    else if (ARP.downloadAs.ToLower() == DownloadAs.showpdf.ToString())
                    {
                        SM.exportAsStream(crystalReport, Response, ARP.reportName);
                    }
                    else
                    {
                        SM.standardView(CRV);
                    }
                }
            }
            catch (Exception custom)
            {
                throw custom;
            }
        }

        protected void PDF_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsPDF(crystalReport, Response, ARP.reportName);
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsExcel(crystalReport, Response, ARP.reportName);
        }
    }
}