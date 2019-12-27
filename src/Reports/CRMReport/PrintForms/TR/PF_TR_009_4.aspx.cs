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

namespace CRMReport.Reports.PF
{
    public partial class PF_TR_009_41 : System.Web.UI.Page
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
            PFP.reportName = Request.QueryString["ReportName"];
            PFP.downloadAs = DownloadAs.showpdf.ToString();
            PFP.TransferNumber = Request.QueryString["TransferNumber"] != null ? Request.QueryString["TransferNumber"] : "";
            PFP.NitiBankName = Request.QueryString["NitiBankName"] != null ? Request.QueryString["NitiBankName"] : "";
            PFP.NitiBankType = Request.QueryString["NitiBankType"] != null ? Request.QueryString["NitiBankType"] : "";
            PFP.NitiBankNo = Request.QueryString["NitiBankNo"] != null ? Request.QueryString["NitiBankNo"] : "";
            PFP.CustomerBankName = Request.QueryString["CustomerBankName"] != null ? Request.QueryString["CustomerBankName"] : "";
            PFP.CustomerBankType = Request.QueryString["CustomerBankType"] != null ? Request.QueryString["CustomerBankType"] : "";
            PFP.CustomerBankNo = Request.QueryString["CustomerBankNo"] != null ? Request.QueryString["CustomerBankNo"] : "";
            PFP.ContactID = Request.QueryString["ContactID"] != null ? Request.QueryString["ContactID"] : "";

            if (PFP.downloadAs == null)
            {
                PFP.downloadAs = string.Empty;
            }

            try
            {
                PFP.reportName = "PF_TR_009_4";
                PFP.fullReportPath = "\\Reports\\PF\\" + PFP.reportName + ".rpt";

                string reportPath = SM.reportPath(PFP.fullReportPath);
                crystalReport.Load(reportPath);

                //Main Report
                cmd = new SqlCommand("AP2SP_PF_TR_009", con);
                cmd.Parameters.AddWithValue("@TransferNumber", PFP.TransferNumber);
                cmd.Parameters.AddWithValue("@NitiBankName", PFP.NitiBankName);
                cmd.Parameters.AddWithValue("@NitiBankType", PFP.NitiBankType);
                cmd.Parameters.AddWithValue("@NitiBankNo", PFP.NitiBankNo);
                cmd.Parameters.AddWithValue("@CustomerBankName", PFP.CustomerBankName);
                cmd.Parameters.AddWithValue("@CustomerBankType", PFP.CustomerBankType);
                cmd.Parameters.AddWithValue("@CustomerBankNo", PFP.CustomerBankNo);
                cmd.Parameters.AddWithValue("@ContactID", PFP.ContactID);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@TransferNumber", PFP.ContractNumber);
                crystalReport.SetParameterValue("@NitiBankName", PFP.NitiBankName);
                crystalReport.SetParameterValue("@NitiBankType", PFP.NitiBankType);
                crystalReport.SetParameterValue("@NitiBankNo", PFP.NitiBankNo);
                crystalReport.SetParameterValue("@CustomerBankName", PFP.CustomerBankName);
                crystalReport.SetParameterValue("@CustomerBankType", PFP.CustomerBankType);
                crystalReport.SetParameterValue("@CustomerBankNo", PFP.CustomerBankNo);
                crystalReport.SetParameterValue("@ContactID", PFP.ContactID);

                CRV.ReportSource = crystalReport;
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