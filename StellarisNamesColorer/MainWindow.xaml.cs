using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StellarisNamesColorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<Color, string> _colorsMap = new Dictionary<Color, string>()
        {
            [Colors.Blue] = "\u0011B^\u0011!",
            [Colors.Teal] = "\u0011T^\u0011!",
            [Colors.Green] = "\u0011G^\u0011!",
            [Colors.Orange] = "\u0011O^\u0011!",
            [Colors.Brown] = "\u0011L^\u0011!",
            [Colors.Purple] = "\u0011M^\u0011!",
            [Colors.Pink] = "\u0011P^\u0011!",
            [Colors.Red] = "\u0011R^\u0011!",
            [Colors.DarkOrange] = "\u0011S^\u0011!",
            [Colors.LightGray] = "\u0011T^\u0011!",
            [Colors.White] = "\u0011W^\u0011!",
            [Colors.Yellow] = "\u0011Y^\u0011!",
        };

        private Color _currentColor;

        public MainWindow()
        {
            InitializeComponent();

            AssignColors();

            CopyButton.Click += OnCopyButtonClicked;
        }

        private void AssignColors()
        {
            LinearGradientBrush backGroundBrush = new LinearGradientBrush();
            backGroundBrush.StartPoint = new Point(0, 0);
            backGroundBrush.EndPoint = new Point(1, 1);
            backGroundBrush.GradientStops.Add(new GradientStop(Colors.White, 0.0));
            backGroundBrush.GradientStops.Add(new GradientStop(Colors.DarkBlue, 1));
            ColorSelection.Background = backGroundBrush;
            WindowObject.Background = backGroundBrush;

            foreach (var item in _colorsMap)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = GetColorName(item.Key);
                Brush brush = new SolidColorBrush(item.Key);
                textBlock.Foreground = brush;
                ColorSelection.Items.Add(textBlock);
            }

            ColorSelection.SelectionChanged += (object sender, SelectionChangedEventArgs e) =>
            {
                string colorText = ((TextBlock)ColorSelection.SelectedItem).Text;
                var color = (Color)ColorConverter.ConvertFromString(colorText);
                TextField.Foreground = new SolidColorBrush(color);
            };
        }

        static string GetColorName(Color col)
        {
            PropertyInfo colorProperty = typeof(Colors).GetProperties()
                .FirstOrDefault(p => Color.AreClose((Color)p.GetValue(null), col));
            return colorProperty != null ? colorProperty.Name : "unnamed color";
        }

        [STAThread]
        private void OnCopyButtonClicked(object sender, RoutedEventArgs e)
        {
            string colorText = ((TextBlock)ColorSelection.SelectedItem).Text;
            var color = (Color)ColorConverter.ConvertFromString(colorText);
            string[] specialSymbols = _colorsMap[color].Split("^");

            string result = specialSymbols[0] + TextField.Text + specialSymbols[1];
            Clipboard.SetText(result);
        }
    }
}