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
using CRMReport.Reports.EX;
using Newtonsoft.Json;

namespace CRMReport.Reports.EX
{
    public partial class RPT_Executive31 : System.Web.UI.Page
    {
        //Parameter
        EXParams EXP = new EXParams();
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
            EXP.dbConnection = connection.dbConnection;
            EXP.dbUsername = connection.dbUsername;
            EXP.dbPassword = connection.dbPassword;
            con.ConnectionString = EXP.dbConnection;

            //Parameter 
            EXP.token = Request.QueryString["token"];
            EXP.param = Request.QueryString["params"];
            EXP.downloadAs = Request.QueryString["DownloadAs"];

            if (EXP.downloadAs == null)
            {
                EXP.downloadAs = string.Empty;
            }

            try
            {
                EXP.reportName = "RPT_Executive3";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(EXP.token, EXP.reportName);
                EXP.fullReportPath = "/Reports/EX/" + EXP.reportName + ".rpt";

                string reportPath = SM.reportPath(EXP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //EXP.BatchID = Request.QueryString["BatchID"] != null ? Request.QueryString["BatchID"] : "";
                //EXP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
                //EXP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : "";
                //EXP.HomeType = Request.QueryString["HomeType"] != null ? Request.QueryString["HomeType"] : "";
                //EXP.StatusProject = Request.QueryString["StatusProject"] != null ? Request.QueryString["StatusProject"] : "";
                //EXP.ProjectGroup = Request.QueryString["ProjectGroup"] != null ? Request.QueryString["ProjectGroup"] : "";
                //EXP.ProjectType2 = Request.QueryString["ProjectType2"] != null ? Request.QueryString["ProjectType2"] : "";
                //EXP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";

                var paramDicts = SM.checkForParams(EXP.param);
                EXParams jsonParam = JsonConvert.DeserializeObject<EXParams>(paramDicts);

                if (jsonParam != null)
                {
                    EXP.BatchID = jsonParam.BatchID;
                    EXP.DateStart = jsonParam.DateStart;
                    EXP.DateEnd = jsonParam.DateEnd;
                    EXP.HomeType = jsonParam.HomeType;
                    EXP.StatusProject = jsonParam.StatusProject;
                    EXP.ProjectGroup = jsonParam.ProjectGroup;
                    EXP.ProjectType2 = jsonParam.ProjectType2;
                    EXP.UserName = jsonParam.UserName;

                    //Convert DateTime
                    if (!string.IsNullOrEmpty(EXP.DateStart))
                    {
                        EXP.actualDS = new DateTime(long.Parse(EXP.DateStart));
                    }
                    else
                    {
                        EXP.actualDS = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(EXP.DateEnd))
                    {
                        EXP.actualDE = new DateTime(long.Parse(EXP.DateEnd));
                    }
                    else
                    {
                        EXP.actualDE = new DateTime(7000, 12, 31, 00, 00, 00);
                    }
                }
                
                SqlCommand cmd = new SqlCommand("AP2SP_ExecutiveReportV4_ByBU", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@BatchID", EXP.BatchID);
                cmd.Parameters.AddWithValue("@DateStart", EXP.DateStart);
                cmd.Parameters.AddWithValue("@DateEnd", EXP.DateEnd);
                cmd.Parameters.AddWithValue("@HomeType", EXP.HomeType);
                cmd.Parameters.AddWithValue("@StatusProject", EXP.StatusProject);
                cmd.Parameters.AddWithValue("@ProjectGroup", EXP.ProjectGroup);
                cmd.Parameters.AddWithValue("@ProjectType2", EXP.ProjectType2);
                cmd.Parameters.AddWithValue("@UserName", EXP.UserName);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@BatchID", EXP.BatchID);
                crystalReport.SetParameterValue("@DateStart", EXP.DateStart);
                crystalReport.SetParameterValue("@DateEnd", EXP.DateEnd);
                crystalReport.SetParameterValue("@HomeType", EXP.HomeType);
                crystalReport.SetParameterValue("@StatusProject", EXP.StatusProject);
                crystalReport.SetParameterValue("@ProjectGroup", EXP.ProjectGroup);
                crystalReport.SetParameterValue("@ProjectType2", EXP.ProjectType2);
                crystalReport.SetParameterValue("@UserName", EXP.UserName);

                CRV.ReportSource = crystalReport;
                CRV.Visible = true;

                CRS.ReportDocument.SetDatabaseLogon(EXP.dbUsername, EXP.dbPassword);

                if (!string.IsNullOrEmpty(EXP.reportName))
                {
                    if (EXP.downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        SM.exportAsPDF(crystalReport, Response, EXP.reportName);
                    }
                    else if (EXP.downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        SM.exportAsExcel(crystalReport, Response, EXP.reportName);
                    }
                    else if (EXP.downloadAs.ToLower() == DownloadAs.showpdf.ToString())
                    {
                        SM.exportAsStream(crystalReport, Response, EXP.reportName);
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
            SM.exportAsPDF(crystalReport, Response, EXP.reportName);
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsExcel(crystalReport, Response, EXP.reportName);
        }
    }
}