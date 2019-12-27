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
using CRMReport.Reports.MD;
using Newtonsoft.Json;

namespace CRMReport.Reports.MD
{
    public partial class RP_Master_0011 : System.Web.UI.Page
    {
        //Parameter
        MDParams MDP = new MDParams();
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
            MDP.dbConnection = connection.dbConnection;
            MDP.dbUsername = connection.dbUsername;
            MDP.dbPassword = connection.dbPassword;
            con.ConnectionString = MDP.dbConnection;

            //Parameter 
            MDP.token = Request.QueryString["token"];
            MDP.param = Request.QueryString["params"];
            MDP.downloadAs = Request.QueryString["downloadAs"];

            if (MDP.downloadAs == null)
            {
                MDP.downloadAs = string.Empty;
            }

            try
            {

                MDP.reportName = "RP_Master_001";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(MDP.token, MDP.reportName);
                MDP.fullReportPath = "/Reports/MD/" + MDP.reportName + ".rpt";

                string reportPath = SM.reportPath(MDP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                var paramDicts = SM.checkForParams(MDP.param);
                MDParams jsonParam = JsonConvert.DeserializeObject<MDParams>(paramDicts);

                if (jsonParam != null)
                {
                    MDP.ProjectNo = jsonParam.ProjectNo;
                    MDP.ProjectNameTH = jsonParam.ProjectNameTH;
                    MDP.BrandID = jsonParam.BrandID;
                    MDP.CompanyID = jsonParam.CompanyID;
                    MDP.ProductTypeKey = jsonParam.ProductTypeKey;
                    MDP.IsActive = jsonParam.IsActive;
                }


                //MDP.ProjectNo = Request.QueryString["ProjectNo"] != null ? Request.QueryString["ProjectNo"] : "";
                //MDP.ProjectNameTH = Request.QueryString["ProjectNameTH"] != null ? Request.QueryString["ProjectNameTH"] : "";
                //MDP.BrandID = Request.QueryString["BrandID"] != null ? Request.QueryString["BrandID"] : "";
                //MDP.CompanyID = Request.QueryString["CompanyID"] != null ? Request.QueryString["CompanyID"] : "";
                //MDP.ProductTypeKey = Request.QueryString["ProductTypeKey"] != null ? Request.QueryString["ProductTypeKey"] : "";
                //MDP.ProjectStatusKey = Request.QueryString["ProjectStatusKeys"] != null ? Request.QueryString["ProjectStatusKeys"] : "";
                //MDP.IsActive = Request.QueryString["IsActive"] != null ? Request.QueryString["IsActive"] : null;

                //Main Report
                cmd = new SqlCommand("RP_Master_001", con);
                cmd.Parameters.AddWithValue("@ProjectNo", MDP.ProjectNo);
                cmd.Parameters.AddWithValue("@ProjectNameTH", MDP.ProjectNameTH);
                cmd.Parameters.AddWithValue("@BrandID", MDP.BrandID);
                cmd.Parameters.AddWithValue("@CompanyID", MDP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductTypeKey", MDP.ProductTypeKey);

                if(!string.IsNullOrEmpty(MDP.ProjectStatusKey))
                {
                    cmd.Parameters.AddWithValue("@ProjectStatusKey", "(" + MDP.ProjectStatusKey + ")");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ProjectStatusKey", "");
                }

                if(string.IsNullOrEmpty(MDP.IsActive))
                {
                    cmd.Parameters.AddWithValue("@isActive", DBNull.Value );
                }
                else
                {
                    cmd.Parameters.AddWithValue("@isActive", Convert.ToBoolean(MDP.IsActive));
                }
                
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);
                
                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@ProjectNo", MDP.ProjectNo);
                crystalReport.SetParameterValue("@ProjectNameTH", MDP.ProjectNameTH);
                crystalReport.SetParameterValue("@BrandID", MDP.BrandID);
                crystalReport.SetParameterValue("@CompanyID", MDP.CompanyID);
                crystalReport.SetParameterValue("@ProductTypeKey", MDP.ProductTypeKey);

                if (!string.IsNullOrEmpty(MDP.ProjectStatusKey))
                {
                    crystalReport.SetParameterValue("@ProjectStatusKey", "(" + MDP.ProjectStatusKey + ")");
                }
                else
                {
                    crystalReport.SetParameterValue("@ProjectStatusKey", "");
                }
                
                
                if (string.IsNullOrEmpty(MDP.IsActive))
                {
                    crystalReport.SetParameterValue("@isActive", DBNull.Value);
                }
                else
                {
                    crystalReport.SetParameterValue("@isActive", Convert.ToBoolean(MDP.IsActive));
                }
                
                CRV.ReportSource = crystalReport;
                CRV.Visible = true;
                CRS.ReportDocument.SetDatabaseLogon(MDP.dbUsername, MDP.dbPassword);

                if (!string.IsNullOrEmpty(MDP.reportName))
                {
                    if (MDP.downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        SM.exportAsPDF(crystalReport, Response, MDP.reportName);
                    }
                    else if (MDP.downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        SM.exportAsExcel(crystalReport, Response, MDP.reportName);
                    }
                    else if (MDP.downloadAs.ToLower() == DownloadAs.showpdf.ToString())
                    {
                        SM.exportAsStream(crystalReport, Response, MDP.reportName);
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
            SM.exportAsPDF(crystalReport, Response, MDP.reportName);
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsExcel(crystalReport, Response, MDP.reportName);
        }
    }
}