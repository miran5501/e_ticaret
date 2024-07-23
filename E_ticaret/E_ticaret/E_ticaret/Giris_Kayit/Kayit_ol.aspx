<%@ Page Title="" Language="C#" MasterPageFile="~/Giris_Yapilmadan/Giris_Yapilmamis_Sayfa.Master" AutoEventWireup="true" CodeBehind="Kayit_ol.aspx.cs" Inherits="E_ticaret.Kayit_ol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper">
        <form id="form1" runat="server">
            <h1>Kayıt Ol</h1>

            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

            <div class="input-box">
                <asp:TextBox ID="txtAdSoyad" runat="server" placeholder="Adınız ve Soyadınız" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <div class="input-box">
                <asp:TextBox ID="txtKullaniciAdi" runat="server" placeholder="Kullanıcı Adı" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <div class="input-box">
                <asp:TextBox ID="txtSifre" runat="server" placeholder="Şifre" CssClass="form-control" Required="true" TextMode="Password"></asp:TextBox>
            </div>
            <div class="input-box">
                <asp:TextBox ID="txtSifreTekrar" runat="server" placeholder="Şifre Tekrar" CssClass="form-control" Required="true" TextMode="Password"></asp:TextBox>
            </div>
            <div class="input-box">
                <asp:TextBox ID="txtEmail" runat="server" placeholder="E-posta Adresiniz" CssClass="form-control" Required="true" TextMode="Email"></asp:TextBox>
            </div>
            <div class="input-box">
                <asp:TextBox ID="txtTelefon" runat="server" placeholder="Telefon Numaranız" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnKayitOl" runat="server" Text="Kayıt Ol" CssClass="btn btn-primary" OnClick="btnKayitOl_Click" />
            <div class="register-link"><p>Hesabın var mı? <a href="GirisYap.aspx">Giriş Yap</a></p></div>
        </form>
    </div>
</asp:Content>
