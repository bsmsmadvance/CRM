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
    public partial class PF_LC_005 : System.Web.UI.Page
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
            PFP.ReceivePromotionID = Request.QueryString["ReceivePromotionID"] != null ? Request.QueryString["ReceivePromotionID"] : "";
            PFP.PromotionType = Request.QueryString["PromotionType"] != null ? Request.QueryString["PromotionType"] : "";
            PFP.DocumentID = Request.QueryString["DocumentID"] != null ? Request.QueryString["DocumentID"] : "";
            PFP.DocumentType = Request.QueryString["DocumentType"] != null ? Request.QueryString["DocumentType"] : "";
            PFP.UserID = Request.QueryString["UserID"] != null ? Request.QueryString["UserID"] : "";

            if (PFP.downloadAs == null)
            {
                PFP.downloadAs = string.Empty;
            }

            try
            {

                PFP.reportName = "PF_LC_005_poReceivePromotion";
                PFP.fullReportPath = "\\Reports\\PF\\" + PFP.reportName + ".rpt";

                string reportPath = SM.reportPath(PFP.fullReportPath);
                crystalReport.Load(reportPath);

                //Main Report
                cmd = new SqlCommand("Z_SP_PRINTOUT_ReceivePromotion", con);
                cmd.Parameters.AddWithValue("@ReceivePromotionID", PFP.ReceivePromotionID);
                cmd.Parameters.AddWithValue("@PromotionType", PFP.PromotionType);
                cmd.Parameters.AddWithValue("@DocumentID", PFP.DocumentID);
                cmd.Parameters.AddWithValue("@DocumentType", PFP.DocumentType);
                cmd.Parameters.AddWithValue("@UserID", PFP.UserID);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                //SubReport 1 (Z_SP_PRINTOUT_ReceivePromotion_1)
                cmd = new SqlCommand("Z_SP_PRINTOUT_ReceivePromotion_1", con);
                cmd.Parameters.AddWithValue("@ReceivePromotionID", PFP.ReceivePromotionID);
                cmd.Parameters.AddWithValue("@PromotionType", PFP.PromotionType);
                cmd.Parameters.AddWithValue("@DocumentID", PFP.DocumentID);
                cmd.Parameters.AddWithValue("@DocumentType", PFP.DocumentType);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                //SubReport 2 (po_ReceivePromotion_2)
                cmd = new SqlCommand("Z_SP_PRINTOUT_ReceivePromotion_2", con);
                cmd.Parameters.AddWithValue("@ReceivePromotionID", PFP.ReceivePromotionID);
                cmd.Parameters.AddWithValue("@PromotionType", PFP.PromotionType);
                cmd.Parameters.AddWithValue("@DocumentID", PFP.DocumentID);
                cmd.Parameters.AddWithValue("@DocumentType", PFP.DocumentType);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                dtAdapter.Fill(dt2);
                ds.Tables.Add(dt2);

                foreach (ReportDocument rd in crystalReport.Subreports)
                {
                    if (rd.Name == "Z_SP_PRINTOUT_ReceivePromotion_1")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[1]);
                    }
                    else
                    if (rd.Name == "poReceivePromotion_2")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[2]);
                    }

                    crystalReport.Subreports[rd.Name].SetDatabaseLogon(PFP.dbUsername, PFP.dbPassword);
                }

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@ReceivePromotionID", PFP.ReceivePromotionID);
                crystalReport.SetParameterValue("@PromotionType", PFP.PromotionType);
                crystalReport.SetParameterValue("@DocumentID", PFP.DocumentID);
                crystalReport.SetParameterValue("@DocumentType", PFP.DocumentType);
                crystalReport.SetParameterValue("@UserID", PFP.UserID);

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