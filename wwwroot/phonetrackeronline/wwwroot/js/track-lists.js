var phonesList = document.getElementById("phonesList");
var locsList = document.getElementById("locsList");
var popup = document.getElementById("popup");

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
    popup.style.display = "none";
}

function phonesList_add() {
    phonesList.style.display = "none";
    popup.style.display = "block";
}

var locsListBtn = document.getElementById("locsListBtn");
var phonesListBtn = document.getElementById("phonesListBtn");

locsListBtn.addEventListener("click", () => { locsList_click(); })
phonesListBtn.addEventListener("click", () => { phonesList_click(); })

phonesList_click();

var addBtn = document.getElementById("addBtn");
addBtn.addEventListener("click", () => { phonesList_add(); });