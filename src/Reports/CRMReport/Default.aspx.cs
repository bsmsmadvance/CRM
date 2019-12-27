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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var reportName = Request.QueryString["ReportName"];
            var groupName = Request.QueryString["Group"];

            if (!string.IsNullOrEmpty(reportName) || !string.IsNullOrEmpty(groupName))
            {
                //var fullReportPath = "/Reports/" + groupName + "/" + reportName + ".rpt";

                var fullReportPath = "/Reports/" + reportName + ".rpt";

                ReportDocument rpt = new ReportDocument();

                rpt.Load(Server.MapPath(fullReportPath));
                this.CrystalReportViewer1.ReportSource = rpt;
                rpt.SetDatabaseLogon("sa", "@minMitDev02");

                SqlConnection con = new SqlConnection(@"Data Source=softever.co.th;initial catalog=AP_CRM;User ID=sa;Password=@minMitDev02;");
                SqlCommand cmd = new SqlCommand("RPT." + reportName, con);
                cmd.Parameters.AddWithValue("@alias", "KBank");
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