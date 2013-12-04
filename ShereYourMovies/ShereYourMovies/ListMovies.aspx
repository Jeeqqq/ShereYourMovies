<%@ Page Title="Leffat" Language="C#" MasterPageFile="~/MasterSite/Site.Master" AutoEventWireup="true" CodeBehind="ListMovies.aspx.cs" Inherits="ShereYourMovies.ListMovies" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <asp:Label runat="server" Text="Etsi muiden käyttäjien elokuvia :" ForeColor="LightGray" Font-Size="Large"></asp:Label>
    <asp:TextBox runat="server" ID="txtFindUsers"></asp:TextBox>
    <asp:Button runat="server" ID="searchUser" OnClick="searchUser_Click" Text="Hae"/>
    <asp:Label runat="server" ID="lblInfo" ForeColor="LightGray"></asp:Label>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="otsikko" runat="server" style="margin-bottom:20px;" ></h2>
<asp:DataPager ID="DataPagerMovies" runat="server" PagedControlID="ListView1" PageSize="6" >
    <Fields>
        <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
        <asp:NumericPagerField />
        <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
    </Fields>
</asp:DataPager>
    <asp:ListView ID="ListView1" runat="server" OnPagePropertiesChanging="ListView1_PagePropertiesChanging" >
        
        <LayoutTemplate>

            <div runat="server" id="ShowMovies" >
                <div runat="server" id="itemPlaceholder" />
               
          </div>
        </LayoutTemplate>
         <ItemTemplate>
             
          <div id="div1" runat="server" class="moviesList">
              
            <asp:Panel ID="myPanel" runat="server" BackColor="WhiteSmoke" BorderWidth="1" ToolTip=<%#Eval("Tooltip") %> >
                <h4><asp:Label ID="Label1" ToolTip=<%#Eval("Nimi") %> runat="server" ><%#Eval("Nimi") %></asp:Label> </h4>
                
                  <table>
                   <tr>
                        <td rowspan="10" ><asp:ImageButton runat="server" ID="Image1" BorderWidth="1" CommandArgument='<%#Eval("ElokuvaID") %>' OnCommand="openMovieInfo_Command"  CssClass="listImage" ImageUrl='<%#Eval("DbTiedot.Poster") %>' /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Nimi : </td>
                        <td><asp:Label ToolTip=<%#Eval("DbTiedot.Title") %> runat="server" ><%#Eval("DbTiedot.Title") %></asp:Label> </td>
                    </tr> 
                    <tr>
                        <td></td>
                        <td>Oma Arvosana : </td>
                        <td><%#Eval("Arvosana") %></td>
                    </tr>  
                     <tr>
                        <td></td>
                        <td>Imdb Arvosana : </td>
                        <td><%#Eval("DbTiedot.ImdbRating") %> / 10</td>
                    </tr>    
                     <tr>
                        <td></td>
                        <td>Lista : </td>
                        <td><%#Eval("Lista") %></td>
                    </tr>  
                     <tr>
                        <td></td>
                        <td>Pituus : </td>
                        <td><%#Eval("Pituus") %></td>
                    </tr>
                      <tr>
                        <td></td>
                        <td>Linkki Imdb:seen : </td>
                        <td><a href='<%#Eval("ImbdLinkki") %>' target="_blank" />Imdb</<a></td>
                    </tr>  
                   <tr>
                        <td></td>
                        <td>Julkaisuvuosi : </td>
                        <td><%#Eval("DbTiedot.Year") %></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Peukuta Elokuvaa</td>
                        <td ><asp:ImageButton ToolTip="Peukuta Elokuvaa" runat="server" BorderWidth="0" CommandArgument='<%#Eval("ElokuvaID") %>' ID="ImageButton2" BackColor="Transparent"  OnCommand="ImageButton2_Command" Width="40px"  ImageUrl='~/Images/thumbs-up-icon.png' /></td>
                    
                        
                    </tr>
                          
                                       
                </table>
              </asp:Panel>
             
          </div>
                
        </ItemTemplate>
            
    </asp:ListView>
    <asp:ListView ID="ListView2" runat="server" OnItemDataBound="ListView2_ItemDataBound"  DeleteMethod="ListView2_DeleteItem" UpdateMethod="ListView2_UpdateItem" OnItemUpdating="ListView2_ItemUpdating" OnItemCommand="ListView2_ItemCommand" OnItemEditing="ListView2_ItemEditing" DataKeyNames="ElokuvaID" SelectMethod="ListView2_GetData" >
        
        <LayoutTemplate>
            <div runat="server" id="ShowMovies" >
                <asp:LinkButton runat="server" ID="btnBack" CommandName="Back" >Takaisin</asp:LinkButton> 
                <br> </br>
           <asp:Label BackColor="IndianRed" ForeColor="White" Font-Bold="true" Font-Size="Medium" id="infoMsg" runat="server"></asp:Label>
           <div runat="server" id="itemPlaceholder" /> 
          </div>
        </LayoutTemplate>
         <ItemTemplate>
             
          <div id="div1" runat="server" class="oneMovie">
              <asp:Panel  ID="formElements"  runat="server">
                  <h2><asp:Label ID="Label1" ToolTip=<%#Eval("Nimi") %> runat="server" ><%#Eval("Nimi") %></asp:Label> </h2>
                
                  <table class="float-left">
                      <tr>
                        <td></td>
                        <th colspan="2" style="text-align:center">Elokuvan tiedot</th>
                        </tr>
                   <tr>
                        <td rowspan="14" ><asp:Image runat="server" ID="Image1" BorderWidth="1"  ImageUrl='<%#Eval("DbTiedot.Poster") %>' /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Nimi : </td>
                        <td><asp:TextBox ID="txtNimi" runat="server" BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Bind("DbTiedot.Title") %>'  ToolTip='<%#Eval("DbTiedot.Title") %>' /></td>
                    </tr> 
                    <tr>
                        <td></td>
                        <td>Oma Arvosana : </td>
                        <td><asp:TextBox runat="server" ID="txtArvosana" BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Bind("Arvosana") %>' /></td>
                    </tr>  
                     <tr>
                        <td></td>
                        <td>Imdb Arvosana : </td>
                        <td><asp:TextBox runat="server" ID="txtArvosana2" BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Bind("DbTiedot.ImdbRating") %>' /></td>
                    </tr>    
                     <tr>
                        <td></td>
                        <td>Lista : </td>
                        <td><asp:TextBox runat="server" ID="txtLista" BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Bind("Lista") %>' /></td>
                    </tr> 
                      <tr>
                        <td></td>
                        <td>Ikäraja : </td>
                        <td><asp:TextBox runat="server"  ID="txtIkäraja" BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Bind("DbTiedot.Rated") %>' /></td>
                    </tr>  
                      <tr>
                        <td></td>
                        <td>Genret : </td>
                        <td><asp:TextBox runat="server" ID="txtGenre" BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Bind("DbTiedot.Genre") %>' /></td>
                    </tr>
                      <tr>
                        <td></td>
                        <td>Ohjaaja(t) : </td>
                        <td><asp:TextBox runat="server" ID="txtOhjaaja" BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Bind("DbTiedot.Director") %>' /></td>
                    </tr>
                      <tr>
                        <td></td>
                        <td>Käsikirjoittaja(t) : </td>
                        <td><asp:TextBox runat="server" ID="txtKasikirjoittaja" BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Bind("DbTiedot.Writer") %>' /></td>
                    </tr>
                      <tr>
                        <td></td>
                        <td>Näyttelijät : </td>
                        <td><asp:TextBox runat="server" ID="txtNayttelija" Wrap="true" BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Bind("DbTiedot.Actors") %>' /></td>
                    </tr>
                       <tr>
                        <td></td>
                        <td>Tyyppi : </td>
                        <td><asp:TextBox runat="server" ID="txtTyyppii" BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Bind("DbTiedot.Type") %>' /></td>
                    </tr> 
                      <tr>
                        <td></td>
                        <td>Julkaisuvuosi : </td>
                        <td><asp:TextBox runat="server" ID="txtYear" BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Bind("DbTiedot.Year") %>'/></td>
                    </tr>        
                      <tr>
                        <td></td>
                        <td>Linkki Imdb:seen : </td>
                        <td><a href='<%#Eval("ImbdLinkki") %>' target="_blank" />Imdb</<a></td>
                    </tr>  
                                        
                </table>
                
                
                  <table class="float-left">
                      <tr>
                        <th colspan="2">Tiedoston tiedot</th>
                        </tr>
                    <tr>
                       
                        <td>Pituus : </td>
                        <td><asp:TextBox runat="server"  BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Eval("Pituus") %>' /></td>
                    </tr> 
                    <tr>
                        
                        <td>Video : </td>
                        <td><asp:TextBox runat="server"  BorderWidth="0" BackColor="Transparent" Enabled="false" Text='<%#Eval("GetVideoInfo") %>' /></td>
                    </tr>  
                     <tr>
                        
                        <td>Ääni : </td>
                        <td><asp:TextBox runat="server"  BorderWidth="0" BackColor="Transparent"  Enabled="false" Text='<%#Eval("SoundEncoding") %>' /></td>
                    </tr>    
                     <tr>
                        
                        <td>Katsottu : </td>
                        <td><asp:CheckBox runat="server" Checked='<%#Eval("Watched") %>' Enabled="False" /> </td>
                    </tr
                      <tr>
                        
                        <td>Juoni : </td>
                        <td></td>
                    </tr> 
                      <tr>
                        <td colspan="2" rowspan="10" style="width:300px"><%#Eval("DbTiedot.Plot") %> </td>
                    </tr>                     
                </table> 
              
             </asp:Panel> 
               <div class="clear-fix"></div>
              <asp:Panel ID="toolbarpanel" runat="server">
              <div id="ToolBar" class="float-left" runat="server">
                  <asp:LinkButton runat="server" ID="btnEdit" CommandName="Edit">Muokkaa</asp:LinkButton>
                  <asp:LinkButton runat="server" ID="btnEtsi" CommandName="Etsi" >Etsi</asp:LinkButton>
                  <asp:LinkButton runat="server" ID="btnDelete" CommandName="Delete" >Poista</asp:LinkButton>
              </div> 
              </asp:Panel>     
          </div>
                
        </ItemTemplate>
         <EditItemTemplate>
                  
          <div id="div1" runat="server" class="oneMovie">
              <asp:Panel  ID="formElements"  runat="server">
                  <h2><asp:Label ID="Label1" ToolTip=<%#Eval("Nimi") %> runat="server" ><%#Eval("Nimi") %></asp:Label> </h2>
                
                  <table class="float-left">
                      <tr>
                        <td></td>
                        <th colspan="2" style="text-align:center">Elokuvan tiedot</th>
                        </tr>
                   <tr>
                        <td rowspan="14" ><asp:Image runat="server" ID="Image1" BorderWidth="1"  ImageUrl='<%#Eval("DbTiedot.Poster") %>' /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Nimi : </td>
                        <td><asp:TextBox ID="txtNimi" runat="server" BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("DbTiedot.Title") %>'  ToolTip='<%#Eval("DbTiedot.Title") %>' /></td>
                    </tr> 
                    <tr>
                        <td></td>
                        <td>Oma Arvosana : </td>
                        <td><asp:TextBox runat="server" ID="txtArvosana" BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("Tahdet") %>' /></td>
                    </tr>  
                     <tr>
                        <td></td>
                        <td>Imdb Arvosana : </td>
                        <td><asp:TextBox runat="server" ID="txtArvosana2" BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("DbTiedot.ImdbRating") %>' /></td>
                    </tr>    
                     <tr>
                        <td></td>
                        <td>Lista : </td>
                        <td><asp:TextBox runat="server" ID="txtLista" BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("Lista") %>' /></td>
                    </tr> 
                      <tr>
                        <td></td>
                        <td>Ikäraja : </td>
                        <td><asp:TextBox runat="server"  ID="txtIkäraja" BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("DbTiedot.Rated") %>' /></td>
                    </tr>  
                      <tr>
                        <td></td>
                        <td>Genret : </td>
                        <td><asp:TextBox runat="server" ID="txtGenre" BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("DbTiedot.Genre") %>' /></td>
                    </tr>
                      <tr>
                        <td></td>
                        <td>Ohjaaja(t) : </td>
                        <td><asp:TextBox runat="server" ID="txtOhjaaja" BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("DbTiedot.Director") %>' /></td>
                    </tr>
                      <tr>
                        <td></td>
                        <td>Käsikirjoittaja(t) : </td>
                        <td><asp:TextBox runat="server" ID="txtKasikirjoittaja" BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("DbTiedot.Writer") %>' /></td>
                    </tr>
                      <tr>
                        <td></td>
                        <td>Näyttelijät : </td>
                        <td><asp:TextBox runat="server" ID="txtNayttelija" Wrap="true" BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("DbTiedot.Actors") %>' /></td>
                    </tr>
                       <tr>
                        <td></td>
                        <td>Tyyppi : </td>
                        <td><asp:TextBox runat="server" ID="txtTyyppii" BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("DbTiedot.Type") %>' /></td>
                    </tr> 
                      <tr>
                        <td></td>
                        <td>Julkaisuvuosi : </td>
                        <td><asp:TextBox runat="server" ID="txtYear" BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("DbTiedot.Year") %>'/></td>
                    </tr>        
                      <tr>
                        <td></td>
                        <td>Linkki Imdb:seen : </td>
                        <td><a href='<%#Eval("ImbdLinkki") %>' target="_blank" />Imdb</<a></td>
                    </tr>  
                                        
                </table>
                
                
                  <table class="float-left">
                      <tr>
                        <th colspan="2">Tiedoston tiedot</th>
                        </tr>
                    <tr>
                       
                        <td>Pituus : </td>
                        <td><asp:TextBox ID="TextBox1" runat="server"  BorderWidth="0" BackColor="WhiteSmoke" Enabled="false" Text='<%#Eval("Pituus") %>' /></td>
                    </tr> 
                    <tr>
                        
                        <td>Video : </td>
                        <td><asp:TextBox ID="TextBox2" runat="server"  BorderWidth="0" BackColor="WhiteSmoke" Enabled="false" Text='<%#Eval("GetVideoInfo") %>' /></td>
                    </tr>  
                     <tr>
                        
                        <td>Ääni : </td>
                        <td><asp:TextBox ID="TextBox3" runat="server"  BorderWidth="0" BackColor="WhiteSmoke"  Enabled="false" Text='<%#Eval("SoundEncoding") %>' /></td>
                    </tr>    
                     <tr>
                        
                        <td>Katsottu : </td>
                        <td><asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Bind("Watched") %>'  /> </td>
                    </tr>
                      <tr>
                        
                        <td>Juoni : </td>
                        <td></td>
                    </tr> 
                      <tr>
                        <td colspan="2" rowspan="10" style="width:300px"><asp:TextBox ID="TextBox4" Rows="10" runat="server"  BorderWidth="0" BackColor="WhiteSmoke"  Text='<%#Bind("DbTiedot.Plot") %>' Height="200px" Width="400px" TextMode="MultiLine" /> </td>
                    </tr>                      
                </table> 
              
             </asp:Panel>   
                 <div class="clear-fix"></div>
              <asp:Panel ID="toolbarpanel" runat="server">
              <div id="ToolBar" class="float-left" runat="server">
                  <asp:LinkButton runat="server" ID="btnCancel" CommandName="Cancel">Peruuta</asp:LinkButton>
                  <asp:LinkButton runat="server" ID="btnEtsi"  CommandName="Etsi" >Etsi</asp:LinkButton>
                  <asp:LinkButton runat="server" ID="btnTallenna" CommandName="Update" >Tallenna</asp:LinkButton>
            

              </div>
                   </asp:Panel>
          </div>
         </EditItemTemplate>   
    </asp:ListView>
    <br />
    <br />
    <br />
    
    <asp:ListView ID="ListView3" runat="server" O OnSelectedIndexChanged="ListView3_SelectedIndexChanged" OnSelectedIndexChanging="ListView3_SelectedIndexChanging" >
        
        <LayoutTemplate>

            <asp:Panel ID="searchPanel" runat="server" BackColor="SteelBlue" >
                <div style="padding:10px;">
                <asp:TextBox ID="Search" runat="server"  />
                <asp:Button ID="btnSearch" runat="server" OnCommand="btnSearch_Command" Text="Hae nimen mukaan" />
                <asp:Label ID="searchInfo" ForeColor="Wheat" runat="server" Text="" />  <br />
                <div runat="server" id="itemPlaceholder" />
                </div>
               </asp:Panel>
          
        </LayoutTemplate>
         <ItemTemplate>
             
          <div id="div1" runat="server" class="moviesList" style="margin-top:10px;width:300px;">
              
            <asp:Panel ID="myPanel" runat="server" BackColor="WhiteSmoke" BorderWidth="1"  >
                <h4 style="width:280px;"><asp:Label ID="Label1" ToolTip=<%#Eval("Title") %> runat="server" ><%#Eval("Title") %></asp:Label> </h4>
                
                  <table style="padding:10px;text-align:center;width:300px;">
                    <tr>
                        
                        <td>Julkaisuvuosi : </td>
                        <td><%#Eval("Year") %></td>
                    </tr>  
                     <tr>
                        
                        <td>Tyyppi : </td>
                        <td><%#Eval("Type") %></td>
                    </tr>     
                     <tr>
                        <td colspan="2"><asp:Button ID="select" runat="server" CommandName="Select" OnCommand="btnSelect_Command" text="Valitse"/></td>
                    </tr>    
                                       
                </table>
              </asp:Panel>
             
          </div>
                
        </ItemTemplate>
            
    </asp:ListView>
</asp:Content>
