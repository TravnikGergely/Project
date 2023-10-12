using FarFromFreedom.Model;
using FarFromFreedom.Model.Characters;
using FarFromFreedom.Repository;
using System;
using System.IO;
using System.Windows;

namespace FarFromFreedom.Logic
{
    public class MenuLogic : IMenuLogic
    {
        IMenuModel model;
        IFarFromFreedomRepository repository;
        IGameModel prevGameModel;

        public MenuLogic(IMenuModel model)
        {
            this.model = model;
            this.repository = FarFromFreedomRepository.Instance();
            this.CheckForSave();

        }

        /// <summary>
        /// Megnézi, hogy van e betölthető mentés. Ha van az egy boolban eltárolódik majd. 
        /// </summary>
        private void CheckForSave()
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Saves", "SaveFile.xml")))
            {
                try
                {
                    this.prevGameModel = this.repository.LoadGameFromXML(true);


                    this.model.CanContiue = true;
                    this.model.ContinueOpacity = 0.8;
                }
                catch (Exception)
                {
                    this.model.CanContiue = false;
                    this.model.ContinueOpacity = 0.4;
                }
            }
        }


        public void DescSelectedIndex()
        {
            if (this.model.SelectedIndex > 0)
            {
                this.SetIndexesOpacity(this.model.SelectedIndex, 0.8);

                if (this.model.SelectedIndex - 1 == 1 && !this.model.CanContiue)
                {
                    this.model.SelectedIndex -= 2;
                }
                else
                {
                    this.model.SelectedIndex--;
                }
                this.SetIndexesOpacity(this.model.SelectedIndex, 1);

            }
        }


        public void IncSelectedIndex()
        {

            if (this.model.SelectedIndex < 3)
            {
                this.SetIndexesOpacity(this.model.SelectedIndex, 0.8);
                if (this.model.SelectedIndex + 1 == 1 && !this.model.CanContiue)
                {
                    this.model.SelectedIndex += 2;
                }
                else
                {
                    this.model.SelectedIndex++;
                }
                this.SetIndexesOpacity(this.model.SelectedIndex, 1);
            }
        }

        /// <summary>
        /// Beállítja a megfelelő indexű menüelem átlátszatlanságát.
        /// </summary>
        /// <param name="index"> A menüelem indexe. </param>
        /// <param name="opacity"> A kívánt átlátszatlanság. </param>
        private void SetIndexesOpacity(int index, double opacity)
        {
            switch (index)
            {
                case 0: this.model.NewGameOpacity = opacity; break;
                case 1: this.model.ContinueOpacity = opacity; break;
                case 2: this.model.HighscoreOpacity = opacity; break;
                case 3: this.model.ExitGameOpacity = opacity; break;
            }
        }

        /// <summary>
        /// Ezzel a metódussal érjük el, hogy kiválaszt egy menüelemet a felhasználó.
        /// Ennek a hatására egy új modelnek kell majd betöltődni a BaseControl osztályba 
        /// </summary>
        /// <param name="selectedIndex"></param>
        public IModel SelectIndex()
        {
            switch (this.model.SelectedIndex)
            {
                case 0:
                    IFarFromFreedomRepository repo = FarFromFreedomRepository.Instance();
                    repo.ResetGameModels();
                    IGameModel game = repo.GameModelMap[0][1];
                    game.Character = new MainCharacter("gobby", "alma", 6, 6, 1, 0, new Rect(604, 312, 70, 100));
                    return game;
                case 1:
                    return this.repository.LoadGameFromXML(false);
                case 2:
                    this.LoadHighscores();
                    return this.model;
                case 3:
                    return null;
                default:
                    break;
            }
            return new MenuModel();
        }

        public void LoadHighscores()
        {
            this.model.IsHighscoreList = true;
            this.model.GetHighscores = this.repository.LoadHighscores();
        }
    }
}
