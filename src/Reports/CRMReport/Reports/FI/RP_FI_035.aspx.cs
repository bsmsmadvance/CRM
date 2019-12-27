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
    public partial class RP_FI_0351 : System.Web.UI.Page
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

                FIP.reportName = "RP_FI_035";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(FIP.token, FIP.reportName);
                FIP.fullReportPath = "/Reports/FI/" + FIP.reportName + ".rpt";

                string reportPath = SM.reportPath(FIP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                var paramDicts = SM.checkForParams(FIP.param);

                FIParams jsonParam = JsonConvert.DeserializeObject<FIParams>(paramDicts);

                if (jsonParam != null)
                {
                    FIP.Code = jsonParam.Code;
                    FIP.CardMachineTypeKey = jsonParam.CardMachineTypeKey;
                    FIP.BankAccountID = jsonParam.BankAccountID;
                    FIP.CompanyID = jsonParam.CompanyID;
                    FIP.ProjectID = jsonParam.ProjectID;
                    FIP.ProjectStatusKey = jsonParam.ProjectStatusKey;
                    FIP.ReceiveBy = jsonParam.ReceiveBy;
                    FIP.ReceiveDateFrom = jsonParam.ReceiveDateFrom;
                    FIP.ReceiveDateTo = jsonParam.ReceiveDateTo;
                    FIP.CardMachineStatusKey = jsonParam.CardMachineStatusKey;
                }
                
                //FIP.Code = Request.QueryString["Code"] != null ? Request.QueryString["Code"] : "";
                //FIP.CardMachineTypeKey = Request.QueryString["CardMachineTypeKey"] != null ? Request.QueryString["CardMachineTypeKey"] : "";
                //FIP.BankAccountID = Request.QueryString["BankAccountID"] != null ? Request.QueryString["BankAccountID"] : "";
                //FIP.CompanyID = Request.QueryString["CompanyID"] != null ? Request.QueryString["CompanyID"] : "";
                //FIP.ProjectID = Request.QueryString["ProjectID"] != null ? Request.QueryString["ProjectID"] : "";
                //FIP.ProjectStatusKey = Request.QueryString["ProjectStatusKey"] != null ? Request.QueryString["ProjectStatusKey"] : "";
                //FIP.ReceiveBy = Request.QueryString["ReceiveBy"] != null ? Request.QueryString["ReceiveBy"] : "";
                //FIP.ReceiveDateFrom = Request.QueryString["ReceiveDateFrom"] != null ? Request.QueryString["ReceiveDateFrom"] : null;
                //FIP.ReceiveDateTo = Request.QueryString["ReceiveDateTo"] != null ? Request.QueryString["ReceiveDateTo"] : null;
                //FIP.CardMachineStatusKey = Request.QueryString["CardMachineStatusKey"] != null ? Request.QueryString["CardMachineStatusKey"] : "";

                //Convert DateTime
                if (!string.IsNullOrEmpty(FIP.ReceiveDateFrom))
                {
                    FIP.actualDS = new DateTime(long.Parse(FIP.ReceiveDateFrom));
                }
                else
                {
                    FIP.actualDS = new DateTime(1800, 01, 01, 00, 00, 00);
                }

                if (!string.IsNullOrEmpty(FIP.ReceiveDateTo))
                {
                    FIP.actualDE = new DateTime(long.Parse(FIP.ReceiveDateTo));
                }
                else
                {
                    FIP.actualDE = new DateTime(7000, 12, 31, 00, 00, 00);
                }

                //Main Report
                cmd = new SqlCommand("RP_FI_035", con);
                cmd.Parameters.AddWithValue("@Code", FIP.Code);
                cmd.Parameters.AddWithValue("@CardMachineTypeKey", FIP.CardMachineTypeKey);
                cmd.Parameters.AddWithValue("@BankAccountID", FIP.BankAccountID);
                cmd.Parameters.AddWithValue("@CompanyID", FIP.CompanyID);
                cmd.Parameters.AddWithValue("@ProjectID", FIP.ProjectID);
                cmd.Parameters.AddWithValue("@ProjectStatusKey", FIP.ProjectStatusKey);
                cmd.Parameters.AddWithValue("@ReceiveBy", FIP.ReceiveBy);
                cmd.Parameters.AddWithValue("@ReceiveDateFrom", FIP.actualDS);
                cmd.Parameters.AddWithValue("@ReceiveDateTo", FIP.actualDE);
                cmd.Parameters.AddWithValue("@CardMachineStatusKey", FIP.CardMachineStatusKey);
                
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@Code", FIP.Code);
                crystalReport.SetParameterValue("@CardMachineTypeKey", FIP.CardMachineTypeKey);
                crystalReport.SetParameterValue("@BankAccountID", FIP.BankAccountID);
                crystalReport.SetParameterValue("@CompanyID", FIP.CompanyID);
                crystalReport.SetParameterValue("@ProjectID", FIP.ProjectID);
                crystalReport.SetParameterValue("@ProjectStatusKey", FIP.ProjectStatusKey);
                crystalReport.SetParameterValue("@ReceiveBy", FIP.ReceiveBy);
                crystalReport.SetParameterValue("@ReceiveDateFrom", FIP.actualDS);
                crystalReport.SetParameterValue("@ReceiveDateTo", FIP.actualDE);
                crystalReport.SetParameterValue("@CardMachineStatusKey", FIP.CardMachineStatusKey);

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