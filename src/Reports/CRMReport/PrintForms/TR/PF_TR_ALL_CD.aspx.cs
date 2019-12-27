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
    public partial class PF_TR_ALL_CD1 : System.Web.UI.Page
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
            PFP.Layer1 = Request.QueryString["Layer1"] != null ? Request.QueryString["Layer1"] : "";
            PFP.Layer2 = Request.QueryString["Layer2"] != null ? Request.QueryString["Layer2"] : "";
            PFP.Layer3 = Request.QueryString["Layer3"] != null ? Request.QueryString["Layer3"] : "";
            PFP.Layer4 = Request.QueryString["Layer4"] != null ? Request.QueryString["Layer4"] : "";
            PFP.Layer5 = Request.QueryString["Layer5"] != null ? Request.QueryString["Layer5"] : "";
            PFP.Layer6 = Request.QueryString["Layer6"] != null ? Request.QueryString["Layer6"] : "";
            PFP.Layer7 = Request.QueryString["Layer7"] != null ? Request.QueryString["Layer7"] : "";
            PFP.Layer8 = Request.QueryString["Layer8"] != null ? Request.QueryString["Layer8"] : "";
            PFP.Layer9 = Request.QueryString["Layer9"] != null ? Request.QueryString["Layer9"] : "";
            PFP.Layer10 = Request.QueryString["Layer10"] != null ? Request.QueryString["Layer10"] : "";
            PFP.AssignName = Request.QueryString["AssignName"] != null ? Request.QueryString["AssignName"] : "";
            PFP.HomeType = Request.QueryString["HomeType"] != null ? Request.QueryString["HomeType"] : "";
            PFP.HomeOffice = Request.QueryString["HomeOffice"] != null ? Request.QueryString["HomeOffice"] : "";
            PFP.Hurdle = Request.QueryString["Hurdle"] != null ? Request.QueryString["Hurdle"] : "";
            PFP.AddressType = Request.QueryString["AddressType"] != null ? Request.QueryString["AddressType"] : "";
            PFP.AddressTypeChange = Request.QueryString["AddressTypeChange"] != null ? Request.QueryString["AddressTypeChange"] : "";
            PFP.Other = Request.QueryString["Other"] != null ? Request.QueryString["Other"] : "";
            PFP.LVDate = Request.QueryString["LVDate"] != null ? Request.QueryString["LVDate"] : "";
            PFP.LVDateCust = Request.QueryString["LVDateCust"] != null ? Request.QueryString["LVDateCust"] : "";
            PFP.TransferNumber = Request.QueryString["TransferNumber"] != null ? Request.QueryString["TransferNumber"] : "";
            PFP.UserName = Request.QueryString["UserName"] != null ? Request.QueryString["UserName"] : "";
            PFP.Layer11 = Request.QueryString["Layer11"] != null ? Request.QueryString["Layer11"] : "";


            if (PFP.downloadAs == null)
            {
                PFP.downloadAs = string.Empty;
            }

            try
            {
                PFP.reportName = "PF_TR_ALL_CD";
                PFP.fullReportPath = "\\Reports\\PF\\" + PFP.reportName + ".rpt";

                string reportPath = SM.reportPath(PFP.fullReportPath);
                crystalReport.Load(reportPath);

                //Main Report
                cmd = new SqlCommand("AP2SP_PF_TR_001CD", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                /* cmd.Parameters.AddWithValue("@Layer1", PFP.Layer1);
                cmd.Parameters.AddWithValue("@Layer2", PFP.Layer2);
                cmd.Parameters.AddWithValue("@Layer3", PFP.Layer3);
                cmd.Parameters.AddWithValue("@Layer4", PFP.Layer4);
                cmd.Parameters.AddWithValue("@Layer5", PFP.Layer5);
                cmd.Parameters.AddWithValue("@Layer6", PFP.Layer6);
                cmd.Parameters.AddWithValue("@Layer7", PFP.Layer7);
                cmd.Parameters.AddWithValue("@Layer8", PFP.Layer8);
                cmd.Parameters.AddWithValue("@Layer9", PFP.Layer9);
                cmd.Parameters.AddWithValue("@Layer10", PFP.Layer10);
                cmd.Parameters.AddWithValue("@AssignName", PFP.AssignName);
                cmd.Parameters.AddWithValue("@HomeType", PFP.HomeType);
                cmd.Parameters.AddWithValue("@HomeOffice", PFP.HomeOffice);
                cmd.Parameters.AddWithValue("@Hurdle", PFP.Hurdle);
                cmd.Parameters.AddWithValue("@AddressType", PFP.AddressType);
                cmd.Parameters.AddWithValue("@AddressTypeChange", PFP.AddressTypeChange);
                cmd.Parameters.AddWithValue("@Other", PFP.Other);
                cmd.Parameters.AddWithValue("LVDate", PFP.LVDate);
                cmd.Parameters.AddWithValue("LVDateCust", PFP.LVDateCust);
                cmd.Parameters.AddWithValue("@TransferNumber", PFP.TransferNumber);
                cmd.Parameters.AddWithValue("@UserName", PFP.UserName);
                cmd.Parameters.AddWithValue("@Layer11", PFP.Layer11); */
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                dtAdapter.Fill(dt);
                ds.Tables.Add(dt);

                /* if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        PFP.OperateType = row["OperateType"].ToString();
                    }

                    if (PFP.OperateType == null)
                    {
                        PFP.OperateType = "";
                    }
                } */

                //PF_TR_001CD.rpt
                cmd = new SqlCommand("AP2SP_PF_TR_001CD", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                dtAdapter.Fill(dt1);
                ds.Tables.Add(dt1);

                cmd = new SqlCommand("AP2SP_PF_TR_001CD_1", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                dtAdapter.Fill(dt2);
                ds.Tables.Add(dt2);

                //PF_TR_002CD.rpt
                cmd = new SqlCommand("AP2SP_PF_TR_002CD", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt3 = new DataTable();
                dtAdapter.Fill(dt3);
                ds.Tables.Add(dt3);

                //PF_TR_003CD.rpt
                cmd = new SqlCommand("AP2SP_PF_TR_003", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt4 = new DataTable();
                dtAdapter.Fill(dt4);
                ds.Tables.Add(dt4);

                cmd = new SqlCommand("AP2SP_PF_TR_003_1", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt5 = new DataTable();
                dtAdapter.Fill(dt5);
                ds.Tables.Add(dt5);

                //PF_TR_005CD.rpt
                cmd = new SqlCommand("AP2SP_PF_TR_005CD", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt6 = new DataTable();
                dtAdapter.Fill(dt6);
                ds.Tables.Add(dt6);

                cmd = new SqlCommand("AP2SP_PF_TR_005CD_1", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt7 = new DataTable();
                dtAdapter.Fill(dt7);
                ds.Tables.Add(dt7);

                //PF_TR_005CD_1.rpt
                cmd = new SqlCommand("AP2SP_PF_TR_005CD", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt8 = new DataTable();
                dtAdapter.Fill(dt8);
                ds.Tables.Add(dt8);

                cmd = new SqlCommand("AP2SP_PF_TR_005CD_1", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt9 = new DataTable();
                dtAdapter.Fill(dt9);
                ds.Tables.Add(dt9);

                //PF_LC_003CD.rpt
                cmd = new SqlCommand("AP2SP_PF_TR_003CD", con);
                cmd.Parameters.AddWithValue("@TransferNumber", PFP.TransferNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt10 = new DataTable();
                dtAdapter.Fill(dt10);
                ds.Tables.Add(dt10);

                //PF_LC_005.rpt
                cmd = new SqlCommand("AP2SP_PF_LC_005", con);
                cmd.Parameters.AddWithValue("@TransferNumber", PFP.TransferNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt11 = new DataTable();
                dtAdapter.Fill(dt11);
                ds.Tables.Add(dt11);

                //PF_LC_006.rpt
                cmd = new SqlCommand("AP2SP_PF_LC_006", con);
                cmd.Parameters.AddWithValue("@TransferNumber", PFP.TransferNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt12 = new DataTable();
                dtAdapter.Fill(dt12);
                ds.Tables.Add(dt12);

                //PF_TR_004.rpt
                cmd = new SqlCommand("AP2SP_PF_TR_004", con);
                cmd.Parameters.AddWithValue("@TransferNumber", PFP.TransferNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt13 = new DataTable();
                dtAdapter.Fill(dt13);
                ds.Tables.Add(dt13);

                //PF_TR_006CD.rpt
                cmd = new SqlCommand("AP2SP_PF_TR_006CD", con);
                cmd.Parameters.AddWithValue("@ContractNumber", PFP.ContractNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt14 = new DataTable();
                dtAdapter.Fill(dt14);
                ds.Tables.Add(dt14);

                //PF_LC_003CDEng.rpt
                cmd = new SqlCommand("AP2SP_PF_LC_003CDEng", con);
                cmd.Parameters.AddWithValue("@TransferNumber", PFP.TransferNumber);
                cmd.CommandType = CommandType.StoredProcedure;
                dtAdapter = new SqlDataAdapter(cmd);
                DataTable dt15 = new DataTable();
                dtAdapter.Fill(dt15);
                ds.Tables.Add(dt15);


                List<string> srName = new List<string>();
                foreach (ReportDocument rd in crystalReport.Subreports)
                {
                    srName.Add(rd.Name);
                    if (rd.Name == "PF_TR_001CD.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[1]);
                    }
                    else if (rd.Name == "PF_TR_002CD.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[3]);
                    }
                    else if (rd.Name == "PF_TR_003CD.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[4]);
                    }
                    else if (rd.Name == "PF_TR_005CD.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[6]);
                    }
                    else if (rd.Name == "PF_TR_005CD_1.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[8]);
                    }
                    else if (rd.Name == "PF_LC_003CD.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[10]);
                    }
                    else if (rd.Name == "PF_LC_005.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[11]);
                    }
                    else if (rd.Name == "PF_LC_006.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[12]);
                    }
                    else if (rd.Name == "PF_TR_004.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[13]);
                    }
                    else if (rd.Name == "PF_TR_006CD.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[14]);
                    }
                    else if (rd.Name == "PF_LC_003CDEng.rpt")
                    {
                        crystalReport.Subreports[rd.Name].SetDataSource(ds.Tables[15]);
                    }
                    crystalReport.Subreports[rd.Name].SetDatabaseLogon(PFP.dbUsername, PFP.dbPassword);
                }

                crystalReport.SetDataSource(ds.Tables[0]);
                crystalReport.SetParameterValue("@ContractNumber", PFP.ContractNumber);
                crystalReport.SetParameterValue("@Layer1", PFP.Layer1);
                crystalReport.SetParameterValue("@Layer2", PFP.Layer2);
                crystalReport.SetParameterValue("@Layer3", PFP.Layer3);
                crystalReport.SetParameterValue("@Layer4", PFP.Layer4);
                crystalReport.SetParameterValue("@Layer5", PFP.Layer5);
                crystalReport.SetParameterValue("@Layer6", PFP.Layer6);
                crystalReport.SetParameterValue("@Layer7", PFP.Layer7);
                crystalReport.SetParameterValue("@Layer8", PFP.Layer8);
                crystalReport.SetParameterValue("@Layer9", PFP.Layer9);
                crystalReport.SetParameterValue("@Layer10", PFP.Layer10);
                crystalReport.SetParameterValue("@AssignName", PFP.AssignName);
                crystalReport.SetParameterValue("@HomeType", PFP.HomeType);
                crystalReport.SetParameterValue("@HomeOffice", PFP.HomeOffice);
                crystalReport.SetParameterValue("@Hurdle", PFP.Hurdle);
                crystalReport.SetParameterValue("@AddressType", PFP.AddressType);
                crystalReport.SetParameterValue("@AddressTypeChange", PFP.AddressTypeChange);
                crystalReport.SetParameterValue("@Other", PFP.Other);
                crystalReport.SetParameterValue("LVDate", PFP.LVDate);
                crystalReport.SetParameterValue("LVDateCust", PFP.LVDateCust);
                crystalReport.SetParameterValue("@TransferNumber", PFP.TransferNumber);
                crystalReport.SetParameterValue("@UserName", PFP.UserName);
                crystalReport.SetParameterValue("@Layer11", PFP.Layer11);

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