using FarFromFreedom.Logic;
using FarFromFreedom.Model;
using FarFromFreedom.Model.Characters;
using FarFromFreedom.Repository;
using System.Windows;
using System.Windows.Input;

namespace FarFromFreedom
{
    public class MenuSubControl
    {
        IMenuLogic? logic;

        public void Init(IMenuModel model)
        {
            logic = new MenuLogic(model);
        }

        public void Dispose()
        {
            logic = null;
        }

        public int HandleInput(BaseControl control, IMenuModel menuModel, object sender, KeyEventArgs e)
        {
            if (e != null)
            {
                if (menuModel.IsHighscoreList)
                {
                    if (e.Key == Key.Escape)
                    {
                        menuModel.IsHighscoreList = !menuModel.IsHighscoreList;
                        return 1;
                    }
                    return -1;
                }
                switch (e.Key)
                {

                    case Key.Enter:
                        this.HandleSelection(control, logic?.SelectIndex());
                        return 1;
                    case Key.Escape:
                        menuModel.IsWelcomePage = true;
                        return 1;
                    case Key.Up:
                        logic?.DescSelectedIndex();
                        return 1;
                    case Key.Down:
                        logic?.IncSelectedIndex();
                        return 1;
                    case Key.S:
                        logic?.IncSelectedIndex();
                        return 1;
                    case Key.W:
                        logic?.DescSelectedIndex();
                        return 1;
                    case Key.Space:
                        this.HandleSelection(control, logic?.SelectIndex());
                        return 1;
                    case Key.Back:
                        menuModel.IsWelcomePage = true;
                        return 1;
                }
            }
            return -1;
        }

        private void HandleSelection(BaseControl control, IModel model)
        {
            if (model == null)
            {
                Window.GetWindow(control).Close();
                return;
            }

            if (model is IGameModel gameModel)
            {
                if (gameModel.Level == 1 && gameModel.RoomID == 1 && gameModel.Character.Highscore == 0)
                {
                    Window original = Window.GetWindow(control);
                    original.WindowStartupLocation = WindowStartupLocation.Manual;
                }
                gameModel.PauseModel = null;
                gameModel.Won = false;
                control.ChangeModel(gameModel);
            }


        }

        private void NewGame(BaseControl control)
        {
            IFarFromFreedomRepository repo = FarFromFreedomRepository.Instance();
            IGameModel game = repo.GameModelMap[0][1];
            game.Character = new MainCharacter("Gobby", "alma", 6, 6, 1, 0, new Rect(400, 200, 100, 100));

            control.ChangeModel(game);
        }

        private void GameLoad(BaseControl control)
        {
            IFarFromFreedomRepository repo = FarFromFreedomRepository.Instance();
            IGameModel game = repo.GameModelMap[0][1];
            game.Character = new MainCharacter("Gobby", "alma", 6, 6, 1, 0, new Rect(400, 200, 100, 100));

            control.ChangeModel(game);

        }


    }
}
