public static class ApiEndpoints
{
    public static class Games
    {
        public const string Base = "api/Game";
        public const string GetAll = Base;
        public const string GetById = $"{Base}/{{0}}";
        public const string Acquire = $"{Base}/{{0}}/acquire/{{1}}";
    }
}