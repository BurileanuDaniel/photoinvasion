$(document).ready(function () {
    scaleImageWrappers();
});

$(window).load(function () {
    scaleImages($('.imageWrapper').first().css('width'));
});

$(window).resize(function () {
    scaleImageWrappers();
    scaleImages($('.imageWrapper').first().css('width'));
});

function scaleImageWrappers() {
    $('.imageWrapper').each(function () {
        var width = $(this).css('width');

        $(this).css('height', width);
    });
};

function scaleImages(size) {
    $('.imageWrapper img').each(function () {

        console.log($(this).css('width') + ' h:' + $(this).css('height'));
        console.log(parseInt($(this).css('width')) > parseInt($(this).css('height')));
        var currentImage = $(this);
        if (parseInt(currentImage.css('width')) > parseInt(currentImage.css('height'))) {
            currentImage.css('width', size);
        }
        else {
            currentImage.css('height', (parseInt(size) - 90) + 'px');
        }

    });
};