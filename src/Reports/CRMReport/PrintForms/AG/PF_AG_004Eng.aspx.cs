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
    public partial class PF_AG_004Eng1 : System.Web.UI.Page
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
            PFP.ContractNumber = Request.QueryString["ContractNumber"] != null ? Request.QueryString["ContractNumber"] : "";

            if (PFP.downloadAs == null)
            {
                PFP.downloadAs = string.Empty;
            }

            try
            {

                PFP.reportName = "PF_AG_004Eng";
                PFP.fullReportPath = "\\Reports\\PF\\" + PFP.reportName + ".rpt";

                string reportPath = SM.reportPath(PFP.fullReportPath);
                crystalReport.Load(reportPath);

                //Main Report
                cmd = new SqlCommand("AP2SP_PF_AG_004Eng", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        PFP.HistoryID = row["HistoryID"].ToString();
                        PFP.OperateType = row["OperateType"].ToString();
                    }
                }

                //Sub_PF_AG_005.rpt
                cmd = new SqlCommand("AP2SP_PF_AG_004_2Eng", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                cmd = new SqlCommand("AP2SP_PF_AG_004_1_2Eng", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                dtAdapter.Fill(dt2);
                ds.Tables.Add(dt2);

                //PF_AG_010.rpt
                cmd = new SqlCommand("AP2SP_PF_AG_010Eng", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt3 = new DataTable();
                dtAdapter.Fill(dt3);
                ds.Tables.Add(dt3);

                //PF_AG_025.rpt
                cmd = new SqlCommand("AP2SP_PF_AG_025", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.Parameters.AddWithValue("@HistoryID", PFP.HistoryID);
                cmd.Parameters.AddWithValue("@OperateType", PFP.OperateType);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt4 = new DataTable();
                dtAdapter.Fill(dt4);
                ds.Tables.Add(dt4);

                List<string> srName = new List<string>();
                foreach (ReportDocument rd in crystalReport.Subreports)
                {
                    srName.Add(rd.Name);
                    if (rd.Name == "Sub_PF_AG_005.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[1]);
                    }
                    else if (rd.Name == "PF_AG_010.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[3]);
                    }
                    else if (rd.Name == "PF_AG_025.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[4]);
                    }
                    crystalReport.Subreports[rd.Name].SetDatabaseLogon(PFP.dbUsername, PFP.dbPassword);
                }

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

        }
    }
}