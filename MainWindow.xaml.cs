using System.Windows;
using System.Windows.Controls;

namespace WorkshopSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            vm.Register();
        }
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            vm.ClearAll();
        }

        //common function for list box change event
        private void Lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.ShowCustomMessage();
        }
    }
}
