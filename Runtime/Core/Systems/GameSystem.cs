using System.Linq;
using UnityEngine;

namespace ZType.Core.Systems
{
    public abstract class GameSystem : ScriptableObject
    {
        #region Constants

        private const string GameSystemsPath = "GameSystems";

        #endregion

        #region Static

        #region Properties

        public static GameSystem[] GameSystems =>
            _gameSystems ?? Resources.LoadAll<GameSystem>(GameSystemsPath);

        #endregion

        #region Vairables

        private static GameSystem[] _gameSystems;

        #endregion

        #region Functions

        public static TGameSystem GetGameSystem<TGameSystem>() where TGameSystem : GameSystem
        {
            return GameSystems.FirstOrDefault(gs => gs is TGameSystem) as TGameSystem;
        }

        #endregion

        #endregion
    }
}