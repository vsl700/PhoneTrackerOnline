var locList = false;
var phonesList = document.getElementById("phonesList");
var locsList = document.getElementById("locsList");

function list_click() {
    alert("CLICK");
    if (locList) {
        phonesList.style.display = "block";
        locsList.style.display = "none";
    } else {
        locList.style.display = "block";
        phonesList.style.display = "none";
    }

    locList = !locList;
}

phonesList.style.display = "none";