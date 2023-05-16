using CommunityToolkit.Maui.Converters;
using TimeTracker.BL.Models;
using System.Globalization;

namespace TimeTracker.App.Converters;

public class ModelIsNewToIsVisibleInvertedConverter : BaseConverterOneWay<ModelBase, bool>
{
    public override bool ConvertFrom(ModelBase value, CultureInfo? culture)
        => value.ID != Guid.Empty;
    
    public override bool DefaultConvertReturnValue { get; set; } = true;
}