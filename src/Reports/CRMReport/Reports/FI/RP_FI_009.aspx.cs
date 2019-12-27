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
    public partial class RP_FI_0091 : System.Web.UI.Page
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
                FIP.reportName = "RP_FI_009";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(FIP.token, FIP.reportName);
                FIP.fullReportPath = "/Reports/FI/" + FIP.reportName + ".rpt";

                string reportPath = SM.reportPath(FIP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //FIP.CompanyID = Request.QueryString["CompanyID"] != null ? Request.QueryString["CompanyID"] : "";
                //FIP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
                //FIP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
                //FIP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : "";
                //FIP.DateStart2 = Request.QueryString["DateStart2"] != null ? Request.QueryString["DateStart2"] : "";
                //FIP.DateEnd2 = Request.QueryString["DateEnd2"] != null ? Request.QueryString["DateEnd2"] : "";
                //FIP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //FIP.BankAccount = Request.QueryString["BankAccount"] != null ? Request.QueryString["BankAccount"] : "";
                //FIP.DateStart3 = Request.QueryString["DateStart3"] != null ? Request.QueryString["DateStart3"] : "";
                //FIP.DateEnd3 = Request.QueryString["DateEnd3"] != null ? Request.QueryString["DateEnd3"] : "";
                //FIP.UnitNumber = Request.QueryString["UnitNumber"] != null ? Request.QueryString["UnitNumber"] : "";
                //FIP.Deposit3 = Request.QueryString["Deposit3"] != null ? Request.QueryString["Deposit3"] : "";
                //FIP.Method = Request.QueryString["Method"] != null ? Request.QueryString["Method"] : "";
                //FIP.PaymentType = Request.QueryString["PaymentType"] != null ? Request.QueryString["PaymentType"] : "";

                var paramDicts = SM.checkForParams(FIP.param);
                FIParams jsonParam = JsonConvert.DeserializeObject<FIParams>(paramDicts);

                if (jsonParam != null)
                {
                    FIP.CompanyID = jsonParam.CompanyID;
                    FIP.ProductID = jsonParam.ProductID;
                    FIP.DateStart = jsonParam.DateStart;
                    FIP.DateEnd = jsonParam.DateEnd;
                    FIP.DateStart2 = jsonParam.DateStart2;
                    FIP.DateEnd2 = jsonParam.DateEnd2;
                    FIP.UserName = jsonParam.UserName;
                    FIP.BankAccount = jsonParam.BankAccount;
                    FIP.DateStart3 = jsonParam.DateStart3;
                    FIP.DateEnd3 = jsonParam.DateEnd3;
                    FIP.UnitNumber = jsonParam.UnitNumber;
                    FIP.Deposit3 = jsonParam.Deposit3;
                    FIP.Method = jsonParam.Method;
                    FIP.PaymentType = jsonParam.PaymentType;


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
                    if (!string.IsNullOrEmpty(FIP.DateStart3))
                    {
                        FIP.actualDS3 = new DateTime(long.Parse(FIP.DateStart3));
                    }
                    else
                    {
                        FIP.actualDS3 = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(FIP.DateEnd3))
                    {
                        FIP.actualDE3 = new DateTime(long.Parse(FIP.DateEnd3));
                    }
                    else
                    {
                        FIP.actualDE3 = new DateTime(7000, 01, 01, 00, 00, 00);
                    }
                }

                SqlCommand cmd = new SqlCommand("AP2SP_RP_FI_009", con);
                cmd.Parameters.AddWithValue("@CompanyID", FIP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductID", FIP.ProductID);
                cmd.Parameters.AddWithValue("@DateStart", FIP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", FIP.actualDE);
                cmd.Parameters.AddWithValue("@DateStart2", FIP.actualDS2);
                cmd.Parameters.AddWithValue("@DateEnd2", FIP.actualDE2);
                cmd.Parameters.AddWithValue("@UserName", FIP.UserName);
                cmd.Parameters.AddWithValue("@BankAccount", FIP.BankAccount);
                cmd.Parameters.AddWithValue("@DateStart3", FIP.actualDS3);
                cmd.Parameters.AddWithValue("@DateEnd3", FIP.actualDE3);
                cmd.Parameters.AddWithValue("@UnitNumber", FIP.UnitNumber);
                cmd.Parameters.AddWithValue("@Deposit3", FIP.Deposit3);
                cmd.Parameters.AddWithValue("@Method", FIP.Method);
                cmd.Parameters.AddWithValue("@PaymentType", FIP.PaymentType);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);
                
                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@CompanyID", FIP.CompanyID);
                crystalReport.SetParameterValue("@ProductID", FIP.ProductID);
                crystalReport.SetParameterValue("@DateStart", FIP.actualDS);
                crystalReport.SetParameterValue("@DateEnd", FIP.actualDE);
                crystalReport.SetParameterValue("@DateStart2", FIP.actualDS2);
                crystalReport.SetParameterValue("@DateEnd2", FIP.actualDE2);
                crystalReport.SetParameterValue("@UserName", FIP.UserName);
                crystalReport.SetParameterValue("@BankAccount", FIP.BankAccount);
                crystalReport.SetParameterValue("@DateStart3", FIP.actualDS3);
                crystalReport.SetParameterValue("@DateEnd3", FIP.actualDE3);
                crystalReport.SetParameterValue("@UnitNumber", FIP.UnitNumber);
                crystalReport.SetParameterValue("@Deposit3", FIP.Deposit3);
                crystalReport.SetParameterValue("@Method", FIP.Method);
                crystalReport.SetParameterValue("@PaymentType", FIP.PaymentType);
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