<%@ Page Title="" Language="C#" MasterPageFile="~/Giris_Yapilmadan/Giris_Yapilmamis_Sayfa.Master" AutoEventWireup="true" CodeBehind="GirisYap.aspx.cs" Inherits="E_ticaret.GirisYap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrapper">
        <form runat="server">
            <h1>Giriş Yap</h1>
            <div class="input-box">
                <asp:TextBox ID="txtKullaniciAdi_giris" runat="server" placeholder="Kullanıcı Adı" CssClass="form-control" Required="true"></asp:TextBox>
            </div>
            <div class="input-box">
                <asp:TextBox ID="txtSifre_giris" runat="server" placeholder="Şifre" CssClass="form-control" Required="true" TextMode="Password"></asp:TextBox>
            </div>
            <div class="remember-forgot">
                <%--<asp:CheckBox ID="chkBeniHatirla" runat="server" />
                <asp:Label ID="lblBeniHatirla" runat="server" Text="Beni Hatırla:"></asp:Label>--%>
                <a href="#">Şifremi Unuttum</a>
            </div>

            <asp:Button ID="btnGirisYap" runat="server" Text="Giriş Yap" CssClass="btn" OnClick="btnGirisYap_Click" />
            <div class="register-link"><p>Hesabın yok mu? <a href="Kayit_ol.aspx">Kayıt ol</a></p></div>
        </form>
    </div>
</asp:Content>
