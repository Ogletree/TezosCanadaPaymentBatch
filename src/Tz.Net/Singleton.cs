using System;

namespace Tz.Net
{
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> lazy = new Lazy<Singleton>(() => new Singleton());

        public static Singleton Instance => lazy.Value;
        public string LastOperation = "";
        private Singleton()
        {
        }
    }
}