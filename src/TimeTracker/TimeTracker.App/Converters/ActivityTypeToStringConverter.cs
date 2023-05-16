using System.Globalization;
using CommunityToolkit.Maui.Converters;
using TimeTracker.App.Resources.Texts;
using TimeTracker.Common.Enums;

namespace TimeTracker.App.Converters;

public class ActivityTypeToStringConverter: BaseConverterOneWay<Types, string>
{
    public override string ConvertFrom(Types value, CultureInfo? culture)
        => TypesTexts.ResourceManager.GetString(value.ToString(), culture)
           ?? TypesTexts.none;
    public override string DefaultConvertReturnValue { get; set; } = TypesTexts.none;
}