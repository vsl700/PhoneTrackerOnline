var currentId = 0; // To determine which phone is currently chosen
let selectedPhoneElement = null;
let selectedLocElement = null;

var phonesList = document.getElementById("phonesList");
var locsList = document.getElementById("locsList-0");
var locationMarkers;
var currentLocationMarkers;

function locsList_click() {
    let currentLocsList = document.getElementById("locsList-" + currentId);
    let buttons = currentLocsList.getElementsByClassName("loc-select");
    locationMarkers = [];
    for (i = 0; i < buttons.length; i++) {
        let index = i;

        // Placing markers...
        let tempLat = buttons[index].getElementsByClassName("lat")[0].textContent.replace(",", ".");
        tempLat = parseFloat(tempLat);
        let tempLng = buttons[index].getElementsByClassName("lng")[0].textContent.replace(",", ".");
        tempLng = parseFloat(tempLng);

        let tempImageSrc = buttons[index].getElementsByClassName("loc-icon")[0].src;
        
        CreateMarker(tempLat, tempLng, tempImageSrc);
        locationMarkers[index] = marker;
    }

    currentLocsList.style.display = "block";
    phonesList.style.display = "none";
    locsListBtn.style.display = "none";
    phonesListBtn.style.display = "block";
}

function phonesList_click() {
    if (selectedLocElement != null) {
        selectedLocElement.style.backgroundColor = null;
        selectedLocElement = null;
    }

    let currentLocsList = document.getElementById("locsList-" + currentId);
    if (locationMarkers != null) {
        let buttons = currentLocsList.getElementsByClassName("loc-select");
        for (i = 0; i < buttons.length; i++) {
            locationMarkers[i].setMap(null);
        }
    }

    currentLocsList.style.display = "none";
    phonesList.style.display = "block";
    locsListBtn.style.display = "block";
    phonesListBtn.style.display = "none";
}

var locsListBtn = document.getElementById("locsListBtn");
var phonesListBtn = document.getElementById("phonesListBtn");

locsListBtn.addEventListener("click", () => { locsList_click(); })
phonesListBtn.addEventListener("click", () => { phonesList_click(); })

phonesList_click();


var phoneButtons = document.getElementsByClassName("phone-select");
for (i = 0; i < phoneButtons.length; i++) {
    let index = i;
    phoneButtons[index].onclick = function () {
        if (selectedPhoneElement != null) {
            selectedPhoneElement.style.backgroundColor = null;
        }

        var id = phoneButtons[index].id;
        currentId = parseInt(id.substring(id.lastIndexOf("-") + 1));

        selectedPhoneElement = phoneButtons[index];
        selectedPhoneElement.style.backgroundColor = "lightgray";
    }
}

var locsLists = document.getElementsByClassName("locsList");
for (i = 0; i < locsLists.length; i++) {
    let buttons = locsLists[i].getElementsByClassName("loc-select");
    for (j = 0; j < buttons.length; j++) {
        let index = j;
        buttons[index].onclick = function () {
            if (selectedLocElement != null) {
                selectedLocElement.style.backgroundColor = null;
            }

            selectedLocElement = buttons[index];
            selectedLocElement.style.backgroundColor = "lightgray";

            let tempLat = buttons[index].getElementsByClassName("lat")[0].textContent.replace(",", ".");
            tempLat = parseFloat(tempLat);
            let tempLng = buttons[index].getElementsByClassName("lng")[0].textContent.replace(",", ".");
            tempLng = parseFloat(tempLng);

            MoveCamera(tempLat, tempLng);
        }
    }
}