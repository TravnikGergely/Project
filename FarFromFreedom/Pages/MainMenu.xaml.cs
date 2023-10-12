using FarFromFreedom.Repository;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FarFromFreedom.Pages
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainMenu"/> class.
    /// </summary>
    public partial class MainMenu : Window
    {
        IFarFromFreedomRepository repository = FarFromFreedomRepository.Instance();
        const double border_height = 105;
        const double border_width = 350;
        const double windowHeigth = 700;
        const double windowWidth = 800;

        int selected_index = 0;


        public void createMenu()
        {
            double top_margin = 100;
            double vertical_dif = (windowHeigth - (5 * border_height) - (2 * top_margin)) / 6;
            double side_margin = (windowWidth - border_width) / 2;
            BitmapImage bmp = new BitmapImage(new Uri(System.IO.Path.Combine("MenuImages", "bg.png"), UriKind.RelativeOrAbsolute));
            ImageBrush ib = new ImageBrush(bmp);

            this.BgGrid = new Grid();
            this.Background = ib;

            //this.BgGrid.Children.Clear();

            bmp = new BitmapImage(new Uri(System.IO.Path.Combine("MenuImages", "new game.png"), UriKind.RelativeOrAbsolute));
            ib = new ImageBrush(bmp);
            Border NewGame = new Border()
            {
                Background = ib,
                Height = border_height,
                Width = border_width,
                Margin = new Thickness(side_margin, top_margin, side_margin, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Opacity = 1,
                Name = "New_Game"
            };
            NewGame.IsMouseDirectlyOverChanged += Border_IsMouseDirectlyOverChanged;
            NewGame.MouseLeftButtonUp += Border_MouseLeftButtonUp;
            top_margin += border_height + vertical_dif;

            bmp = new BitmapImage(new Uri(System.IO.Path.Combine("MenuImages", "continue.png"), UriKind.RelativeOrAbsolute));
            ib = new ImageBrush(bmp);
            Border Continue = new Border()
            {
                Background = ib,
                Height = border_height,
                Width = border_width,
                Margin = new Thickness(side_margin, top_margin, side_margin, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Opacity = 0.8,
                //Opacity = this.model.CanContinue() ? 0.8 : 0.4,
                Name = "Continue"
            };
            Continue.IsMouseDirectlyOverChanged += Border_IsMouseDirectlyOverChanged;
            Continue.MouseLeftButtonUp += Border_MouseLeftButtonUp;
            top_margin += border_height + vertical_dif;

            bmp = new BitmapImage(new Uri(System.IO.Path.Combine("MenuImages", "Stats.png"), UriKind.RelativeOrAbsolute));
            ib = new ImageBrush(bmp);
            Border Stats = new Border()
            {
                Background = ib,
                Height = border_height,
                Width = border_width,
                Margin = new Thickness(side_margin, top_margin, side_margin, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Opacity = 0.8,
                Name = "Stats"
            };
            Stats.IsMouseDirectlyOverChanged += Border_IsMouseDirectlyOverChanged;
            Stats.MouseLeftButtonUp += Border_MouseLeftButtonUp;
            top_margin += border_height + vertical_dif;

            bmp = new BitmapImage(new Uri(System.IO.Path.Combine("MenuImages", "options.png"), UriKind.RelativeOrAbsolute));
            ib = new ImageBrush(bmp);
            Border Options = new Border()
            {
                Background = ib,
                Height = border_height,
                Width = border_width,
                Margin = new Thickness(side_margin, top_margin, side_margin, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Opacity = 0.8,
                Name = "Options"
            };
            Options.IsMouseDirectlyOverChanged += Border_IsMouseDirectlyOverChanged;
            Options.MouseLeftButtonUp += Border_MouseLeftButtonUp;
            top_margin += border_height + vertical_dif;

            bmp = new BitmapImage(new Uri(System.IO.Path.Combine("MenuImages", "exit game.png"), UriKind.RelativeOrAbsolute));
            ib = new ImageBrush(bmp);
            Border ExitGame = new Border()
            {
                Background = ib,
                Height = border_height,
                Width = border_width,
                Margin = new Thickness(side_margin, top_margin, side_margin, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Opacity = 0.8,
                Name = "ExitGame"
            };
            ExitGame.IsMouseDirectlyOverChanged += Border_IsMouseDirectlyOverChanged;
            ExitGame.MouseLeftButtonUp += Border_MouseLeftButtonUp;

            this.BgGrid = new Grid();
            this.BgGrid.Children.Add(NewGame);
            this.BgGrid.Children.Add(Continue);
            this.BgGrid.Children.Add(Stats);
            this.BgGrid.Children.Add(Options);
            this.BgGrid.Children.Add(ExitGame);
            this.BgGrid.KeyDown += BgGrid_KeyDown;
            this.AddChild(this.BgGrid);

        }

        private void BgGrid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e != null)
            {
                switch (e.Key)
                {

                    case Key.Enter:
                        this.SelectIndex(this.selected_index);
                        break;
                    case Key.Escape:
                        break;
                    case Key.Up:
                        this.IncSelectedIndex();
                        break;
                    case Key.Down:
                        this.DescSelectedIndex();
                        break;
                    case Key.S:
                        this.DescSelectedIndex();
                        break;
                    case Key.W:
                        this.IncSelectedIndex();
                        break;
                    case Key.Space:
                        this.SelectIndex(this.selected_index);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Border actual = sender as Border;
            switch (actual.Name)
            {
                case "New_Game":
                    this.SelectIndex(0);
                    return;
                case "Continue":
                    this.SelectIndex(1);
                    return;
                case "Stats":
                    this.SelectIndex(2);
                    return;
                case "Options":
                    this.SelectIndex(3);

                    return;
                case "ExitGame":
                    this.SelectIndex(4);
                    return;
                default:
                    return;
            }
        }
        private void SelectIndex(int index)
        {
            switch (index)
            {
                case 0://this.logic.New_Game();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                    return;
                case 1://this.logic.Continue();
                    System.Windows.MessageBox.Show("Continue ra nyomtál");
                    return;
                case 2://this.logic.Stats();
                    System.Windows.MessageBox.Show("Stats ra nyomtál");
                    return;
                case 3://this.logic.Options();
                    System.Windows.MessageBox.Show("Options re nyomtál");
                    return;
                case 4://this.logic.ExitGame();
                    System.Windows.MessageBox.Show("ExitGame re nyomtál");
                    return;
            }
        }
        private void IncSelectedIndex()
        {
            // ki kell hagyni a continue-t ha nem lehet folytatni, 
            // a mostanit vissza kell rakni 0.8 as opacityre 
            // a következőt át kell rakni 1 es  opacityre 
            if (selected_index < 4)
            {
                this.selected_index++;
            }
        }
        private void DescSelectedIndex()
        {
            // ki kell hagyni a continue-t ha nem lehet folytatni, 
            // a mostanit vissza kell rakni 0.8 as opacityre 
            // a következőt át kell rakni 1 es  opacityre
            if (selected_index > 0)
            {
                this.selected_index--;
            }
        }
        private void Border_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Border actual = sender as Border;
            if (actual.IsMouseDirectlyOver)
            {
                if (actual.Name == "Continue" /*&& !this.model.CanContue()*/)
                {
                    return;
                }
                actual.Opacity = 1;
                switch (actual.Name)
                {
                    case "New_Game":
                        this.selected_index = 0;
                        return;
                    case "Continue":
                        this.selected_index = 1;
                        return;
                    case "Stats":
                        this.selected_index = 2;
                        return;
                    case "Options":
                        this.selected_index = 3;
                        return;
                    case "ExitGame":
                        this.selected_index = 4;
                        return;
                    default:
                        return;
                }
            }
            else
            {
                if (this.BgGrid.Children.IndexOf(actual) != selected_index)
                {
                    actual.Opacity = 0.8;
                }
            }

        }
    }
}
