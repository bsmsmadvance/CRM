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
    public partial class RP_AR_007_2_ : System.Web.UI.Page
    {
        //Parameter
        ARParams ARP = new ARParams();
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
            ARP.dbConnection = connection.dbConnection;
            ARP.dbUsername = connection.dbUsername;
            ARP.dbPassword = connection.dbPassword;
            con.ConnectionString = ARP.dbConnection;

            //Parameter 
            ARP.reportName = Request.QueryString["ReportName"];
            ARP.downloadAs = Request.QueryString["DownloadAs"];
            ARP.CompanyID = Request.QueryString["CompanyID"] != null ? Request.QueryString["CompanyID"] : "";
            ARP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
            ARP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
            ARP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : "" ;
            ARP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";

            //ARP.DateStart = "636921792000000000";
            //ARP.DateEnd = "636921792000000000";

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
            if (!string.IsNullOrEmpty(ARP.DateStart2))
            {
                ARP.actualDS2 = new DateTime(long.Parse(ARP.DateStart2));
            }
            else
            {
                ARP.actualDS2 = new DateTime(1800, 01, 01, 00, 00, 00);
            }
            if (!string.IsNullOrEmpty(ARP.DateEnd2))
            {
                ARP.actualDE2 = new DateTime(long.Parse(ARP.DateEnd2));
            }
            else
            {
                ARP.actualDE2 = new DateTime(7000, 01, 01, 00, 00, 00);
            }

            if (ARP.downloadAs == null)
            {
                ARP.downloadAs = string.Empty;
            }

            try
            {
                ARP.reportName = "RP_AR_007(2)";
                ARP.fullReportPath = "\\Reports\\AR\\" + ARP.reportName + ".rpt";

                string reportPath = SM.reportPath(ARP.fullReportPath);
                crystalReport.Load(reportPath);

                SqlCommand cmd = new SqlCommand("AP2SP_RP_AR_007(2)", con);
                cmd.Parameters.AddWithValue("@CompanyID", ARP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductID", ARP.ProductID);
                cmd.Parameters.AddWithValue("@DateStart", ARP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", ARP.actualDE);
                cmd.Parameters.AddWithValue("@UserName", ARP.UserName);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@CompanyID", ARP.CompanyID);
                crystalReport.SetParameterValue("@ProductID", ARP.ProductID);
                //crystalReport.SetParameterValue("@DateStart", DateStart);
                //crystalReport.SetParameterValue("@DateEnd", DateEnd);
                crystalReport.SetParameterValue("@UserName", ARP.UserName);
                crystalReport.SetParameterValue("@DS", ARP.actualDS);
                crystalReport.SetParameterValue("@DE", ARP.actualDE);

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