<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminAnaSayfa.Master" AutoEventWireup="true" CodeBehind="Urun_Ekle.aspx.cs" Inherits="E_ticaret.Urun_Ekle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div>
            <h2>Ürün ekle</h2>
            <hr />
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            <br />
            <h5>Ürün ismi</h5>
            <asp:TextBox ID="txtUrunismi" runat="server" placeholder="" CssClass="form-control" Required="true"></asp:TextBox>
            <br />
            <h5>Ürün fiyatı</h5>
            <asp:TextBox ID="txtUrunfiyat" runat="server" placeholder="" CssClass="form-control" Required="true"></asp:TextBox>
            <br />
            <h5>Ürün resim linki</h5>
            <asp:TextBox ID="txtUrunresmi" runat="server" placeholder="" CssClass="form-control" Required="true"></asp:TextBox>
            <br />
            <h5>Ürün kategori</h5>
            <asp:DropDownList ID="ddlUrunKategori" runat="server" CssClass="form-control" Required="true">
                <asp:ListItem Value="" Text="Seçiniz" />
            </asp:DropDownList>
            <br />
            <h5>Ürün stok durumu</h5>
            <asp:RadioButtonList ID="rblStokDurumu" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                <asp:ListItem Value="var" Text="Var" />
                <asp:ListItem Value="yok" Text="Yok" />
            </asp:RadioButtonList>
            <br />
            <h5>Ürün açıklaması (opsiyonel)</h5>
            <asp:TextBox ID="txtUrunaciklama" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
            <br />
            <asp:Button ID="btnUrunekle" runat="server" Text="Ürünü ekle" CssClass="btn btn-primary" OnClick="btnUrunekle_Click" />
        </div>
    </form>
</asp:Content>
