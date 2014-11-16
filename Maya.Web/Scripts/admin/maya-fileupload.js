$(function () {
    // disable the default browser action for file drops on the document
    $(document).bind('drop dragover', function (e) {
        e.preventDefault();
    });

    // focus button
    $(document).on("focus", ".btn-file input", function (evt) {
        $(this).closest('.btn-file').addClass("focused");
    });
    $(document).on("focusout", ".btn-file input", function (evt) {
        $(this).closest('.btn-file').removeClass("focused");
    });

    function fileUploadedTemplate(data) {
        var $template = $($(".fileupload .preview").html());
        $template.find("img").attr("src", data.url);
        $template.find("input").val(data.url);

        return $template.html();
    }

    function fileLinkTemplate(data) {
        var html = '<a href="' + data.url + '" target="_blank">' + data.fileName + '</a>';
        return html;
    }

    function validateFileUpload(files, maxSize, allowedExtensions) {
        var errors = [];

        var extensionWhitelist = $("html").data("whitelist");

        $.each(files, function (index, file) {
            // whitelisted extensions
            if (extensionWhitelist && extensionWhitelist.length > 0) {
                var acceptFileTypes = new RegExp("(\.|\/)(" + extensionWhitelist + ")$", "i");
                if (!acceptFileTypes.test(file["name"])) {
                    // deny
                    errors.push("The file type " + file["name"].split('.').pop() + " is not permitted.");
                }
            }

            // accept file types
            if (errors.length == 0 && allowedExtensions) {
                var acceptFileTypes = new RegExp("(\.|\/)(" + allowedExtensions + ")$", "i");

                if (file["name"].length && !acceptFileTypes.test(file["name"])) {
                    errors.push("The file type is not permitted here.");
                }
            }

            // maximum file size
            if (errors.length == 0 && maxSize) {
                if (parseInt(file["size"]) > parseInt(maxSize)) {
                    errors.push("File size is too big.");
                }
            }
        });
        return errors;
    }

    /********************************************* 
    * Standard file uploads (Shared/File.cshtml) *
    **********************************************/
    $(".fileupload input[type=file]").fileupload({
        dataType: "json",
        pasteZone: null,
        add: function (e, data) {
            var uploadErrors = validateFileUpload(data.files, $(this).data("max-size"), $(this).data("allowed-extensions"));
            var group = $(this).closest(".form-group");
            group.removeClass("has-error");

            if (uploadErrors.length > 0) {
                group.addClass("has-error");
                group.find(".details").text(uploadErrors.join(" "));
            } else {
                data.submit();
            }

        },
        progressall: function (e, data) {
            $(this).closest(".fileupload").find(".preview .progress").show().css("width", parseInt(data.loaded / data.total * 100, 10) + "%");
        },
        done: function (e, data) {
            var html = fileUploadedTemplate(data.result);
            var fileupload = $(this).closest(".fileupload");
            var preview = fileupload.find(".preview");

            if ($(this).prop("multiple")) {
                // multiple files, just append
                preview.append(html);
            } else {
                // single file, replace existing
                preview.html(html);
                fileupload.find(".dropdown-toggle").removeClass("hidden");

                var link = fileLinkTemplate(data.result);
                fileupload.find(".details").html(link);
            }
            $(this).focus();
        },
        always: function () {
            // reset progress bar
            $(this).closest(".fileupload").find(".preview .progress").hide().css("width", "0%");
        }
    });
})