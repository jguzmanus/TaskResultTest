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

namespace TaskResultTest
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public static readonly DependencyProperty WindowContextProperty = DependencyProperty.Register(
         "WindowContext", typeof(IWindowContext), typeof(MainWindow), new PropertyMetadata(default(IWindowContext)));

      public IWindowContext WindowContext
      {
         get { return (IWindowContext) GetValue(WindowContextProperty); }
         set { SetValue(WindowContextProperty, value); }
      }
      public MainWindow()
      {
         InitializeComponent();
         DataContext = new WindowContext();
      }
   }
}
