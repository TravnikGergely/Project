using FarFromFreedom.Model.Characters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FarFromFreedom.Renderer
{
    public class MainCharacterRender
    {
        public MainCharacterRender(IMainCharacter character)
        {
            this.character = character;
            gobbyBack = new List<Brush>();
            gobbyFront = new List<Brush>();
            gobbyLeft = new List<Brush>();
            gobbyRight = new List<Brush>();
            string path = Path.Combine("Images", "gobby");
            int files = Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length;
            for (int i = 1; i <= 4; i++)
            {
                gobbyBack.Add(GetBrushes(Path.Combine(path, $"gobbyBack{i}.png")));
                gobbyFront.Add(GetBrushes(Path.Combine(path, $"gobbyFront{i}.png")));
                gobbyLeft.Add(GetBrushes(Path.Combine(path, $"gobbyLeft{i}.png")));
                gobbyRight.Add(GetBrushes(Path.Combine(path, $"gobbyRight{i}.png")));
            }
        }
        public List<Brush> gobbyBack { get; set; }
        public List<Brush> gobbyFront { get; set; }
        public List<Brush> gobbyLeft { get; set; }
        public List<Brush> gobbyRight { get; set; }

        private IMainCharacter character;

        public int Counter => counter;

        private int counter = 0;
        public void counterUp()
        {
            if (counter >= 3 || character.DirectionHelper.DirectionChanged)
            {
                counter = 0;
                character.DirectionHelper.DefaultDirectionChange();
            }
            else
            {
                counter++;
            }
            character.CharacterMoved = false;
        }
        public MainCharacterRender()
        {
            gobbyBack = new List<Brush>();
            gobbyFront = new List<Brush>();
            gobbyLeft = new List<Brush>();
            gobbyRight = new List<Brush>();
            string path = Path.Combine("Images", "gobby");
            int files = Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length;
            for (int i = 1; i <= 4; i++)
            {
                gobbyBack.Add(GetBrushes(Path.Combine(path, $"gobbyBack{i}.png")));
                gobbyFront.Add(GetBrushes(Path.Combine(path, $"gobbyFront{i}.png")));
                gobbyLeft.Add(GetBrushes(Path.Combine(path, $"gobbyLeft{i}.png")));
                gobbyRight.Add(GetBrushes(Path.Combine(path, $"gobbyRight{i}.png")));
            }

        }

        private ImageBrush GetBrushes(string file) => new ImageBrush(new BitmapImage(new Uri(file, UriKind.RelativeOrAbsolute)));
    }
}
