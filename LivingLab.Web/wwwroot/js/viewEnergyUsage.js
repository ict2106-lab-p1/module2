$(document).ready(async function() {
    const data = await getData();
    if (!data) return;
    
    console.log(data);
    showLineChart(data);
})

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
        dates.push(i);
    }
    return dates;
}