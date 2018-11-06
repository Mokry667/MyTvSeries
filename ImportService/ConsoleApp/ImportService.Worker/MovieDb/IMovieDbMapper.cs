using MyTvSeries.Domain.Entities;

namespace ImportService.Worker.MovieDb
{
    public interface IMovieDbMapper
    {
        Series MapSeriesFromImportToSeriesFromDb(Series seriesFromDb, Series seriesFromImport);
        Person MapPersonFromImportToPersonFromDb(Person personFromDb, Person personFromImport);
        Season MapSeasonFromImportToSeasonFromDb(Season seasonFromDb, Season seasonFromImport);
        Episode MapEpisodeFromImportToEpisodeFromDb(Episode episodeFromDb, Episode episodeFromImport);
        Character MapCharacterFromImportToCharacterFromDb(Character characterFromDb, Character characterFromImport);
        Crew MapCrewFromImportToCrewFromDb(Crew crewFromDb, Crew crewFromImport);
        Series MapSeriesExternalIdsFromImportToSeriesFromDb(Series seriesFromDb, Series seriesFromImport);
        Series MapSeriesBroadcastAndRuntimeFromImportToSeriesFromDb(Series seriesFromDb, Series seriesFromImport);
    }
}
