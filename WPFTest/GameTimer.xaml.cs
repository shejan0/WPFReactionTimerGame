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
using System.Timers;
namespace WPFTest
{
    /// <summary>
    /// Interaction logic for GameTimer.xaml
    /// </summary>
    public partial class GameTimer : UserControl
    {
        Timer timer;
        int correct = 0, pressed = 0;
        int elapsed = 0;
       

        public GameTimer()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1; //1 ms response times
            timer.Elapsed += Timer_Elapsed;
            timer.Stop();
            speedLbl.Content = "Timer not running";
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            elapsed++; //increment milliseconds
            //curTimeLbl.Content = "Current time: " + elapsed + " ms";
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            updateAccuracy();
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            speedLbl.Content = "Timer started";
            pressed = 0;
            correct = 0;
            elapsed = 0;
            updateAccuracy();
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            speedLbl.Content = "Timer stopped";
        }
        
        public void inputAction(bool isCorrect) 
        {
            pressed++;
            if (isCorrect)
            {
                correct++;
            }
            updateSpeed();
            updateAccuracy();
        }
       void updateSpeed()
        {
            if (timer.Enabled)
            {
                Action a = () => { speedLbl.Content = "Speed: " + elapsed + "ms"; };
                speedLbl.Dispatcher.Invoke(a);
                elapsed = 0;
            }
            
        }
        void updateAccuracy()
        {
            if (pressed != 0)
            {
                Action a = () => { accuracyLbl.Content = "Accuracy: " + (((double)correct) / pressed) * 100 + "%"; };
                accuracyLbl.Dispatcher.Invoke(a);
            }
            else
            {
                Action a =() => { accuracyLbl.Content = "No accuracy recorded"; };
                accuracyLbl.Dispatcher.Invoke(a);
                
            }
            
        }
    }
}
