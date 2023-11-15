//vorlage liste funktion

function init() {
    let mainView = document.getElementById("mainView");
    let token = localStorage.getItem('jwtToken')
    let sessionData = {
        "token":token
    }
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
            let formattedData = JSON.stringify(data, null, 2); 
            mainView.textContent = formattedData;
        })
        .catch(error => {
            mainView.innerText = error;
        });
}

document.addEventListener("DOMContentLoaded", init);