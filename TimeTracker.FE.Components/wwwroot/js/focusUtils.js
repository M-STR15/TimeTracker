// wwwroot/js/focusUtils.js

export function delayBlur(element) {
    setTimeout(() => {
        if (element && typeof element.invokeMethodAsync === 'function') {
            element.invokeMethodAsync('HandleBlur');
        }
    }, 200);
}

export function removeFocus(element) {
    //console.log('removeFocus element:', element);
    if (element && typeof element.blur === 'function'){
        //console.warn('Element má .blur(), typ:', typeof element);
        element.blur();
    }
    //else{
    //    console.warn('Element nemá .blur(), typ:', typeof element);
    //}
}