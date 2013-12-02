<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RSSFeeds.aspx.cs" Inherits="ShereYourMovies.RSSFeeds" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>RSS Feed</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <asp:Button ID="Button1" runat="server" Text="Lisää Feed" OnClick="Button1_Click"/>

        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/RSSFeeds1.xml"
            XPath="rss/item"></asp:XmlDataSource>
   
    </div>
            <asp:DataList ID="DataList1" runat="server" ForeColor="#333333" ShowFooter="False" ShowHeader="False" >
                <ItemTemplate>
                    <table>
                        <td style="width: 200px"><%#Eval("pubDate")%></td>
                        <td style="width: 500px"><%#Eval("title")%></td>
                    </table>
                </ItemTemplate>
                <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <ItemStyle BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataList>
    </form>
</body>
</html>