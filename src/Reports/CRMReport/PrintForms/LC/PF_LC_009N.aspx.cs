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
    public partial class PF_LC_009N1 : System.Web.UI.Page
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

                PFP.reportName = "PF_LC_009N";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(PFP.token, PFP.reportName);
                PFP.fullReportPath = "/PrintForms/LC/" + PFP.reportName + ".rpt";

                string reportPath = SM.reportPath(PFP.fullReportPath);
                crystalReport.Load(reportPath);


                //Check for params
                //PFP.FloorPlan = Request.QueryString["FloorPlan"] != null ? Request.QueryString["FloorPlan"] : "";
                //PFP.RoomPlan = Request.QueryString["RoomPlan"] != null ? Request.QueryString["RoomPlan"] : "";
                //PFP.QuotationNo = Request.QueryString["QuotationNo"] != null ? Request.QueryString["QuotationNo"] : "";
                var paramDicts = SM.checkForParams(PFP.param);

                PFParams jsonParam = JsonConvert.DeserializeObject<PFParams>(paramDicts);

                if (jsonParam != null)
                {
                    PFP.FloorPlan = jsonParam.FloorPlan;
                    PFP.RoomPlan = jsonParam.RoomPlan;
                    PFP.QuotationNo = jsonParam.QuotationNo;
                }

                //Main Report
                cmd = new SqlCommand("PF_LC_009N", con);
                cmd.Parameters.AddWithValue("@QuotationNo", PFP.QuotationNo);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);
                
                //Download Images
                if (!string.IsNullOrEmpty(PFP.FloorPlan))
                {

                    var dr = dt.Rows[0];
                    var projectNo = dr["ProjectNo"].ToString();
                    var imageName = dr["FloorPlanName"].ToString();

                    SM.downloadImageFromURL(projectNo, PFP.QuotationNo, PFP.FloorPlan, imageName);
                }

                if (!string.IsNullOrEmpty(PFP.RoomPlan))
                {
                    var dr = dt.Rows[0];
                    var projectNo = dr["ProjectNo"].ToString();
                    var imageName = dr["FloorPlanName"].ToString();
                    SM.downloadImageFromURL(projectNo, PFP.QuotationNo, PFP.RoomPlan, imageName);
                }


                //SubReport 1
                cmd = new SqlCommand("PF_LC_009N_1", con);
                cmd.Parameters.AddWithValue("@QuotationNo", PFP.QuotationNo);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                //SubReport 2
                cmd = new SqlCommand("PF_LC_009N_2", con);
                cmd.Parameters.AddWithValue("@QuotationNo", PFP.QuotationNo);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                dtAdapter.Fill(dt2);
                ds.Tables.Add(dt2);

                foreach (ReportDocument rd in crystalReport.Subreports)
                {
                    if (rd.Name == "PF_LC_009N_1.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[1]);
                    }
                    else
                    if (rd.Name == "PF_LC_009N_2.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[2]);
                    }

                    crystalReport.Subreports[rd.Name].SetDatabaseLogon(PFP.dbUsername, PFP.dbPassword);
                }

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@QuotationNo", PFP.QuotationNo);

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