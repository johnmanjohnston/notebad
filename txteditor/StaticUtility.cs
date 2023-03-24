using System.Windows;

namespace txteditor.Utility
{
    static class StaticUtility
    {
        public static void UnsavedWarning() 
        {
            MessageBox.Show("You have unsaved changes, save before creating a new file!",
                                    "Notebad - Unsaved Changes",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
        }
    }
}
