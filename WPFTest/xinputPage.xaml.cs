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
using SharpDX.XInput;
using SharpDX;

namespace WPFTest
{
    /// <summary>
    /// Interaction logic for xinputPage.xaml
    /// </summary>
    public partial class xinputPage : Page
    {
        Timer timer;
        Random rand;
        private Controller[] controllers = { new Controller(UserIndex.One), new Controller(UserIndex.Two), new Controller(UserIndex.Three), new Controller(UserIndex.Four) };
        //private GamepadButtonFlags[] gamepadButtons = { GamepadButtonFlags.DPadUp, GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadLeft, GamepadButtonFlags.DPadRight, GamepadButtonFlags.Start, GamepadButtonFlags.Back, GamepadButtonFlags.LeftThumb, GamepadButtonFlags.RightThumb, GamepadButtonFlags.LeftShoulder, GamepadButtonFlags.RightShoulder,GamepadButtonFlags.A, GamepadButtonFlags.B, GamepadButtonFlags.X, GamepadButtonFlags.Y };
        private GamepadKeyCode[] gamepadKeyCodes = { GamepadKeyCode.DPadUp, GamepadKeyCode.DPadDown, GamepadKeyCode.DPadLeft, GamepadKeyCode.DPadRight, GamepadKeyCode.Start, GamepadKeyCode.Back, GamepadKeyCode.LeftThumbPress, GamepadKeyCode.LeftThumbDown, GamepadKeyCode.LeftThumbDownright, GamepadKeyCode.LeftThumbLeft, GamepadKeyCode.LeftThumbRight, GamepadKeyCode.LeftThumbUp, GamepadKeyCode.LeftThumbUpright,GamepadKeyCode.RightThumbPress, GamepadKeyCode.RightThumbDown, GamepadKeyCode.RightThumbDownRight, GamepadKeyCode.RightThumbDownleft, GamepadKeyCode.RightThumbRight, GamepadKeyCode.RightThumbUp, GamepadKeyCode.RightThumbUpRight, GamepadKeyCode.RightThumbUpLeft, GamepadKeyCode.LeftShoulder, GamepadKeyCode.RightShoulder, GamepadKeyCode.A, GamepadKeyCode.B, GamepadKeyCode.X, GamepadKeyCode.Y };

        GamepadKeyCode nextKey;  
        public xinputPage()
        {
            InitializeComponent();
            rand = new Random();
            timer = new Timer();
            timer.Interval = 100;
            timer.Elapsed += Timer_Elapsed;
            timer.Stop();
           
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                foreach (Controller c in controllers)
                {
                    if (c.IsConnected)
                    {
                        State state = c.GetState();
                        Result r = c.GetKeystroke(DeviceQueryType.Gamepad,out Keystroke keystroke);
                        if (r.Success)
                        {
                            if((keystroke.Flags & (KeyStrokeFlags.KeyUp))!=0)
                            {
                                Action a = () => { buttonPressLbl.Content = keystroke.VirtualKey; };
                                Console.WriteLine(keystroke.VirtualKey);
                                buttonPressLbl.Dispatcher.Invoke(a);
                                //timerControl.inputAction((keystroke.VirtualKey & nextKey) != 0);
                                timerControl.inputAction(keystroke.VirtualKey==nextKey);
                                getNextKey();
                            }
                        }
                        /*
                        //buttonPressLbl.Content = state.ToString();
                        //Console.WriteLine(state.Gamepad.ToString());
                        //Console.WriteLine(state.Gamepad.Buttons.ToString());
                        if (state.Gamepad.Buttons != GamepadButtonFlags.None) //no buttons are pressed
                        {
                            if ((state.Gamepad.Buttons & nextButton) != 0)
                            {
                                c.GetKeystroke(DeviceQueryType.Gamepad,out Keystroke key);
                                GamepadKeyCode.
                            }
                            Action a = () => { buttonPressLbl.Content = state.Gamepad.Buttons; };
                            buttonPressLbl.Dispatcher.Invoke(a);
                            getNextButton();
                        }
                        */
                    }
                }
            }catch(Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            
        }
        GamepadKeyCode getNextKey()
        {
            //have to run on the dispatcher thread since the GUI is owned by a different thread
            nextKey= gamepadKeyCodes[rand.Next(gamepadKeyCodes.Length)];
            Action a = () => { nextButtonLbl.Content = "Next Action: "+nextKey.ToString(); };
            nextButtonLbl.Dispatcher.Invoke(a);
            //nextButtonLbl.Content = nextButton.ToString();
            return nextKey;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
            getNextKey();
        }
    }
}
