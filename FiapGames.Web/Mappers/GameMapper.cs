using FiapCloudGames.Utils.DTOs;
using FiapGames.Web.Components.Shared;

namespace FiapGames.Web.Mappers
{
    public static class GameMapper
    {
        public static GameCard Mapper(GameDto game)
        {
            return new()
            {
                IdGame = game.Id,
                Title = game.Title,
                ImageUrl = "",
                Price = game.Price.ToString()
            };
        }

        public static List<GameCard> MapperList(List<GameDto> games)
        {
            List<GameCard> gameCards = new List<GameCard>();

            for(int i = 0; i < games.Count - 1; i++)
            {
                gameCards.Add(Mapper(games[i]));
            }

            return gameCards;
        }
    }
}