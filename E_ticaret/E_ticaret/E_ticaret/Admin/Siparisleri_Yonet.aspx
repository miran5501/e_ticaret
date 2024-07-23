<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminAnaSayfa.Master" AutoEventWireup="true" CodeBehind="Siparisleri_Yonet.aspx.cs" Inherits="E_ticaret.Siparisleri_Yonet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" class="container mt-4">
        <div class="container mt-5">
            <h2>Siparişler</h2>
            <asp:Button ID="btnOnaylanmis" runat="server" Text="Onaylanmış Siparişler" CssClass="btn btn-primary btn-sm" OnClick="BtnOnaylanmis_Click"/>
            <asp:Button ID="btnBekliyor" runat="server" Text="Bekleyen Siparişler" CssClass="btn btn-primary btn-sm" OnClick="BtnBekliyor_Click"/>
            <asp:Button ID="btnTümSiparisler" runat="server" Text="Tüm Siparişler" CssClass="btn btn-primary btn-sm" OnClick="BtnTümSiparisler_Click"/>
            <br />
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Siparişi oluşturan</th>
                                <th scope="col">Siparişi Oluşturma Tarihi</th>
                                <th scope="col">Sipariş Adresi</th>
                                <th scope="col">Sipariş Notu</th>
                                <th scope="col">Sipariş Tutarı</th>
                                <th scope="col">Sipariş Durumu</th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>


                    <tr>

                        <th scope="col"><%# Container.ItemIndex + 1 %></th>
                        <td><%# Eval("KullaniciAdSoyad") %></td>
                        <td><%# Eval("Siparis_Olusturma_Tarihi") %></td>
                        <td><%# Eval("Siparis_Teslim_Adresi") %></td>
                        <td><%# Eval("Siparis_Notu") %></td>
                        <td><%# Eval("Siparis_Tutari") %></td>
                        <td><%# Eval("Siparis_Durumu") %></td>
                        <td><asp:Button ID="btnSepetDetay" runat="server" Text="Sipariş Detayları" CssClass="btn btn-primary btn-sm" OnClick="BtnSepetDetay_Click" CommandArgument='<%# Eval("SiparislerID") %>'/></td>

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
