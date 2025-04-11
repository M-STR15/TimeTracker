function delayBlur(component) {
    setTimeout(() => {
        component.invokeMethodAsync('HandleBlur');
    }, 200);
}

window.removeFocus = function (element) {
    if (element) {
        element.blur();
    }
};