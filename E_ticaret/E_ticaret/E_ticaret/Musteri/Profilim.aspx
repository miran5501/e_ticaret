<%@ Page Title="" Language="C#" MasterPageFile="~/Musteri/MusteriAnaSayfa.Master" AutoEventWireup="true" CodeBehind="Profilim.aspx.cs" Inherits="E_ticaret.Musteri.Profilim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="container" style="height: 90vh; display: flex; justify-content: center; align-items: center;">
            <div class="card w-75">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <h5 class="card-title">Kullanıcı Bilgilerim</h5>

                            <div class="mb-3">
                                <asp:Label ID="lblAdSoyad" runat="server" AssociatedControlID="txtAdSoyad" CssClass="form-label">Ad Soyad:</asp:Label>
                                <asp:TextBox ID="txtAdSoyad" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="form-label">E-mail:</asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblTelefon" runat="server" AssociatedControlID="txtTelefon" CssClass="form-label">Telefon Numarası:</asp:Label>
                                <asp:TextBox ID="txtTelefon" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnBilgileriGuncelle" runat="server" Text="Bilgileri Güncelle" CssClass="btn btn-primary btn-sm" OnClick="BtnBilgileriGuncelle_Click" />
                            <div class="mb-3">
                                <asp:Label ID="lblAdresBasligi" runat="server" AssociatedControlID="txtAdres" CssClass="form-label">Adres Başlığı</asp:Label>
                                <asp:TextBox ID="txtAdresBasligi" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblAdres" runat="server" AssociatedControlID="txtAdres" CssClass="form-label">Adres</asp:Label>
                                <asp:TextBox ID="txtAdres" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnYeniAdres" runat="server" Text="Yeni Adres Ekle" CssClass="btn btn-primary btn-sm" OnClick="BtnYeniAdres_Click" />
                        </div>
                        <div class="col-md-6">
                            <h5 class="card-title">Şifre Değiştirme</h5>

                            <div class="mb-3">
                                <asp:Label ID="lblEskiSifre" runat="server" AssociatedControlID="txtEskiSifre" CssClass="form-label">Eski Şifre:</asp:Label>
                                <asp:TextBox ID="txtEskiSifre" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblYeniSifre" runat="server" AssociatedControlID="txtYeniSifre" CssClass="form-label">Yeni Şifre:</asp:Label>
                                <asp:TextBox ID="txtYeniSifre" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="mb-3">
                                <asp:Label ID="lblYeniSifreTekrar" runat="server" AssociatedControlID="txtYeniSifreTekrar" CssClass="form-label">Yeni Şifre Tekrar:</asp:Label>
                                <asp:TextBox ID="txtYeniSifreTekrar" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnSifreyiDegistir" runat="server" Text="Şifreyi Değiştir" CssClass="btn btn-primary btn-sm" OnClick="BtnSifreyiDegistir_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
