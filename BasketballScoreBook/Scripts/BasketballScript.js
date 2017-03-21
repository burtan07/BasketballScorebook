var HomeTeamScore = 0;
var AwayTeamScore = 0;

function ShowHomeTeamName() {
    var homeTeamID = $("#HomeTeamDropDownList").val();

    if (homeTeamID == 9) {
        $("#HomeTeamNameLabel").val("ACA Eagles Varsity Boys");
        $("#HomeTeamName").show();
    }
    else if (homeTeamID == 10) {
        $("#HomeTeamNameLabel").val("ACA Eagles V Girls");
        $("#HomeTeamName").show();
    }
    else if (homeTeamID == 15) {
        $("#HomeTeamNameLabel").val("CCA V Girls");
        $("#HomeTeamName").show();
    }
    else if (homeTeamID == 16) {
        $("#HomeTeamNameLabel").val("CCA V Boys");
        $("#HomeTeamName").show();
    }
};

function ShowAwayTeamName() {
    var awayTeamID = $("#AwayTeamDropDownList").val();

    if (awayTeamID == 9) {
        $("#AwayTeamNameLabel").val("ACA Eagles Varsity Boys");
        $("#AwayTeamName").show();
    }
    else if (awayTeamID == 10) {
        $("#AwayTeamNameLabel").val("ACA Eagles V Girls");
        $("#AwayTeamName").show();
    }
    else if (homeTeamID == 15) {
        $("#AwayTeamNameLabel").val("CCA V Girls");
        $("#AwayTeamName").show();
    }
    else if (homeTeamID == 16) {
        $("#AwayTeamNameLabel").val("CCA V Boys");
        $("#AwayTeamName").show();
    }
};

function HomeScorePlus1() {
        
    HomeTeamScore += 1;
    $("#HomeTeamScore").val(HomeTeamScore);
};

function HomeScorePlus2() {
    HomeTeamScore += 2;
    $("#HomeTeamScore").val(HomeTeamScore);
};

function HomeScorePlus3() {
    HomeTeamScore += 3;
    $("#HomeTeamScore").val(HomeTeamScore);
};

function AwayScorePlus1() {
    AwayTeamScore += 1;
    $("#AwayTeamScore").val(AwayTeamScore);
};

function AwayScorePlus2() {
    AwayTeamScore += 2;
    $("#AwayTeamScore").val(AwayTeamScore);
};

function AwayScorePlus3() {
    AwayTeamScore += 3;
    $("#AwayTeamScore").val(AwayTeamScore);
};