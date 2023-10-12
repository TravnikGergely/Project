using System.Collections.Generic;

namespace FarFromFreedom.Model
{
    public class MenuModel : IMenuModel
    {
        private bool isWelcomePage = true;
        private bool canContiue;
        private readonly int _newGameWidth = 350;
        private readonly int _newGameHeight = 105;
        private double _newGameOpacity = 1;
        private readonly int _continueWidth = 350;
        private readonly int _continueHeight = 105;
        private double _continueOpacity = 0.5;
        private readonly int _optionsWidth = 350;
        private readonly int _optionsHeight = 105;
        private double _optionsOpacity = 0.8;
        private readonly int _highscoreWidth = 350;
        private readonly int _highscoreHeight = 105;
        private double _highscoreOpacity = 0.8;
        private readonly int _exitGameWidth = 350;
        private readonly int _exitGameHeight = 105;
        private double _exitGameOpacity = 0.8;
        private int selectedIndex = 0;
        private bool isHighscoreList = false;
        Dictionary<string, int> highscores;


        public MenuModel()
        {
        }

        public bool IsWelcomePage
        {
            get { return isWelcomePage; }
            set { isWelcomePage = value; }
        }

        public int NewGameWidth => _newGameWidth;

        public int NewGameHeight => _newGameHeight;

        public double NewGameOpacity { get => _newGameOpacity; set => _newGameOpacity = value; }

        public int ContinueWidth => _continueWidth;

        public int ContinueHeight => _continueHeight;

        public double ContinueOpacity { get => _continueOpacity; set => _continueOpacity = value; }

        public int OptionsWidth => _optionsWidth;

        public int OptionsHeight => _optionsHeight;

        public double OptionsOpacity { get => _optionsOpacity; set => _optionsOpacity = value; }

        public int HighscoreWidth => _highscoreWidth;

        public int HighscoreHeight => _highscoreHeight;

        public double HighscoreOpacity { get => _highscoreOpacity; set => _highscoreOpacity = value; }

        public int ExitGameWidth => _exitGameWidth;

        public int ExitGameHeight => _exitGameHeight;

        public double ExitGameOpacity { get => _exitGameOpacity; set => _exitGameOpacity = value; }

        public int SelectedIndex { get => selectedIndex; set => selectedIndex = value; }

        public bool CanContiue { get => canContiue; set => canContiue = value; }
        public bool IsHighscoreList { get => isHighscoreList; set => isHighscoreList = value; }

        public Dictionary<string, int> GetHighscores { get => this.highscores; set => this.highscores = value; }

        public void ResetParams()
        {

            NewGameOpacity = 1;
            if (CanContiue)
            {
                ContinueOpacity = 0.8;
            }
            else
            {
                ContinueOpacity = 0.4;
            }
            OptionsOpacity = 0.8;
            HighscoreOpacity = 0.8;
            ExitGameOpacity = 0.8;
            SelectedIndex = 0;
        }
    }
}
