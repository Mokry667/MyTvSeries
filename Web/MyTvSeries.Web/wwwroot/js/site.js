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
    var rating = $('#' + episodeDropdownId).val();
    var seriesId = $('#SeriesId').val();
    var seasonId = $('#Id').val();

    var ajaxData = {
        episodeIdString: $(that).attr('episodeid'),
        ratingString: rating,
        seriesIdString: seriesId,
        seasonIdString: seasonId
    };

    $.ajax({
        type: 'POST',
        url: '/Seasons/RateEpisode/',
        data: ajaxData,
        success: function (result) { }
    });
}

function upvoteReview(that) {

    var icon = $(that).find('i');
    var buttonLabel = $(that).find('#favouriteButtonLabel');
    var likeIcon = 'far fa-thumbs-up';
    var unlikeIcon = 'fa fa-thumbs-up';

    if (icon.hasClass(unlikeIcon)) {
        buttonLabel.text("Upvote this review");
        icon.removeClass(unlikeIcon);
        icon.addClass(likeIcon);
    }
    else if (icon.hasClass(likeIcon)) {
        buttonLabel.text("Remove upvote");
        icon.removeClass(likeIcon);
        icon.addClass(unlikeIcon);
    }

    var ajaxData = {
        reviewIdString: $(that).find('#reviewId').val(),
    };

    $.ajax({
        type: 'POST',
        url: '/Series/RateReview/',
        data: ajaxData,
        success: function (result) { }
    });
}

$('.seriesNotification').click(function () {
    var notificationId = this.id;

    var ajaxData = {
        notificationIdString: notificationId,
    };

    $.ajax({
        type: 'POST',
        url: '/Home/MakeSeriesNotificationRead/',
        data: ajaxData,
        success: function (result) { }
    });
});

$('.personNotification').click(function () {
    var notificationId = this.id;

    var ajaxData = {
        notificationIdString: notificationId,
    };

    $.ajax({
        type: 'POST',
        url: '/Home/MakePersonNotificationRead/',
        data: ajaxData,
        success: function (result) { }
    });
});
