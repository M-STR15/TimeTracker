const chartStore = new WeakMap();

/**
 * @param {HTMLCanvasElement} canvas - DOM prvek z Blazoru (ElementReference)
 * @param {object} config - konfigurace grafu
 * @param {DotNetObjectReference<MChart>} dotNetRef - reference na MChart
 */
export function setup(canvas, config, dotNetRef) {
    console.log("canvas =", canvas);
    console.log("typeof canvas.getContext =", typeof canvas.getContext);


    if (!canvas || typeof canvas.getContext !== "function") {
        console.error("Provided element is not a canvas!");
        return;
    }

    const ctx = canvas.getContext("2d");

    // Zničit předchozí graf, pokud existuje
    if (chartStore.has(canvas)) {
        chartStore.get(canvas).destroy();
    }

    const chart = new Chart(ctx, config);
    chartStore.set(canvas, chart);

    // zavolání metody OnChartReady v Blazoru
    if (dotNetRef) {
        dotNetRef.invokeMethodAsync("OnChartReady");
    }
}