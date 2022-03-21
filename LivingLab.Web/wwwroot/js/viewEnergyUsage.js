$(document).ready(function() {
    showLineChart();
})

function showLineChart() {
    const ctx = $("#lineChart");
    const lineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [1, 2, 3, 4, 5],
            datasets: [{
                label: 'My First Dataset',
                data: [65, 59, 80, 81, 56, 55, 40],
                fill: false,
                borderColor: 'rgb(75, 192, 192)',
                tension: 0.1
            }]
        }
    })
}