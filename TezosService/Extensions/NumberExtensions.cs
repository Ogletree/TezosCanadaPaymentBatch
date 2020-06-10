using System;

namespace TezosService.Extensions
{
    public static class NumberExtensions
    {
        public static decimal ToTez(this decimal source)
        {
            source = source / 1000000;
            source = Math.Truncate(1000000 * source) / 1000000;
            return source;
        }
    }
}
