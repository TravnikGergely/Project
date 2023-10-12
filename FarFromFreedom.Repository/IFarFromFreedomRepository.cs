using FarFromFreedom.Model;
using FarFromFreedom.Model.Characters;
using System.Collections.Generic;

namespace FarFromFreedom.Repository
{
    public interface IFarFromFreedomRepository
    {
        List<Dictionary<int, IGameModel>> GameModelMap { get; }
        static IFarFromFreedomRepository Instance() { return Instance(); }
        IGameModel LoadGame(string fileName);
        IGameModel LoadGameFromXML(bool check);
        Dictionary<string, int> LoadHighscores();

        void ResetGameModels();
        void SaveHighScore(string userName, int score);
        void SaveGame(IGameModel gameModel, string filename);
        void SaveGameToXml(IMainCharacter mc, int level, int roomid, List<int> clearedRoomIDs);
    }
}