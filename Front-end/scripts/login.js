function init() {
    const loginForm = document.querySelector('form');
    const userLoginInput = document.getElementById('UserLogin');
    const pwdLoginInput = document.getElementById('pwdLogin');
    const loginInfoDiv = document.getElementById('loginInfo');

    loginForm.addEventListener('submit', function (event) {
        event.preventDefault(); 
        const userName = userLoginInput.value;
        const password = pwdLoginInput.value;

        if (!userName || !password) {
            loginInfoDiv.textContent = 'Benutzername und Passwort sind erforderlich!';
            return;
        }

        const loginData = {
            UserName: userName,
            Password: password
        };

        //API request
        fetch('http://localhost:5092/api/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Login fehlgeschlagen');
                }
                return response.json(); 
            })
            .then(data => {

                localStorage.setItem('userToken', data);
                window.location.href = '../service/dashboard.html';
            })
            .catch(error => {
                loginInfoDiv.textContent = error.message;
            });
    });

    document.addEventListener('DOMContentLoaded', init});