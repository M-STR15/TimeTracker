import * as Chart from '/_content/TimeTracker.FE.Components/lib/chart.js/chart.umd.js';
// Testovací log pro kontrolu, jestli je Chart dostupný
console.log('Chart:', Chart);

export function setupChart(element, config) {
    console.log('setupChart element:', element);

    // Kontrola, že element je canvas
    if (element instanceof HTMLCanvasElement) {
        const ctx = element.getContext('2d');
        if (typeof Chart !== 'undefined') {
            new Chart(ctx, config);
        } else {
            console.error('Chart.js není dostupný.');
        }
    } else {
        console.warn('Element není canvas nebo není dostupný.', element);
    }
}