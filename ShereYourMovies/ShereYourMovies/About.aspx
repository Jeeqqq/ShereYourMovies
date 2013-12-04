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
        <h3>Tutustu meihin!</h3>
        <p>        
            Laitettais varmaan noihin linkkeihin lisää juttuja jos meillä riittäis kerrottavaa...
        </p>
        <ul>
            <li><a runat="server" href="~/">Historiaa (Oikeasti etusivu)</a></li>
            <li><a runat="server" href="~/About">Kokemuksia (Oikeasti tämäsivu)</a></li>
            <li><a runat="server" href="~/Contact">Ota yhteyttä (Onko meillä oikeasti contact sivu?)</a></li>
        </ul>
    </aside>
</asp:Content>