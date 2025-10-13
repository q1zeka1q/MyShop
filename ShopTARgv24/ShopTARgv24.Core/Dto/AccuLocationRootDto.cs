
namespace ShopTARgv24.Core.Dto
{
    public class AccuLocationRootDto
    {
        public string? LocalObservationDateTime { get; set; }
        public string? EpochTime { get; set; }
        public string? WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public string? PrecipitationType { get; set; }
        public bool IsDayTime { get; set; }

        public TemperatureDto? Temperature { get; set; }
        public string? MobileLink { get; set; }  
        public string? Link { get; set; }

    }
    public class TemperatureDto

        {
            public MetriclDto? Metric { get; set; }
            public ImperialDto? Imperial { get; set; }
    }
    public class MetriclDto
    {
        public double? Value { get; set; }
        public string? Unit { get; set; }
        public int? UnitType { get; set; }
    }
    public class ImperialDto
    {
        public double Value { get; set; }
        public string? Unit { get; set; }
        public int UnitType { get; set; }
    }
}
