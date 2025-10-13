
namespace ShopTARgv24.Core.Dto
{
    public class AccuLocationRootDto
    {
        public string LocalObservationDateTime { get; set; }
        public string EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }

        public class TemperatureInfo
        {
            public MetricTemperature Metric { get; set; }
            public ImperialTemperature Imperial { get; set; }
        }

        public class MetricTemperature
        {
            public double Value { get; set; }
            public string Unit { get; set; }
            public int UnitType { get; set; }
        }

        public class ImperialTemperature
        {
            public double Value { get; set; }
            public string Unit { get; set; }
            public int UnitType { get; set; }
        }

    }
}
