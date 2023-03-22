using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace txteditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SizeChanged += WindowSizeChanged;
            this.textboxmain.TextChanged += TextboxChange;
            this.KeyDown += OnKeyDown;
        }

        public void TextboxChange(object sender, TextChangedEventArgs e)
        {
            if (linecounterlabel != null && textboxmain != null)
            {
                linecounterlabel.Text = textboxmain.LineCount.ToString() + " Line(s)";
            }
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            textboxmain.Height = e.NewSize.Height - 120;
            textboxmain.Width = e.NewSize.Width - 19;
        }

        private void OnKeyDown(object sender, KeyEventArgs e) 
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) 
            {
                if (Keyboard.IsKeyDown(Key.OemPlus)) 
                {
                    textboxmain.FontSize += 2;
                }

                if (Keyboard.IsKeyDown(Key.OemMinus)) 
                {
                    textboxmain.FontSize -= 2; 
                }
            }
        }
    }
}
