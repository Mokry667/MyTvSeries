// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#favouriteButton').click(function () {
    var icon = $(this).find('#favouriteIcon');
    var buttonLabel = $('#favouriteButtonLabel');
    var isFavouriteValue = $("#IsFavourite");
    var fullStar = 'fas fa-star';
    var emptyStar = 'far fa-star';

    if (icon.hasClass(emptyStar)) {
        buttonLabel.text("Remove from favourites");
        icon.removeClass(emptyStar);
        icon.addClass(fullStar);
        isFavouriteValue.val("True");
    }
    else if (icon.hasClass(fullStar)) {
        buttonLabel.text("Add to favourites");
        icon.removeClass(fullStar);
        icon.addClass(emptyStar);
        isFavouriteValue.val("False");
    }
});

function rateEpisode(that) {
    var episodeDropdownId = that.id;
    var rating = $('#' + episodeDropdownId).val()

    var ajaxData = {
        episodeIdString: $(that).attr('episodeid'),
        ratingString: rating
    };

    $.ajax({
        type: 'POST',
        url: '/Seasons/RateEpisode/',
        data: ajaxData,
        success: function (result) { }
    });
}
