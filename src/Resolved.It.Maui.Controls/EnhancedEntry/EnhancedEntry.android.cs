using AndroidX.AppCompat.Widget;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace Resolved.It.Maui.Controls;
public partial class EnhancedEntry
{
    private static void RemoveBorder(IElementHandler? handler)
    {
        if (handler?.PlatformView is not AppCompatEditText textBox)
            return;
        
        textBox.SetBackgroundColor(Colors.Transparent.ToPlatform());
    }    
    
    private static void AddEvents(IElementHandler? handler)
    {
    }
    
    private static void RemoveEvents(IElementHandler? handler)
    {
    }  
    
    private static void FillWidth(IElementHandler? handler)
    {
    }    
    
    private static void OpenDropdown(IElementHandler? handler)
    {
    }
}