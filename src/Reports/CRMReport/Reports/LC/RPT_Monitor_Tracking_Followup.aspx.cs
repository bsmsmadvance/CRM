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
    public partial class RPT_Monitor_Tracking_Followup1 : System.Web.UI.Page
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
                LCP.reportName = "RPT_Monitor_Tracking_Followup";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(LCP.token, LCP.reportName);
                LCP.fullReportPath = "/Reports/LC/" + LCP.reportName + ".rpt";

                string reportPath = SM.reportPath(LCP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //LCP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
                //LCP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
                //LCP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : "";
                //LCP.DateStart2 = Request.QueryString["DateStart2"] != null ? Request.QueryString["DateStart2"] : "";
                //LCP.DateEnd2 = Request.QueryString["DateEnd2"] != null ? Request.QueryString["DateEnd2"] : "";
                //LCP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //LCP.LCByProduct = Request.QueryString["LCByProduct"] != null ? Request.QueryString["LCByProduct"] : "";
                //LCP.Lead = Request.QueryString["Lead"] != null ? Request.QueryString["Lead"] : "";
                //LCP.FirstWalk = Request.QueryString["FirstWalk"] != null ? Request.QueryString["FirstWalk"] : "";
                //LCP.Revisit = Request.QueryString["Revisit"] != null ? Request.QueryString["Revisit"] : "";
                //LCP.FollowUpStatus = Request.QueryString["FollowUpStatus"] != null ? Request.QueryString["FollowUpStatus"] : "";

                var paramDicts = SM.checkForParams(LCP.param);
                LCParams jsonParam = JsonConvert.DeserializeObject<LCParams>(paramDicts);

                if (jsonParam != null)
                {
                    LCP.ProductID = jsonParam.ProductID;
                    LCP.DateStart = jsonParam.DateStart;
                    LCP.DateEnd = jsonParam.DateEnd;
                    LCP.DateStart2 = jsonParam.DateStart2;
                    LCP.DateEnd2 = jsonParam.DateEnd2;
                    LCP.UserName = jsonParam.UserName;
                    LCP.LCByProduct = jsonParam.LCByProduct;
                    LCP.Lead = jsonParam.Lead;
                    LCP.FirstWalk = jsonParam.FirstWalk;
                    LCP.Revisit = jsonParam.Revisit;
                    LCP.FollowUpStatus = jsonParam.FollowUpStatus;

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
                }

                SqlCommand cmd = new SqlCommand("sp_RPT_MornitorTracking_Followup", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@ProductID", LCP.ProductID);
                cmd.Parameters.AddWithValue("@DateStart", LCP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", LCP.actualDE);
                cmd.Parameters.AddWithValue("@DateStart2", LCP.actualDS2);
                cmd.Parameters.AddWithValue("@DateEnd2", LCP.actualDE2);
                cmd.Parameters.AddWithValue("@Username", LCP.UserName);
                cmd.Parameters.AddWithValue("@LCByProduct", LCP.LCByProduct);
                cmd.Parameters.AddWithValue("@Lead", Convert.ToInt16(LCP.UserName));
                cmd.Parameters.AddWithValue("@FirstWalk", Convert.ToInt16(LCP.FirstWalk));
                cmd.Parameters.AddWithValue("@Revisit", Convert.ToInt16(LCP.Revisit));
                cmd.Parameters.AddWithValue("@FollowUpStatus", LCP.FollowUpStatus);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);
                
                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@ProductID", LCP.ProductID);
                crystalReport.SetParameterValue("@DateStart", LCP.actualDS);
                crystalReport.SetParameterValue("@DateEnd", LCP.actualDE);
                crystalReport.SetParameterValue("@DateStart2", LCP.actualDS2);
                crystalReport.SetParameterValue("@DateEnd2", LCP.actualDE2);
                crystalReport.SetParameterValue("@Username", LCP.UserName);
                crystalReport.SetParameterValue("@LCByProduct", LCP.LCByProduct);
                crystalReport.SetParameterValue("@Lead", Convert.ToInt16(LCP.UserName));
                crystalReport.SetParameterValue("@FirstWalk", Convert.ToInt16(LCP.FirstWalk));
                crystalReport.SetParameterValue("@Revisit", Convert.ToInt16(LCP.Revisit));
                crystalReport.SetParameterValue("@FollowUpStatus", LCP.FollowUpStatus);

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