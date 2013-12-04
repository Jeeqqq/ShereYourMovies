<%@ Page Title="Download" Language="C#" MasterPageFile="~/MasterSite/Site.Master" AutoEventWireup="true" CodeBehind="Downloads.aspx.cs" Inherits="ShereYourMovies.Downloads" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <p>
            Aloittaaksesi Share Your Movies portaalin todellisen käytön, 
            täytyy sinun rekisteröinnin jälkeen ladata myös clientti 
            jolla voit lisätä elokuvia listaan ja muokata niitä, 
            sekä tietienkin ladata niitä nettiiin.
        </p>
        <asp:Button runat="server" ID="btnDownload" Text="Download" OnClick="btnDownload_Click"/>
    </div>
</asp:Content>
