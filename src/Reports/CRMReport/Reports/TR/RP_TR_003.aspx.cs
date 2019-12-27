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
using CRMReport.Reports.TR;
using Newtonsoft.Json;

namespace CRMReport.Reports.TR
{
    public partial class RP_TR_0031 : System.Web.UI.Page
    {
        //Parameter
        TRParams TRP = new TRParams();
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
            TRP.dbConnection = connection.dbConnection;
            TRP.dbUsername = connection.dbUsername;
            TRP.dbPassword = connection.dbPassword;
            con.ConnectionString = TRP.dbConnection;

            //Parameter 
            TRP.token = Request.QueryString["token"];
            TRP.param = Request.QueryString["params"];
            TRP.downloadAs = Request.QueryString["DownloadAs"];

            if (TRP.downloadAs == null)
            {
                TRP.downloadAs = string.Empty;
            }

            try
            {
                TRP.reportName = "RP_TR_003";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(TRP.token, TRP.reportName);
                TRP.fullReportPath = "/Reports/TR/" + TRP.reportName + ".rpt";

                string reportPath = SM.reportPath(TRP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //TRP.CompanyID = Request.QueryString["CompanyID"] != null ? Request.QueryString["CompanyID"] : "";
                //TRP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
                //TRP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
                //TRP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : "";
                //TRP.BankCheque = Request.QueryString["BankCheque"] != null ? Request.QueryString["BankCheque"] : "";
                //TRP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //TRP.UnitNumber = Request.QueryString["UnitNumber"] != null ? Request.QueryString["UnitNumber"] : "";

                var paramDicts = SM.checkForParams(TRP.param);
                TRParams jsonParam = JsonConvert.DeserializeObject<TRParams>(paramDicts);

                if (jsonParam != null)
                {
                    TRP.CompanyID = jsonParam.CompanyID;
                    TRP.ProductID = jsonParam.ProductID;
                    TRP.DateStart = jsonParam.DateStart;
                    TRP.DateEnd = jsonParam.DateEnd;
                    TRP.BankCheque = jsonParam.BankCheque;
                    TRP.UserName = jsonParam.UserName;
                    TRP.UnitNumber = jsonParam.UnitNumber;

                    //Convert DateTime
                    if (!string.IsNullOrEmpty(TRP.DateStart))
                    {
                        TRP.actualDS = new DateTime(long.Parse(TRP.DateStart));
                    }
                    else
                    {
                        TRP.actualDS = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(TRP.DateEnd))
                    {
                        TRP.actualDE = new DateTime(long.Parse(TRP.DateEnd));
                    }
                    else
                    {
                        TRP.actualDE = new DateTime(7000, 12, 31, 00, 00, 00);
                    }
                }

                SqlCommand cmd = new SqlCommand("AP2SP_RP_TR_003", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@CompanyID", TRP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductID", TRP.ProductID);
                cmd.Parameters.AddWithValue("@DateStart", TRP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", TRP.actualDE);
                cmd.Parameters.AddWithValue("@BankCheque", TRP.BankCheque);
                cmd.Parameters.AddWithValue("@UserName", TRP.UserName);
                cmd.Parameters.AddWithValue("@UnitNumber", TRP.UnitNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);
                
                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@CompanyID", TRP.CompanyID);
                crystalReport.SetParameterValue("@ProductID", TRP.ProductID);
                crystalReport.SetParameterValue("@DateStart", TRP.actualDS);
                crystalReport.SetParameterValue("@DateEnd", TRP.actualDE);
                crystalReport.SetParameterValue("@BankCheque", TRP.BankCheque);
                crystalReport.SetParameterValue("@UserName", TRP.UserName);
                crystalReport.SetParameterValue("@UnitNumber", TRP.UnitNumber);
                //crystalReport.SetParameterValue("@DS", TRP.actualDS);
                //crystalReport.SetParameterValue("@DE", TRP.actualDE);

                CRV.ReportSource = crystalReport;
                CRV.Visible = true;

                CRS.ReportDocument.SetDatabaseLogon(TRP.dbUsername, TRP.dbPassword);

                if (!string.IsNullOrEmpty(TRP.reportName))
                {
                    if (TRP.downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        SM.exportAsPDF(crystalReport, Response, TRP.reportName);
                    }
                    else if (TRP.downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        SM.exportAsExcel(crystalReport, Response, TRP.reportName);
                    }
                    else if (TRP.downloadAs.ToLower() == DownloadAs.showpdf.ToString())
                    {
                        SM.exportAsStream(crystalReport, Response, TRP.reportName);
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
            SM.exportAsPDF(crystalReport, Response, TRP.reportName);
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsExcel(crystalReport, Response, TRP.reportName);
        }
    }
}