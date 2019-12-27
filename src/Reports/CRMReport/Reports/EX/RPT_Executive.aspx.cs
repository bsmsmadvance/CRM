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
    public partial class RPT_Executive1 : System.Web.UI.Page
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
                EXP.reportName = "RPT_Executive";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(EXP.token, EXP.reportName);
                EXP.fullReportPath = "/Reports/EX/" + EXP.reportName + ".rpt";

                string reportPath = SM.reportPath(EXP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //EXP.BatchID = Request.QueryString["BatchID"] != null ? Request.QueryString["BatchID"] : "";
                //EXP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                var paramDicts = SM.checkForParams(EXP.param);

                EXParams jsonParam = JsonConvert.DeserializeObject<EXParams>(paramDicts);

                if (jsonParam != null)
                {
                    EXP.BatchID = jsonParam.BatchID;
                }

                SqlCommand cmd = new SqlCommand("AP2SP_ExecutiveReportV2", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@BatchID", EXP.BatchID);
                cmd.Parameters.AddWithValue("@UserName", EXP.UserName);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                //AP2SP_ExecutiveReportV2_1
                cmd = new SqlCommand("AP2SP_ExecutiveReportV2_1", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@BatchID", EXP.BatchID);
                cmd.Parameters.AddWithValue("@UserName", EXP.UserName);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                //AP2SP_ExecutiveReportV2_2
                cmd = new SqlCommand("AP2SP_ExecutiveReportV2_2", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@BatchID", EXP.BatchID);
                cmd.Parameters.AddWithValue("@UserName", EXP.UserName);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                dtAdapter.Fill(dt2);
                ds.Tables.Add(dt2);

                //AP2SP_ExecutiveReportV2_3
                cmd = new SqlCommand("AP2SP_ExecutiveReportV2_3", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@BatchID", EXP.BatchID);
                cmd.Parameters.AddWithValue("@UserName", EXP.UserName);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt3 = new DataTable();
                dtAdapter.Fill(dt3);
                ds.Tables.Add(dt3);
                

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@BatchID", EXP.BatchID);
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