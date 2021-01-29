$(document).ready(function () {

    $("body").loading({
        stoppable: false,
        message: "Loading...",
        theme: "dark"
    });

    $.get(document.baseURI + "Home/GetRegions", {}, function (response) {
        let html = "<option value=''>Regions</option>";
        response.forEach(data =>
            html += "<option value=" + data.iso + ">" + data.name + "</option>"
        );
        $("#region").html(html);
    }).done(function () {
        cargarReporte();
    });



    $("#btnReport").click(() => {
        $("body").loading({
            stoppable: false,
            message: "Loading...",
            theme: "dark"
        });
        cargarReporte();
    }); 



    $("#btnExportXML").click(() => {
        window.location.href = document.baseURI + 'Home/GetXML?iso=' + $("#region").val();
    }); 



    $("#btnExportJSON").click(() => {
        window.location.href = document.baseURI + 'Home/GetJson?iso=' + $("#region").val();
    }); 



    $("#btnExportCSV").click(() => {
        window.location.href = document.baseURI + 'Home/GetCSV?iso=' + $("#region").val();
    }); 

});


function cargarReporte() {
    let region = $("#region").val();
    let html = "";
    $.get(document.baseURI + "Home/GetReport", { iso: region }, function (response) {
        response.forEach(data => {
            html += "<tr>";
            html += "<td>" + (region != "" ? data.region.province == "" ? data.region.name : data.region.province: data.region.name) + "</td>";
            html += "<td>" + data.confirmed.toLocaleString() + "</td>";
            html += "<td>" + data.deaths.toLocaleString() + "</td>";
            html += "</tr>";
        });
        $("#covidCasesTable tbody").html(html);
    }).done(function () {
        region != "" ? $("#covidCasesTable thead th:eq(0)").text("PROVINCE") : $("#covidCasesTable thead th:eq(0)").text("REGION");
        $(":loading").loading("stop");
    });
}