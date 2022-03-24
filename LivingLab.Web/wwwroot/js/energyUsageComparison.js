
var allArray = [];
function selectComparisonType() {

    var type = $("#compare :selected").val();
    console.log(type);

    if (type === "DeviceType") {
        for (var i = 1; i <= 4; i++) {
            $("#opt" + i).text("Select Device " + i + ":");
        }
    }
    else {
        for (var i = 1; i <= 4; i++) {
            $("#opt" + i).text("Select Lab " + i + ":");
        }
    }

    for (var i = 1; i <= 4; i++) {
        $("#option" + i).empty();
    }


    $.ajax({
        url: "/EnergyUsageComparison/GetType",
        type: "GET",
        data: { "compareType": type },
        success: function (response) {
            console.log(response);
            for (var i = 1; i <= 4; i++) {
                $("#option" + i).append($("<option></option>")
                    .attr("value", "")
                    .text("Please Select"));
            }
            for (var i = 0; i < response.length; i++) {
                for (var j = 1; j <= 4; j++) {
                    $("#option" + j).append($("<option></option>")
                        .attr("value", response[i])
                        .text(response[i]));
                }
                allArray.push(response[i]);
            }

        },
        error: function (response) {
            console.log(response);
        }
    });
}


$("#startDate, #endDate").change(function () {
    var startDate = document.getElementById("startDate").value;
    var endDate = document.getElementById("endDate").value;

    if ((Date.parse(endDate) <= Date.parse(startDate))) {
        alert("End date should be greater than Start date");
        document.getElementById("endDate").value = "";
    }
});

$(document).ready(function () {
    selectComparisonType()

})

function removeChosenType() {
    var ddlSelevted = event.target.id;
    var selected = $("#" + ddlSelevted + " :selected").val();
    var selectedArray = [];
    console.log(ddlSelevted.substring(ddlSelevted.length - 1, ddlSelevted.length));
    //if (selected != "") {
        for (var x = 1; x <= 4; x++) {
            if (x != ddlSelevted.substring(ddlSelevted.length - 1, ddlSelevted.length)) {
                if (selected != "") {
                    $("#option" + x + " option[value = '" + selected + "']").hide();
                }
            }
            var checkAllOptionSelected = $("#option" + x + " :selected").val();
            if (checkAllOptionSelected != "") {
                selectedArray.push(checkAllOptionSelected);
            }
        }
    //}
    var difference = allArray.filter(x => !selectedArray.includes(x));
    for (var j = 0; j < difference.length; j++) {
        for (var x = 1; x <= 4; x++) {
            $("#option" + x + " option[value = '" + difference[j] + "']").show();

        }
    }
}

function toggleModal() {
    document.getElementById("errormodal" + "-backdrop").classList.toggle("hidden");
    document.getElementById("errormodal").classList.toggle("hidden");
    document.getElementById("errormodal" + "-backdrop").classList.toggle("flex");
    document.getElementById("errormodal").classList.toggle("flex");
}

function compareData() {

    if ($('#datatable').is(':empty')) {
       
    } else {
        $("#tableDetails").html("");
        
        $("#graph").html("");

    }

    var startDate = $('#startDate').val();
    var endDate = $('#endDate').val();

    console.log("Start Date: " + startDate);
    console.log("End Date: " + endDate);

    var type = $("#compare :selected").val();

    var num = 0;
    const compareFactor = [];
    for (var i = 1; i <= 4; i++) {
        if ($("#option" + i + " :selected").val() === "") {
            num += 1;
        }
        else {
            compareFactor.push($("#option" + i + " :selected").val());
        }
    }
    console.log("Seeeeee: " + compareFactor);

    if (num > 2) {
        toggleModal()
    }
    else {
        if (type != "DeviceType") {
            //var graph = document.getElementById("graph").getContext("2d");

            //var myChart = new Chart(grapharea);

            //graph.hide();

            //document.getElementById("displayGraph").classList.toggle("hidden");

            //var x = document.getElementById("displayGraph");
            //x.style.display = "none";

            $('#graph').remove(); // this is my <canvas> element
            $('#displayGraph').append('<canvas id="graph"><canvas>');

            //myChart.destroy();

            $.ajax({
                url: "/EnergyUsageComparison/GetGraph",
                type: "POST",
                data: { "startDate": JSON.stringify(startDate), "endDate": JSON.stringify(endDate), "compareFactor": JSON.stringify(compareFactor) },
                success: function (response) {
                    var aData = response;
                    var aLabels = aData[0];
                    var aDatasets1 = aData[1];
                    var aDatasets2 = aData[2];

                    var benchmark = aData[3][0];

                    console.log("Data for benchmark: " + aData[3]);
                    console.log("Benchmark: " + aData[3][0]);


                    const data = {
                        labels: aLabels,
                        datasets: [
                            {
                                label: 'Energy Usage',
                                data: aDatasets1,
                                borderColor: "blue",
                                backgroundColor: "rgba(66, 134, 244, 0.1)",
                                order: 0
                            },
                            {
                                label: 'Energy Intensity',
                                data: aDatasets2,
                                borderColor: 'rgb(255, 99, 132)',
                                backgroundColor: 'transparent',
                                type: 'line',
                                order: 1
                            }
                        ]
                    };

                    var ctx = $("#graph").get(0).getContext("2d");

                    Chart.pluginService.register({
                        afterDraw: function (chart) {
                            if (typeof chart.config.options.lineAt != 'undefined') {
                                var lineAt = chart.config.options.lineAt;
                                var ctxPlugin = chart.chart.ctx;
                                var xAxe = chart.scales[chart.config.options.scales.xAxes[0].id];
                                var yAxe = chart.scales[chart.config.options.scales.yAxes[0].id];

                                if (yAxe.min != 0) return;

                                ctxPlugin.strokeStyle = "light green";
                                ctxPlugin.aLabels = "Benchmark";
                                ctxPlugin.beginPath();
                                lineAt = (lineAt - yAxe.min) * (100 / yAxe.max);
                                lineAt = (100 - lineAt) / 100 * (yAxe.height) + yAxe.top;
                                ctxPlugin.moveTo(xAxe.left, lineAt);
                                ctxPlugin.lineTo(xAxe.right, lineAt);
                                ctxPlugin.stroke();
                            }
                        }
                    });

                    var myNewChart = new Chart(ctx, {
                        type: 'bar',
                        data: data,
                        options: {
                            responsive: true,
                            title: { display: true, text: 'Energy Usage and Energy Intensity' },
                            legend: { position: 'bottom' },
                            scales: {
                                xAxes: [{ gridLines: { display: false }, display: true, scaleLabel: { display: false, labelString: '' } }],
                                yAxes: [{ gridLines: { display: false }, display: true, scaleLabel: { display: false, labelString: '' }, ticks: { stepSize: 50, beginAtZero: true } }]
                            },
                            tooltips: {
                                mode: 'index',
                                intersect: true,
                                enabled: false
                            },
                            lineAt: benchmark
                        }
                    });


                    //changes
                    //myNewChart.destroy();
                }
            });


            //TABLE
                $.ajax ({
                    url: "/EnergyUsageComparison/GetLabTable",
                    type: "POST",
                    data: { "startDate": JSON.stringify(startDate), "endDate": JSON.stringify(endDate), "compareFactor": JSON.stringify(compareFactor) },
                    success: function (response) {

                        $('#datatable').html(BuildDetails(response));
                        console.log("Help me: ", response);
                        console.log("Neeed me: ", response[0]);

                        $(document).ready(function () {
                            $('#tableDetails').DataTable({
                                data: response,
                                columns: [
                                    { title: "Lab Location", data: "labLocation" },
                                    { title: "Energy Usage (kW)", data: "energyUsage" },
                                    { title: "Energy Usage Cost (SGD)", data: "energyUsageCost" },
                                    { title: "Average Energy Usage (kW/hr).", data: "averageEnergyUsage" },
                                    { title: "Energy Intensity (kW/SQM)", data: "energyIntensity" }
                                ],
                                lengthChange: false,
                                paging: false,
                                columnDefs: [
                                    {
                                        "targets": [0,1, 2, 3, 4], // your case first column
                                        "className": "text-center",
                                        "width": "4%"
                                    }]
                            });

                        });
                        
                    }
                })

           
            
            //end table
            
        }
        else {
            document.getElementById("graph").getContext("2d").display = "none";

            //var myChart = new Chart(grapharea);

            

            $.ajax({
                url: "/EnergyUsageComparison/GetDeviceTable",
                type: "POST",
                data: { "startDate": JSON.stringify(startDate), "endDate": JSON.stringify(endDate), "compareFactor": JSON.stringify(compareFactor) },
                success: function (response) {

                    $('#datatable').html(BuildDetails(response));

                    $(document).ready(function () {
                        $('#tableDetails').DataTable({
                            data: response,
                            columns: [
                                { title: "Device Type", data: "deviceType" },
                                { title: "Energy Usage (kW)", data: "energyUsage" },
                                { title: "Energy Usage Cost (SGD)", data: "energyUsageCost" },
                                { title: "Average Energy Usage (kW/hr).", data: "averageEnergyUsage" }
                            ],
                            lengthChange: false,
                            paging: false,
                            columnDefs: [
                                {
                                    "targets": [0, 1, 2, 3], // your case first column
                                    "className": "text-center",
                                    "width": "4%"
                                }]
                        });
                    });

                }
            })
        }
        
    }
}

function BuildDetails(dataTable) {
    var content = [];
    for (var row in dataTable) {
        for (var column in dataTable[row]) {
            content.push("<tr>")
            content.push("<td><b>")
            content.push(column)
            content.push("</td></b>")
            content.push("<td>")
            content.push(dataTable[row][column])
            content.push("</td>")
            content.push("</tr>")
        }
    }
   

    
    var top = "<table border='1' class='dvhead' id='tableDetails'>";
    var bottom = "</table>";
    return top + bottom;
}

