<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Palletizing.aspx.cs" Inherits="Palletizing.Palletizing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script
        src="https://code.jquery.com/jquery-3.6.0.js"
        integrity="sha256-H+K7U5CnXl1h5ywQfKtSj8PCmoN9aaq30gDh27Xc0jk="
        crossorigin="anonymous"></script>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
</head>
<link href="main.css?v=11" rel="stylesheet" />
<script src="main.js?v=23"></script>
<body>
    <form id="form1" runat="server">
        <img class="brand-logo m-auto" src="download.jpg" />
        <div id="inputfields" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <h1>Korvel Palletising system</h1>
                        <p>
                            Please input all necessary values for the calculation of the pallet
                        </p>
                        <asp:TextBox ID="PalletLength" CssClass="input-box" placeholder="Pallet Length" runat="server" Text="">

                        </asp:TextBox>
                        <asp:TextBox ID="PalletWidth" CssClass="input-box" placeholder="Pallet Width" runat="server" Text="">

                        </asp:TextBox>
                        <asp:TextBox ID="CartonLength" CssClass="input-box" placeholder="Carton Length" runat="server" Text="">

                        </asp:TextBox>
                        <asp:TextBox ID="CartonWidth" CssClass="input-box" placeholder="Carton Width" runat="server" Text="">

                        </asp:TextBox>
                        <asp:Button ID="calcBtn" CssClass="submit-btn btn" Text="Calculate Pallet" OnClick="calcBtn_Click" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div id="outputfields" runat="server">
            <p id="outputs" runat="server"></p>
            <div class="hidden-res">

                <p id="NoRotTotalH" runat="server">
                </p>
                <p id="NoRotTotalV" runat="server">
                </p>
                <p id="NoRotHC" runat="server">
                </p>
                <p id="NoRotHR" runat="server">
                </p>
                <p id="NoRotVC" runat="server">
                </p>
                <p id="NoRotVR" runat="server">
                </p>
                <p id="RotTotal" runat="server">
                </p>
                <p id="RotVC" runat="server">
                </p>
                <p id="RotVR" runat="server">
                </p>
                <p id="RotHC" runat="server">
                </p>
                <p id="RotHR" runat="server">
                </p>
                <p id="RotVPL" runat="server">
                </p>
                <p id="RotHPL" runat="server">
                </p>
            </div>
        </div>
        <div></div>

        <div class="container" id="canvasNoRotation">
        <canvas id="NoRotationLayer" width="0" height="0"></canvas>
        </div>
        <div class="container" id="canvasWithRotation">
        <canvas id="RotationLayer" width="0" height="0"></canvas>
        </div>
        <div class="container" id="canvasWithRotationSecondLayer">
        <canvas id="RotationLayerTwo" width="0" height="0"></canvas>
        </div>
    </form>
</body>
</html>
