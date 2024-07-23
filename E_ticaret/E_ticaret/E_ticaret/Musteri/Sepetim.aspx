<%@ Page Title="" Language="C#" MasterPageFile="~/Musteri/MusteriAnaSayfa.Master" AutoEventWireup="true" CodeBehind="Sepetim.aspx.cs" Inherits="E_ticaret.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" class="container mt-4">
        <div class="container mt-5">
            <h2>Sepetim</h2>
            <br />
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Ürün</th>
                                <th scope="col">Adet</th>
                                <th scope="col">Fiyat</th>
                                <th scope="col">Sepeti İptal Et</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex + 1 %></td>
                        <td>
                            <div class="d-flex align-items-center">
                                <img src='<%# Eval("Urun_Resmi") %>' alt="Ürün Resmi" style="max-height: 50px; object-fit: contain;" />
                                <div class="ms-3">
                                    <strong><%# Eval("Urun_Adi") %></strong><br />
                                    <%# Eval("Urun_Aciklamasi") %>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="d-flex align-items-center">
                                <asp:Button ID="btnAdetAzalt" runat="server" Text="-" CssClass="btn btn-secondary btn-sm" OnClick="BtnAdetAzalt_Click" CommandArgument='<%# Eval("Sepet_UrunID") %>' />
                                <div class="ms-2 me-2"><%# Eval("Urun_adet") %></div>
                                <asp:Button ID="btnAdetArttir" runat="server" Text="+" CssClass="btn btn-secondary btn-sm" OnClick="BtnAdetArttir_Click" CommandArgument='<%# Eval("Sepet_UrunID") %>' />
                            </div>
                        </td>
                        <td>
                            <div class="d-flex align-items-center"><%# Eval("Sepet_urun_fiyat") %></div>
                        </td>
                        <td>
                            <div class="d-flex align-items-center">
                                <asp:Button ID="btnUrunuSil" runat="server" Text="Ürünü sil" CssClass="btn btn-danger btn-sm" OnClick="BtnUrunuSil_Click" CommandArgument='<%# Eval("Sepet_UrunID") %>' />
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div class="mt-4">
            <strong>Toplam Tutar:</strong>
            <asp:Label ID="LabelToplamTutar" runat="server" Text=""></asp:Label>
        </div>
        <div class="mt-4">
            <asp:Button ID="BtnSepetiTemizle" runat="server" Text="Sepeti Temizle" OnClick="BtnSepetiTemizle_Click" CssClass="btn btn-danger" />
        </div>
        <div class="mt-4">
            <strong>Teslim Adresi: zorunlu hale getir</strong>
            <asp:DropDownList ID="ddlAdres" runat="server" CssClass="form-control">
                <asp:ListItem Value="" Text="Seçiniz" />
            </asp:DropDownList>
        </div>
        <div class="mt-4">
            <strong>Sipariş Notu</strong>
            <asp:TextBox ID="TextBoxNot" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
        </div>


        <div class="mt-4">
            <asp:Button ID="BtnOnayla" runat="server" Text="Sepeti Onayla" OnClick="BtnOnayla_Click" CssClass="btn btn-success" />
        </div>
    </form>
</asp:Content>
