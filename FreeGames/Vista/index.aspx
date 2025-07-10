<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="FreeGames.index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title translate="no">Free Games</title>
    <link rel="icon" href="../img/favicon.ico" type="image/x-icon" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" crossorigin="anonymous" />
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form class="bg-black" id="freegames" runat="server">
        <nav class="navbar navbar-expand-lg" style="background-color: #38b6ff;">
            <div class="container">
                <a class="navbar-brand d-flex fw-bold align-items-center justify-content-center gap-1" href="index.aspx">
                    <img src="../img/logo.png" alt="Logo" width="50" />
                    <span>Free Games</span>
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <div class="navbar-nav row d-flex justify-content-around w-100">
                        <div class="col-12 col-md-6 col-lg-auto">
                            <asp:DropDownList CssClass="form-select" ID="ddlPlatform" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPlatform_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-12 col-md-6 col-lg-auto">
                            <asp:DropDownList CssClass="form-select" ID="ddlSortBy" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-12 col-md-6 col-lg-auto">
                            <asp:DropDownList CssClass="form-select" ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </nav>

        <div class="container">
            <div class="banner pb-3">
    <img class="w-100 img-fluid" src="../img/banner.jpg" alt="Banner Free Games" />
    <h1 class="text-center text-wrap m-auto p-2 rounded-pill fs-2">Unlock Unlimited Entertainment: Discover an Array of Free PC and Browser Games!</h1>
</div>
        </div>
        <nav class="navbar navbar-expand-lg bg-black">
            <div class="container">
                <span class="navbar-brand">Search by: </span>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarFilter" aria-controls="navbarFilter" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarFilter">
                    <div class="navbar-nav row d-flex w-40">
                        <div class="col-12 col-md-6 col-lg-auto">
                            <asp:DropDownList CssClass="form-select" ID="ddlPlatformFilter" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlPlatformFilter_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-12 col-md-6 col-lg-auto">
                            <asp:DropDownList CssClass="form-select" ID="ddlSortByFilter" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlSortByFilter_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="col-12 col-md-6 col-lg-auto">
                            <asp:DropDownList CssClass="form-select" ID="ddlCategoryFilter" runat="server" AutoPostBack="false" OnSelectedIndexChanged="ddlCategoryFilter_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="d-flex p-2">
                        <asp:Button class="btn" ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" ValidationGroup="filter" style="background-color:#38b6ff; font-weight: 600;"/>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvPlatformFilter" runat="server" ControlToValidate="ddlPlatformFilter" ErrorMessage="Please select a platform" ValidationGroup="filter">*</asp:RequiredFieldValidator>
                    <asp:Label ID="lblprueba" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </nav>
        <main class="container mt-3">
            <div class="row gap-1">
                <asp:ListView ID="lvGames" runat="server" ItemPlaceholderID="itemPlaceholder" OnPagePropertiesChanging="lvGames_PagePropertiesChanging">
                    <ItemTemplate>
                        <div class="card-body col-md-12 col-lg-3 border p-3 bg-white rounded-3">
                            <h3><%# Eval("title") %></h3>
                            <img src='<%# Eval("thumbnail") %>' alt="Thumbnail" style="max-width: 100px; max-height: 100px;" />
                            <p><strong>Description:</strong><%# Eval("short_description") %></p>
                            <p><a href='<%# Eval("game_url") %>'>Game URL</a></p>
                            <p><strong>Genre:</strong> <%# Eval("genre") %></p>
                            <p><strong>Platform:</strong> <%# Eval("platform") %></p>
                            <p><strong>Release Date:</strong> <%# Eval("release_date") %></p>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </main>

        <div class="container text-center d-flex justify-content-center mt-3">
            <asp:DataPager ID="DataPagerGames" runat="server" PagedControlID="lvGames" PageSize="12"  style="font-size: 20px; width: 100%; font-weight:bold;">
                <Fields>
                    <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False" />
                </Fields>
            </asp:DataPager>
        </div>


        <footer class="fw-bold text-center py-3 mt-3" style="background-color: #38b6ff;">
            <p class="mb-0 fs-5">&copy; 2024 Tamara - Todos los derechos reservados</p>
        </footer>

    </form>
    <script src="../js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
</body>
</html>
