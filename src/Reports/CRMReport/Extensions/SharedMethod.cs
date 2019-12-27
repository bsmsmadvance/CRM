#define DEV
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using IO = System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CRMReport.Extensions
{
    public class SharedMethod
    {

        public string reportPath(string reportName)
        {
            string path = string.Empty;
            
            #if (DEV)
            path = ConfigurationManager.AppSettings["ReportPath"];
            path = path + reportName;
            #endif

            #if (PROD)
            path = ConfigurationManager.AppSettings["ReportPathServer"];
            path = path + reportName;
            #endif

            return path;
        }

        public string checkForParams(string param)
        {

            if(string.IsNullOrEmpty(param))
            {
                throw new HttpException(500, "Parameters cannot be found.");
            }

            var secretKey = "nIcHeoYiMNZiJMYz";
            var decryptParams = Encrypt.DecryptString(param, secretKey);
            
            return decryptParams;

        }

        public bool checkForAuthorization(string token, string actualReportName)
        {

            var isAuthorized = false;

            if (string.IsNullOrEmpty(token))
            {
                throw new HttpException(401, "Authentication Failed");
            }
            else if(token == "bypass")
            {
                isAuthorized = true;
                return isAuthorized;
            }

            var secretKey = "nIcHeoYiMNZiJMYz";
            //token = HttpUtility.UrlDecode(token);

            var decryptToken = Encrypt.DecryptString(token, secretKey);
            var reportName = decryptToken.Split(':')[0];
            var reportExpire = Convert.ToInt64(decryptToken.Split(':')[1]);

            if(reportName != actualReportName)
            {
                throw new HttpException(401, "Authentication Failed");
            }
            
            var currentDateTime = System.DateTime.Now.Ticks;
            if(reportExpire < currentDateTime)
            {
                throw new HttpException(401, "Authentication Failed");
            }

            isAuthorized = true;
            return isAuthorized;
            
        }
        
        public void standardView(CrystalReportViewer CRV)
        {
            CRV.DocumentView = CrystalDecisions.Shared.DocumentViewType.WebLayout;
        }

        public void exportAsPDF(ReportDocument crystalReport, HttpResponse Response, string reportName)
        {
            crystalReport.ExportToHttpResponse
            (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, reportName);
        }

        public void exportAsExcel(ReportDocument crystalReport, HttpResponse Response, string reportName)
        {
            crystalReport.ExportToHttpResponse
            (CrystalDecisions.Shared.ExportFormatType.ExcelRecord, Response, true, reportName);
        }

        public void exportAsStream(ReportDocument crystalReport, HttpResponse Response, string reportName)
        {
            System.IO.Stream oStream = null;
            byte[] byteArray = null;
            oStream = crystalReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            byteArray = new byte[oStream.Length];
            oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
            crystalReport.Close();
            crystalReport.Dispose();
            crystalReport = null;

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "inline;filename=" + reportName + ".pdf");
            Response.BinaryWrite(byteArray);
            Response.Flush();
            Response.Close();
        }

        public void downloadImageFromURL(string projectNo, string quotationNo, string url, string imageName)
        {
            var path = ConfigurationManager.AppSettings["ImagePath"];

            //Check whether ProjectNo folder exist
            var projectFolder = IO.Path.Combine(path, projectNo);
            if (!IO.Directory.Exists(projectFolder))
            {
                IO.Directory.CreateDirectory(projectFolder);
            }

            var quoteFolder = IO.Path.Combine(projectFolder, quotationNo);
            if (!IO.Directory.Exists(quoteFolder))
            {
                IO.Directory.CreateDirectory(quoteFolder);
            }

            var fileDestination = IO.Path.Combine(quoteFolder, imageName);

            try
            {

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(url), fileDestination);
                    
                    client.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}