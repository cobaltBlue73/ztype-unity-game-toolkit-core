using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ZType.Core.LevelSystems
{
    public abstract class LevelSystem : MonoBehaviour
    {
        #region Properties

        public LevelManager LevelManager => _levelManager ? 
            _levelManager : _levelManager = LevelManager.Instance;
       
        #endregion

        #region Variables

        private LevelManager _levelManager;

        #endregion

        
    }
}