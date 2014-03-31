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
});