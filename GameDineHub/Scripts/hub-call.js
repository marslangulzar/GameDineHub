//let output = document.getElementById('stopwatch');

var notification = $.connection.teamHub;

$.connection.hub.start()
    .done(function () {
        console.log("signalR done........");
        // showClientNotification();
    })
    .fail(function (error) {
        alert(error)
    })
$.connection.hub.disconnected(function () {
    setTimeout(function () {
        $.connection.hub.start();
    }, 5000); // Restart connection after 5 seconds.
});
notification.client.callInProgress = function (callSid) {

}
notification.client.publishThemeCast = function (themeCast) {
    $('#votingViewDisplay').addClass("d-none");
    $('#castViewDisplay').removeClass("d-none");
    $('#display-alert').addClass("d-none");
    $('#publishThemeCastText').text(themeCast);
    $('#display-alert-message').text(themeCast);
}
notification.client.SetOwnSong = function (song) {
    $('#ownSong').empty();
    $('#ownSong').text(song);
}

notification.client.refreshAdminView = function (startbuttonEnable) {
    if (startbuttonEnable) {
        $("#startVoting").removeAttr("disabled", false);
    }
    if (typeof (refreshCastVotingGrid) != "undefined") {
        refreshCastVotingGrid();
    }
    if (typeof (refreshVotingGrid) != "undefined") {
        refreshVotingGrid();
    }
}
notification.client.refreshTeamVotingView = function () {
    if (typeof (refreshTeamVotingGrid) != "undefined") {
        refreshTeamVotingGrid();
    }
}
//notification.client.getTeamNameView = function () {

//    if (typeof (getTeamName) != "undefined") {
//        debugger;
//        getTeamName();
//    }
//}
notification.client.votingStart = function () {
    //$('#votingViewDisplay').removeClass("d-none");
    //$('#castViewDisplay').addClass("d-none");
    //$('#display-alert').removeClass("d-none");
    //if (typeof (refreshTeamVotingGrid) != "undefined") {
    //    refreshTeamVotingGrid();
        location.reload();
    //}

    $('#votingRoundStatus').val(true);
}



notification.client.endVoting = function () {

    var teamId = localStorage.getItem("teamId");
    if (teamId != null) {
        $('#votingSectionMainDiv').addClass("d-none");
        $('#addTeamSection').addClass("d-none");
        $('#songListSection').removeClass("d-none");
        $("#select2-searchStr-container").text('');
    }
    $('#votingRoundStatus').val(false);
    $('#display-alert').removeClass("d-none");
    $('#winnerSection').removeClass("d-none");
    $('#winingBg').addClass("animatedBg");
    $("#startVoting").attr("disabled", true);
    if (typeof (refreshVotingGrid) != "undefined") {
        refreshVotingGrid();
    }

    if (typeof (refreshTeamVotingGrid) != "undefined") {
        refreshTeamVotingGrid();
    }
    //debugger;
    //if (typeof (getTeamName) != "undefined") {
    //    debugger;
    //    getTeamName();
    //}
}




