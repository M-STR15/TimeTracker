import Chart from '../lib/chart.js/chart.js/chart.umd.js';

export function setupChart(element, config) {
    console.log('setupChart element:', element);

    // Kontrola, že element je canvas
    if (element instanceof HTMLCanvasElement) {
        const ctx = element.getContext('2d');
        new Chart(ctx, config);
    } else {
        console.warn('Element není canvas nebo není dostupný.', element);
    }
}