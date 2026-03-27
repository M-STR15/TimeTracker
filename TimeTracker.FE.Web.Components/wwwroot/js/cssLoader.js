export function loadCss(cssPath) {
    return new Promise((resolve, reject) => {
        // Kontrola, jestli už není načtené
        if (document.querySelector(`link[href="${cssPath}"]`)) {
            resolve();
            return;
        }

        const link = document.createElement("link");
        link.rel = "stylesheet";
        link.href = cssPath;
        link.onload = () => resolve();
        link.onerror = () => reject(new Error(`Failed to load CSS: ${cssPath}`));
        document.head.appendChild(link);
    });
}

export function loadCssBatch(cssPaths) {
    return Promise.all(cssPaths.map(p => loadCss(p)));
}
