$(document).ready(async function() {
    initDatepicker();
    $("#filter").click(filter);
    const data = await getData();
    if (!data) return;
    
    showLineChart(data);
})

/**
 * Initialize the datepicker
 */
function initDatepicker() {
    const $start = $('#start');
    const $end = $('#end');
    const today = new Date();
    const firstDay = new Date(today.getFullYear(), today.getMonth(), 1);
    const oneMonthAgo = new Date(today.getFullYear(), today.getMonth() - 1, today.getDate());
    
    $.datepicker.setDefaults({
        maxDate: today,
    })
    
    $start.datepicker({
        defaultDate: firstDay,
        minDate: oneMonthAgo,
        onSelect: function(dateText) {
            $end.datepicker("option", "minDate", dateText);
        }
    })
    
    $end.datepicker({
        defaultDate: today,
        onSelect: function(dateText) {
            $start.datepicker("option", "maxDate", dateText);
        }
    });
}

function filter(e) {
    const start = $('#start').val();
    const end = $('#end').val();
    console.log(start, end);
}

/**
 * Setup the line chart with
 * benchmark and actual usage.
 * 
 * @param {Object} data
 */
function showLineChart(data) {
    const ctx = $("#lineChart");
    const lineChart = new Chart(ctx, {
        type: "line",
        data: {
            labels: getDates(),
            datasets: [{
                label: "Actual Usage",
                data: getLogs(data),
                fill: false,
                borderColor: 'rgb(75, 192, 192)',
                tension: 0.1
            }, {
                label: "Benchmark",
                data: getBenchmark(data),
                fill: false,
                borderColor: 'rgb(255, 99, 132)',
                tension: 0.1
            }]
        }
    })
}

/**
 * Get actual usage logs from data.
 * 
 * @param {Object} data
 * @returns {Array} logs
 */
function getLogs(data) {
    const logs = [];
    for (let i = 0; i < data.logs.length; i++) {
        logs.push(data.logs[i].energyUsage);
    }
    return logs;
}

/**
 * Temporary solution to set a straight line
 * for benchmark.
 * 
 * @param {Object} data
 * @returns {Array} benchmarks
 */
function getBenchmark(data) {
    const benchmark = [];
    for (let i = 0; i < getDates().length; i++) {
        benchmark.push(data.lab.energyUsageBenchmark);
    }
    return benchmark;
}

/**
 * Ajax call to get data from the server.
 */
async function getData() {
    try {
        return await $.ajax({
            url: "/EnergyUsage/ViewUsage",
            type: "GET",
        })
    } catch (error) {
        console.log(error)
    }
}


/**
 * Get all dates for current month
 */
function getDates() {
    const dates = [];
    const today = new Date();
    const month = today.getMonth();
    const year = today.getFullYear();
    for (let i = 1; i <= new Date(year, month + 1, 0).getDate(); i++) {
        const date = new Date(year, month, i).toLocaleDateString('en-US', {
            day: 'numeric',
            month: 'short'
        });
        dates.push(date);
    }
    return dates;
}