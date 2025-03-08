function delayBlur(component) {
    setTimeout(() => {
        component.invokeMethodAsync('HandleBlur');
    }, 200);
}