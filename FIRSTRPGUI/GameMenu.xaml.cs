using GameEngine.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameEngine.EventArgs;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FIRSTRPGUI
{
    /// <summary>
    /// Interaction logic for GameMenu.xaml
    /// </summary>
    /// 

    public partial class GameMenu : Window
    {

        public GameSession Session => DataContext as GameSession;
        private readonly PopupQuestionArgs PopupQues = new PopupQuestionArgs();
        Window mW;
        //public MediaPlayer mPlayer => MP as MediaPlayer();
        public GameMenu()
        {
            InitializeComponent();
        }

        public Window SetMW
        {
            get { return mW; }
            set { mW = value; }
        }


        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ResetGame(object sender, RoutedEventArgs e)
        {
            bool ResetAns = PopupQues.ResetQuestion();

            if (ResetAns)
            {
                //mW.mediaPlayer.Stop();
                MainWindow window2 = new MainWindow();
                window2.Show();
                mW.Close();
            }
                 

        }

        private void ExitGame(object sender, RoutedEventArgs e)
        {

                mW.Close();   

        }


    }
}
