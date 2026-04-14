using FiapCloudGames.Utils.DTOs;

namespace FiapGames.Web.Constants
{
    public static class GameEndpoint
    {
        private static string Base = "Game";

        public static string GetAll() => Base;
        public static string GetById(Guid id) => $"{Base}/{id}";
        public static string Create() => $"{Base}";
        public static string Update(Guid id) => $"{Base}/{id}";
        public static string Delete(Guid id) => $"{Base}/{id}";
        public static string AcquireGame(Guid gameId, Guid userId) => $"{Base}/{gameId}/acquire/{userId}";

    }
}