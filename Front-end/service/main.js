function init() {
    let mainView = document.getElementById("mainView");
    let token = localStorage.getItem('jwtToken');
    let sessionData = {
        "token":token
    };

    fetch('http://localhost:5092/api/dashboard', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(sessionData)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Invalid Session');
        }
        return response.json(); 
    })
    .then(data => {
        // Initialize an empty string to hold our HTML
        let displayHtml = '';
        // Loop through each element in the data array
        for (const element of data) {
            // Create the HTML for each card using template literals and the values from the element
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
                                <label id="labelFor${element.name}Input" for="${element.name}Input">Name: </label>
                                <input id="${element.name}Input" value="${element.name}" type="text">
                                <br>
                                <label id="labelFor${element.email}Input" for="${element.email}Input">Email:</label>
                                <input id="${element.email}Input" value="${element.email}" type="email">
                                <br>
                                <label id="labelFor${element.tel}Input" for="${element.tel}Input">Tel: </label>
                                <input id="${element.tel}Input" value="${element.tel}" type="tel">
                                <br>
                                <label id="labelFor${element.service}Input" for="${element.service}Input">Service: </label>
                                <input id="${element.service}Input" value="${element.service}" type="text">
                            </div>
                            <div class="col-2"></div>
                            <div class="col-4">
                                <label id="labelFor${element.startDate}Input" for="${element.startDate}Input">Start Date:</label>
                                <input id="${element.startDate}Input" value="${element.startDate.split('T')[0]}" type="date">
                                <br>
                                <label id="labelFor${element.finishDate}Input" for="${element.finishDate}Input">End Date:</label>
                                <input id="${element.finishDate}Input" value="${element.finishDate.split('T')[0]}" type="date">
                                <br>
                                <label id="labelFor${element.status}Input" for="${element.status}Input">Status: </label>
                                <input id="${element.status}Input" value="${element.status}" type="text">
                                <br>
                                <label id="labelFor${element.note}Input" for="${element.note}Input">Note: </label>
                                <input id="${element.note}Input" value="${element.note}" type="text">
                            </div>
                            <div class="col-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-6"></div>
                            <div class="col-6">
                                <button type="button" class="btn btn-danger">Delete</button>
                                <button type="button" class="btn btn-primary">Update</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>`;
        }
        // Set the innerHTML of the mainView to the new concatenated HTML string
        mainView.innerHTML = displayHtml;
    })
    .catch(error => {
        mainView.innerText = error.message;
    });
}

document.addEventListener("DOMContentLoaded", init);