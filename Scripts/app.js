$(function () {

    $('#my-files').on("click",function () {
        console.log("clicked");
        $("li").toggleClass("invisible");

    });
});