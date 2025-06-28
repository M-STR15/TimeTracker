console.log("✅ lottieLoader.js loaded");
export async function loadLottieAnimation(containerId, animationPath) {
    console.log("✅ loadLottieAnimation called", containerId, animationPath);

    const container = document.getElementById(containerId);
    if (!container) {
        console.warn("❌ Container not found:", containerId);
        return;
    }

    // Dynamicky importuj Lottie z CDN, pokud ještě není načtený
    if (!window.lottie) {
        console.log("📦 Importing Lottie from CDN...");
        await import('https://cdn.jsdelivr.net/npm/lottie-web@5.10.1/build/player/lottie_light.min.js');
    }

    if (window.lottie) {
        window.lottie.loadAnimation({
            container: container,
            renderer: 'svg',
            loop: true,
            autoplay: true,
            path: animationPath
        });
        console.log("✅ Lottie animation loaded");
    } else {
        console.error("❌ Lottie not available after import");
    }
}