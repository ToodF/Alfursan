var AlfursanAjax = {

    Request: function (url, method, data, resultContainer, succesCallback) {
        if ($(resultContainer).size() > 0) {
            $(resultContainer).append('<div class="loading-container"></div>');
        }
        $.ajax({
            type: method,
            url: url,
            data: data,
        }).done(function (result) {
            if (result.ResponseMessage != undefined && result.ResponseMessage != "" ) {
                var messageCode = "alert-success";
                if (result.ReturnCode == "2") {
                    messageCode = "alert-danger";
                } else if (result.ReturnCode == "3") {
                    messageCode = "alert-warning";
                } else if (result.ReturnCode == "4") {
                    messageCode = "alert-info";
                }
                var alertDiv = $(resultContainer + " .alert");
                if (alertDiv.size() > 0) {
                    alertDiv.removeAttr("class");
                    alertDiv.addClass("alert");
                    alertDiv.addClass(messageCode);
                    alertDiv.html(result.ResponseMessage);
                } else {
                    $(resultContainer).append('<div class="alert ' + messageCode + '" role="alert">' + result.ResponseMessage + '</div>');
                }
                alertDiv.show("slow");
            }
            if (succesCallback) {
                succesCallback(result);
            }
            $(".loading-container").remove();
        }).fail(function () {
            $(".loading-container").remove();
            alert("hata");
        });
    },

}