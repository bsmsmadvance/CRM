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
    public partial class RP_PrintLabel1 : System.Web.UI.Page
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
                AGP.reportName = "RP_PrintLabel";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(AGP.token, AGP.reportName);
                AGP.fullReportPath = "/Reports/AG/" + AGP.reportName + ".rpt";

                string reportPath = SM.reportPath(AGP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //AGP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
                //AGP.UnitNumber = Request.QueryString["UnitNumber"] != null ? Request.QueryString["UnitNumber"] : "";
                //AGP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //AGP.PrintLabel = Request.QueryString["PrintLabel"] != null ? Request.QueryString["PrintLabel"] : "";

                var paramDicts = SM.checkForParams(AGP.param);
                AGParams jsonParam = JsonConvert.DeserializeObject<AGParams>(paramDicts);

                if (jsonParam != null)
                {
                    AGP.ProductID = jsonParam.ProductID;
                    AGP.UnitNumber = jsonParam.UnitNumber;
                    AGP.UserName = jsonParam.UserName;
                    AGP.PrintLabel = jsonParam.PrintLabel;
                }

                SqlCommand cmd = new SqlCommand("AP2SP_PrintLabel", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@ProductID", AGP.ProductID);
                cmd.Parameters.AddWithValue("@UnitNumber", AGP.UnitNumber);
                cmd.Parameters.AddWithValue("@UserName", AGP.UserName);
                cmd.Parameters.AddWithValue("@PrintLabel", AGP.PrintLabel);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);
                
                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@ProductID", AGP.ProductID);
                crystalReport.SetParameterValue("@UnitNumber", AGP.UnitNumber);
                crystalReport.SetParameterValue("@UserName", AGP.UserName);
                crystalReport.SetParameterValue("@PrintLabel", AGP.PrintLabel);

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