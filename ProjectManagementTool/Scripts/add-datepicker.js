$(document).ready(function () {
    $("#PossibleStartDate").datepicker({ format: "mm/dd/yy", autoclose: true });
    //var possibleStartDate = $("#PossibleStartDate").val();
    $("#PossibleEndDate").datepicker({ format: "mm/dd/yy", autoclose: true });
    $('#PossibleStartDate').on('change', function () {

        var starttime = new Date($("#PossibleStartDate").val());
      
            var endtime = new Date($("#PossibleEndDate").val());
            var timedifference = Math.abs(endtime.getTime() - starttime.getTime());
            var diffDays = Math.ceil(timedifference / (1000 * 3600 * 24));
            //alert(diffDays);
            $("#Duration").val(diffDays);
    });
    $('#PossibleEndDate').on('change', function () {
        var starttime = new Date($("#PossibleStartDate").val());
        var endtime = new Date($("#PossibleEndDate").val());
        var timedifference = Math.abs(endtime.getTime() - starttime.getTime());
        var diffDays = Math.ceil(timedifference / (1000 * 3600 * 24));
        //alert(diffDays);
        $("#Duration").val(diffDays);
    });
    })