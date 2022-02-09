var currentId = 0; // To determine which phone is currently chosen
let selectedPhoneElement = null;
let selectedLocElement = null;

var phonesList = document.getElementById("phonesList");
var locsList = document.getElementById("locsList-0");

function locsList_click() {
    document.getElementById("locsList-" + currentId).style.display = "block";
    phonesList.style.display = "none";
    locsListBtn.style.display = "none";
    phonesListBtn.style.display = "block";
}

function phonesList_click() {
    if (selectedLocElement != null) {
        selectedLocElement.style.backgroundColor = null;
        selectedLocElement = null;
    }

    document.getElementById("locsList-" + currentId).style.display = "none";
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
        }
    }
}