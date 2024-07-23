<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminAnaSayfa.Master" AutoEventWireup="true" CodeBehind="Kullanicilari_Yonet.aspx.cs" Inherits="E_ticaret.Kullanicilari_Yonet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" class="container mt-4">
        <div class="container mt-5">
            <h2>Kullanıcı Listesi</h2>
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Kullanıcı Adı Soyadı</th>
                                <th scope="col">Kullanıcı Adı</th>
                                <th scope="col">E-mail</th>
                                <th scope="col">Telefon numarası</th
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <th scope="col"><%# Container.ItemIndex + 1 %></th>
                                    <td><%# Eval("KullaniciAdSoyad") %></td>
                                    <td><%# Eval("KullaniciAdi") %></td>
                                    <td><%# Eval("E_mail") %></td>
                                    <td><%# Eval("Telefon_No") %></td>
                                    <td>
                                        <asp:Button ID="btnSepetOnayla" runat="server" Text="Kullanıcıyı sil" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Eval("KullaniciID") %>' Visible='<%# Convert.ToInt32(Eval("KullaniciYetki")) != 1 %>' OnClientClick="return confirm('Bu kullanıcıyı silmek istediğinizden emin misiniz?');" OnClick="BtnKullaniciSil_Click"/>
                                    </td>
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
