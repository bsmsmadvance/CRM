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
using CRMReport.Reports.LC;


namespace CRMReport.Reports.LC
{
    public partial class RPT_FollowByLC1 : System.Web.UI.Page
    {
        //Parameter
        LCParams LCP = new LCParams();
        DBConnection connection = new DBConnection();
        ReportDocument crystalReport = new ReportDocument();
        SharedMethod SM = new SharedMethod();

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter dtAdapter = new SqlDataAdapter();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Default Value
            LCP.dbConnection = connection.dbConnection;
            LCP.dbUsername = connection.dbUsername;
            LCP.dbPassword = connection.dbPassword;
            con.ConnectionString = LCP.dbConnection;

            //Parameter 
            LCP.reportName = Request.QueryString["ReportName"];
            LCP.downloadAs = Request.QueryString["DownloadAs"];
            LCP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
            LCP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
            LCP.UserID = Request.QueryString["UserID"] != null ? Request.QueryString["UserID"] : "";
            LCP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
            LCP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : "";

            //ARP.DateStart = "636921792000000000";
            //ARP.DateEnd = "636921792000000000";

            //Convert DateTime
            if (!string.IsNullOrEmpty(LCP.DateStart))
            {
                LCP.actualDS = new DateTime(long.Parse(LCP.DateStart));
            }
            else
            {
                LCP.actualDS = new DateTime(1800, 01, 01, 00, 00, 00);
            }
            if (!string.IsNullOrEmpty(LCP.DateEnd))
            {
                LCP.actualDE = new DateTime(long.Parse(LCP.DateEnd));
            }
            else
            {
                LCP.actualDE = new DateTime(7000, 12, 31, 00, 00, 00);
            }

            if (LCP.downloadAs == null)
            {
                LCP.downloadAs = string.Empty;
            }

            try
            {
                LCP.reportName = "RPT_FollowByLC";
                LCP.fullReportPath = "/Reports/LC/" + LCP.reportName + ".rpt";

                string reportPath = ConfigurationManager.AppSettings["ReportPath"];
                crystalReport.Load(Server.MapPath(LCP.fullReportPath));
                //crystalReport.Load("C:\\inetpub\\wwwroot\\crmreport\\Reports\\LC\\RPT_FollowByLC.rpt");

                SqlCommand cmd = new SqlCommand("AP2SP_FollowCustomer", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@ProductID", LCP.ProductID);
                cmd.Parameters.AddWithValue("@UserID", LCP.UserID);
                cmd.Parameters.AddWithValue("@DateStart", LCP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", LCP.actualDE);
                cmd.Parameters.AddWithValue("@Username", LCP.UserName);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);
                
                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@ProductID", LCP.ProductID);
                crystalReport.SetParameterValue("@UserID", LCP.UserID);
                crystalReport.SetParameterValue("@DateStart", LCP.actualDS);
                crystalReport.SetParameterValue("@DateEnd", LCP.actualDE);
                crystalReport.SetParameterValue("@UserName", LCP.UserName);
                //crystalReport.SetParameterValue("@DS", TRP.actualDS);
                //crystalReport.SetParameterValue("@DE", TRP.actualDE);

                CRV.ReportSource = crystalReport;
                CRV.Visible = true;

                CRS.ReportDocument.SetDatabaseLogon(LCP.dbUsername, LCP.dbPassword);

                if (!string.IsNullOrEmpty(LCP.reportName))
                {
                    if (LCP.downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        SM.exportAsPDF(crystalReport, Response, LCP.reportName);
                    }
                    else if (LCP.downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        SM.exportAsExcel(crystalReport, Response, LCP.reportName);
                    }
                    else if (LCP.downloadAs.ToLower() == DownloadAs.showpdf.ToString())
                    {
                        SM.exportAsStream(crystalReport, Response, LCP.reportName);
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
            SM.exportAsPDF(crystalReport, Response, LCP.reportName);
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsExcel(crystalReport, Response, LCP.reportName);
        }
    }
}