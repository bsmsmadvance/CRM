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
using CRMReport.Reports.PF;
using Newtonsoft.Json;

namespace CRMReport.Reports.PF
{
    public partial class PF_AG_023TH1 : System.Web.UI.Page
    {
        //Parameter
        PFParams PFP = new PFParams();
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
            PFP.dbConnection = connection.dbConnection;
            PFP.dbUsername = connection.dbUsername;
            PFP.dbPassword = connection.dbPassword;
            con.ConnectionString = PFP.dbConnection;

            //Parameter 
            PFP.token = Request.QueryString["token"];
            PFP.param = Request.QueryString["params"];
            PFP.downloadAs = DownloadAs.showpdf.ToString();

            if (PFP.downloadAs == null)
            {
                PFP.downloadAs = string.Empty;
            }

            try
            {
                PFP.reportName = "PF_AG_023TH";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(PFP.token, PFP.reportName);
                PFP.fullReportPath = "/PrintForms/AG/" + PFP.reportName + ".rpt";

                string reportPath = SM.reportPath(PFP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //PFP.ContractNumber = Request.QueryString["ContractNumber"] != null ? Request.QueryString["ContractNumber"] : "";
                var paramDicts = SM.checkForParams(PFP.param);

                PFParams jsonParam = JsonConvert.DeserializeObject<PFParams>(paramDicts);

                if (jsonParam != null)
                {
                    PFP.ContractNumber = jsonParam.ContractNumber;
                }

                //Main Report
                cmd = new SqlCommand("AP2SP_PF_AG_023TH", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@ContractNumber", PFP.ContractNumber);

                CRV.ReportSource = crystalReport;
                CRV.Visible = true;
                CRS.ReportDocument.SetDatabaseLogon(PFP.dbUsername, PFP.dbPassword);

                if (!string.IsNullOrEmpty(PFP.reportName))
                {
                    if (PFP.downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        SM.exportAsPDF(crystalReport, Response, PFP.reportName);
                    }
                    else if (PFP.downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        SM.exportAsExcel(crystalReport, Response, PFP.reportName);
                    }
                    else if (PFP.downloadAs.ToLower() == DownloadAs.showpdf.ToString())
                    {
                        SM.exportAsStream(crystalReport, Response, PFP.reportName);
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
            SM.exportAsPDF(crystalReport, Response, PFP.reportName);
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsExcel(crystalReport, Response, PFP.reportName);
        }
    }
}