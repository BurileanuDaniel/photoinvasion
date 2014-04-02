$(document).ready(function () {
    $('#ratePhoto a').mouseover(function () {
        $(this).children(":first").removeClass('fa-star-o').addClass('fa-star');
        $(this).prevAll('#ratePhoto a').each(function () {
            $(this).children(":first").removeClass('fa-star-o').addClass('fa-star');
        });
    });

    $('#ratePhoto a').mouseout(function () {
        $('i').each(function () {
            $(this).removeClass('fa-star').addClass('fa-star-o');
        });
    });

    
    (function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=1453983538170294";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

});