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
            <asp:DataList ID="DataList1" runat="server" BackColor="White" BorderColor="#404040" BorderStyle="Solid" GridLines="Vertical" Width="250px">
                <ItemTemplate>
                    <%#Eval("title")%><br />
                    <%#Eval("author")%><br />
                    <%#Eval("pubDate")%><br />
                </ItemTemplate>
                <AlternatingItemStyle BackColor="CadetBlue" />
                <ItemStyle BackColor="AliceBlue" ForeColor="Black" />
            </asp:DataList>
    </form>
</body>
</html>