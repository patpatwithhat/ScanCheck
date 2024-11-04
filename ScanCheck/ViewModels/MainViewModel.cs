using Caliburn.Micro;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ScanCheck.ViewModels
{
    public class MainViewModel : Screen
    {
        #region Properties
        private string? _selectedFolderPath;

        public string? SelectedFolderPath
        {
            get => _selectedFolderPath;
            set
            {
                _selectedFolderPath = value;
                NotifyOfPropertyChange();
            }
        }
        #endregion


        #region Button Actions
        public void BrowseFolder()
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = "Select a Folder"
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SelectedFolderPath = dialog.FileName;
            }
        }
        #endregion

    }
}
