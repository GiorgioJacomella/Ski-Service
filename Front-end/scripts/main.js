
/// calculate date
function datumfunction(deliveryDate, currentDate, prioritätInput) {
  const priorität = parseInt(prioritätInput);

  if (priorität === 1) {
    deliveryDate.setDate(currentDate.getDate() + 12);
  } else if (priorität === 2) {
    deliveryDate.setDate(currentDate.getDate() + 7);
  } else if (priorität === 3) {
    deliveryDate.setDate(currentDate.getDate() + 5);
  } else {
    console.log("error");
  }
  return deliveryDate;
}


///Radio input value
function getRadioValue(name) {
  var group = document.getElementsByName(name);

  for (var i = 0; i < group.length; i++) {
    if (group[i].checked) {
      return group[i].value;
    }
  }

  return '';
}

/// email auf format prüfen
function validateEmail(email) {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
}

/// prüfen ob eingabefehler vorliegen
function hasObjects(array) {
  for (let i = 0; i < array.length; i++) {
    if (typeof array[i] === 'object' && !Array.isArray(array[i])) {
      return true;
    }
  }
  return false;
}

/// API Post Call
function sendAPI(jsonData, event, price, newdeliveryDate) {
  event.preventDefault();
  const jsonDataString = JSON.stringify(jsonData);

  const requestOptions = {
    method: 'POST',
    body: jsonDataString,
    headers: {
      'Content-Type': 'application/json;charset=UTF-8'
    }
  };

    fetch('http://localhost:5092/api/Registration', requestOptions)//correct this API URL
    .then((response) => response.json())
    .then((data) => {
        console.log('Antwort vom Server:', data);
        preview.innerHTML = `<div class="alert alert-success" role="alert">
        ${newdeliveryDate} <br>
        Kosten: ${price} CHF
        <br>
        Bestellung wurde Abgegeben
        </div>`;
    })
    .catch((error) => {
        console.error('Fehler beim Senden der Anfrage:', error);

        preview.innerHTML = `<div class="alert alert-danger" role="alert">ERROR OCCURED</div>`;

    });
}


/// Eingabe entgegennehmen und prüfen
function checkInputs() {
  const nameInput = document.getElementById('nameInput').value;
  const emailInput = document.getElementById('emailInput').value;
  const telInput = document.getElementById('telInput').value;
  const prioritätInput = document.getElementById('prioritätInput').value;
  const serviceInput = getRadioValue('inputRadio');
  const preview = document.getElementById('preview');
  const missingInputs = [];

  if (nameInput == '' || nameInput === ' ' || nameInput === '  ') {
    missingInputs.push('Name');
  }

  if (emailInput === '' || emailInput === ' ' || emailInput === '  ' || validateEmail(emailInput) === false) {
    missingInputs.push('Email');
  }

  if (telInput === '' || telInput === ' ' || telInput === '  ') {
    missingInputs.push('Telephone');
  }

  if (serviceInput === null || serviceInput === NaN || serviceInput === "") {
    missingInputs.push('Service');
  }

  console.log(missingInputs);

  if (missingInputs.length === 0) {
    let currentDate = new Date();
    let deliveryDate = new Date();
    let price = 0;
    // Pass prioritätInput to the datumfunction
    let newdeliveryDate = datumfunction(deliveryDate, currentDate, prioritätInput);

    if (serviceInput == "KleinerService") {
      price = 50;
    }
    else if (serviceInput == "GrosserService") {
      price = 120;
    }
    else if (serviceInput == "RennskiService") {
      price = 150;
    }
    else if (serviceInput == "BindungMontierenEinstellen") {
      price = 40;
    }
    else if (serviceInput == "FellZuschneiden") {
      price = 50;
    }
    else if (serviceInput == "Heisswachsen") {
      price = 50;
    }
    else {

    }


    if (parseInt(prioritätInput) == 1) {
      price = price;
    }
    else if (parseInt(prioritätInput) == 2) {
      price += 15;
    }
    else if (parseInt(prioritätInput) == 3) {
      price += 30;
    }
    else {
      console.log("error")
    }

    /// Preview des angebots laden
    preview.innerHTML = `<div class="alert alert-secondary" role="alert">
    ${newdeliveryDate} <br>
    Kosten: ${price} CHF
    </div>`;


  } 

  /// show error
  else {
    preview.innerHTML = `<div class="alert alert-danger" role="alert">
    ${missingInputs}
    </div>`;
  }
}

/// Check inputs and execute API Call
function bestellungSenden(event) {
  const nameInput = document.getElementById('nameInput').value;
  const emailInput = document.getElementById('emailInput').value;
  const telInput = document.getElementById('telInput').value;
  const prioritätInput = document.getElementById('prioritätInput').value;
  const serviceInput = getRadioValue('inputRadio');
  const preview = document.getElementById('preview');
  const missingInputs = [];
  event.preventDefault();

  if (nameInput == '' || nameInput === ' ' || nameInput === '  ') {
    missingInputs.push('Name');
  }

  if (emailInput === '' || emailInput === ' ' || emailInput === '  ' || validateEmail(emailInput) === false) {
    missingInputs.push('Email');
  }

  if (telInput === '' || telInput === ' ' || telInput === '  ') {
    missingInputs.push('Telephone');
  }

  if (serviceInput === null || serviceInput === NaN || serviceInput === "") {
    missingInputs.push('Service');
  }

  console.log(missingInputs);

  if (missingInputs.length === 0) {
    let currentDate = new Date();
    let deliveryDate = new Date();
    let price = 0;
    let prioritySubmit;
    // Pass prioritätInput to the datumfunction
    let newdeliveryDate = datumfunction(deliveryDate, currentDate, prioritätInput);

    if (serviceInput == "KleinerService") {
      price = 50;
    }
    else if (serviceInput == "GrosserService") {
      price = 120;
    }
    else if (serviceInput == "RennskiService") {
      price = 150;
    }
    else if (serviceInput == "BindungMontierenEinstellen") {
      price = 40;
    }
    else if (serviceInput == "FellZuschneiden") {
      price = 50;
    }
    else if (serviceInput == "Heisswachsen") {
      price = 50;
    }
    else {

    }

    if (parseInt(prioritätInput) == 1) {
      price = price;
      prioritySubmit = "Tief";
    }
    else if (parseInt(prioritätInput) == 2) {
      price += 15;
      prioritySubmit = "Standart";
    }
    else if (parseInt(prioritätInput) == 3) {
      price += 30;
      prioritySubmit = "Hoch";
    }
    else {
      console.log("error")
    }

    const submitObject = {
      "name": nameInput,
      "email": emailInput,
      "tel": telInput,
      "priority": prioritySubmit,
      "service": serviceInput,
      "startDate": currentDate.toISOString(),
      "finishDate": newdeliveryDate.toISOString(),
      "status": "Unfinished", 
      "note": "",
    }

    sendAPI(submitObject, event, price, newdeliveryDate);

    event.preventDefault;


  } 
  else {
    preview.innerHTML = `<div class="alert alert-danger" role="alert">
    ${missingInputs}
    </div>`;
  }
}

/// eventlistener für interaktionen
function init() {
  const anmeldenForm = document.getElementById('anmeldenForm');
  const inputFields = anmeldenForm.getElementsByTagName('input');
  const button = document.getElementById('anmeldeButton');

  for (let i = 0; i < inputFields.length; i++) {
    inputFields[i].addEventListener('input', checkInputs);
    inputFields[i].addEventListener('click', checkInputs);
    inputFields[i].addEventListener('change', checkInputs);
    inputFields[i].addEventListener('keyup', checkInputs);
  }
  button.addEventListener('click', bestellungSenden)
}

/// eventlistener für DOM
document.addEventListener('DOMContentLoaded', init);