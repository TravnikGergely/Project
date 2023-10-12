using FarFromFreedom.Logic;
using System.Windows;

namespace FarFromFreedom
{
    /// <summary>
    /// Interaction logic for SaveHighscore.xaml
    /// </summary>
    public partial class SaveHighscore : Window
    {
        IGameLogic logic;
        public SaveHighscore(IGameLogic logic)
        {
            this.logic = logic;
            InitializeComponent();
            if (this.logic.Won)
            {
                this.label.Content = "Congratulations, You won!";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.logic.SaveHighscore(this.tbox.Text);
            this.Close();
        }
    }
}
