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
using CRMReport.Reports.LC;
using Newtonsoft.Json;

namespace CRMReport.Reports.LC
{
    public partial class RP_CommissionByProjectSale_H1 : System.Web.UI.Page
    {
        //Parameter
        LCParams LCP = new LCParams();
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
            LCP.dbConnection = connection.dbConnection;
            LCP.dbUsername = connection.dbUsername;
            LCP.dbPassword = connection.dbPassword;
            con.ConnectionString = LCP.dbConnection;

            //Parameter 
            LCP.token = Request.QueryString["token"];
            LCP.param = Request.QueryString["params"];
            LCP.downloadAs = Request.QueryString["DownloadAs"];

            if (LCP.downloadAs == null)
            {
                LCP.downloadAs = string.Empty;
            }

            try
            {
                LCP.reportName = "RP_CommissionByProjectSale_H";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(LCP.token, LCP.reportName);
                LCP.fullReportPath = "/Reports/LC/" + LCP.reportName + ".rpt";

                string reportPath = SM.reportPath(LCP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //LCP.ProductID = Request.QueryString["ProductID"] != null ? Request.QueryString["ProductID"] : "";
                //LCP.Month = Request.QueryString["Month"] != null ? Request.QueryString["Month"] : "";
                //LCP.Year = Request.QueryString["Year"] != null ? Request.QueryString["Year"] : "";
                //LCP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //LCP.HomeType = Request.QueryString["HomeType"] != null ? Request.QueryString["HomeType"] : "";
                //LCP.ProjectGroup = Request.QueryString["ProjectGroup"] != null ? Request.QueryString["ProjectGroup"] : "";
                //LCP.ProjectType2 = Request.QueryString["ProjectType2"] != null ? Request.QueryString["ProjectType2"] : "";

                var paramDicts = SM.checkForParams(LCP.param);
                LCParams jsonParam = JsonConvert.DeserializeObject<LCParams>(paramDicts);

                if (jsonParam != null)
                {
                    LCP.ProductID = jsonParam.ProductID;
                    LCP.Month = jsonParam.Month;
                    LCP.Year = jsonParam.Year;
                    LCP.UserName = jsonParam.UserName;
                    LCP.HomeType = jsonParam.HomeType;
                    LCP.ProjectGroup = jsonParam.ProjectGroup;
                    LCP.ProjectType2 = jsonParam.ProjectType2;
                    
                }

                SqlCommand cmd = new SqlCommand("ZCM_SP_Report_ComissionByProjectSale_H", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@ProductID", LCP.ProductID);
                cmd.Parameters.AddWithValue("@Month", Convert.ToInt16(LCP.Month));
                cmd.Parameters.AddWithValue("@Year", Convert.ToInt16(LCP.Year));
                cmd.Parameters.AddWithValue("@UserName", LCP.UserName);
                cmd.Parameters.AddWithValue("@HomeType", LCP.HomeType);
                cmd.Parameters.AddWithValue("@ProjectGroup", LCP.ProjectGroup);
                cmd.Parameters.AddWithValue("@ProjectType2", LCP.ProjectType2);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                //Comm_Rpt_OntopPaidOther
                cmd = new SqlCommand("ZCM_SP_Report_OntopPaidOther", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@ProductID", LCP.ProductID);
                cmd.Parameters.AddWithValue("@Month", Convert.ToInt16(LCP.Month));
                cmd.Parameters.AddWithValue("@Year", Convert.ToInt16(LCP.Year));
                cmd.Parameters.AddWithValue("@UserName", LCP.UserName);
                cmd.Parameters.AddWithValue("@HomeType", LCP.HomeType);
                cmd.Parameters.AddWithValue("@ProjectGroup", LCP.ProjectGroup);
                cmd.Parameters.AddWithValue("@ProjectType2", LCP.ProjectType2);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                //ZCM_SP_Report_CommissionContractCancel
                cmd = new SqlCommand("ZCM_SP_Report_CommissionContractCancel", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@ProductID", LCP.ProductID);
                cmd.Parameters.AddWithValue("@Month", Convert.ToInt16(LCP.Month));
                cmd.Parameters.AddWithValue("@Year", Convert.ToInt16(LCP.Year));
                cmd.Parameters.AddWithValue("@UserName", LCP.UserName);
                cmd.Parameters.AddWithValue("@HomeType", LCP.HomeType);
                cmd.Parameters.AddWithValue("@ProjectGroup", LCP.ProjectGroup);
                cmd.Parameters.AddWithValue("@ProjectType2", LCP.ProjectType2);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                dtAdapter.Fill(dt2);
                ds.Tables.Add(dt2);

                foreach (ReportDocument rd in crystalReport.Subreports)
                {
                    if (rd.Name == "Comm_Rpt_OntopPaidOther")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[1]);
                    }
                    if (rd.Name == "ZCM_SP_Report_CommissionContractCancel")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[2]);
                    }

                    crystalReport.Subreports[rd.Name].SetDatabaseLogon(LCP.dbUsername, LCP.dbPassword);
                }

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@ProductID", LCP.ProductID);
                crystalReport.SetParameterValue("@Month", Convert.ToInt16(LCP.Month));
                crystalReport.SetParameterValue("@Year", Convert.ToInt16(LCP.Year));
                crystalReport.SetParameterValue("@UserName", LCP.UserName);
                crystalReport.SetParameterValue("@HomeType", LCP.HomeType);
                crystalReport.SetParameterValue("@ProjectGroup", LCP.ProjectGroup);
                crystalReport.SetParameterValue("@ProjectType2", LCP.ProjectType2);
                //crystalReport.SetParameterValue("@DS", TRP.actualDS);
                //crystalReport.SetParameterValue("@DE", TRP.actualDE);

                CRV.ReportSource = crystalReport;
                CRV.Visible = true;

                CRS.ReportDocument.SetDatabaseLogon(LCP.dbUsername, LCP.dbPassword);

                if (!string.IsNullOrEmpty(LCP.reportName))
                {
                    if (LCP.downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        SM.exportAsPDF(crystalReport, Response, LCP.reportName);
                    }
                    else if (LCP.downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        SM.exportAsExcel(crystalReport, Response, LCP.reportName);
                    }
                    else if (LCP.downloadAs.ToLower() == DownloadAs.showpdf.ToString())
                    {
                        SM.exportAsStream(crystalReport, Response, LCP.reportName);
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
            SM.exportAsPDF(crystalReport, Response, LCP.reportName);
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsExcel(crystalReport, Response, LCP.reportName);
        }
    }
}