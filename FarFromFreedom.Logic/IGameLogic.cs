using FarFromFreedom.Model;
using FarFromFreedom.Model.Characters;
using System.Collections.Generic;
using System.Windows.Input;

namespace FarFromFreedom.Logic
{
    public interface IGameLogic
    {
        int CurrentLevel { get; }
        int CurrentRoom { get; }
        bool Won { get; }

        void BulletMove();
        void EnemyDamaged();
        void EnemyDestroy();
        bool EnemyHit();
        bool EnemyIsCollision(Queue<Enemy> queue, Enemy enemy);
        void EnemyMove();
        bool GameEnd();
        IGameModel GameLoad(string fileName);
        void GameSave(string fileName);
        void GameSave();
        void HighscoreUp(double highscore);
        void ItemPicked();
        void levelUp();
        void PLayerMove(Key key);
        int PlayerShoot(Key key, int counter);
        void RoomDown();
        void RoomUp();
        void GenerateDoors();
        int DoorIntersect();
        IGameModel ChangeRoom(int roomid);
        void DisposeOutOFBoundsTears();

        void Pause(bool pause);


        void Win();
        void SaveHighscore(string pname);
    }
}