using UnityEngine;

namespace ZType.Core.Systems
{
    public abstract class LevelSystem : MonoBehaviour
    {
        #region Variables

        private LevelManager _levelManager;

        #endregion

        #region Properties

        public LevelManager LevelManager => _levelManager ? _levelManager : _levelManager = LevelManager.Instance;

        #endregion
    }
}