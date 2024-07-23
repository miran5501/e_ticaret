<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminAnaSayfa.Master" AutoEventWireup="true" CodeBehind="Siparis_Detay.aspx.cs" Inherits="E_ticaret.Admin.Siparis_Detay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" class="container mt-4">
        <div class="container mt-5">
            <h2>Sipariş Detayları</h2>
            <asp:Label ID="lblSiparisVeren" runat="server" CssClass="form-label"><Strong>Siparis Veren: </Strong></asp:Label>
            <br />
            <asp:Label ID="lblSiparisTarihi" runat="server" CssClass="form-label"><Strong>Siparis Tarihi: </Strong></asp:Label>
            <br />
            <asp:Label ID="lblSiparisTeslimDurumu" runat="server" CssClass="form-label"><Strong>Siparis Durumu: </Strong></asp:Label>
            <br />
            <asp:Label ID="lblSiparisTutari" runat="server" CssClass="form-label"><Strong>Siparis Tutari: </Strong></asp:Label>
            <br />
            <asp:Label ID="lblSiparisTeslimAdresi" runat="server" CssClass="form-label"><Strong>Siparis Teslim Adresi: </Strong></asp:Label>
            <br />
            <asp:Label ID="lblSiparisNotu" runat="server" CssClass="form-label"><Strong>Siparis Notu: </Strong></asp:Label>
            <br />




            <asp:Button ID="btnTeslimEdildi" runat="server" Text="Teslim Edildi" CssClass="btn btn-success btn-sm" OnClick="BtnTeslimEdildi_Click" />
            <asp:Button ID="btnSepetOnayla" runat="server" Text="Siparişi Onayla" CssClass="btn btn-success btn-sm" OnClick="BtnSepetOnayla_Click" />

            <asp:Button ID="btnSepetIptal" runat="server" Text="Siparişi İptal Et" CssClass="btn btn-danger btn-sm" OnClick="BtnSepetIptal_Click" />

            <h2>Siparişteki Ürünler</h2>
            <br />
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Ürün Fotoğrafı</th>
                                <th scope="col">Ürün Adı</th>
                                <th scope="col">Ürün Açıklaması</th>
                                <th scope="col">Ürün Adedi</th>
                                <th scope="col">Ürün Fiyatı</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>


                    <tr>

                        <th scope="col"><%# Container.ItemIndex + 1 %></th>
                        <td>
                            <img class="card-img-top" src='<%# Eval("Urun_Resmi") %>' alt="Ürün Resmi" style="object-fit: contain; max-height: 80px;" /></td>
                        <td><%# Eval("Urun_Adi") %></td>
                        <td><%# Eval("Urun_Aciklamasi") %></td>
                        <td><%# Eval("Siparis_urun_adet") %></td>
                        <td><%# Eval("Siparis_urun_fiyat") %></td>

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
