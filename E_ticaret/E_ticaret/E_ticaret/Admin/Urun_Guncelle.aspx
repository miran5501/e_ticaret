<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminAnaSayfa.Master" AutoEventWireup="true" CodeBehind="Urun_Guncelle.aspx.cs" Inherits="E_ticaret.Admin.Urun_Guncelle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div>
            <h2>Ürün Güncelle</h2>
            <hr />
            <br />
            <h5>Ürün ismi</h5>
            <asp:TextBox ID="txtUrunismiGuncelle" runat="server" placeholder="" CssClass="form-control" Required="true"></asp:TextBox>
            <br />
            <h5>Ürün fiyatı</h5>
            <asp:TextBox ID="txtUrunfiyatGuncelle" runat="server" placeholder="" CssClass="form-control" Required="true"></asp:TextBox>
            <br />
            <h5>Ürün resim linki</h5>
            <asp:TextBox ID="txtUrunresmiGuncelle" runat="server" placeholder="" CssClass="form-control" Required="true"></asp:TextBox>
            <br />
            <h5>Ürün kategori</h5>
            <asp:DropDownList ID="ddlUrunKategoriGuncelle" runat="server" CssClass="form-control" Required="true">
                <asp:ListItem Value="" Text="Seçiniz" />
            </asp:DropDownList>
            <br />
            <h5>Ürün stok durumu</h5>
            <asp:RadioButtonList ID="rblStokDurumuGuncelle" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                <asp:ListItem Value="1" Text="Var" />
                <asp:ListItem Value="0" Text="Yok" />
            </asp:RadioButtonList>
            <br />
            <h5>Ürün açıklaması (opsiyonel)</h5>
            <asp:TextBox ID="txtUrunaciklamaGuncelle" runat="server" placeholder="" CssClass="form-control"></asp:TextBox>
            <br />
            <asp:Button ID="btnUrunGuncelle" runat="server" Text="Ürünü Güncelle" CssClass="btn btn-primary"  OnClick="btnUrunGuncelle_Click"/>
        </div>
    </form>
</asp:Content>
