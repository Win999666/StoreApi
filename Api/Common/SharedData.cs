namespace Api.Common
{
    public static class SharedData
    {
        public static class Roles
        {
            public const string Admin = "admin";
            public const string Consimer = "consimer";
            public static IReadOnlyList<string> AllRoles
            {
                get => new List<string>() { Admin, Consimer };
            }
        }
    }
}
