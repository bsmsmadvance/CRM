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
    public partial class PF_FI_002_New1 : System.Web.UI.Page
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
                PFP.reportName = "PF_FI_002_New";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(PFP.token, PFP.reportName);
                PFP.fullReportPath = "/PrintForms/FI/" + PFP.reportName + ".rpt";

                string reportPath = SM.reportPath(PFP.fullReportPath);
                crystalReport.Load(reportPath);
                
                //Check for params
                var paramDicts = SM.checkForParams(PFP.param);

                PFParams jsonParam = JsonConvert.DeserializeObject<PFParams>(paramDicts);

                if (jsonParam != null)
                {
                    PFP.ReceivedID = jsonParam.ReceivedID;
                    PFP.UserID = jsonParam.UserID;
                }

                //Main Report
                cmd = new SqlCommand("AP2SP_PF_FI_002_New", con);
                cmd.Parameters.AddWithValue("@ReceivedID", PFP.ReceivedID);
                cmd.Parameters.AddWithValue("@UserID", PFP.UserID);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        PFP.RCReference = row["RCReferent"].ToString();
                    }

                    if (PFP.RCReference == null)
                    {
                        PFP.RCReference = "";
                    }
                }

                //PF_FI_002_2
                cmd = new SqlCommand("AP2SP_PF_FI_002_New_1", con);
                cmd.Parameters.AddWithValue("@ReceivedID", PFP.ReceivedID);
                cmd.Parameters.AddWithValue("@RCReferent", PFP.RCReference);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                //SubMask_2
                cmd = new SqlCommand("AP2SP_PF_FI_002_New_2", con);
                cmd.Parameters.AddWithValue("@ReceivedID", PFP.ReceivedID);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                dtAdapter.Fill(dt2);
                ds.Tables.Add(dt2);

                //PF_FI_002_3
                cmd = new SqlCommand("AP2SP_PF_FI_002_New_1", con);
                cmd.Parameters.AddWithValue("@ReceivedID", PFP.ReceivedID);
                cmd.Parameters.AddWithValue("@RCReferent", PFP.RCReference);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt3 = new DataTable();
                dtAdapter.Fill(dt3);
                ds.Tables.Add(dt3);

                //SubMask
                cmd = new SqlCommand("AP2SP_PF_FI_002_New_2", con);
                cmd.Parameters.AddWithValue("@ReceivedID", PFP.ReceivedID);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt4 = new DataTable();
                dtAdapter.Fill(dt4);
                ds.Tables.Add(dt4);


                List<string> srName = new List<string>();
                foreach (ReportDocument rd in crystalReport.Subreports)
                {
                    srName.Add(rd.Name);
                    if (rd.Name == "PF_FI_002_2")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[1]);
                    }
                    else if (rd.Name == "SubMask_2")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[2]);
                    }
                    else if (rd.Name == "PF_FI_002_3")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[3]);
                    }
                    else if (rd.Name == "SubMask")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[4]);
                    }
                    crystalReport.Subreports[rd.Name].SetDatabaseLogon(PFP.dbUsername, PFP.dbPassword);
                }

                crystalReport.SetDataSource(ds.Tables[0]);
                cmd.Parameters.AddWithValue("@ReceivedID", PFP.ReceivedID);
                cmd.Parameters.AddWithValue("@UserID", PFP.UserID);

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