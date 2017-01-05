$(document).ready(function () {
    // if text input field value is not empty show the "X" button
    $("#searchBox").keyup(function () {
        $("#x").fadeIn();
        if ($.trim($("#searchBox").val()) == "") {
            $("#x").fadeOut();
        }
    });
    // on click of "X", delete input field value and hide "X"
    $("#x").click(function () {
        $("#searchBox").val("");
        $(this).hide();
    });
});


$(function () {
    //$('.button').click(function () {
    //    var planetQuery = $('.search').val();
    //    console.log("Planet: " + JSON.stringify(planetQuery));
    //    $.get("Planet/FindPlanetByName?name=" + planetQuery);
    //});
});