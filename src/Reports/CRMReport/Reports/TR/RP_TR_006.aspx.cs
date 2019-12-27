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
using CRMReport.Reports.TR;
using Newtonsoft.Json;

namespace CRMReport.Reports.TR
{
    public partial class RP_TR_0061 : System.Web.UI.Page
    {
        //Parameter
        TRParams TRP = new TRParams();
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
            TRP.dbConnection = connection.dbConnection;
            TRP.dbUsername = connection.dbUsername;
            TRP.dbPassword = connection.dbPassword;
            con.ConnectionString = TRP.dbConnection;

            //Parameter 
            TRP.token = Request.QueryString["token"];
            TRP.param = Request.QueryString["param"];
            TRP.downloadAs = Request.QueryString["DownloadAs"];
            
            //ARP.DateStart = "636921792000000000";
            //ARP.DateEnd = "636921792000000000";

            

            if (TRP.downloadAs == null)
            {
                TRP.downloadAs = string.Empty;
            }

            try
            {
                TRP.reportName = "RP_TR_006";

                //Check for authorization
                var isAuthorized = SM.checkForAuthorization(TRP.token, TRP.reportName);
                TRP.fullReportPath = "/Reports/TR/" + TRP.reportName + ".rpt";

                string reportPath = SM.reportPath(TRP.fullReportPath);
                crystalReport.Load(reportPath);

                //Check for params
                //TRP.Projects = Request.QueryString["Projects"] != null ? Request.QueryString["Projects"] : "";
                //TRP.UnitNumber = Request.QueryString["UnitNumber"] != null ? Request.QueryString["UnitNumber"] : "";
                //TRP.DateStart = Request.QueryString["DateStart"] != null ? Request.QueryString["DateStart"] : "";
                //TRP.DateEnd = Request.QueryString["DateEnd"] != null ? Request.QueryString["DateEnd"] : "";
                //TRP.StatusAG = Request.QueryString["StatusAG"] != null ? Request.QueryString["StatusAG"] : "";
                //TRP.LoanStatus = Request.QueryString["LoanStatus"] != null ? Request.QueryString["LoanStatus"] : "";
                //TRP.LoanStatus1 = Request.QueryString["LoanStatus1"] != null ? Request.QueryString["LoanStatus1"] : "";
                //TRP.BankOnly = Request.QueryString["BankOnly"] != null ? Request.QueryString["BankOnly"] : "";
                //TRP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
                //TRP.DateStart2 = Request.QueryString["DateStart2"] != null ? Request.QueryString["DateStart2"] : "";
                //TRP.DateEnd2 = Request.QueryString["DateEnd2"] != null ? Request.QueryString["DateEnd2"] : "";
                //TRP.DateStart3 = Request.QueryString["DateStart3"] != null ? Request.QueryString["DateStart3"] : "";
                //TRP.DateEnd3 = Request.QueryString["DateEnd3"] != null ? Request.QueryString["DateEnd3"] : "";
                //TRP.DateStart4 = Request.QueryString["DateStart4"] != null ? Request.QueryString["DateStart4"] : "";
                //TRP.DateEnd4 = Request.QueryString["DateEnd4"] != null ? Request.QueryString["DateEnd4"] : "";
                //TRP.HomeType = Request.QueryString["HomeType"] != null ? Request.QueryString["HomeType"] : "";
                //TRP.StatusProject = Request.QueryString["StatusProject"] != null ? Request.QueryString["StatusProject"] : "";
                //TRP.ProjectGroup = Request.QueryString["ProjectGroup"] != null ? Request.QueryString["ProjectGroup"] : "";
                //TRP.ProjectType2 = Request.QueryString["ProjectType2"] != null ? Request.QueryString["ProjectType2"] : "";

                var paramDicts = SM.checkForParams(TRP.param);
                TRParams jsonParam = JsonConvert.DeserializeObject<TRParams>(paramDicts);

                if (jsonParam != null)
                {
                    TRP.Projects = jsonParam.Projects;
                    TRP.UnitNumber = jsonParam.UnitNumber;
                    TRP.DateStart = jsonParam.DateStart;
                    TRP.DateEnd = jsonParam.DateEnd;
                    TRP.StatusAG = jsonParam.StatusAG;
                    TRP.LoanStatus = jsonParam.LoanStatus;
                    TRP.LoanStatus1 = jsonParam.LoanStatus1;
                    TRP.BankOnly = jsonParam.BankOnly;
                    TRP.UserName = jsonParam.UserName;
                    TRP.DateStart2 = jsonParam.DateStart2;
                    TRP.DateEnd2 = jsonParam.DateEnd2;
                    TRP.DateStart3 = jsonParam.DateStart3;
                    TRP.DateEnd3 = jsonParam.DateEnd3;
                    TRP.DateStart4 = jsonParam.DateStart4;
                    TRP.DateEnd4 = jsonParam.DateEnd4;
                    TRP.HomeType = jsonParam.HomeType;
                    TRP.StatusProject = jsonParam.StatusProject;
                    TRP.ProjectGroup = jsonParam.ProjectGroup;
                    TRP.ProjectType2 = jsonParam.ProjectType2;

                    //Convert DateTime
                    if (!string.IsNullOrEmpty(TRP.DateStart))
                    {
                        TRP.actualDS = new DateTime(long.Parse(TRP.DateStart));
                    }
                    else
                    {
                        TRP.actualDS = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(TRP.DateEnd))
                    {
                        TRP.actualDE = new DateTime(long.Parse(TRP.DateEnd));
                    }
                    else
                    {
                        TRP.actualDE = new DateTime(7000, 12, 31, 00, 00, 00);
                    }


                    if (!string.IsNullOrEmpty(TRP.DateStart2))
                    {
                        TRP.actualDS2 = new DateTime(long.Parse(TRP.DateStart2));
                    }
                    else
                    {
                        TRP.actualDS2 = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(TRP.DateEnd2))
                    {
                        TRP.actualDE2 = new DateTime(long.Parse(TRP.DateEnd2));
                    }
                    else
                    {
                        TRP.actualDE2 = new DateTime(7000, 12, 31, 00, 00, 00);
                    }


                    if (!string.IsNullOrEmpty(TRP.DateStart3))
                    {
                        TRP.actualDS3 = new DateTime(long.Parse(TRP.DateStart3));
                    }
                    else
                    {
                        TRP.actualDS3 = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(TRP.DateEnd3))
                    {
                        TRP.actualDE3 = new DateTime(long.Parse(TRP.DateEnd3));
                    }
                    else
                    {
                        TRP.actualDE3 = new DateTime(7000, 12, 31, 00, 00, 00);
                    }


                    if (!string.IsNullOrEmpty(TRP.DateStart4))
                    {
                        TRP.actualDS4 = new DateTime(long.Parse(TRP.DateStart4));
                    }
                    else
                    {
                        TRP.actualDS4 = new DateTime(1800, 01, 01, 00, 00, 00);
                    }
                    if (!string.IsNullOrEmpty(TRP.DateEnd4))
                    {
                        TRP.actualDE4 = new DateTime(long.Parse(TRP.DateEnd4));
                    }
                    else
                    {
                        TRP.actualDE4 = new DateTime(7000, 12, 31, 00, 00, 00);
                    }
                }

                SqlCommand cmd = new SqlCommand("AP2SP_RP_TR_006", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@Projects", TRP.Projects);
                cmd.Parameters.AddWithValue("@UnitNumber", TRP.UnitNumber);
                cmd.Parameters.AddWithValue("@DateStart", TRP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", TRP.actualDE);
                cmd.Parameters.AddWithValue("@StatusAG", TRP.StatusAG);
                cmd.Parameters.AddWithValue("@LoandStatus", TRP.LoanStatus);
                cmd.Parameters.AddWithValue("@LoanStatus1", TRP.LoanStatus1);
                cmd.Parameters.AddWithValue("@BankOnly", TRP.BankOnly);
                cmd.Parameters.AddWithValue("@UserName", TRP.UserName);
                cmd.Parameters.AddWithValue("@DateStart2", TRP.actualDS2);
                cmd.Parameters.AddWithValue("@DateEnd2", TRP.actualDE2);
                cmd.Parameters.AddWithValue("@DateStart3", TRP.actualDS3);
                cmd.Parameters.AddWithValue("@DateEnd3", TRP.actualDE3);
                cmd.Parameters.AddWithValue("@DateStart4", TRP.actualDS4);
                cmd.Parameters.AddWithValue("@DateEnd4", TRP.actualDE4);
                cmd.Parameters.AddWithValue("@HomeType", TRP.HomeType);
                cmd.Parameters.AddWithValue("@StatusProject", TRP.StatusProject);
                cmd.Parameters.AddWithValue("@ProjectGroup", TRP.ProjectGroup);
                cmd.Parameters.AddWithValue("@ProjectType2", TRP.ProjectType2);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                //RPT1
                cmd = new SqlCommand("AP2SP_RP_TR_006_1", con);
                cmd.CommandTimeout = 6000;
                cmd.Parameters.AddWithValue("@Projects", TRP.Projects);
                cmd.Parameters.AddWithValue("@UnitNumber", TRP.UnitNumber);
                cmd.Parameters.AddWithValue("@DateStart", TRP.actualDS);
                cmd.Parameters.AddWithValue("@DateEnd", TRP.actualDE);
                cmd.Parameters.AddWithValue("@StatusAG", TRP.StatusAG);
                cmd.Parameters.AddWithValue("@LoandStatus", TRP.LoanStatus);
                cmd.Parameters.AddWithValue("@LoanStatus1", TRP.LoanStatus1);
                cmd.Parameters.AddWithValue("@BankOnly", TRP.BankOnly);
                cmd.Parameters.AddWithValue("@UserName", TRP.UserName);
                cmd.Parameters.AddWithValue("@DateStart2", TRP.actualDS2);
                cmd.Parameters.AddWithValue("@DateEnd2", TRP.actualDE2);
                cmd.Parameters.AddWithValue("@DateStart3", TRP.actualDS3);
                cmd.Parameters.AddWithValue("@DateEnd3", TRP.actualDE3);
                cmd.Parameters.AddWithValue("@DateStart4", TRP.actualDS4);
                cmd.Parameters.AddWithValue("@DateEnd4", TRP.actualDE4);
                cmd.Parameters.AddWithValue("@HomeType", TRP.HomeType);
                cmd.Parameters.AddWithValue("@StatusProject", TRP.StatusProject);
                cmd.Parameters.AddWithValue("@ProjectGroup", TRP.ProjectGroup);
                cmd.Parameters.AddWithValue("@ProjectType2", TRP.ProjectType2);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                foreach (ReportDocument rd in crystalReport.Subreports)
                {
                    if (rd.Name == "RPT1")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[1]);
                    }

                    crystalReport.Subreports[rd.Name].SetDatabaseLogon(TRP.dbUsername, TRP.dbPassword);
                }

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@Projects", TRP.Projects);
                crystalReport.SetParameterValue("@UnitNumber", TRP.UnitNumber);
                crystalReport.SetParameterValue("@DateStart", TRP.actualDS);
                crystalReport.SetParameterValue("@DateEnd", TRP.actualDE);
                crystalReport.SetParameterValue("@StatusAG", TRP.StatusAG);
                crystalReport.SetParameterValue("@LoandStatus", TRP.LoanStatus);
                crystalReport.SetParameterValue("@LoanStatus1", TRP.LoanStatus1);
                crystalReport.SetParameterValue("@BankOnly", TRP.BankOnly);
                crystalReport.SetParameterValue("@UserName", TRP.UserName);
                crystalReport.SetParameterValue("@DateStart2", TRP.actualDS2);
                crystalReport.SetParameterValue("@DateEnd2", TRP.actualDE2);
                crystalReport.SetParameterValue("@DateStart3", TRP.actualDS3);
                crystalReport.SetParameterValue("@DateEnd3", TRP.actualDE3);
                crystalReport.SetParameterValue("@DateStart4", TRP.actualDS4);
                crystalReport.SetParameterValue("@DateEnd4", TRP.actualDE4);
                crystalReport.SetParameterValue("@HomeType", TRP.HomeType);
                crystalReport.SetParameterValue("@StatusProject", TRP.StatusProject);
                crystalReport.SetParameterValue("@ProjectGroup", TRP.ProjectGroup);
                crystalReport.SetParameterValue("@ProjectType2", TRP.ProjectType2);
                //crystalReport.SetParameterValue("@DS", TRP.actualDS);
                //crystalReport.SetParameterValue("@DE", TRP.actualDE);

                CRV.ReportSource = crystalReport;
                CRV.Visible = true;

                CRS.ReportDocument.SetDatabaseLogon(TRP.dbUsername, TRP.dbPassword);

                if (!string.IsNullOrEmpty(TRP.reportName))
                {
                    if (TRP.downloadAs.ToLower() == DownloadAs.pdf.ToString())
                    {
                        SM.exportAsPDF(crystalReport, Response, TRP.reportName);
                    }
                    else if (TRP.downloadAs.ToLower() == DownloadAs.excel.ToString())
                    {
                        SM.exportAsExcel(crystalReport, Response, TRP.reportName);
                    }
                    else if (TRP.downloadAs.ToLower() == DownloadAs.showpdf.ToString())
                    {
                        SM.exportAsStream(crystalReport, Response, TRP.reportName);
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
            SM.exportAsPDF(crystalReport, Response, TRP.reportName);
        }

        protected void Excel_Click(object sender, ImageClickEventArgs e)
        {
            SM.exportAsExcel(crystalReport, Response, TRP.reportName);
        }
    }
}