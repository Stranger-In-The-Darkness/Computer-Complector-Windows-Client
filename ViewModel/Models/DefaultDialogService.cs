using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;
using System.Windows;

using ViewModel.Interfaces;

namespace ViewModel.Models
{
    public class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                DefaultExt = ".json",
                Filter = "XML-file (*.xml)|*.xml|JSON-file (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 4
            };
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                DefaultExt = ".json",
                Filter = "XML-file (*.xml)|*.xml|JSON-file (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 4
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ShowMessage(string message, string caption)
        {
            MessageBox.Show(message, caption);
        }

        public void ShowMessage(string message, string caption, MessageBoxButton button, Action<MessageBoxResult> action)
        {
            var res = MessageBox.Show(message,caption, button);
            action?.Invoke(res);
        }

        public void ShowMessage(string message, string caption, MessageBoxButton button, MessageBoxImage icon, Action<MessageBoxResult> action)
        {
            var res = MessageBox.Show(message, caption, button, icon);
            action?.Invoke(res);
        }

        public void ShowMessage(string message, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, Action<MessageBoxResult> action)
        {
            var res = MessageBox.Show(message, caption, button, icon, defaultResult);
            action?.Invoke(res);
        }

        public void ShowMessage(string message, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options, Action<MessageBoxResult> action)
        {
            var res = MessageBox.Show(message, caption, button, icon, defaultResult, options);
            action?.Invoke(res);
        }
    }
}
