var map;

function InitializeMap() {
    var latlng = new google.maps.LatLng(-34.397, 150.644);
    var myOptions = {
        zoom: 8,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById("map"), myOptions);
    map.style.height = document.getElementById('body').clientHeight + 'px';
}
window.onload = InitializeMap;