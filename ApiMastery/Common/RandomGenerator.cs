namespace ApiMastery.Common
{
    public static class RandomGenerator
    {
        public static string GeneratorRandomString(int size)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrsduvwxyz$#-_.";

            return new string(Enumerable.Repeat(chars, size).
                Select(s => s[random.Next(chars.Length)]).ToArray());
        }
    }
}
