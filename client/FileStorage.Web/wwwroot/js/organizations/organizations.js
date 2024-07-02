
$(function () {
    const orgSelector = "#orgSelector";
    const filesTableSelector = "#filesTable";
    const loaderSelector = "#loader";

    $(filesTableSelector + " tbody").on("click", "tr", function () {
        const selectedFileClass = "selected-file";
        const row = $(this);

        row.hasClass(selectedFileClass)
            ? row.removeClass(selectedFileClass)
            : row.addClass(selectedFileClass);

        row.siblings().removeClass(selectedFileClass);
    });

    $(orgSelector).on("change", function () {
        hideElement(filesTableSelector);
        displayElement(loaderSelector);
        loadOrgFiles($(this).val());
    });

    function loadOrgFiles(orgId) {
        $.ajax({
            url: `/Files/?orgId=${orgId}`,
            success: (response) => {
                $(`${filesTableSelector} tbody`).html(response);
                hideElement(loaderSelector);
                displayElement(filesTableSelector);
            },
        });
    }

    function displayElement(selector) {
        $(selector).removeClass("d-none");
    }

    function hideElement(selector) {
        $(selector).addClass("d-none");
    }


    $("#uploadFileBtn").on("click", function () {
        const fdata = new FormData();
        let file = $("#uploadFileInput")[0].files[0];
        fdata.append("file", file);
        $.ajax({
            url: `/Files/UploadFile?orgId=${$(orgSelector).val()}`,
            type: "POST",
            data: fdata,
            processData: false,
            contentType: false,
            success: (response) => {
                $(`${filesTableSelector} tbody`).append(response);
            },
        });
    });

    $("#deleteFileBtn").on("click", function () {
        const deleteFileModalContent = $("#deleteFileModal");
        const md = $("#modal");

        md.html(deleteFileModalContent.html());
        md.modal("show");
    });
    
    $("#modal").on("click", "#deleteFileConfirmBtn", function () {
        const md = $("#modal");
        const selectedFile = $(`${filesTableSelector} tbody`).children(".selected-file").first();

        const fileId = selectedFile.attr("data-id");
        if (fileId) {
            $.ajax({
                url: `/Files/DeleteFile/${fileId}?orgId=${$(orgSelector).val()}`,
                type: "DELETE",
                success: () => {
                    selectedFile.remove();
                },
            });
        }

        md.modal("hide");
    });
});