<%@ Page Title="Contact" Language="C#" MasterPageFile="~/MasterSite/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="ShereYourMovies.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Ota meihin yhteyttä!</h2>
    </hgroup>

    <section class="contact">
        <header>
            <h3>Puhelin:</h3>
        </header>
        <p>
            <span class="label">Viikolla 9:00 - 16:00:</span>
            <span>Suuri-Johtaja-123-123</span>
        </p>
        <p>
            <span class="label">Muina aikoina:</span>
            <span>Älä ota yhteyttä me ollaan nukkumassa</span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Email:</h3>
        </header>
        <p>
            <span class="label">Support:</span>
            <span><a href="mailto:Support@example.com">Support@example.com</a></span>
        </p>
        <p>
            <span class="label">Marketing:</span>
            <span><a href="mailto:Marketing@example.com">Marketing@example.com</a></span>
        </p>
        <p>
            <span class="label">General:</span>
            <span><a href="mailto:General@example.com">General@example.com</a></span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Osoite:</h3>
        </header>
        <p>
            Piippukatu 2<br />
            Jyväskylä, 40510?
        </p>
    </section>
</asp:Content>