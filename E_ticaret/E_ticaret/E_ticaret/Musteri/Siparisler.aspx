<%@ Page Title="" Language="C#" MasterPageFile="~/Musteri/MusteriAnaSayfa.Master" AutoEventWireup="true" CodeBehind="Siparisler.aspx.cs" Inherits="E_ticaret.Musteri.Siparisler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <form id="form1" runat="server" class="container mt-4">
     <div class="container mt-5">
         <h2>Geçmiş Siparişlerim</h2>
         <br />
         <asp:Repeater ID="Repeater1" runat="server">
             <HeaderTemplate>
                 <table class="table table-striped">
                     <thead>
                         <tr>
                             <th scope="col">#</th>
                             <th scope="col">Siparişi Oluşturma Tarihi</th>
                             <th scope="col">Sipariş Tutarı</th>
                             <th scope="col">Sipariş Durumu</th>
                             <th scope="col">Teslim Adresi</th>
                         </tr>
                     </thead>
                     <tbody>
             </HeaderTemplate>
             <ItemTemplate>

                 <tr>
                     <th scope="col"><%# Container.ItemIndex + 1 %></th>
                     <td><%# Eval("Siparis_Olusturma_Tarihi") %></td>
                     <td><%# Eval("Siparis_Tutari") %></td>             
                     <td><%# Eval("Siparis_Durumu")%></td>
                     <td><%# Eval("Siparis_Teslim_Adresi") %></td> 
                 </tr>

             </ItemTemplate>
             <FooterTemplate>
                 </tbody>
     </table>
             </FooterTemplate>
         </asp:Repeater>

     </div>
 </form>
</asp:Content>
