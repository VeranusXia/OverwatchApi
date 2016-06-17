namespace OverwatchApi
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    internal static class Converters
    {
        static Converters()
        {
            RegisterDefaultConverter(SanitizedNumber<int>());
            RegisterDefaultConverter(SanitizedNumber<long>());
            RegisterDefaultConverter(SanitizedNumber<double>());
            RegisterDefaultConverter(Duration);

            RegisterDefaultConverter(Passthrough);
        }

        private static readonly Func<string, string> Passthrough = s => s;

        private static void RegisterDefaultConverter<T>(Func<string, T> converter)
        {
            DefaultConverters.Add(typeof (T), converter);
        }

        public static Func<string, T> GetDefaultConverter<T>() //TODO: nasty. Way that doesn't compromise compile-time type safety?
        {
            object converter;
            return DefaultConverters.TryGetValue(typeof (T), out converter) ? (Func<string, T>) converter : DefaultDotNet<T>();
        }

        private static readonly Dictionary<Type, object> DefaultConverters = new Dictionary<Type, object>();

        public static Func<string, T> DefaultDotNet<T>()
        {
            return s => (T) Convert.ChangeType(s, typeof (T), CultureInfo.InvariantCulture);
        }

        public static Func<string, TimeSpan> Duration { get; } = s =>
        {
            TimeSpan t;
            if (TimeSpan.TryParseExact(s, @"mm\:ss", CultureInfo.InvariantCulture, out t))
                return t;
            if (TimeSpan.TryParseExact(s, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out t))
                return t;

            //TODO: days?
            throw new ArgumentException();
        };

        public static Func<string, T> SanitizedNumber<T>() => s =>
            DefaultDotNet<T>()(string.Concat(s.Where(char.IsNumber)));
    }
}