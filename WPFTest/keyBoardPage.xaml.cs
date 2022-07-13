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

namespace WPFTest
{
    /// <summary>
    /// Interaction logic for keyBoardPage.xaml
    /// </summary>
    public partial class keyBoardPage : Page
    {
        Random rand;
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char nextchar;
        public keyBoardPage()
        {
            InitializeComponent();
            rand = new Random();
        }

        private void KeyBoardPage_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.Key);
            keyPressLbl.Content = e.Key;
            timerControl.inputAction(e.Key.ToString()[0] == nextchar);
            getNextRandomChar();
            
        }
        char getNextRandomChar()
        {
            nextchar = chars[rand.Next(chars.Length)];
            nextKeyLbl.Content = "Next Key: "+nextchar;
            return nextchar;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).KeyUp += KeyBoardPage_KeyUp;
            getNextRandomChar();
        }
    }
}
