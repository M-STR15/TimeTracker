function delayBlur(component) {
    setTimeout(() => {
        component.invokeMethodAsync('HandleBlur');
    }, 200);
}

function removeFocus(element) {
    element.blur();
}