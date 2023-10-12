using FarFromFreedom.Model.Characters;
using System.Collections.Generic;

namespace FarFromFreedom.Model
{
    public interface IGameModel : IModel
    {
        IMainCharacter Character { get; set; }
        List<IEnemy> Enemies { get; set; }
        List<IItem> Items { get; set; }
        List<Bullet> Bullets { get; set; }
        List<Door> Doors { get; set; }

        int UpperNeighbour { get; set; }
        int LowerNeighbour { get; set; }
        int RightNeighbour { get; set; }
        int LeftNeighbour { get; set; }
        int RoomID { get; set; }
        int Level { get; set; }
        bool Won { get; set; }

        public PauseModel? PauseModel { get; set; }

        void Init(List<IEnemy> enemies, List<IItem> Items);
    }
}