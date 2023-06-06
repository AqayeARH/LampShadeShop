
$(document).ready(function() {
    LoadCkEditor4();
});

function LoadCkEditor4() {
    if (document.getElementById("CkEditor4")) {
        $("body").append("<script src='/ckeditor4/ckeditor/ckeditor.js'></script>");

        CKEDITOR.replace("CkEditor4",
            {
                customConfig: "/ckeditor4/ckeditor/config.js"
            });
    }
}