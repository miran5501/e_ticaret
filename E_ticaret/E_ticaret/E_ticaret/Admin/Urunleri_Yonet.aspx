<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminAnaSayfa.Master" AutoEventWireup="true" CodeBehind="Urunleri_Yonet.aspx.cs" Inherits="E_ticaret.Admin.Urunleri_Yonet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" class="container mt-4">


        <div class="row row-cols-1 row-cols-md-3 g-4 justify-content-center">
            <asp:DataList ID="Datalist1" runat="server" RepeatColumns="3" CssClass="col mb-4">
                <ItemTemplate>
                    <div class="card" style="width: 18rem;">
                        <img class="card-img-top" src='<%# Eval("Urun_Resmi") %>' alt="Ürün Resmi" style="object-fit: contain; max-height: 150px;" />
                        <hr>
                        <div class="card-body">
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item">
                                    <strong>
                                        <asp:Label ID="LabelUrunAdi" runat="server" Text='<%# Eval("Urun_Adi") %>'></asp:Label>
                                    </strong>
                                </li>
                                <li class="list-group-item">
                                    <strong>Fiyat:</strong>
                                    <asp:Label ID="LabelFiyat" runat="server" Text='<%# Eval("Urun_Fiyat") %>'></asp:Label>
                                </li>
                                <li class="list-group-item">
                                    <strong>Açıklama:</strong>
                                    <asp:Label ID="LabelAciklama" runat="server" Text='<%# Eval("Urun_Aciklamasi") %>'></asp:Label>
                                </li>
                                <li class="list-group-item">
                                    <strong>Stok Durumu:</strong>
                                    <%# Convert.ToBoolean(Eval("Urun_Stok_Durum")) ? "Stokta Var" : "Stokta Yok" %>
                                </li>
                                <li class="list-group-item">
                                    <asp:Button ID="btnUrunGuncelle" runat="server" Text="Ürünü Güncelle" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("UrunID") %>' OnClick="BtnUrunGuncelle_Click" />
                                    <asp:Button ID="btnUrunSil" runat="server" Text="Ürünü sil" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Eval("UrunID") %>' OnClientClick="return confirm('Bu ürünü silmek istediğinizden emin misiniz?');" OnClick="BtnUrunSil_Click"/>
                                </li>
                            </ul>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>

    </form>
</asp:Content>
