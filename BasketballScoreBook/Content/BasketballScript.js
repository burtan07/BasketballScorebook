function ShowHomeTeamName(){
    var homeTeamID = $("#HomeTeamDropDownList").val();
    
    if(homeTeamID == 9)
    {
        $("#HomeTeamNameLabel").val("ACA Eagles Varsity Boys");
        $("#HomeTeamName").show();
    }
    else if(homeTeamID == 10)
    {
        $("#HomeTeamNameLabel").val("ACA Eagles V Girls");
        $("#HomeTeamName").show();
    }
    else if(homeTeamID == 15)
    {
        $("#HomeTeamNameLabel").val("CCA V Girls");
        $("#HomeTeamName").show();
    }
    else if(homeTeamID == 16)
    {
        $("#HomeTeamNameLabel").val("CCA V Boys");
        $("#HomeTeamName").show();
    }
};
function ShowAwayTeamName(){
    var awayTeamID = $("#AwayTeamDropDownList").val();

    if(awayTeamID == 9)
    {
        $("#AwayTeamNameLabel").val("ACA Eagles Varsity Boys");
        $("#AwayTeamName").show();
    }
    else if(awayTeamID == 10)
    {
        $("#AwayTeamNameLabel").val("ACA Eagles V Girls");
        $("#AwayTeamName").show();
    }
    else if(homeTeamID == 15)
    {
        $("#AwayTeamNameLabel").val("CCA V Girls");
        $("#AwayTeamName").show();
    }
    else if(homeTeamID == 16)
    {
        $("#AwayTeamNameLabel").val("CCA V Boys");
        $("#AwayTeamName").show();
    }
};