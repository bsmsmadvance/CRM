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
using CRMReport.Reports.AG;
using Newtonsoft.Json;

namespace CRMReport.Reports.AG
{
    public partial class RP_AG_0011 : System.Web.UI.Page
    {
        //Parameter
        AGParams AGP = new AGParams();
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
            AGP.dbConnection = connection.dbConnection;
            AGP.dbUsername = connection.dbUsername;
            AGP.dbPassword = connection.dbPassword;
            con.ConnectionString = AGP.dbConnection;

            //Parameter 
            AGP.token = Request.QueryString["token"];
            AGP.param = Request.QueryString["params"];
            AGP.downloadAs = Request.QueryString["DownloadAs"];

            if (AGP.downloadAs == null)
            {
                AGP.downloadAs = string.Empty;
            }

            try
            {
                AGP.reportName = "RP_AG_001";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(AGP.token, AGP.reportName);
                AGP.fullReportPath = "/Reports/AG/" + AGP.reportName + ".rpt";

                string reportPath = SM.reportPath(AGP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //AGP.CompanyID = Request.QueryString["CompanyID"] != null ? Request.QueryString["CompanyID"] : "";
                //AGP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
                //AGP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
                //AGP.DateStart2 = Request.QueryString["DateStart2"] != null ? Request.QueryString["DateStart2"] : "";
                //AGP.StatusPeriod = Request.QueryString["StatusPeriod"] != null ? Request.QueryString["StatusPeriod"] : "";
                //AGP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //AGP.CustomerStatus = Request.QueryString["CustomerStatus"] != null ? Request.QueryString["CustomerStatus"] : "";

                var paramDicts = SM.checkForParams(AGP.param);
                AGParams jsonParam = JsonConvert.DeserializeObject<AGParams>(paramDicts);

                if (jsonParam != null)
                {
                    AGP.CompanyID = jsonParam.CompanyID;
                    AGP.ProductID = jsonParam.ProductID;
                    AGP.DateStart = jsonParam.DateStart;
                    AGP.DateStart2 = jsonParam.DateStart2;
                    AGP.StatusPeriod = jsonParam.StatusPeriod;
                    AGP.UserName = jsonParam.UserName;
                    AGP.CustomerStatus = jsonParam.CustomerStatus;


                    //Convert DateTime
                    if (!string.IsNullOrEmpty(AGP.DateStart))
                    {
                        AGP.actualDS = new DateTime(long.Parse(AGP.DateStart));
                    }
                    else
                    {
                        AGP.actualDS = new DateTime(1800, 01, 01, 00, 00, 00);
                    }

                    if (AGP.downloadAs == null)
                    {
                        AGP.downloadAs = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(AGP.DateStart2))
                    {
                        AGP.actualDS2 = new DateTime(long.Parse(AGP.DateStart2));
                    }
                    else
                    {
                        AGP.actualDS2 = new DateTime(1800, 01, 01, 00, 00, 00);
                    }

                }

                SqlCommand cmd = new SqlCommand("AP2SP_RP_AG_001", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@CompanyID", AGP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductID", AGP.ProductID);
                cmd.Parameters.AddWithValue("@DateStart", AGP.actualDS);
                cmd.Parameters.AddWithValue("@DateStart2", AGP.actualDS2);
                cmd.Parameters.AddWithValue("@StatusPeriod", AGP.StatusPeriod);
                cmd.Parameters.AddWithValue("@UserName", AGP.UserName);
                cmd.Parameters.AddWithValue("@CustomerStatus", AGP.CustomerStatus);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                //AG_001_1.rpt
                cmd = new SqlCommand("AP2SP_RP_AG_001_1", con);
                cmd.Parameters.AddWithValue("@CompanyID", AGP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductID", AGP.ProductID);
                cmd.Parameters.AddWithValue("@StatusPeriod", AGP.StatusPeriod);
                cmd.Parameters.AddWithValue("@DateStart", AGP.actualDS);
                cmd.Parameters.AddWithValue("@DateStart2", AGP.actualDS2);
                cmd.Parameters.AddWithValue("@UserName", AGP.UserName);
                cmd.Parameters.AddWithValue("@CustomerStatus", AGP.CustomerStatus);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                foreach (ReportDocument rd in crystalReport.Subreports)
                {
                    if (rd.Name == "AG_001_1.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[1]);
                    }

                    crystalReport.Subreports[rd.Name].SetDatabaseLogon(AGP.dbUsername, AGP.dbPassword);
                }


                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@CompanyID", AGP.CompanyID);
                crystalReport.SetParameterValue("@ProductID", AGP.ProductID);
                crystalReport.SetParameterValue("@DateStart", AGP.actualDS);
                crystalReport.SetParameterValue("@DateStart2", AGP.actualDS2);
                crystalReport.SetParameterValue("@StatusPeriod", AGP.StatusPeriod);
                crystalReport.SetParameterValue("@UserName", AGP.UserName);
                crystalReport.SetParameterValue("@CustomerStatus", AGP.CustomerStatus);

                CRV.ReportSource = crystalReport;
                CRV.Visible = true;

                CRS.ReportDocument.SetDatabaseLogon(AGP.dbUsername, AGP.dbPassword);

                if (!string.IsNullOrEmpty(AGP.reportName))
                {
                    if (AGP.downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        SM.exportAsPDF(crystalReport, Response, AGP.reportName);
                    }
                    else if (AGP.downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        SM.exportAsExcel(crystalReport, Response, AGP.reportName);
                    }
                    else if (AGP.downloadAs.ToLower() == DownloadAs.showpdf.ToString())
                    {
                        SM.exportAsStream(crystalReport, Response, AGP.reportName);
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
            SM.exportAsPDF(crystalReport, Response, AGP.reportName);
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsExcel(crystalReport, Response, AGP.reportName);
        }
    }
}