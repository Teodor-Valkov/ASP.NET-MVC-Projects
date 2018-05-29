$(function () {
    $.validator.methods.date = function (value, element) {
        return true;
    };

    $("input.datepicker").datepicker({
        format: "dd-mm-yyyy",
        weekStart: 1,
        todayBtn: "linked",
        language: "bg",
        todayHighlight: true,
    });
});