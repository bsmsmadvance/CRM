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
using CRMReport.Reports.AG;
using Newtonsoft.Json;

namespace CRMReport.Reports.AG
{
    public partial class RP_AG_0061 : System.Web.UI.Page
    {
        //Parameter
        AGParams AGP = new AGParams();
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
            AGP.dbConnection = connection.dbConnection;
            AGP.dbUsername = connection.dbUsername;
            AGP.dbPassword = connection.dbPassword;
            con.ConnectionString = AGP.dbConnection;

            //Parameter 
            AGP.token = Request.QueryString["token"];
            AGP.param = Request.QueryString["params"];
            AGP.downloadAs = Request.QueryString["DownloadAs"];
            

            if (AGP.downloadAs == null)
            {
                AGP.downloadAs = string.Empty;
            }

            try
            {
                AGP.reportName = "RP_AG_006";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(AGP.token, AGP.reportName);
                AGP.fullReportPath = "/Reports/AG/" + AGP.reportName + ".rpt";

                string reportPath = SM.reportPath(AGP.fullReportPath);
                crystalReport.Load(reportPath);


                //Check for params
                //AGP.CompanyID = Request.QueryString["CompanyID"] != null ? Request.QueryString["CompanyID"] : "";
                //AGP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
                //AGP.UnitNumber = Request.QueryString["UnitNumber"] != null ? Request.QueryString["UnitNumber"] : "";
                //AGP.StatusAG = Request.QueryString["StatusAG"] != null ? Request.QueryString["StatusAG"] : "";
                //AGP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
                //AGP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : "";
                //AGP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //AGP.HomeType = Request.QueryString["HomeType"] != null ? Request.QueryString["HomeType"] : "";
                //AGP.StatusProject = Request.QueryString["StatusProject"] != null ? Request.QueryString["StatusProject"] : "";
                //AGP.ProjectGroup = Request.QueryString["ProjectGroup"] != null ? Request.QueryString["ProjectGroup"] : "";
                //AGP.ProjectType2 = Request.QueryString["ProjectType2"] != null ? Request.QueryString["ProjectType2"] : "";

                var paramDicts = SM.checkForParams(AGP.param);
                AGParams jsonParam = JsonConvert.DeserializeObject<AGParams>(paramDicts);

                if (jsonParam != null)
                {
                    AGP.CompanyID = jsonParam.CompanyID;
                    AGP.ProductID = jsonParam.ProductID;
                    AGP.UnitNumber = jsonParam.UnitNumber;
                    AGP.StatusAG = jsonParam.StatusAG;
                    AGP.DateStart = jsonParam.DateStart;
                    AGP.DateEnd = jsonParam.DateEnd;
                    AGP.UserName = jsonParam.UserName;
                    AGP.HomeType = jsonParam.HomeType;
                    AGP.StatusProject = jsonParam.StatusProject;
                    AGP.ProjectGroup = jsonParam.ProjectGroup;
                    AGP.ProjectType2 = jsonParam.ProjectType2;


                    //Convert DateTime
                    if (!string.IsNullOrEmpty(AGP.DateStart))
                    {
                        AGP.actualDS = new DateTime(long.Parse(AGP.DateStart));
                    }
                    else
                    {
                        AGP.actualDS = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(AGP.DateEnd))
                    {
                        AGP.actualDE = new DateTime(long.Parse(AGP.DateEnd));
                    }
                    else
                    {
                        AGP.actualDE = new DateTime(7000, 12, 31, 00, 00, 00);
                    }

                }

                SqlCommand cmd = new SqlCommand("AP2SP_RP_AG_006", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@CompanyID", AGP.CompanyID);
                cmd.Parameters.AddWithValue("@ProductID", AGP.ProductID);
                cmd.Parameters.AddWithValue("@UnitNumber", AGP.UnitNumber);
                cmd.Parameters.AddWithValue("@StatusAG", AGP.StatusAG);
                cmd.Parameters.AddWithValue("@DateStart", AGP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", AGP.actualDE);
                cmd.Parameters.AddWithValue("@UserName", AGP.UserName);
                cmd.Parameters.AddWithValue("@HomeType", AGP.HomeType);
                cmd.Parameters.AddWithValue("@StatusProject", AGP.StatusProject);
                cmd.Parameters.AddWithValue("@ProjectGroup", AGP.ProjectGroup);
                cmd.Parameters.AddWithValue("@ProjectType2", AGP.ProjectType2);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);
                
                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@CompanyID", AGP.CompanyID);
                crystalReport.SetParameterValue("@ProductID", AGP.ProductID);
                crystalReport.SetParameterValue("@UnitNumber", AGP.UnitNumber);
                crystalReport.SetParameterValue("@StatusAG", AGP.StatusAG);
                crystalReport.SetParameterValue("@DateStart", AGP.actualDS);
                crystalReport.SetParameterValue("@DateEnd", AGP.actualDE);
                crystalReport.SetParameterValue("@UserName", AGP.UserName);
                crystalReport.SetParameterValue("@HomeType", AGP.HomeType);
                crystalReport.SetParameterValue("@StatusProject", AGP.StatusProject);
                crystalReport.SetParameterValue("@ProjectGroup", AGP.ProjectGroup);
                crystalReport.SetParameterValue("@ProjectType2", AGP.ProjectType2);

                CRV.ReportSource = crystalReport;
                CRV.Visible = true;

                CRS.ReportDocument.SetDatabaseLogon(AGP.dbUsername, AGP.dbPassword);

                if (!string.IsNullOrEmpty(AGP.reportName))
                {
                    if (AGP.downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        SM.exportAsPDF(crystalReport, Response, AGP.reportName);
                    }
                    else if (AGP.downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        SM.exportAsExcel(crystalReport, Response, AGP.reportName);
                    }
                    else if (AGP.downloadAs.ToLower() == DownloadAs.showpdf.ToString())
                    {
                        SM.exportAsStream(crystalReport, Response, AGP.reportName);
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
            SM.exportAsPDF(crystalReport, Response, AGP.reportName);
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsExcel(crystalReport, Response, AGP.reportName);
        }
    }
}