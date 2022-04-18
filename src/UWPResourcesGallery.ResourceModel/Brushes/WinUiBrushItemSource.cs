using System;
using System.Collections.Generic;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace UWPResourcesGallery.ResourceModel.Brushes
{
    public class WinUIBrushItemSource : GenericItemsSource<WinUIBrush>
    {
        public static void LoadSystemBrushes()
        {
            var resources = (ResourceDictionary)Application.Current.Resources.MergedDictionaries[0]
                .ThemeDictionaries["Default"];
            foreach (var (resourceKey, resource) in resources)
            {
                var xamlDefinition = "";
                var color = "";
                switch (resource)
                {
                    case SolidColorBrush solidColorBrush:
                    {
                        color = solidColorBrush.Color.ToString();
                        if (solidColorBrush.Opacity != 1.0)
                        {
                            xamlDefinition = SystemBrushDefinitionFactory.SolidColorBrushDefinition(
                                (string)resourceKey,
                                color, solidColorBrush.Opacity
                            );
                        }
                        else
                        {
                            xamlDefinition = SystemBrushDefinitionFactory.SolidColorBrushDefinition(
                                (string)resourceKey,
                                color
                            );
                        }

                        break;
                    }
                    case AcrylicBrush acrylicBrush:
                        xamlDefinition = SystemBrushDefinitionFactory.AcrylicBrushDefinition(
                            (string)resourceKey,
                            acrylicBrush.BackgroundSource.ToString(),
                            acrylicBrush.TintColor.ToString(),
                            acrylicBrush.TintOpacity,
                            acrylicBrush.FallbackColor.ToString());
                        break;
                    default:
                        continue;
                }

                var brush = new WinUIBrush(
                    (string)resourceKey,
                    color,
                    xamlDefinition
                );
                Items.Add(brush);
            }

            ((List<WinUIBrush>)Items).Sort((x, y) => string.Compare(x.Key, y.Key, StringComparison.Ordinal));
        }
    }
}
