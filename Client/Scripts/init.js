$(document).ready(function () {
    $.cookie('X-Token', '39162f6d-a03b-425f-9952-56b83599f7ff');

    var subscription = postal.subscribe({
        channel: "test",
        topic: "TestMessage",
        callback: function (data, envelope) {
            $('h1')[0].innerText=data.Text;
        }
    });
});