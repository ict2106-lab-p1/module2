
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
            $.ajax({
                url: "/EnergyUsageComparison/GetGraph",
                type: "POST",
                data: { "type": JSON.stringify(type), "compareFactor": JSON.stringify(compareFactor) },
                success: function (response) {
                    var aData = response;
                    var aLabels = aData[0];
                    var aDatasets1 = aData[1];
                    var aDatasets2 = aData[2];

                    //var benchmark = aData[3][0];

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


                    var options = {
                        legend: {
                            display: true,
                        },
                        tooltips: {
                            enabled: false,
                        },
                        scales: {
                            xAxes: [{
                                display: true,
                                ticks: {
                                    beginAtZero: true
                                },
                            }],
                            yAxes: [{
                                display: true,
                                ticks: {
                                    beginAtZero: true
                                },
                            }]
                        },
                        annotation: {
                            annotations: [{
                                type: 'line',
                                mode: 'horizontal',
                                scaleID: 'y-axis-0',
                                value: '26',
                                borderColor: 'tomato',
                                borderWidth: 1
                            }],
                            drawTime: "afterDraw" // (default)
                        }
                    };

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
                            annotation: {
                                annotations: [{
                                    type: 'line',
                                    mode: 'horizontal',
                                    scaleID: 'y-axis-0',
                                    value: '26',
                                    borderColor: 'tomato',
                                    borderWidth: 1
                                }],
                                drawTime: "afterDraw" // (default)
                            }
                        }
                    });
                }
            });
        }
        
    }
}

