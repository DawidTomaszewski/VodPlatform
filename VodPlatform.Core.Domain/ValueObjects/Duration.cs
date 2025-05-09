using VodPlatform.Core.Domain.Exceptions.ValueObjects;

namespace VodPlatform.Core.Domain.ValueObjects
{
    public class Duration
    {
        public int TotalSeconds { get; private set; }

        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        private Duration() { }

        public Duration(int totalSeconds)
        {
            if (totalSeconds < 0)
                throw new InvalidDurationException(totalSeconds);

            TotalSeconds = totalSeconds;
            Hours = TotalSeconds / 3600;
            Minutes = (TotalSeconds % 3600) / 60;
            Seconds = TotalSeconds % 60;
        }

        public static Duration FromTotalSeconds(int totalSeconds)
        {
            return new Duration(totalSeconds);
        }

        public int ToTotalSeconds()
        {
            return this.TotalSeconds;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Duration other)
                return false;

            return TotalSeconds == other.TotalSeconds;
        }
    }
}
