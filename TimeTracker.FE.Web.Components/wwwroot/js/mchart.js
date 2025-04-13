const chartStore = {};

export function setup(id, config) {
    const canvas = document.getElementById(id);
    if (!canvas) {
        console.error(`Canvas element with id '${id}' not found.`);
        return;
    }

    const ctx = canvas.getContext('2d');

    // Zničit předchozí graf, pokud existuje
    if (chartStore[id]) {
        chartStore[id].destroy();
    }

    const chart = new Chart(ctx, config);
    chartStore[id] = chart;
}