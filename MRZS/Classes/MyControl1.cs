using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MRZS.Classes
{
    //this control do not using
    public partial class HighlightingTextBlock : Control
    {
        ///// <summary>
        ///// The name of the TextBlock part.
        ///// </summary>
        //private string TextBlockName = "Text";

        ///// <summary>
        ///// Gets or sets the text block reference.
        ///// </summary>
        //private TextBlock TextBlock { get; set; }

        ///// <summary>
        ///// Gets or sets the inlines list.
        ///// </summary>
        //private List<Inline> Inlines { get; set; }

        //#region public string Text
        ///// <summary>
        ///// Gets or sets the contents of the TextBox.
        ///// </summary>
        //public string Text
        //{
        //    get { return GetValue(TextProperty) as string; }
        //    set { SetValue(TextProperty, value); }
        //}

        ///// <summary>
        ///// Identifies the Text dependency property.
        ///// </summary>
        //public static readonly DependencyProperty TextProperty =
        //    DependencyProperty.Register(
        //        "Text",
        //        typeof(string),
        //        typeof(HighlightingTextBlock),
        //        new PropertyMetadata(OnTextPropertyChanged));

        ///// <summary>
        ///// TextProperty property changed handler.
        ///// </summary>
        ///// <param name="d">AutoCompleteBox that changed its Text.</param>
        ///// <param name="e">Event arguments.</param>
        //private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    HighlightingTextBlock source = d as HighlightingTextBlock;

        //    if (source.TextBlock != null)
        //    {
        //        while (source.TextBlock.Inlines.Count > 0)
        //        {
        //            source.TextBlock.Inlines.RemoveAt(0);
        //        }
        //        string value = e.NewValue as string;
        //        source.Inlines = new List<Inline>();
        //        if (value != null)
        //        {
        //            for (int i = 0; i < value.Length; i++)
        //            {
        //                Inline run = new Run { Text = value[i].ToString() };
        //                source.TextBlock.Inlines.Add(run);
        //                source.Inlines.Add(run);
        //            }

        //            source.ApplyHighlighting();
        //        }
        //    }
        //}

        //#endregion public string Text

        //#region public string HighlightText
        ///// <summary>
        ///// Gets or sets the highlighted text.
        ///// </summary>
        //public string HighlightText
        //{
        //    get { return GetValue(HighlightTextProperty) as string; }
        //    set { SetValue(HighlightTextProperty, value); }
        //}

        ///// <summary>
        ///// Identifies the HighlightText dependency property.
        ///// </summary>
        //public static readonly DependencyProperty HighlightTextProperty =
        //    DependencyProperty.Register(
        //        "HighlightText",
        //        typeof(string),
        //        typeof(HighlightingTextBlock),
        //        new PropertyMetadata(OnHighlightTextPropertyChanged));

        ///// <summary>
        ///// HighlightText property changed handler.
        ///// </summary>
        ///// <param name="d">AutoCompleteBox that changed its HighlightText.</param>
        ///// <param name="e">Event arguments.</param>
        //private static void OnHighlightTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    HighlightingTextBlock source = d as HighlightingTextBlock;
        //    source.ApplyHighlighting();
        //}

        //#endregion public string HighlightText

        //#region public Brush HighlightBrush
        ///// <summary>
        ///// Gets or sets the highlight brush.
        ///// </summary>
        //public Brush HighlightBrush
        //{
        //    get { return GetValue(HighlightBrushProperty) as Brush; }
        //    set { SetValue(HighlightBrushProperty, value); }
        //}

        ///// <summary>
        ///// Identifies the HighlightBrush dependency property.
        ///// </summary>
        //public static readonly DependencyProperty HighlightBrushProperty =
        //    DependencyProperty.Register(
        //        "HighlightBrush",
        //        typeof(Brush),
        //        typeof(HighlightingTextBlock),
        //        new PropertyMetadata(null, OnHighlightBrushPropertyChanged));

        ///// <summary>
        ///// HighlightBrushProperty property changed handler.
        ///// </summary>
        ///// <param name="d">HighlightingTextBlock that changed its HighlightBrush.</param>
        ///// <param name="e">Event arguments.</param>
        //private static void OnHighlightBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    HighlightingTextBlock source = d as HighlightingTextBlock;
        //    source.ApplyHighlighting();
        //}
        //#endregion public Brush HighlightBrush

        //#region public FontWeight HighlightFontWeight
        ///// <summary>
        ///// Gets or sets the font weight used on highlighted text.
        ///// </summary>
        //public FontWeight HighlightFontWeight
        //{
        //    get { return (FontWeight)GetValue(HighlightFontWeightProperty); }
        //    set { SetValue(HighlightFontWeightProperty, value); }
        //}

        ///// <summary>
        ///// Identifies the HighlightFontWeight dependency property.
        ///// </summary>
        //public static readonly DependencyProperty HighlightFontWeightProperty =
        //    DependencyProperty.Register(
        //        "HighlightFontWeight",
        //        typeof(FontWeight),
        //        typeof(HighlightingTextBlock),
        //        new PropertyMetadata(FontWeights.Normal, OnHighlightFontWeightPropertyChanged));

        ///// <summary>
        ///// HighlightFontWeightProperty property changed handler.
        ///// </summary>
        ///// <param name="d">HighlightingTextBlock that changed its HighlightFontWeight.</param>
        ///// <param name="e">Event arguments.</param>
        //private static void OnHighlightFontWeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    HighlightingTextBlock source = d as HighlightingTextBlock;
        //    FontWeight value = (FontWeight)e.NewValue;
        //}
        //#endregion public FontWeight HighlightFontWeight

        ///// <summary>
        ///// Initializes a new HighlightingTextBlock class.
        ///// </summary>
        //public HighlightingTextBlock()
        //{
        //    DefaultStyleKey = typeof(HighlightingTextBlock);
        //    Loaded += OnLoaded;
        //}

        ///// <summary>
        ///// Loaded method handler.
        ///// </summary>
        ///// <param name="sender">The loaded event.</param>
        ///// <param name="e">The event data.</param>
        //private void OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    OnApplyTemplate();
        //}

        ///// <summary>
        ///// Override the apply template handler.
        ///// </summary>
        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();

        //    // Grab the template part
        //    TextBlock = GetTemplateChild(TextBlockName) as TextBlock;

        //    // Re-apply the text value
        //    string text = Text;
        //    Text = null;
        //    Text = text;
        //}

        ///// <summary>
        ///// Apply the visual highlighting.
        ///// </summary>
        //private void ApplyHighlighting()
        //{
        //    if (Inlines == null)
        //    {
        //        return;
        //    }

        //    string text = Text ?? string.Empty;
        //    string highlight = HighlightText ?? string.Empty;
        //    StringComparison compare = StringComparison.OrdinalIgnoreCase;

        //    int cur = 0;
        //    while (cur < text.Length)
        //    {
        //        int i = highlight.Length == 0 ? -1 : text.IndexOf(highlight, cur, compare);
        //        i = i < 0 ? text.Length : i;

        //        // Clear
        //        while (cur < i && cur < text.Length)
        //        {
        //            Inlines[cur].Foreground = Foreground;
        //            Inlines[cur].FontWeight = FontWeight;
        //            cur++;
        //        }

        //        // Highlight
        //        int start = cur;
        //        while (cur < start + highlight.Length && cur < text.Length)
        //        {
        //            Inlines[cur].Foreground = HighlightBrush;
        //            Inlines[cur].FontWeight = HighlightFontWeight;
        //            cur++;
        //        }
        //    }
        //}
    }
}
