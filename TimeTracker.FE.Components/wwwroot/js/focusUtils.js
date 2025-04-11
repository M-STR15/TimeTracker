// wwwroot/js/focusUtils.js

export function delayBlur(component) {
    setTimeout(() => {
        component.invokeMethodAsync('HandleBlur');
    }, 200);
}

export function removeFocus(element) {
    if (element) {
        element.blur();
    }
}