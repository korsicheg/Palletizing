$(document).ready(function () {
    // Pallet variables-----------------------------------------------
    var PalletLength = parseInt($('#PalletLength').val());
    var PalletWidth = parseInt($('#PalletWidth').val());
    var HorizontalPalletLength = parseInt($('#RotHPL').text());
    var VerticalPalletLength = parseInt($('#RotVPL').text());
    // ---------------------------------------------------------------
    // Carton variables-----------------------------------------------
    var CartonLength = parseInt($('#CartonLength').val());
    var CartonWidth = parseInt($('#CartonWidth').val());
    var CartonHeight = parseInt($('#CartonHeight').val());
    var CartonWeight = parseInt($('#CartonWeight').val());
    // ---------------------------------------------------------------
    // Rotation variables---------------------------------------------
    var RotationTotal = parseInt($('#RotTotal').text());
    var RotationVerticalColumns = parseInt($('#RotVC').text());
    var RotationVerticalRows = parseInt($('#RotVR').text());
    var RotationHorizontalColumns = parseInt($('#RotHC').text());
    var RotationHorizontalRows = parseInt($('#RotHR').text());
    // Non-rotation variables-----------------------------------------
    var NoRotationTotalH = parseInt($('#NoRotTotalH').text());
    var NoRotationTotalV = parseInt($('#NoRotTotalV').text());
    var NoRotationVerticalColumns = parseInt($('#NoRotVC').text());
    var NoRotationVerticalRows = parseInt($('#NoRotVR').text());
    var NoRotationHorizontalColumns = parseInt($('#NoRotHC').text());
    var NoRotationHorizontalRows = parseInt($('#NoRotHR').text());
    // ---------------------------------------------------------------

    // No Rotation Canvas Drawing-------------------------------------
    var c = document.getElementById("NoRotationLayer");
    c.width = PalletLength + 1;
    c.height = PalletWidth + 1;
    var ctx = c.getContext("2d");
    let MText = "<h2 class='text-center results-title'>Your Results are Ready!</h2>";
    MText += "<h3 class='text-center'>Without Rotation</h3>";
    MText += "<h4 class='text-center'>Top View</h4>";
    if (isNaN(NoRotationTotalV)) {
        NoRotationTotalV = 0;
    }
    if (isNaN(NoRotationTotalH)) {
        NoRotationTotalH = 0;
    }
    if (NoRotationTotalV == 0 && NoRotationTotalH == 0) {
    }
    else {
        if (NoRotationTotalH >= NoRotationTotalV) {
            for (let columns = 0; columns < NoRotationHorizontalColumns; columns++) {
                for (let rows = 0; rows < NoRotationHorizontalRows; rows++) {
                    ctx.fillStyle = "#000000";
                    ctx.fillRect(columns * CartonLength, rows * CartonWidth, CartonLength, CartonWidth);
                    ctx.fillStyle = "#0000FF";
                    ctx.fillRect(columns * CartonLength + 1, rows * CartonWidth + 1, CartonLength, CartonWidth);
                }
            }
            MText += "<p>Total Boxes :" + NoRotationTotalH + "</p>";
            MText += "<p>Columns :" + NoRotationHorizontalColumns + "</p>";
            MText += "<p>Rows :" + NoRotationHorizontalRows + "</p>";
            $("#canvasNoRotation").append(MText.toString());
        }
        else {
            for (let columns = 0; columns < NoRotationVerticalColumns; columns++) {
                for (let rows = 0; rows < NoRotationVerticalRows; rows++) {
                    ctx.fillStyle = "#000000";
                    ctx.fillRect(columns * CartonWidth, rows * CartonLength, CartonWidth, CartonLength);
                    ctx.fillStyle = "#0000FF";
                    ctx.fillRect(columns * CartonWidth + 1, rows * CartonLength + 1, CartonWidth, CartonLength);
                }
            }
            MText += "<p>Total Boxes :" + NoRotationTotalV + "</p>";
            MText += "<p>Columns :" + NoRotationVerticalColumns + "</p>";
            MText += "<p>Rows :" + NoRotationVerticalRows + "</p>";
            $("#canvasNoRotation").append(MText.toString());
        }
    }

    // Rotation Canvas Drawing-------------------------------------
    var c = document.getElementById("RotationLayer");
    c.width = PalletLength;
    c.height = PalletWidth;
    var ctx = c.getContext("2d");
    let RText = "<h3 class='text-center'>With Rotation</h3>";
    RText += "<h4 class='text-center'>Top View</h4>";
    console.log(HorizontalPalletLength);
    for (let columns = 0; columns < RotationHorizontalColumns; columns++) {
        for (let rows = 0; rows < RotationHorizontalRows; rows++) {
            ctx.fillStyle = "#000000";
            ctx.fillRect(columns * CartonLength, rows * CartonWidth, CartonLength, CartonWidth);
            ctx.fillStyle = "#0000FF";
            ctx.fillRect(columns * CartonLength + 1, rows * CartonWidth + 1, CartonLength, CartonWidth);
        }
    }
    for (let columns = 0; columns < RotationVerticalColumns; columns++) {
        for (let rows = 0; rows < RotationVerticalRows; rows++) {
            ctx.fillStyle = "#000000";
            ctx.fillRect(HorizontalPalletLength + (columns * CartonWidth), rows * CartonLength, CartonWidth, CartonLength);
            ctx.fillStyle = "#0000FF";
            ctx.fillRect(HorizontalPalletLength + (columns * CartonWidth + 1), rows * CartonLength + 1, CartonWidth, CartonLength);
        }
    }
    RText += "<p>Total Boxes :" + RotationTotal + "</p>";
    RText += "<p>Horizontal Columns :" + RotationHorizontalColumns + "</p>";
    RText += "<p>Horizontal Rows :" + RotationHorizontalRows + "</p>";
    RText += "<p>Vertical Columns :" + RotationVerticalColumns + "</p>";
    RText += "<p>Vertical Rows :" + RotationVerticalRows + "</p>";

    if (isNaN(RotationTotal)) {

    }
    else {
        $("#canvasWithRotation").append(RText.toString());
    }

    var c = document.getElementById("RotationLayerTwo");
    c.width = PalletLength;
    c.height = PalletWidth;
    var ctx = c.getContext("2d");
    var RMText = "<h4 class='text-center'>Second Layer</h4>";
    console.log(HorizontalPalletLength);
    for (let columns = 0; columns < RotationVerticalColumns; columns++) {
        for (let rows = 0; rows < RotationVerticalRows; rows++) {
            ctx.fillStyle = "#000000";
            ctx.fillRect(columns * CartonWidth, rows * CartonLength, CartonWidth, CartonLength);
            ctx.fillStyle = "#0000FF";
            ctx.fillRect(columns * CartonWidth + 1, rows * CartonLength + 1, CartonWidth, CartonLength);
        }
    }
    for (let columns = 0; columns < RotationHorizontalColumns; columns++) {
        for (let rows = 0; rows < RotationHorizontalRows; rows++) {
            ctx.fillStyle = "#000000";
            ctx.fillRect(VerticalPalletLength + (columns * CartonLength), rows * CartonWidth, CartonLength, CartonWidth);
            ctx.fillStyle = "#0000FF";
            ctx.fillRect(VerticalPalletLength + (columns * CartonLength + 1), rows * CartonWidth + 1, CartonLength, CartonWidth);
        }
    }

    if (isNaN(RotationTotal)) {

    }
    else {
        $("#canvasWithRotationSecondLayer").append(RMText.toString());
    }
    
});