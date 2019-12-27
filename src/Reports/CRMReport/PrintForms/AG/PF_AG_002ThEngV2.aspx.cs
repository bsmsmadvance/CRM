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
    public partial class PF_AG_002ThEngV21 : System.Web.UI.Page
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
            PFP.downloadAs = DownloadAs.showpdf.ToString();
            PFP.BookingNumber = Request.QueryString["BookingNumber"] != null ? Request.QueryString["BookingNumber"] : "";

            if (PFP.downloadAs == null)
            {
                PFP.downloadAs = string.Empty;
            }

            try
            {

                PFP.reportName = "PF_AG_002ThEng";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(PFP.token, PFP.reportName);
                PFP.fullReportPath = "/PrintForms/AG/" + PFP.reportName + ".rpt";

                string reportPath = SM.reportPath(PFP.fullReportPath);
                crystalReport.Load(reportPath);

                //Main Report
                cmd = new SqlCommand("AP2SP_PF_AG_002_TH_EN", con);
                cmd.Parameters.AddWithValue("@BookingNumber", PFP.BookingNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                //Report2.rpt
                cmd = new SqlCommand("AP2SP_PF_AG_002_1", con);
                cmd.Parameters.AddWithValue("@BookingNumber", PFP.BookingNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                //Report3.rpt
                cmd = new SqlCommand("AP2SP_PF_AG_002_1Eng", con);
                cmd.Parameters.AddWithValue("@BookingNumber", PFP.BookingNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                dtAdapter.Fill(dt2);
                ds.Tables.Add(dt2);

                //poCustomerAddress.rpt
                cmd = new SqlCommand("CRM_SP_PRINTOUT_CustomerAddress2", con);
                cmd.Parameters.AddWithValue("@BoongingNumber", PFP.BookingNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt3 = new DataTable();
                dtAdapter.Fill(dt3);
                ds.Tables.Add(dt3);

                List<string> srName = new List<string>();
                foreach (ReportDocument rd in crystalReport.Subreports)
                {
                    srName.Add(rd.Name);
                    if (rd.Name == "Report2.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[1]);
                    }
                    else if (rd.Name == "Report3.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[2]);
                    }
                    else if (rd.Name == "poCustomerAddress.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[3]);
                    }
                    crystalReport.Subreports[rd.Name].SetDatabaseLogon(PFP.dbUsername, PFP.dbPassword);
                }

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@BookingNumber", PFP.BookingNumber);

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