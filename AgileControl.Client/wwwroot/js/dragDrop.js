// Добавляем объект dragDrop в глобальную область видимости
window.dragDrop = {
    init: function (element, dotNetHelper) {
        element.addEventListener('dragover', function (e) {
            e.preventDefault();
            element.classList.add('drag-over');
        });

        element.addEventListener('dragleave', function () {
            element.classList.remove('drag-over');
        });

        element.addEventListener('drop', function (e) {
            e.preventDefault();
            element.classList.remove('drag-over');
            const taskId = e.dataTransfer.getData('text/plain');
            dotNetHelper.invokeMethodAsync('HandleItemDropped', taskId);
        });
    },

    setDragData: function (taskId) {
        event.dataTransfer.setData('text/plain', taskId);
    }
};

// Явно объявляем функцию для Blazor
export function initializeDragDrop(element, dotNetHelper) {
    window.dragDrop = {
        init: function (element, dotNetHelper) {
            element.addEventListener('dragover', function (e) {
                e.preventDefault();
                element.classList.add('drag-over');
            });

            element.addEventListener('dragleave', function () {
                element.classList.remove('drag-over');
            });

            element.addEventListener('drop', function (e) {
                e.preventDefault();
                element.classList.remove('drag-over');
                const taskId = e.dataTransfer.getData('text/plain');
                dotNetHelper.invokeMethodAsync('HandleItemDropped', taskId);
            });
        },
    }
}

export function setDragData(taskId) {
    window.dragDrop = {
        setDragData: function (taskId) {
            event.dataTransfer.setData('text/plain', taskId);
        }
    }
}