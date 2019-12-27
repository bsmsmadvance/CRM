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
using CRMReport.Reports.FI;
using Newtonsoft.Json;

namespace CRMReport.Reports.FI
{
    public partial class rpt_Z_BudgetReport_Monitor1 : System.Web.UI.Page
    {
        //Parameter
        FIParams FIP = new FIParams();
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
            FIP.dbConnection = connection.dbConnection;
            FIP.dbUsername = connection.dbUsername;
            FIP.dbPassword = connection.dbPassword;
            con.ConnectionString = FIP.dbConnection;

            //Parameter 
            FIP.token = Request.QueryString["token"];
            FIP.param = Request.QueryString["params"];
            FIP.downloadAs = Request.QueryString["DownloadAs"];

            if (FIP.downloadAs == null)
            {
                FIP.downloadAs = string.Empty;
            }

            try
            {
                FIP.reportName = "rpt_Z_BudgetReport_Monitor";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(FIP.token, FIP.reportName);
                FIP.fullReportPath = "/Reports/FI/" + FIP.reportName + ".rpt";

                string reportPath = SM.reportPath(FIP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //FIP.HomeType = Request.QueryString["HomeType"] != null ? Request.QueryString["HomeType"] : "";
                //FIP.CompanyID = Request.QueryString["CompanyID"] != null ? Request.QueryString["CompanyID"] : null;
                //FIP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
                //FIP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
                //FIP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : "";
                //FIP.DateStart2 = Request.QueryString["DateStart2"] != null ? Request.QueryString["DateStart2"] : "";
                //FIP.DateEnd2 = Request.QueryString["DateEnd2"] != null ? Request.QueryString["DateEnd2"] : "";
                //FIP.Status3 = Request.QueryString["Status3"] != null ? Request.QueryString["Status3"] : "";
                //FIP.Status4 = Request.QueryString["Status4"] != null ? Request.QueryString["Status4"] : "";
                //FIP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //FIP.ProjectGroup = Request.QueryString["ProjectGroup"] != null ? Request.QueryString["ProjectGroup"] : "";
                //FIP.ProjectType2 = Request.QueryString["ProjectType2"] != null ? Request.QueryString["ProjectType2"] : "";

                var paramDicts = SM.checkForParams(FIP.param);
                FIParams jsonParam = JsonConvert.DeserializeObject<FIParams>(paramDicts);

                if (jsonParam != null)
                {
                    FIP.HomeType = jsonParam.HomeType;
                    FIP.CompanyID = jsonParam.CompanyID;
                    FIP.ProductID = jsonParam.ProductID;
                    FIP.DateStart = jsonParam.DateStart;
                    FIP.DateEnd = jsonParam.DateEnd;
                    FIP.DateStart2 = jsonParam.DateStart2;
                    FIP.DateEnd2 = jsonParam.DateEnd2;
                    FIP.Status3 = jsonParam.Status3;
                    FIP.Status4 = jsonParam.Status4;
                    FIP.UserName = jsonParam.UserName;
                    FIP.ProjectGroup = jsonParam.ProjectGroup;
                    FIP.ProjectType2 = jsonParam.ProjectType2;
                    
                    //Convert DateTime
                    if (!string.IsNullOrEmpty(FIP.DateStart))
                    {
                        FIP.actualDS = new DateTime(long.Parse(FIP.DateStart));
                    }
                    else
                    {
                        FIP.actualDS = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(FIP.DateEnd))
                    {
                        FIP.actualDE = new DateTime(long.Parse(FIP.DateEnd));
                    }
                    else
                    {
                        FIP.actualDE = new DateTime(7000, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(FIP.DateStart2))
                    {
                        FIP.actualDS2 = new DateTime(long.Parse(FIP.DateStart2));
                    }
                    else
                    {
                        FIP.actualDS2 = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(FIP.DateEnd2))
                    {
                        FIP.actualDE2 = new DateTime(long.Parse(FIP.DateEnd2));
                    }
                    else
                    {
                        FIP.actualDE2 = new DateTime(7000, 01, 01, 00, 00, 00);
                    }
                }

                SqlCommand cmd = new SqlCommand("sp_Z_BudgetReport_Monitor", con);
                cmd.Parameters.AddWithValue("@HomeType", FIP.HomeType);
                cmd.Parameters.AddWithValue("@CompanyID", FIP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductID", FIP.ProductID);
                cmd.Parameters.AddWithValue("@DateStart", FIP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", FIP.actualDE);
                cmd.Parameters.AddWithValue("@DateStart2", FIP.actualDS2);
                cmd.Parameters.AddWithValue("@DateEnd2", FIP.actualDE2);
                cmd.Parameters.AddWithValue("@Status3", FIP.Status3);
                cmd.Parameters.AddWithValue("@Status4", FIP.Status4);
                cmd.Parameters.AddWithValue("@UserName", FIP.UserName);
                cmd.Parameters.AddWithValue("@ProjectGroup", FIP.ProjectGroup);
                cmd.Parameters.AddWithValue("@ProjectType2", FIP.ProjectType2);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@HomeType", FIP.HomeType);
                crystalReport.SetParameterValue("@CompanyID", FIP.CompanyID);
                crystalReport.SetParameterValue("@ProductID", FIP.ProductID);
                crystalReport.SetParameterValue("@DateStart", FIP.actualDS);
                crystalReport.SetParameterValue("@DateEnd", FIP.actualDE);
                crystalReport.SetParameterValue("@DateStart2", FIP.actualDS2);
                crystalReport.SetParameterValue("@DateEnd2", FIP.actualDE2);
                crystalReport.SetParameterValue("@Status3", FIP.Status3);
                crystalReport.SetParameterValue("@Status4", FIP.Status4);
                crystalReport.SetParameterValue("@UserName", FIP.UserName);
                crystalReport.SetParameterValue("@ProjectGroup", FIP.ProjectGroup);
                crystalReport.SetParameterValue("@ProjectType2", FIP.ProjectType2);
                //crystalReport.SetParameterValue("@DS", FIP.actualDS);

                CRV.ReportSource = crystalReport;
                CRV.Visible = true;

                CRS.ReportDocument.SetDatabaseLogon(FIP.dbUsername, FIP.dbPassword);

                if (!string.IsNullOrEmpty(FIP.reportName))
                {
                    if (FIP.downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        SM.exportAsPDF(crystalReport, Response, FIP.reportName);
                    }
                    else if (FIP.downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        SM.exportAsExcel(crystalReport, Response, FIP.reportName);
                    }
                    else if (FIP.downloadAs.ToLower() == DownloadAs.showpdf.ToString())
                    {
                        SM.exportAsStream(crystalReport, Response, FIP.reportName);
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
            SM.exportAsPDF(crystalReport, Response, FIP.reportName);
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsExcel(crystalReport, Response, FIP.reportName);
        }
    }
}