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
using Newtonsoft.Json;

namespace CRMReport.Reports.LC
{
    public partial class RP_LC_0021 : System.Web.UI.Page
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
            LCP.token = Request.QueryString["token"];
            LCP.param = Request.QueryString["params"];
            LCP.downloadAs = Request.QueryString["DownloadAs"];

            if (LCP.downloadAs == null)
            {
                LCP.downloadAs = string.Empty;
            }

            try
            {
                LCP.reportName = "RP_LC_002";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(LCP.token, LCP.reportName);
                LCP.fullReportPath = "/Reports/LC/" + LCP.reportName + ".rpt";

                string reportPath = SM.reportPath(LCP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //LCP.CompanyID = Request.QueryString["CompanyID"] != null ? Request.QueryString["CompanyID"] : "";
                //LCP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
                //LCP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
                //LCP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : "";
                //LCP.DateStart2 = Request.QueryString["DateStart2"] != null ? Request.QueryString["DateStart2"] : "";
                //LCP.DateEnd2 = Request.QueryString["DateEnd2"] != null ? Request.QueryString["DateEnd2"] : "";
                //LCP.DateStart3 = Request.QueryString["DateStart3"] != null ? Request.QueryString["DateStart3"] : "";
                //LCP.DateEnd3 = Request.QueryString["DateEnd3"] != null ? Request.QueryString["DateEnd3"] : "";
                //LCP.StatusAG = Request.QueryString["StatusAG"] != null ? Request.QueryString["StatusAG"] : "";
                //LCP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //LCP.StatusAG2 = Request.QueryString["StatusAG2"] != null ? Request.QueryString["StatusAG2"] : "";
                //LCP.AccountType = Request.QueryString["AccountType"] != null ? Request.QueryString["AccountType"] : "";

                var paramDicts = SM.checkForParams(LCP.param);
                LCParams jsonParam = JsonConvert.DeserializeObject<LCParams>(paramDicts);

                if (jsonParam != null)
                {
                    LCP.CompanyID = jsonParam.CompanyID;
                    LCP.ProductID = jsonParam.ProductID;
                    LCP.DateStart = jsonParam.DateStart;
                    LCP.DateEnd = jsonParam.DateEnd;
                    LCP.DateStart2 = jsonParam.DateStart2;
                    LCP.DateEnd2 = jsonParam.DateEnd2;
                    LCP.DateStart3 = jsonParam.DateStart3;
                    LCP.DateEnd3 = jsonParam.DateEnd3;
                    LCP.StatusAG = jsonParam.StatusAG;
                    LCP.UserName = jsonParam.UserName;
                    LCP.StatusAG2 = jsonParam.StatusAG2;
                    LCP.AccountType = jsonParam.AccountType;
                    
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
                    if (!string.IsNullOrEmpty(LCP.DateStart2))
                    {
                        LCP.actualDS2 = new DateTime(long.Parse(LCP.DateStart2));
                    }
                    else
                    {
                        LCP.actualDS2 = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(LCP.DateEnd2))
                    {
                        LCP.actualDE2 = new DateTime(long.Parse(LCP.DateEnd2));
                    }
                    else
                    {
                        LCP.actualDE2 = new DateTime(7000, 12, 31, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(LCP.DateStart3))
                    {
                        LCP.actualDS3 = new DateTime(long.Parse(LCP.DateStart3));
                    }
                    else
                    {
                        LCP.actualDS3 = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(LCP.DateEnd3))
                    {
                        LCP.actualDE3 = new DateTime(long.Parse(LCP.DateEnd3));
                    }
                    else
                    {
                        LCP.actualDE3 = new DateTime(7000, 12, 31, 00, 00, 00);
                    }

                }

                SqlCommand cmd = new SqlCommand("AP2SP_RP_LC_002", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@CompanyID", LCP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductID", LCP.ProductID);
                cmd.Parameters.AddWithValue("@DateStart", LCP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", LCP.actualDE);
                cmd.Parameters.AddWithValue("@DateStart2", LCP.actualDS2);
                cmd.Parameters.AddWithValue("@DateEnd2", LCP.actualDE2);
                cmd.Parameters.AddWithValue("@DateStart3", LCP.actualDS3);
                cmd.Parameters.AddWithValue("@DateEnd3", LCP.actualDE3);
                cmd.Parameters.AddWithValue("@Username", LCP.UserName);
                cmd.Parameters.AddWithValue("@StatusAG2", LCP.StatusAG2);
                cmd.Parameters.AddWithValue("@AccountType", LCP.AccountType);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@CompanyID", LCP.CompanyID);
                crystalReport.SetParameterValue("@ProductID", LCP.ProductID);
                crystalReport.SetParameterValue("@DateStart", LCP.actualDS);
                crystalReport.SetParameterValue("@DateEnd", LCP.actualDE);
                crystalReport.SetParameterValue("@DateStart2", LCP.actualDS2);
                crystalReport.SetParameterValue("@DateEnd2", LCP.actualDE2);
                crystalReport.SetParameterValue("@DateStart3", LCP.actualDS3);
                crystalReport.SetParameterValue("@DateEnd3", LCP.actualDE3);
                crystalReport.SetParameterValue("@UserName", LCP.UserName);
                crystalReport.SetParameterValue("@StatusAG2", LCP.StatusAG2);
                crystalReport.SetParameterValue("@AccountType", LCP.AccountType);

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