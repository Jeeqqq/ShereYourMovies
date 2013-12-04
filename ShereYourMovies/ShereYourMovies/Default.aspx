<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/MasterSite/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShereYourMovies._Default" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Rss feed:</h3>

            <asp:ListView ID="DataList1" runat="server" SelectMethod="DataList1_GetData" >
                <LayoutTemplate>

           <table>
               <tr  runat="server" ID="itemPlaceholder"/>
           </table>

        </LayoutTemplate>
                 <ItemTemplate>
                    <tr id="tr1">
                        <td style="width: 200px"><%#Eval("pubDate")%></td>
                        <td style="width: 500px"><%#Eval("title")%></td>
                    </tr>
                </ItemTemplate>
                
            </asp:ListView>
</asp:Content>
