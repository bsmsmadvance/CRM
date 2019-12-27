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
    public partial class RP_LC_0121 : System.Web.UI.Page
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
                LCP.reportName = "RP_LC_012";

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
                //LCP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //LCP.Status = Request.QueryString["Status"] != null ? Request.QueryString["Status"] : "";
                //LCP.Reason = Request.QueryString["Reason"] != null ? Request.QueryString["Reason"] : "";
                //LCP.UnitNumber = Request.QueryString["UnitNumber"] != null ? Request.QueryString["UnitNumber"] : "";
                //LCP.HomeType = Request.QueryString["HomeType"] != null ? Request.QueryString["HomeType"] : "";
                //LCP.ProjectGroup = Request.QueryString["ProjectGroup"] != null ? Request.QueryString["ProjectGroup"] : "";
                //LCP.ProjectType2 = Request.QueryString["ProjectType2"] != null ? Request.QueryString["ProejctType2"] : "";

                var paramDicts = SM.checkForParams(LCP.param);
                LCParams jsonParam = JsonConvert.DeserializeObject<LCParams>(paramDicts);

                if (jsonParam != null)
                {
                    LCP.CompanyID = jsonParam.CompanyID;
                    LCP.ProductID = jsonParam.ProductID;
                    LCP.DateStart = jsonParam.DateStart;
                    LCP.DateEnd = jsonParam.DateEnd;
                    LCP.UserName = jsonParam.UserName;
                    LCP.Status = jsonParam.Status;
                    LCP.Reason = jsonParam.Reason;
                    LCP.UnitNumber = jsonParam.UnitNumber;
                    LCP.HomeType = jsonParam.HomeType;
                    LCP.ProjectGroup = jsonParam.ProjectGroup;
                    LCP.ProjectType2 = jsonParam.ProjectType2;

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
                }

                SqlCommand cmd = new SqlCommand("AP2SP_RP_LC_012", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@CompanyID", LCP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductID", LCP.ProductID);
                cmd.Parameters.AddWithValue("@DateStart", LCP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", LCP.actualDE);
                cmd.Parameters.AddWithValue("@Username", LCP.UserName);
                cmd.Parameters.AddWithValue("@Status", LCP.Status);
                cmd.Parameters.AddWithValue("@Reason", LCP.Reason);
                cmd.Parameters.AddWithValue("@UnitNumber", LCP.UnitNumber);
                cmd.Parameters.AddWithValue("@HomeType", LCP.HomeType);
                cmd.Parameters.AddWithValue("@ProjectGroup", LCP.ProjectGroup);
                cmd.Parameters.AddWithValue("@ProjectType2", LCP.ProjectType2);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                LCP.ContractNumber = dt.Rows[0]["ContractNumber"].ToString();

                //Report3.rpt
                cmd = new SqlCommand("AP2SP_RP_LC_010_1", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@ContractNumber", LCP.ContractNumber);
                cmd.Parameters.AddWithValue("@UserName", LCP.UserName);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                //SubLC_010_2.rpt
                cmd = new SqlCommand("AP2SP_RP_LC_010_2", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@ContractNumber", LCP.ContractNumber);
                cmd.Parameters.AddWithValue("@UserName", LCP.UserName);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                dtAdapter.Fill(dt2);
                ds.Tables.Add(dt2);

                //Sub_LC10_3
                cmd = new SqlCommand("AP2SP_RP_LC_010_3", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@ContractNumber", LCP.ContractNumber);
                cmd.Parameters.AddWithValue("@UserName", LCP.UserName);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt3 = new DataTable();
                dtAdapter.Fill(dt3);
                ds.Tables.Add(dt3);

                foreach (ReportDocument rd in crystalReport.Subreports)
                {
                    if (rd.Name == "Report3.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[1]);
                    }
                    if (rd.Name == "SubLC_010_2.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[2]);
                    }
                    if (rd.Name == "Sub_LC10_3")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[3]);
                    }

                    crystalReport.Subreports[rd.Name].SetDatabaseLogon(LCP.dbUsername, LCP.dbPassword);
                }

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@CompanyID", LCP.CompanyID);
                crystalReport.SetParameterValue("@ProductID", LCP.ProductID);
                crystalReport.SetParameterValue("@DateStart", LCP.actualDS);
                crystalReport.SetParameterValue("@DateEnd", LCP.actualDE);
                crystalReport.SetParameterValue("@Username", LCP.UserName);
                crystalReport.SetParameterValue("@Status", LCP.Status);
                crystalReport.SetParameterValue("@Reason", LCP.Reason);
                crystalReport.SetParameterValue("@UnitNumber", LCP.UnitNumber);
                crystalReport.SetParameterValue("@HomeType", LCP.HomeType);
                crystalReport.SetParameterValue("@ProjectGroup", LCP.ProjectGroup);
                crystalReport.SetParameterValue("@ProjectType2", LCP.ProjectType2);
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