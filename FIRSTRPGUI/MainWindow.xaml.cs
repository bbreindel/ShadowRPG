using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameEngine.EventArgs;
using GameEngine.Factories;
using GameEngine.GameModels;
using GameEngine.Services;
using GameEngine.ViewModels;
using RadioButton = System.Windows.Controls.RadioButton;

namespace FIRSTRPGUI
{


    public partial class MainWindow : Window
    {
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        private readonly GameSession _gameSession = new GameSession();
        readonly MediaPlayer mediaPlayer = new MediaPlayer();
        private readonly Dictionary<Key, Action> _userInputActions =
            new Dictionary<Key, Action>();

        private readonly PopupQuestionArgs PopupQues = new PopupQuestionArgs();
        

        public event EventHandler MediaEnded;


        public MainWindow()
        {
            InitializeComponent();
            Music_onLoad();
            InitializeUserInputActions();

            _messageBroker.OnMessageRaised += OnGameMessageRaised;

            DataContext = _gameSession;

            RaceRadioButtonSelect.Visibility = Visibility.Collapsed;
            MainGameWindow.Visibility = Visibility.Collapsed;
            AttributePointAssignment.Visibility = Visibility.Collapsed;
            CharacterCreateWindow.Visibility = Visibility.Collapsed;
            ClassRadioButtonSelect.Visibility = Visibility.Collapsed;



        }
        private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveNorth();
        }

        private void OnClick_MoveWest(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveWest();
        }

        private void OnClick_MoveEast(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveEast();
        }

        private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveSouth();
        }

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }

        private void OnClick_UseCurrentConsumable(object sender, RoutedEventArgs e)
        {
            _gameSession.UseCurrentConsumable();
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }

        private void OnClick_DisplayTradeScreen(object sender, RoutedEventArgs e)
        {
            if (_gameSession.CurrentTrader != null)
            {
                TradeScreen tradeScreen = new TradeScreen
                {
                    Owner = this,
                    DataContext = _gameSession
                };
                tradeScreen.ShowDialog();
            }
        }

        private void OnClick_DisplayGameMenuScreen(object sender, RoutedEventArgs e)
        {
            GameMenu gamemenu = new GameMenu
            {
                Owner = this,
                DataContext = _gameSession,
                SetMW = this,
                //mPlayer = mediaPlayer    
                };
                gamemenu.ShowDialog();
        }

        private void OnClick_Craft(object sender, RoutedEventArgs e)
        {
            Recipe recipe = ((FrameworkElement)sender).DataContext as Recipe;
            _gameSession.CraftItemUsing(recipe);
        }

        private void InitializeUserInputActions()
        {
            _userInputActions.Add(Key.W, () => _gameSession.MoveNorth());
            _userInputActions.Add(Key.A, () => _gameSession.MoveWest());
            _userInputActions.Add(Key.S, () => _gameSession.MoveSouth());
            _userInputActions.Add(Key.D, () => _gameSession.MoveEast());
            _userInputActions.Add(Key.Z, () => _gameSession.AttackCurrentMonster());
            _userInputActions.Add(Key.C, () => _gameSession.UseCurrentConsumable());
            _userInputActions.Add(Key.I, () => SetTabFocusTo("InventoryTabItem"));
            _userInputActions.Add(Key.Q, () => SetTabFocusTo("QuestsTabItem"));
            _userInputActions.Add(Key.R, () => SetTabFocusTo("RecipesTabItem"));
            _userInputActions.Add(Key.E, () => SetTabFocusTo("EquippedTabItem"));
            _userInputActions.Add(Key.T, () => OnClick_DisplayTradeScreen(this, new RoutedEventArgs()));
            _userInputActions.Add(Key.Escape, () => OnClick_DisplayGameMenuScreen(this, new RoutedEventArgs()));
        }

        private void MainWindow_OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (_userInputActions.ContainsKey(e.Key))
            {
                _userInputActions[e.Key].Invoke();
            }
        }

        private void SetTabFocusTo(string tabName)
        {
            foreach (object item in PlayerDataTabControl.Items)
            {
                if (item is TabItem tabItem)
                {
                    if (tabItem.Name == tabName)
                    {
                        tabItem.IsSelected = true;
                        return;
                    }
                }
            }
        }
        private void OnClick_DisplayCreateCharacterWindow(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Open(new Uri("file:///C:/Users/Bill/Projects/FIRSTRPG/FIRSTRPG/GameEngine/Music/Tina.MP3"));
            mediaPlayer.Play();
            StartGameWindow.Visibility = Visibility.Collapsed;
            StartGameButton.Visibility = Visibility.Collapsed;
            NextStep.Visibility = Visibility.Collapsed;
            CharacterCreateWindow.Visibility = Visibility.Visible;
            RaceInfoGoesHere.Visibility = Visibility.Collapsed;
            ClassRadioButtonSelect.Visibility = Visibility.Collapsed;
        }

        private void OnClick_CloseStartupWindow(object sender, RoutedEventArgs e)
        {

            Close();

        }

        private void Music_onLoad()
        {

            mediaPlayer.Open(new Uri("file:///C:/Users/Bill/Projects/FIRSTRPG/FIRSTRPG/GameEngine/Music/Opening.MP3"));
            mediaPlayer.Play();


        }
        private void OnClick_DisplayMainWindow(object sender, RoutedEventArgs e)
        {

            AttributePointAssignment.Visibility = Visibility.Collapsed;
            MainGameWindow.Visibility = Visibility.Visible;
        }


        private void OnClick_CloseWindow(object sender, RoutedEventArgs e)
        {

            Close();

        }



        private void OnClickAddAttribute(object sender, RoutedEventArgs e)
        {

            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
            string AttName = (string)btn.Tag;

            if (AttName=="Strength")
            {
                _gameSession.CurrentPlayer.Strength++;
                if (_gameSession.CurrentPlayer.Strength == 18)
                {
                    AddStrength.Visibility = Visibility.Collapsed;
                }
            }
            else if (AttName == "Intelligence") 
            {
                _gameSession.CurrentPlayer.Intelligence++;
                if (_gameSession.CurrentPlayer.Intelligence == 18)
                {
                    AddIntelligence.Visibility = Visibility.Collapsed;
                }
            }
            else if (AttName == "Wisdom")
            {
                _gameSession.CurrentPlayer.Wisdom++;
                if (_gameSession.CurrentPlayer.Wisdom == 18)
                {
                    AddWisdom.Visibility = Visibility.Collapsed;
                }
            }
            else if (AttName == "Constitution")
            {
                _gameSession.CurrentPlayer.Constitution++;
                if (_gameSession.CurrentPlayer.Constitution == 18)
                {
                    AddConstitution.Visibility = Visibility.Collapsed;
                }
            }
            /* else if (AttName == "Dexterity")
            {
            _gameSession.CurrentPlayer.Dexterity++;
            if (_gameSession.CurrentPlayer.Dexterity == 18)
            {
                AddDexterity.Visibility = Visibility.Collapsed;
            }
            
            } */
            else if (AttName == "Charisma")
            {
                _gameSession.CurrentPlayer.Charisma++;
                if (_gameSession.CurrentPlayer.Charisma == 18)
                {
                    AddCharisma.Visibility = Visibility.Collapsed;
                }
            }

            _gameSession.CurrentPlayer.AttributePoints--;

            if (_gameSession.CurrentPlayer.AttributePoints == 0)
            {
                AddStrength.Visibility = Visibility.Collapsed;
                AddIntelligence.Visibility = Visibility.Collapsed;
                AddWisdom.Visibility = Visibility.Collapsed;
                AddConstitution.Visibility = Visibility.Collapsed;
                AddDexterity.Visibility = Visibility.Collapsed;
                AddCharisma.Visibility = Visibility.Collapsed;
            }



            if (_gameSession.CurrentPlayer.AttributePoints <= _gameSession.CurrentPlayer.MaxAttributePoints)
            {
                if (_gameSession.CurrentPlayer.Strength > 1)
                {
                    MinusStrength.Visibility = Visibility.Visible;
                }
                if (_gameSession.CurrentPlayer.Intelligence > 1)
                {
                    MinusIntelligence.Visibility = Visibility.Visible;
                }
                if (_gameSession.CurrentPlayer.Wisdom > 1)
                {
                    MinusWisdom.Visibility = Visibility.Visible;
                }
                if (_gameSession.CurrentPlayer.Constitution > 1)
                {
                    MinusConstitution.Visibility = Visibility.Visible;
                }
                if (_gameSession.CurrentPlayer.Dexterity > 1)
                {
                    MinusDexterity.Visibility = Visibility.Visible;
                }
                if (_gameSession.CurrentPlayer.Charisma > 1)
                {
                    MinusCharisma.Visibility = Visibility.Visible;
                }
            }



           

        }


        private void OnClickMinusAttribute(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = (System.Windows.Controls.Button)sender;
            string AttName = (string)btn.Tag;

            if (AttName == "Strength")
            {
                _gameSession.CurrentPlayer.Strength--;
                if (_gameSession.CurrentPlayer.Strength == 1)
                {
                    MinusStrength.Visibility = Visibility.Collapsed;
                }
            }
            else if (AttName == "Intelligence")
            {
                _gameSession.CurrentPlayer.Intelligence--;
                if (_gameSession.CurrentPlayer.Intelligence == 1)
                {
                    MinusIntelligence.Visibility = Visibility.Collapsed;
                }
            }
            else if (AttName == "Wisdom")
            {
                _gameSession.CurrentPlayer.Wisdom--;
                if (_gameSession.CurrentPlayer.Wisdom == 1)
                {
                    MinusWisdom.Visibility = Visibility.Collapsed;
                }
            }
            else if (AttName == "Constitution")
            {
                _gameSession.CurrentPlayer.Constitution--;
                if (_gameSession.CurrentPlayer.Constitution == 1)
                {
                    MinusConstitution.Visibility = Visibility.Collapsed;
                }
            }
            /* else if (AttName == "Dexterity")
            {
            _gameSession.CurrentPlayer.Dexterity--;
            if (_gameSession.CurrentPlayer.Dexterity == 1)
            {
                MinusDexterity.Visibility = Visibility.Collapsed;
            }
            
            } */
            else if (AttName == "Charisma")
            {
                _gameSession.CurrentPlayer.Charisma--;
                if (_gameSession.CurrentPlayer.Charisma == 1)
                {
                    MinusCharisma.Visibility = Visibility.Collapsed;
                }
            }

            _gameSession.CurrentPlayer.AttributePoints++;

            if (_gameSession.CurrentPlayer.AttributePoints == _gameSession.CurrentPlayer.MaxAttributePoints)
            {
                MinusStrength.Visibility = Visibility.Collapsed;
                MinusIntelligence.Visibility = Visibility.Collapsed;
                MinusWisdom.Visibility = Visibility.Collapsed;
                MinusConstitution.Visibility = Visibility.Collapsed;
                MinusDexterity.Visibility = Visibility.Collapsed;
                MinusCharisma.Visibility = Visibility.Collapsed;
            }

            if (_gameSession.CurrentPlayer.AttributePoints > 0)
            {
                if (_gameSession.CurrentPlayer.Strength < 18)
                {
                    AddStrength.Visibility = Visibility.Visible;
                }
                if (_gameSession.CurrentPlayer.Intelligence < 18)
                {
                    AddIntelligence.Visibility = Visibility.Visible;
                }
                if (_gameSession.CurrentPlayer.Wisdom < 18)
                {
                    AddWisdom.Visibility = Visibility.Visible;
                }
                if (_gameSession.CurrentPlayer.Constitution < 18)
                {
                    AddConstitution.Visibility = Visibility.Visible;
                }
                if (_gameSession.CurrentPlayer.Dexterity < 18)
                {
                    AddDexterity.Visibility = Visibility.Visible;
                }
                if (_gameSession.CurrentPlayer.Charisma < 18)
                {
                    AddCharisma.Visibility = Visibility.Visible;
                }
            }
        }
 

        private void OnSellectSex(Object sender, RoutedEventArgs e)
        {

            System.Windows.Controls.RadioButton btn = (System.Windows.Controls.RadioButton)sender;
            string CharSex = (string)btn.Content;

            _gameSession.CurrentPlayer.Sex = CharSex;

            NextStep.Visibility = Visibility.Visible;

           


        }

        private void UnsellectSex(object sender, RoutedEventArgs e)
        {


        }

        private void CreateCharacterNextStep(object sender, RoutedEventArgs e)
        {



            if (SexRadioButtonSelect.IsVisible)
            {

                //  bool Ans = PopupQues.CharacterCreateContinue(_gameSession.CurrentPlayer.Sex);

                YesNoWindow message =
    new YesNoWindow("Confirm Delete", "Are you sure you want to delete this account?");
                message.Owner = Window.GetWindow(this);
                message.ShowDialog();

                if (message.ClickedYes)
                {
                    CreateRaceButtons();
                    SexRadioButtonSelect.Visibility = Visibility.Collapsed;
                    RaceRadioButtonSelect.Visibility = Visibility.Visible;
                    NextStep.Visibility = Visibility.Collapsed;
                }

            }
            else if (RaceRadioButtonSelect.IsVisible)
            {
                //bool Ans = PopupQues.CharacterCreateContinue(_gameSession.CurrentPlayer.Race);
                YesNoWindow message =
    new YesNoWindow("Confirm Delete", "Are you sure you want to delete this account?");
                message.Owner = Window.GetWindow(this);
                message.ShowDialog();


                if (message.ClickedYes)
                {
                    CreateClassButtons();
                    RaceRadioButtonSelect.Visibility = Visibility.Collapsed;
                    ClassRadioButtonSelect.Visibility = Visibility.Visible;
                    NextStep.Visibility = Visibility.Collapsed;
                }

            }
            else if (ClassRadioButtonSelect.IsVisible)
            {
                // bool Ans = PopupQues.CharacterCreateContinue(_gameSession.CurrentPlayer.CharacterClass);

                YesNoWindow message =
    new YesNoWindow("Confirm Delete "," Are you sure you want to delete this account?");
                message.Owner = Window.GetWindow(this);
                message.ShowDialog();

                if (message.ClickedYes)
                {
                    ClassRadioButtonSelect.Visibility = Visibility.Collapsed;
                    AttributePointAssignment.Visibility = Visibility.Visible;
                    StartGameButton.Visibility = Visibility.Visible;
                    NextStep.Visibility = Visibility.Collapsed;
                }

            }
        }


        /*  private void Unsellect_female(object sender, RoutedEventArgs e)
          {

              _gameSession.CurrentPlayer.Charisma -= 2;
            //  _gameSession.CurrentPlayer.Dexterity--;
              _gameSession.CurrentPlayer.Wisdom--;

              while (_gameSession.CurrentPlayer.Charisma < 1 || _gameSession.CurrentPlayer.Dexterity < 1 || _gameSession.CurrentPlayer.Wisdom < 1)
              {
                  if (_gameSession.CurrentPlayer.Charisma < 1)
                  {
                      _gameSession.CurrentPlayer.Charisma++;
                      _gameSession.CurrentPlayer.AttributePoints--;
                  }
                  if (_gameSession.CurrentPlayer.Dexterity < 1)
                  {
                      _gameSession.CurrentPlayer.Dexterity++;
                      _gameSession.CurrentPlayer.AttributePoints--;
                  } 
                  if (_gameSession.CurrentPlayer.Wisdom < 1)
                  {
                      _gameSession.CurrentPlayer.Wisdom++;
                      _gameSession.CurrentPlayer.AttributePoints--;
                  }
                  // add a function to test all and minus if ap < 0

              }
          }
          private void Unsellect_male(object sender, RoutedEventArgs e)
          {

              _gameSession.CurrentPlayer.Strength -= 2;
              _gameSession.CurrentPlayer.Constitution -= 2;
          } */

        private void CreateRaceButtons()
        {
            int j = 1;

            foreach (CharacterRace cs in _gameSession.AllRaces)
            {


                RadioButton MyControl = new RadioButton();
                MyControl.Content = cs.Name.ToString();
                MyControl.Name = "Button" + cs.Name.ToString();
                MyControl.Click += new RoutedEventHandler(OnSellectRace);
                MyControl.Unchecked += new RoutedEventHandler(UnsellectRace);
                Grid.SetRow(MyControl, j);
                Grid.SetColumn(MyControl, 1);
                RaceRadioButtonSelect.Children.Add(MyControl);
                j++;


            }

        }


        private void CreateClassButtons()
        {
            int j = 1;

            foreach (CharacterClass cc in _gameSession.AllClasses)
            {


                RadioButton MyControl = new RadioButton();
                MyControl.Content = cc.Name.ToString();
                MyControl.Name = "Button" + cc.Name.ToString();
                MyControl.Click += new RoutedEventHandler(OnSellectClass);
                MyControl.Unchecked += new RoutedEventHandler(UnsellectClass);
                Grid.SetRow(MyControl, j);
                Grid.SetColumn(MyControl, 1);
                ClassRadioButtonSelect.Children.Add(MyControl);
                j++;


            }

        }
        


        private void OnSellectRace(Object sender, RoutedEventArgs e)
        {

            System.Windows.Controls.RadioButton btn = (System.Windows.Controls.RadioButton)sender;
            string CharRace = (string)btn.Content;

            //_gameSession.CurrentPlayer.Race = CharRace;
            _gameSession.UpdateRace(CharRace);
           

            NextStep.Visibility = Visibility.Visible;
            RaceInfoGoesHere.Visibility = Visibility.Visible;


        }

        
        private void UnsellectRace(object sender, RoutedEventArgs e)
        {
           

        }


        private void OnSellectClass(Object sender, RoutedEventArgs e)
        {

            System.Windows.Controls.RadioButton btn = (System.Windows.Controls.RadioButton)sender;
            string CharClass = (string)btn.Content;

            //_gameSession.CurrentPlayer.Race = CharRace;
            _gameSession.UpdateClass(CharClass);


            NextStep.Visibility = Visibility.Visible;
            ClassInfoGoesHere.Visibility = Visibility.Visible;


        }


        private void UnsellectClass(object sender, RoutedEventArgs e)
        {


        }



        private void ResetGame(object sender, RoutedEventArgs e)
        {
            YesNoWindow message =
new YesNoWindow("Confirm Delete ", " Are you sure you want to delete this account?");
            message.Owner = Window.GetWindow(this);
            message.ShowDialog();

            if (message.ClickedYes)
            {
                mediaPlayer.Stop();
                MainWindow window2 = new MainWindow();
                window2.Show();
                Close();
            }


        }
        
        private void CreateCharacterBackOneStep(object sender, RoutedEventArgs e)
        {

            if (SexRadioButtonSelect.IsVisible)
            {

                bool ResetAns = PopupQues.EndNewChar();

                if (ResetAns)
                {
                    mediaPlayer.Stop();
                    MainWindow window2 = new MainWindow();
                    window2.Show();
                    Close();
                }

            }
            else if (RaceRadioButtonSelect.IsVisible)
            {
                RaceRadioButtonSelect.Visibility = Visibility.Collapsed;
                SexRadioButtonSelect.Visibility = Visibility.Visible;

            }
            else if (AttributePointAssignment.IsVisible)
            {
                RaceRadioButtonSelect.Visibility = Visibility.Visible;
                AttributePointAssignment.Visibility = Visibility.Collapsed;
            }


        }

        protected override void OnClosing(CancelEventArgs e)
        {
            YesNoWindow message =
            new YesNoWindow("Confirm Delete ", " Are you sure you want to delete this account?");
            message.Owner = Window.GetWindow(this);
            message.ShowDialog();

            if (message.ClickedYes)
            {
              
            }
            else
            {
                e.Cancel = true;
            }


        }

    }
}