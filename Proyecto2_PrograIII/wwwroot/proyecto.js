document.addEventListener("DOMContentLoaded", () => {
    const tabs = document.querySelectorAll('a[data-bs-toggle="tab"]');
    console.log(tabs)

    tabs.forEach(tab => {
        tab.addEventListener('shown.bs.tab', event => {
            const tabId = event.target.id; // ID del tab clickeado
            console.log("Tab activado:", tabId);

            // Puedes ejecutar una acción personalizada aquí
            if (tabId === "tab2-tab") {
                alert("Se activó Tab 2");
            }
        });
    });
});