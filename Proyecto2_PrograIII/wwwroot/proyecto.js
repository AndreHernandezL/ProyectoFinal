window.configurarTabs = function (dotNetRef) {
    const tabs = document.querySelectorAll('button[data-bs-toggle="tab"]');
    tabs.forEach(tab => {
        tab.addEventListener('shown.bs.tab', event => {
            const id = event.target.id;

            // Limpiar el div al cambiar de pestaña
            const container = document.getElementById("graphContainer");
            if (container) {
                container.innerHTML = ""; // Esto limpia el contenido
            }

            dotNetRef.invokeMethodAsync('TabSeleccionado', id);
        });
    });
};