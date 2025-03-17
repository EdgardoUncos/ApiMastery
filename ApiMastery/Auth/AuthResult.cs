namespace ApiMastery.Auth
{
    public class AuthResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }    //Agregado para refresh token
        public bool Result { get; set; }
        public List<string> Errors { get; set; }
    }
}
