var phonesList = document.getElementById("phonesList");
var locsList = document.getElementById("locsList");

function locsList_click() {
    locsList.style.display = "block";
    phonesList.style.display = "none";
    locsListBtn.style.display = "none";
    phonesListBtn.style.display = "block";
}

function phonesList_click() {
    locsList.style.display = "none";
    phonesList.style.display = "block";
    locsListBtn.style.display = "block";
    phonesListBtn.style.display = "none";
}

var locsListBtn = document.getElementById("locsListBtn");
var phonesListBtn = document.getElementById("phonesListBtn");

locsListBtn.addEventListener("click", () => { locsList_click(); })
phonesListBtn.addEventListener("click", () => { phonesList_click(); })

phonesList_click();