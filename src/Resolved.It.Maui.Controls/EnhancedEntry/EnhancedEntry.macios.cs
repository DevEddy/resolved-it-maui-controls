using Microsoft.Maui;
using UIKit;

// ReSharper disable once CheckNamespace
namespace Resolved.It.Maui.Controls;
public partial class EnhancedEntry
{
    private static void RemoveBorder(IElementHandler? handler)
    {
        if (handler?.PlatformView is not UITextField textBox)
            return;
        
        textBox.BorderStyle = UITextBorderStyle.None;
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