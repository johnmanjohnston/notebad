using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace txteditor
{
    public partial class MainWindow : Window
    {
        private string? currentFilePath = null;

        public MainWindow()
        {
            InitializeComponent();
            this.SizeChanged += WindowSizeChanged;
            this.textboxmain.TextChanged += TextboxChange;
            this.KeyDown += OnKeyDown;

            this.savebtn.Click += Save;
            this.saveasbtn.Click += SaveAs;
            this.openbtn.Click += Open;
        }

        public void TextboxChange(object sender, TextChangedEventArgs e)
        {
            if (linecounterlabel != null && textboxmain != null)
            {
                if (currentFilePath == null) 
                {
                    savelabel.Text = "Unsaved Changes";
                }

                int lCount = textboxmain.LineCount;

                linecounterlabel.Text = $"{lCount} Line";
                if (lCount != 1) { linecounterlabel.Text += "s"; }

                savelabel.Text = "Unsaved Changes";
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

        private void Save(object sender, EventArgs e) 
        {
            if (currentFilePath == null) 
            {
                MessageBox.Show("No file loaded :/");
                return;
            }

            File.WriteAllText(currentFilePath, textboxmain.Text);
            savelabel.Text = "Saved";
        }

        private void SaveAs(object sender, EventArgs e) 
        {
        }

        private void Open(object sender, EventArgs e) 
        {
            var dialog = new OpenFileDialog();

            bool? res = dialog.ShowDialog();

            if (res == true) 
            {
                string filePath = dialog.FileName;
                textboxmain.Text = File.ReadAllText(filePath);
                currentFilePath = filePath;
                savelabel.Text = "Saved";
            }
        }
    }
}

