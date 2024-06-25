
$(function () {
    const orgSelector = "#orgSelector";

    hideLoader();

    $(orgSelector).on("change", function () {
        showLoader();
        console.log($(this).val());
        setTimeout(hideLoader, 4000);
    });

    function showLoader() {
        $("#loader").css("display", "block");
    }

    function hideLoader() {
        $("#loader").css("display", "none");
    }
});