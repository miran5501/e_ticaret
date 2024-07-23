<%@ Page Title="" Language="C#" MasterPageFile="~/Musteri/MusteriAnaSayfa.Master" AutoEventWireup="true" CodeBehind="Urunler.aspx.cs" Inherits="E_ticaret.Urunler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" class="container mt-4">

        <div class="row">
            <div class="col-12 text-end">
                <asp:Button ID="BtnSepeteEkle" runat="server" Text="Sepeti Güncelle" OnClick="BtnSepeteEkle_Click" CssClass="btn btn-primary" />
            </div>
        </div>
        <div class="sol-div">
            <!-- Sol div içeriği burada yer alacak -->
            <p>Sol Div İçeriği</p>
        </div>
        <div class="Urunler-listesi">
            <asp:DataList ID="Datalist1" runat="server" RepeatColumns="4" ItemStyle-CssClass="product">
                <ItemTemplate>
                    <div class="resim" style='background-image: url("<%# Eval("Urun_Resmi") %>");'></div>
                    <div class="bilgiler">
                        <p class="name"><%# Eval("Urun_Adi") %></p>
                        <p class="small-text"><%# Eval("Urun_Aciklamasi") %></p>
                        <div class="prices">
                            <span class="new-price">₺<%# string.Format("{0:F2}", Eval("Urun_Fiyat")) %></span>
                        </div>
                    </div>
                    <div class="sepete-ekle">
                        <asp:CheckBox ID="CheckBoxAddToCart" runat="server" Text="Sepete Ekle" />
                        <asp:HiddenField ID="HiddenFieldUrunID" runat="server" Value='<%# Eval("UrunID") %>' />
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </form>
</asp:Content>
