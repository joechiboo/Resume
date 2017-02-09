function createMessage(name, message) {
    var log = $("#chatlog");
    var length = $(log).find("li").length;

    $("#chatlog").prepend(
        '<li data-icon="delete" class=""><a href="#" class="ui-btn ui-btn-icon-right ui-icon-delete">' + name + ':' + message + '</a></li>'
    );
    if (length >= 5) {
        $(log).find("li").eq(5).slideUp();
    }
}

function ajaxMessage(message) {
    var id = $("#memberid").val();

    $.post(
        "/api/Chat",
        {
            memberid: id,
            message: message
        },
        function (data) {
            //alert('succeed');
        }
    )
}