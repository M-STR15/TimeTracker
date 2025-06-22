window.loadLottieAnimation = (containerId, animationPath) => {
    const container = document.getElementById(containerId);
    if (!container) {
        console.error(`Element with ID '${containerId}' not found.`);
        return;
    }

    lottie.loadAnimation({
        container: container, //HTML element to render the animation
        renderer: 'svg',      //Render as SVG
        loop: true,           //Loop the animation
        autoplay: true,       //Autoplay the animatoin
        path: animationPath    //Path to the animatoin JSON
    });
};