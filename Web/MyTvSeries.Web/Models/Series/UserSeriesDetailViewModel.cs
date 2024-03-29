﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Domain.Enums;
using MyTvSeries.Web.Models.Enums;
using MyTvSeries.Web.Models.Validators;

namespace MyTvSeries.Web.Models.Series
{
    public class UserSeriesDetailViewModel
    {
        public long SeriesId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Rating")]
        public decimal SiteRating { get; set; }

        [Display(Name = "Votes")]
        public int UserVotes { get; set; }

        [Display(Name = "Original name")]
        public string OriginalName { get; set; }
        public string Overview { get; set; }
        public SeriesStatus Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? AiredFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? AiredTo { get; set; }

        public DayOfWeek AirDayOfWeek { get; set; }
        public TimeSpan AirTime { get; set; }

        [Display(Name = "Seasons")]
        public int NumberOfSeasons { get; set; }

        [Display(Name = "Episodes")]
        public int NumberOfEpisodes { get; set; }

        [Display(Name = "Episode Runtime")]
        public int EpisodeRuntime { get; set; }

        [Display(Name = "Genres")]
        public List<Genre> Genres { get; set; }


        [Display(Name = "Total runtime")]
        public decimal TotalRuntime { get; set; }

        public WatchStatus WatchStatus { get; set; }


        public SeriesRating SeriesRating { get; set; }

        public byte[] PosterContent { get; set; }

        [Display(Name = "Seasons")]
        public int SeasonsWatched { get; set; }

        [Display(Name = "Episodes")]
        public int EpisodesWatched { get; set; }

        [Display(Name = "Seasons")]
        public List<SeasonSeriesDetailViewModel> SeasonSeries { get; set; }

        [Display(Name = "Cast")]
        public List<CastSeriesDetailViewModel> Cast { get; set; }

        [Display(Name = "Crew")]
        public List<CrewSeriesDetailViewModel> Crew { get; set; }

        [Display(Name = "Reviews")]
        public List<SeriesReviewDetailViewModel> Reviews { get; set; } 

        public bool IsReviewWritten { get; set; }
        public long ReviewId { get; set; }

        [Display(Name="")]
        public bool IsFavourite { get; set; }

        public bool IsMoreSeasons { get; set; }

        public bool IsMoreCast { get; set; }
        public bool IsMoreCrew { get; set; }

    }
}
