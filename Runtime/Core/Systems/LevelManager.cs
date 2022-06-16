using System.Linq;
using ZType.Core.Systems.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ZType.Core.Utility.Singleton;

namespace ZType.Core.Systems
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        #region Internal

        public enum State
        {
            None,
            Initializing,
            Starting,
            Playing,
            Paused,
        }

        #endregion
        #region Inspector

        
        #endregion
        
        #region Properties

        public LevelSystem[] LevelSystems => 
            _levelSystems?? GetComponentsInChildren<LevelSystem>();

        public bool Paused => !enabled;

        #endregion
        
        #region Variables
        
        private LevelSystem[] _levelSystems;
        private ILevelInitializable[] _initializables;
        private ILevelStartable[] _startables;
        private ILevelPausable[] _pausables;
        private IFrameUpdatable[] _frameUpdatables;
        private IPostFrameUpdatable[] _postFrameUpdatables;
        private IPhysicsUpdatable[] _physicsUpdatables;
        
        #endregion

        #region Public Functions

        public TLevelSystem GetLevelSystem<TLevelSystem>() where TLevelSystem : LevelSystem =>
            LevelSystems.FirstOrDefault(system => system is TLevelSystem) as TLevelSystem;

        public void Pause() => enabled = false;
        
        public void Unpause() => enabled = true;
        
        #endregion
        
        #region Event Functions

        protected override void Awake()
        {
            base.Awake();

            void InitAndSort<T>(out T[] array)
            {
                array = LevelSystems.OfType<T>().ToArray(); 
                System.Array.Sort(array);
            }

            InitAndSort(out _initializables);
            InitAndSort(out _startables);
            InitAndSort(out _pausables);
            InitAndSort(out _frameUpdatables);
            InitAndSort(out _postFrameUpdatables);
            InitAndSort(out _physicsUpdatables);
        }

        private void OnEnable()
        {
            if (!Application.isPlaying || !enabled) return;
            
            for (var i = 0; i < _pausables.Length; ++i)
            {
                _pausables[i].OnLevelPaused(Paused);
            }
        }

        private void OnDisable()
        {
            if (!Application.isPlaying || enabled) return;
            
            for (var i = 0; i < _pausables.Length; ++i)
            {
                _pausables[i].OnLevelPaused(Paused);
            }
        }

        private async void Start()
        {
            enabled = false;
            for (var i = 0; i < _initializables.Length; ++i)
            {
                await _initializables[i].OnLevelInitializeAsync();
            }
            await UniTask.WhenAll(_startables.Select(startable => startable.OnLevelStartAsync()));
            enabled = true;
        }
        
        private void Update()
        {
            for (var i = 0; i < _frameUpdatables.Length; ++i)
            {
                _frameUpdatables[i].OnFrameUpdate();
            }
        }
        
        private void LateUpdate()
        {
            for (var i = 0; i < _postFrameUpdatables.Length; ++i)
            {
                _postFrameUpdatables[i].OnPostFrameUpdate();
            }
        }
        
        private void FixedUpdate()
        {
            for (var i = 0; i < _physicsUpdatables.Length; ++i)
            {
                _physicsUpdatables[i].OnPhysicsUpdate();
            }
        }
        
        #endregion
    }
}