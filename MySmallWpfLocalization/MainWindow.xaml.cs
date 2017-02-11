/******************************************************************************/ 
/*                                                                            */
/*   Program: MySmallWpfLocalization                                          */
/*   A little demo for localization in a Wpf project                          */ 
/*                                                                            */ 
/*   11.02.2017 1.0.0.0 uhwgmxorg Start                                       */ 
/*                                                                            */ 
/******************************************************************************/ 
using System.Windows;

namespace MySmallWpfLocalization
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
        }
    }
}
