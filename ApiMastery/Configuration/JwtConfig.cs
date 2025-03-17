namespace ApiMastery.Configuration
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public TimeSpan ExpiryTime { get; set; }  // agregado para el refresh token
    }
}
