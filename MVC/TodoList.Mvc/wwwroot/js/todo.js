const baseUrl = '/Todo';
// Add Todo
$('#addTodoButton').click(function () {
    const title = $('#newTodoTitle').val();
    const status = "Active";

    // Check if the title is empty
    if (title === "") {
        // Display SweetAlert toast error
        Swal.fire({
            toast: true,               // Enable toast mode
            position: 'top-end',       // Position at the top-right corner
            icon: 'error',             // Use error icon
            title: 'Please enter a todo task!',
            showConfirmButton: false,  // Hide the confirm button
            timer: 3000,               // The toast will be visible for 3 seconds
            timerProgressBar: true     // Show progress bar
        });
        return; // Don't make the AJAX request if the title is empty
    }
    debugger;
    $.ajax({
        url: `${baseUrl}/AddTodo`,
        method: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ title: title, status: status }),
        success: function (newTodo) {
            debugger;
            $('#todoList').append(renderTodoItem(newTodo));
            $('#newTodoTitle').val('');
            Swal.fire({
                toast: true,               // Enable toast mode
                position: 'top-end',       // Position at the top-right corner
                icon: 'success',           // Use success icon
                title: 'Todo added successfully!',
                showConfirmButton: false,  // Hide the confirm button
                timer: 3000,               // The toast will be visible for 3 seconds
                timerProgressBar: true     // Show progress bar
            });
        }
    });
});

// Toggle Complete
$('#todoList').on('change', '.toggle-complete', function () {
    const todoId = $(this).closest('.todo-item').data('id');
    const isCompleted = $(this).is(':checked');
    const status = isCompleted ? "completed" : "active";
    $.ajax({
        url: `${baseUrl}/MarkAsComplete/${todoId}?status=${status}`,
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify({ isCompleted: isCompleted }),
        success: function () {
            $(this).siblings('.todo-title').toggleClass('completed', isCompleted);
        }
    });
});

// Delete Todo
$('#todoList').on('click', '.delete-todo', function () {
    const btn = $(this);
    const todoId = btn.closest('.todo-item').data('id');
    $.ajax({
        url: `${baseUrl}/DeleteTodo/${todoId}`,
        method: 'DELETE',
        success: function () {
            $(`.todo-item[data-id=${todoId}]`).remove();
            Swal.fire({
                toast: true,               // Enable toast mode
                position: 'top-end',       // Position at the top-right corner
                icon: 'success',           // Use success icon
                title: 'Todo deleted successfully!',
                showConfirmButton: false,  // Hide the confirm button
                timer: 3000,               // The toast will be visible for 3 seconds
                timerProgressBar: true     // Show progress bar
            });
        }
    });
});

// Clear Completed
$('#clearCompletedButton').click(function () {
    $.ajax({
        url: `${baseUrl}/ClearCompleted`,
        method: 'DELETE',
        success: function () {
            $('.todo-item .toggle-complete:checked').closest('.todo-item').remove();
            Swal.fire({
                toast: true,               // Enable toast mode
                position: 'top-end',       // Position at the top-right corner
                icon: 'success',           // Use success icon
                title: 'Completed tasks cleared!',
                showConfirmButton: false,  // Hide the confirm button
                timer: 3000,               // The toast will be visible for 3 seconds
                timerProgressBar: true     // Show progress bar
            });
        }
    });
});

// Filter Todos
$('#filterAllButton').click(function () { loadTodos("all"); });
$('#filterActiveButton').click(function () { loadTodos("active"); });
$('#filterCompletedButton').click(function () { loadTodos("completed"); });

function loadTodos(status) {
    debugger;
    $('#todoList').empty();
    const baseApiUrl = 'http://localhost:5175/api/Todos/GetAll'
    let url = `${baseApiUrl}?status=${status}`
    $.get(url, function (todos) {
        todos.forEach(function (todo) {
            $('#todoList').append(renderTodoItem(todo));
        });
    });
    return;
}

function renderTodoItem(todo) {
    return `<div class="todo-item" data-id="${todo.id}">
                    <input type="checkbox" class="toggle-complete" ${todo.status === 'Completed' ? "checked" : ""}/> 
                    <span class="todo-title ${todo.status === 'Completed' ? "completed" : ""}">${todo.title}</span>
                    <button type="button" class="delete-todo">Delete</button>
                </div>`;
}

