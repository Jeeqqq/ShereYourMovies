<%@ Page Title="About" Language="C#" MasterPageFile="~/MasterSite/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ShereYourMovies.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Vähän meistä</h2>
    </hgroup>

    <article>
        <p>
            Share Your Movies on uusi palvelu joka laajentaa huippu suositun Your Moviesin aivan uusiin ulottuvuuksiin!
        </p>
        <p>        
            Share your movies on omanlainen sosiaalinen media elokuva friikeille, jossa käyttäjät voivat tarkastella omia, 
            ja muiden käyttäjien elokuvia. Sekä tietenkin jakaa ja tehdä arvosteluja ja jakaa muille hyviä elokuva vinkkejä.
        </p>
    </article>

    <aside>
        <h3>Aside Title</h3>
        <p>        
            Use this area to provide additional information.
        </p>
        <ul>
            <li><a runat="server" href="~/">Home</a></li>
            <li><a runat="server" href="~/About">About</a></li>
            <li><a runat="server" href="~/Contact">Contact</a></li>
        </ul>
    </aside>
</asp:Content>