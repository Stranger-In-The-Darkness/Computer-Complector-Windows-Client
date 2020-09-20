//#define MOCK

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

using System.Net.Http;
using System.Net;
using System.IO;
using ViewModel;
using Model;

#if MOCK
using VM = ViewModel.ViewModelMock;
#else
using VM = ViewModel.ViewModel;
#endif

namespace Computer_Complector
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Expander _currentlyOpened = null;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new VM(
                new ViewModel.Models.DefaultDialogService(), 
                Properties.Settings.Default.APIURIString,
				Properties.Settings.Default.ComponentsRequestFormat,
				Properties.Settings.Default.StatisticsRequestFormat,
                Properties.Settings.Default.Culture);            
        }

        private void ToggleFilter(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;
            (DataContext as VM)?.SelectFilter.Execute(new Tuple<object, object>(check.Tag.ToString(), check.Content));
        }

        private void ClearSelection(object sender, RoutedEventArgs e)
        {
            FiledsListBox.ItemsSource = null;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void FullscreenBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void ClearSelectedFiltersBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var p = (Grid)btn.Parent;
            p.Children.OfType<ComboBox>().First().SelectedIndex = -1;
        }

        private void SelectItemBtn_Click(object sender, RoutedEventArgs e)
        {
            ((VM)DataContext).SelectItem.Execute((int)((Button)sender).Tag);
        }

        private void DeselectItemBtn_Click(object sender, RoutedEventArgs e)
        {
            ((VM)DataContext).DeselectItem.Execute(((Button)sender).Tag);
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            var exp = sender as Expander;
            if (exp != null)
            {
                if (_currentlyOpened != null)
                {
                    if (_currentlyOpened != exp)
                    {
                        _currentlyOpened.IsExpanded = false;
                        _currentlyOpened = exp;
                    }
                }
                else
                {
                    _currentlyOpened = exp;
                }
            }
        }

		//private void LogInOutBtn_Click(object sender, RoutedEventArgs e)
		//{
		//	if (userPanel.DataContext is User currentUser)
		//	{
		//		DataContext = (DataContext as AdminViewModel).ToViewModel();
		//		userPanel.DataContext = null;
		//		logInOutBtn.Content = "Login";
		//	}
		//	else
		//	{
		//		var login = new LoginWindow();
		//		login.Closing += (send, args) =>
		//		{
		//			if (login.IsSuccess)
		//			{
		//				var user = (login.DataContext as LoginViewModel)?.User;
		//				if (user != null)
		//				{
		//					if (user.Role.ToUpper() == "ADMIN")
		//					{
		//						DataContext = (DataContext as VM).ToAdminViewModel();
		//						(DataContext as AdminViewModel).User = user;
		//					}
		//					else
		//					{
		//						(DataContext as VM).User = user;
		//					}
		//					userPanel.DataContext = user;
		//					logInOutBtn.Content = "Logout";
		//				}
		//			}
		//		};

		//		login.ShowDialog();
		//	}
		//}
	}
}