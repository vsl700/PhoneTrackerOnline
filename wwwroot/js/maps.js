var map;
var currentLat, currentLng;

function MoveCamera(lat, lng) {
    var latlng = new google.maps.LatLng(lat, lng);
    map.panTo(latlng);
}

let marker;
function CreateMarker(lat, lng, iconUrl) {
    var latlng = new google.maps.LatLng(lat, lng);
    marker = new google.maps.Marker({
        position: latlng,
        icon: iconUrl,
        map: map
    });
}

function SetCurrentLocation(position) {
    let lat = position.coords.latitude;
    let lng = position.coords.longitude;

    MoveCamera(lat, lng);

    currentLat = lat;
    currentLng = lng;
}

function InitializeMap() {
    var latlng = new google.maps.LatLng(-34.397, 150.644);
    var myOptions = {
        zoom: 16,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map"), myOptions);

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(SetCurrentLocation);
    }
}
window.onload = InitializeMap;
document.getElementById("currentLocBtn").onclick = function () {
    if (currentLat == null) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(SetCurrentLocation);
        }
    } else MoveCamera(currentLat, currentLng);
}