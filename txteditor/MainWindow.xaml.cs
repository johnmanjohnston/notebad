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
using txteditor.Utility;

namespace txteditor
{
    public partial class MainWindow : Window
    {
        private string? currentFilePath = null;
        bool saved = false;

        public MainWindow()
        {
            InitializeComponent();

            saved = false;



            this.SizeChanged             += WindowSizeChanged;
            this.textboxmain.TextChanged += TextboxChange;
            this.KeyDown                 += OnKeyDown;
            this.KeyDown                 += TextboxScroll;
            this.MouseWheel              += TextboxScroll;

            this.savebtn.Click   += Save;
            this.saveasbtn.Click += SaveAs;
            this.openbtn.Click   += Open;
            this.newbtn.Click    += New;

            this.textboxmain.Text = string.Empty;

            sidelinecoutner.Text = "1";
        }

        public void TextboxChange(object sender, TextChangedEventArgs e)
        {
            if (linecounterlabel != null && textboxmain != null)
            {
                if (currentFilePath == null)
                {
                    savelabel.Text = "Unsaved Changes";
                }

                // Line counter
                int lCount = textboxmain.LineCount;

                linecounterlabel.Text = $"{lCount} Line";
                if (lCount != 1) { linecounterlabel.Text += "s"; }

                savelabel.Text = "Unsaved Changes";
                saved = false;

                // Side line counter
                sidelinecoutner.Text = string.Empty;
                for (int i = 1; i <= lCount; i++)
                {
                    sidelinecoutner.Text += $"{i}\n";
                }

                sidelinecounterscroll.ScrollToVerticalOffset(textboxmain.VerticalOffset);
            }
        }

        private void TextboxScroll(object sender, EventArgs e)
        {
            sidelinecounterscroll.ScrollToVerticalOffset(textboxmain.VerticalOffset);
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Prevent textbox overflow from window
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
                    sidelinecoutner.FontSize += 2;
                }

                if (Keyboard.IsKeyDown(Key.OemMinus))
                {
                    textboxmain.FontSize -= 2;
                    sidelinecoutner.FontSize -= 2;
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
            saved = true;
        }

        private void SaveAs(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();

            bool? res = dialog.ShowDialog();

            if (res == true) 
            {
                string filePath = dialog.FileName;
                File.WriteAllText(filePath, textboxmain.Text);
                currentFilePath = filePath;
                savelabel.Text = "Saved";
                saved = true;
            }
        }

        private void Open(object sender, EventArgs e)
        {
            if (!saved && textboxmain.Text != string.Empty) 
            {
                StaticUtility.UnsavedWarning();
                return;
            }

            var dialog = new OpenFileDialog();

            bool? res = dialog.ShowDialog();

            if (res == true)
            {
                string filePath = dialog.FileName;
                textboxmain.Text = File.ReadAllText(filePath);
                currentFilePath = filePath;
                savelabel.Text = "Unchanged";
                saved = true;
            }
        }

        private void New(object sender, EventArgs e) 
        {
            if (!saved && textboxmain.Text != string.Empty) 
            {
                StaticUtility.UnsavedWarning();
                return;
            }

            textboxmain.Text = string.Empty; 
            currentFilePath = null;
        }

        private void textboxmain_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
             sidelinecounterscroll.ScrollToVerticalOffset(textboxmain.VerticalOffset);
        }
    }
}

