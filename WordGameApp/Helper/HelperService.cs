using System;
using System.Linq;


namespace WordGameApp.Helper
{
    public static class HelperService
    {
        private static Random Random = new Random();

        public static string GetRandomLetters(int count)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            return new string(Enumerable.Repeat(chars, count)
                .Select(s => s[Random.Next(s.Length)])
                .ToArray());
        }
    }
}
