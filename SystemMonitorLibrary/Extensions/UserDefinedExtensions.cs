using SystemMonitorLibrary.Config;
using SystemMonitorLibrary.Enums;



namespace SystemMonitorLibrary.Extensions
{
    internal static class UserDefinedExtensions
    {
        public static long ConvertToGB(this long size, DataConversionUnit conversionUnit)
        {

            return (long)(size / GetConversionDivisor(conversionUnit));
        }

        public static decimal ConvertToGB(this decimal size, DataConversionUnit conversionUnit)
        {
            return size / GetConversionDivisor(conversionUnit);
        }

        public static decimal RoundToSettingsPrecision(this decimal number)
        {
            return decimal.Round(number, SettingsManager.Settings.DecimalPrecision);
        }

        private static decimal GetConversionDivisor(DataConversionUnit conversionUnit)
        {
            switch (conversionUnit)
            {
                case DataConversionUnit.Bit:
                    return 1024m * 1024m * 1024m * 8m;
                case DataConversionUnit.Byte:
                    return 1024m * 1024m * 1024m;
                case DataConversionUnit.KiloByte:
                    return 1024m * 1024m;
                case DataConversionUnit.MegaByte:
                    return 1024m;
            }
            return 1m;
        }
    }
}
