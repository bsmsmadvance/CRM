using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace CRMReport
{
    public partial class TestCustDB : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=192.168.2.52;initial catalog=db_crmRevolution@2019;User ID=crmv;Password=apt@ven2018;");

        public string reportName { get; set; }
        public string groupName { get; set; }
        public string fullReportPath { get; set; }
        public DateTime todayDate { get; set; }
        public string downloadAs { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            reportName = Request.QueryString["ReportName"];
            groupName = Request.QueryString["Group"];

            //var fullReportPath = "/Reports/" + groupName + "/" + reportName + ".rpt";
            fullReportPath = "/Reports/" + reportName + ".rpt";

            //var alias = Request.QueryString["alias"];

            if (!string.IsNullOrEmpty(reportName) || !string.IsNullOrEmpty(groupName))
            {

                reportName = Request.QueryString["ReportName"];
                groupName = Request.QueryString["Group"];
                downloadAs = Request.QueryString["DownloadAs"];

                if (downloadAs == null)
                {
                    downloadAs = string.Empty;
                }

                //var fullReportPath = "/Reports/" + groupName + "/" + reportName + ".rpt";
                fullReportPath = "/Reports/" + reportName + ".rpt";

                //var alias = Request.QueryString["alias"];

                if (!string.IsNullOrEmpty(reportName) || !string.IsNullOrEmpty(groupName))
                {
                    if (downloadAs.ToLower() == "pdf")
                    {
                        SqlCommand cmd = new SqlCommand(reportName, con);
                        //cmd.Parameters.AddWithValue("@alias", "KBank");
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        ReportDocument crystalReport = new ReportDocument();
                        crystalReport.Load(Server.MapPath(fullReportPath));
                        crystalReport.SetDataSource(dt);
                        CrystalReportViewer1.ReportSource = crystalReport;

                        crystalReport.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, reportName);
                    }
                    else if (downloadAs.ToLower() == "excel")
                    {
                        SqlCommand cmd = new SqlCommand(reportName, con);
                        //cmd.Parameters.AddWithValue("@alias", "KBank");
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        ReportDocument crystalReport = new ReportDocument();
                        crystalReport.Load(Server.MapPath(fullReportPath));
                        crystalReport.SetDataSource(dt);
                        CrystalReportViewer1.ReportSource = crystalReport;

                        crystalReport.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Response, true, reportName);
                    }
                    else
                    {
                        ReportDocument rpt = new ReportDocument();

                        rpt.Load(Server.MapPath(fullReportPath));
                        this.CrystalReportViewer1.ReportSource = rpt;
                        this.CrystalReportViewer1.DocumentView = CrystalDecisions.Shared.DocumentViewType.WebLayout;
                        rpt.SetDatabaseLogon("crmv", "apt@ven2018");

                        SqlCommand cmd = new SqlCommand(reportName, con);
                        //cmd.Parameters.AddWithValue("@alias", "KBank");
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rpt.SetDataSource(dt);
                        //rpt.SetParameterValue("@alias", "KBank");
                    }

                }


            }
        }
    }
}