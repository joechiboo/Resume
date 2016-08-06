Parse.initialize("NJuzWf1fOnZpoa1H3xwsyP7Oy3NEQOnU3N28c8Qb", "Lo14aOwY5E7xAlB1tlbXzK48SwHiOJT7XCdNb4yN");

$(function () {
    GetCount();
    var chat = $.connection.chatHub;

    chat.client.broadcastCount = function (count) {
        $("#count").text(count);
    };

    $.connection.hub.start().done(function () {
        $(".wrap").on("click", function () {
            var count = $("#count").text();

            ParseCount();
            chat.server.send(count);

            if ($(this).attr("trigger") == "off") {
                $("#s-bulb").css({
                    "fill": "#c4d8d9",
                    "stroke": "#c4d8d9",
                    "stroke-width": "2",
                    "transition": "100ms"
                });

                $("#www-filament").css({
                    "fill": "#ffdf43",
                    "stroke": "#ffdf43",
                    "stroke-width": "3",
                    "transition": "all 600ms cubic-bezier(0.68, -0.55, 0.265, 1.55)"
                });

                $(".light .glow").css({
                    "width": "660px",
                    "height": "660px",
                    "opacity": "0.4",
                    "background": "-webkit-radial-gradient(rgba(255,223,67,1), rgba(255,223,67,0) 70%)",
                    "transition": "all 800ms cubic-bezier(0.68, -0.55, 0.265, 1.55)"
                });

                $(".light .flare").css({
                    "width": "560px",
                    "height": "560px",
                    "opacity": "0.5",
                    "background": "rgba(255,223,67,1)",
                    "transition": "all 600ms cubic-bezier(0.68, -0.55, 0.265, 1.55)"
                });

                $(this).attr("trigger", "on");
            } else {
                $("#s-bulb").css({
                    "fill": "#262832",
                    "stroke": "#262832",
                    "stroke-width": "0",
                    "transition": "300ms"
                });

                $("#www-filament").css({
                    "fill": "#333542",
                    "stroke": "#333542",
                    "stroke-width": "0",
                    "transition": "1500ms"
                });

                $(".light .glow").css({
                    "width": "0px",
                    "height": "0px",
                    "border-radius": "50%",
                    "opacity": "0",
                    "background": "-webkit-radial-gradient(rgba(255,223,67,1), rgba(255,223,67,0) 70%)",
                    "transform": "translate(-50%, -50%)",
                    "transition": "all 1000ms cubic-bezier(0.68, -0.55, 0.265, 1.55)"
                });

                $(".light .flare").css({
                    "width": "0px",
                    "height": "0px",
                    "border-radius": "50%",
                    "opacity": "0",
                    "background": "rgba(255,223,67,0)",
                    "transform": "translate(-50%, -50%)",
                    "transition": "all 1000ms cubic-bezier(0.68, -0.55, 0.265, 1.55)"
                });

                $(this).attr("trigger", "off");
            }
        });
    });
});

function ParseCount() {
    var Data = Parse.Object.extend("Data");
    var query = new Parse.Query(Data);
    query.get("ar4Mxd1lQv", {
        success: function (data) {
            // The object was retrieved successfully.
            data.increment("count");
            data.save();
        },
        error: function (object, error) {
            // The object was not retrieved successfully.
            // error is a Parse.Error with an error code and description.
        }
    });
}

function GetCount() {
    var Data = Parse.Object.extend("Data");
    var query = new Parse.Query(Data);
    query.get("ar4Mxd1lQv", {
        success: function (data) {
            // The object was retrieved successfully.
            $("#count").text(data.get("count"));
        },
        error: function (object, error) {
            // The object was not retrieved successfully.
            // error is a Parse.Error with an error code and description.
        }
    });
}