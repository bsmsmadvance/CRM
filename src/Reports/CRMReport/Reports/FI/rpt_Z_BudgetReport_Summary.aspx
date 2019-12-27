<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageMaster.Master" AutoEventWireup="true" CodeBehind="rpt_Z_BudgetReport_Summary.aspx.cs" Inherits="CRMReport.Reports.FI.rpt_Z_BudgetReport_Summary1" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ImageButton ID="Img1" Height="50px" ImageUrl="~/Images/pdf.png" runat="server" onclick="PDF_Click" />
    <asp:ImageButton ID="img2" Height="50px" ImageUrl="~/Images/excel.png" runat="server" onclick="Excel_Click" />

    <CR:CrystalReportSource ID="CRS" runat="server">
        <Report FileName="rpt_Z_BudgetReport_Summary.rpt">
            </Report>
    </CR:CrystalReportSource>
    <CR:CrystalReportViewer ID="CRV" runat="server" ReportSourceID="CRS"  BorderColor="Black" EnableToolTips="False" HasDrilldownTabs="False" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" ToolPanelView="None" Height="50px" Width="350px" EnableParameterPrompt="False" />
</asp:Content>
