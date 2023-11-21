function init() {
    let mainView = document.getElementById("mainView");
    let token = localStorage.getItem('jwtToken');

    fetch('http://localhost:5092/api/mitarbeiter', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ "token": token })
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Invalid Session');
        }
        return response.json();
    })
    .then(data => {
        let displayHtml = '';

        for (const element of data) {
            displayHtml += `
            <div class="container mb-3">
                <div class="card"> 
                    <div class="card-body">
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-10">
                                <label>ID: ${element.id}</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-1"></div>
                            <div class="col-4">
                                <label for="nameInput${element.id}">Name: </label>
                                <input id="nameInput${element.id}" value="${element.name}" type="text">
                                <br>
                                <label for="emailInput${element.id}">Email:</label>
                                <input id="emailInput${element.id}" value="${element.email}" type="email">
                                <br>
                                <label for="telInput${element.id}">Tel: </label>
                                <input id="telInput${element.id}" value="${element.tel}" type="tel">
                                <br>
                                <label for="serviceInput${element.id}">Service: </label>
                                <input id="serviceInput${element.id}" value="${element.service}" type="text">
                                <br>
                                <label for="startDateInput${element.id}">Start Date:</label>
                                <input id="startDateInput${element.id}" value="${element.startDate.split('T')[0]}" type="date">
                                <br>
                            </div>
                            <div class="col-2"></div>
                            <div class="col-4">
                                <label for="finishDateInput${element.id}">End Date:</label>
                                <input id="finishDateInput${element.id}" value="${element.finishDate.split('T')[0]}" type="date">
                                <br>
                                <label for="priorityInput${element.id}">Priority:</label>
                                <input id="priorityInput${element.id}" value="${element.priority}" type="text">
                                <br>
                                <label for="statusInput${element.id}">Status: </label>
                                <input id="statusInput${element.id}" value="${element.status}" type="text">
                                <br>
                                <label for="noteInput${element.id}">Note: </label>
                                <input id="noteInput${element.id}" value="${element.note}" type="text">
                                <br>
                                <button type="button" class="btn btn-danger delete-btn" data-id="${element.id}">Delete</button>
                                <br>
                                <button type="button" class="btn btn-primary update-btn" data-id="${element.id}">Update</button>
                            
                            </div>
                            <div class="col-1"></div>
                        </div>
                    </div>
                </div>
            </div>`;
        }

        mainView.innerHTML = displayHtml;

        document.querySelectorAll('.update-btn').forEach(button => {
            button.addEventListener('click', event => {
                event.preventDefault(); // Prevent the default button action
                const id = button.getAttribute('data-id');
                const cardBody = button.closest('.card-body');

                const registration = {
                    id: id,
                    name: cardBody.querySelector(`#nameInput${id}`).value,
                    email: cardBody.querySelector(`#emailInput${id}`).value,
                    tel: cardBody.querySelector(`#telInput${id}`).value,
                    service: cardBody.querySelector(`#serviceInput${id}`).value,
                    startDate: cardBody.querySelector(`#startDateInput${id}`).value,
                    finishDate: cardBody.querySelector(`#finishDateInput${id}`).value,
                    priority: cardBody.querySelector(`#priorityInput${id}`).value,
                    status: cardBody.querySelector(`#statusInput${id}`).value,
                    note: cardBody.querySelector(`#noteInput${id}`).value
                };

                updateRegistration(id, registration, token);
            });
        });

        // Event listeners for the delete buttons
        document.querySelectorAll('.delete-btn').forEach(button => {
            button.addEventListener('click', function() {
                const id = this.getAttribute('data-id');
                deleteRegistration(id, token);
            });
        });
    })
    .catch(error => {
        document.getElementById('alertInfo').innerHTML = `<div class="alert alert-danger" role="alert">${error.message}</div>`;
    });
}

function updateRegistration(id, registration, token) {
    fetch(`http://localhost:5092/api/mitarbeiter/registration/${id}?token=${encodeURIComponent(token)}`, { 
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(registration)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Failed to update registration');
        }
        return response.json();
    })
    .then(data => {
        document.getElementById('alertInfo').innerHTML = '<div class="alert alert-success" role="alert">Registration updated successfully.</div>';
        // Optionally, you can update the DOM here to show the updated data
        // For example, update the text fields to reflect the new values, or update a list item if you're displaying a list
    })
    .catch(error => {
        document.getElementById('alertInfo').innerHTML = `<div class="alert alert-danger" role="alert">${error.message}</div>`;
    });
}

function deleteRegistration(id, token) {
    fetch(`http://localhost:5092/api/mitarbeiter/registration/${id}?token=${encodeURIComponent(token)}`, { 
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Failed to delete registration');
        }
        document.getElementById('alertInfo').innerHTML = '<div class="alert alert-success" role="alert">Registration deleted successfully.</div>';
        // Remove the element from the DOM
        const containerToDelete = document.querySelector(`[data-id="${id}"]`).closest('.container');
        containerToDelete.remove();
    })
    .catch(error => {
        document.getElementById('alertInfo').innerHTML = `<div class="alert alert-danger" role="alert">${error.message}</div>`;
    });
}

document.addEventListener("DOMContentLoaded", init); 