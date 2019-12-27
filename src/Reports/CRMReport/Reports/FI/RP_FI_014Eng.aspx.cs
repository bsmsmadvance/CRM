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
    public partial class RP_FI_014Eng1 : System.Web.UI.Page
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
                FIP.reportName = "RP_FI_014Eng";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(FIP.token, FIP.reportName);
                FIP.fullReportPath = "/Reports/FI/" + FIP.reportName + ".rpt";

                string reportPath = SM.reportPath(FIP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //FIP.CompanyID = Request.QueryString["CompanyID"] != null ? Request.QueryString["CompanyID"] : null;
                //FIP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
                //FIP.UnitNumber = Request.QueryString["UnitNumber"] != null ? Request.QueryString["UnitNumber"] : "";
                //FIP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //FIP.PeriodStart = Request.QueryString["PeriodStart"] != null ? Request.QueryString["PeriodStart"] : "";
                //FIP.PeriodEnd = Request.QueryString["PeriodEnd"] != null ? Request.QueryString["PeriodEnd"] : "";
                //FIP.PaymentType = Request.QueryString["PaymentType"] != null ? Request.QueryString["PaymentType"] : "";
                //FIP.PaymentType2 = Request.QueryString["PaymentType2"] != null ? Request.QueryString["PaymentType2"] : "";
                //FIP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
                //FIP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : "";

                var paramDicts = SM.checkForParams(FIP.param);
                FIParams jsonParam = JsonConvert.DeserializeObject<FIParams>(paramDicts);

                if (jsonParam != null)
                {
                    FIP.CompanyID = jsonParam.CompanyID;
                    FIP.ProductID = jsonParam.ProductID;
                    FIP.UnitNumber = jsonParam.UnitNumber;
                    FIP.UserName = jsonParam.UserName;
                    FIP.PeriodStart = jsonParam.PeriodEnd;
                    FIP.PaymentType = jsonParam.PaymentType;
                    FIP.PaymentType2 = jsonParam.PaymentType2;
                    FIP.DateStart = jsonParam.DateStart;
                    FIP.DateEnd = jsonParam.DateEnd;
                    
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
                }

                SqlCommand cmd = new SqlCommand("AP2SP_RP_FI_014ENG", con);
                cmd.Parameters.AddWithValue("@CompanyID", FIP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductID", FIP.ProductID);
                cmd.Parameters.AddWithValue("@UnitNumber", FIP.UnitNumber);
                cmd.Parameters.AddWithValue("@UserName", FIP.UserName);
                cmd.Parameters.AddWithValue("@PeriodStart", FIP.PeriodStart);
                cmd.Parameters.AddWithValue("@PeriodEnd", FIP.PeriodEnd);
                cmd.Parameters.AddWithValue("@PaymentType", FIP.PaymentType);
                cmd.Parameters.AddWithValue("@PaymentType2", FIP.PaymentType2);
                cmd.Parameters.AddWithValue("@DateStart", FIP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", FIP.actualDE);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                //Report1.rpt
                cmd = new SqlCommand("AP2SP_RP_FI_014_1", con);
                cmd.Parameters.AddWithValue("@ProductID", FIP.ProductID);
                cmd.Parameters.AddWithValue("@UnitNumber", FIP.UnitNumber);
                cmd.Parameters.AddWithValue("@UserName", FIP.UserName);
                cmd.Parameters.AddWithValue("@PeriodStart", FIP.PeriodStart);
                cmd.Parameters.AddWithValue("@PeriodEnd", FIP.PeriodEnd);
                cmd.Parameters.AddWithValue("@PaymentType", FIP.PaymentType);
                cmd.Parameters.AddWithValue("@PaymentType2", FIP.PaymentType2);
                cmd.Parameters.AddWithValue("@DateStart", FIP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", FIP.actualDE);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                foreach (ReportDocument rd in crystalReport.Subreports)
                {
                    if (rd.Name == "Report1.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[1]);
                    }

                    crystalReport.Subreports[rd.Name].SetDatabaseLogon(FIP.dbUsername, FIP.dbPassword);
                }


                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@CompanyID", FIP.CompanyID);
                crystalReport.SetParameterValue("@ProductID", FIP.ProductID);
                crystalReport.SetParameterValue("@UnitNumber", FIP.UnitNumber);
                crystalReport.SetParameterValue("@UserName", FIP.UserName);
                crystalReport.SetParameterValue("@PeriodStart", FIP.PeriodStart);
                crystalReport.SetParameterValue("@PeriodEnd", FIP.PeriodEnd);
                crystalReport.SetParameterValue("@PaymentType", FIP.PaymentType);
                crystalReport.SetParameterValue("@PaymentType2", FIP.PaymentType2);
                crystalReport.SetParameterValue("@DateStart", FIP.actualDS);
                crystalReport.SetParameterValue("@DateEnd", FIP.actualDE);
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