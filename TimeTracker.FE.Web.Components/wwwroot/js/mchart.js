export function setup(id, config) {
    const ctx = document.getElementById(id).getContext('2d');
    new window.Chart(ctx, config); // Používáš Chart z CDN
}