window.configurarTabs = function (dotNetRef) {
    const tabs = document.querySelectorAll('button[data-bs-toggle="tab"]');
    tabs.forEach(tab => {
        tab.addEventListener('shown.bs.tab', event => {
            const id = event.target.id;

            // Limpiar el div al cambiar de pestaña
            const container = document.getElementById("graphContainer");
            const containerA = document.getElementById("graphContainerA");
            const containerB = document.getElementById("graphContainerB");
            if (container) {
                container.innerHTML = ""; // Esto limpia el contenido
            }

            if (containerA) {
                containerA.innerHTML = ""; // Esto limpia el contenido
            }

            if (containerB) {
                containerB.innerHTML = ""; // Esto limpia el contenido
            }

            dotNetRef.invokeMethodAsync('TabSeleccionado', id);
        });
    });
};