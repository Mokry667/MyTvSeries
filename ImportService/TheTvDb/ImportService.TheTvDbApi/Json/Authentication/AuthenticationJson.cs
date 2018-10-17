namespace ImportService.TheTvDb.Api.Json.Authentication
{
    public class AuthenticationJson
    {
        public string ApiKey { get; set; }
        public string Username { get; set; }
        public string UserKey { get; set; }

        public AuthenticationJson(string apiKey, string username, string userkey)
        {
            ApiKey = apiKey;
            Username = username;
            UserKey = userkey;
        }
    }
}
