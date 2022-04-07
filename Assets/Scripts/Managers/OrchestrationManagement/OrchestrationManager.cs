using System.Collections;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.OrchestrationManagement
{
    /// <summary>
    /// Implementation of an Orchestration manager.
    /// Coordinates a sequence of spawns of enemies in spawn points.
    /// The OrchestrationManager contains a list of <see cref="Wave"/>, executed in sequence.
    /// </summary>
    public class OrchestrationManager : 
        MyMonoBehaviour, 
        IOrchestrationManager
    {
        [SerializeField] private StaticObjectsSO _staticObjects;
        [SerializeField] private OrchestrationManagerSettingsSO _settings;

        public StaticObjectsSO StaticObjects => _staticObjects;
        
        ///// <summary>
        ///// Collection of waves of enemies to spawn
        ///// </summary>
        //public Wave[] Waves;

        /// <summary>
        /// The cancellation token
        /// </summary>
        private CancellationToken RunCancellationToken;

        /// <summary>
        /// CoRoutine that manages the execution
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public IEnumerator CoRun(CancellationToken cancellationToken)
        {
            _staticObjects.Messenger.PublishOrchestrationStarted(this, null);

            yield return new WaitForSeconds(_settings.DelayBeforeStart);
            foreach(var wave in _settings.Waves)
            {
                if(cancellationToken.Cancel == true)
                {
                    _staticObjects.Messenger.PublishOrchestrationCancelled(this, null);
                    yield break;
                }
                wave.Run(this, cancellationToken);

                yield return new WaitUntil(() => wave.Status == StatusEnum.Done);

                yield return new WaitForSeconds(_settings.DelayBetweenWaves);
            }

            if(cancellationToken.Cancel == false)
            {
                yield return new WaitForSeconds(_settings.DelayAfterEnd);

                _staticObjects.Messenger.PublishOrchestrationComplete(this, null);
            }
        }

        public void LevelGameOver(object publisher, string target)
        {
            RunCancellationToken.Cancel = true; ;
        }

        public void LevelGameStarted(object publisher, string target)
        {
            if (_settings.IsIdle)
                return;
            RunCancellationToken = new CancellationToken();
            StartCoroutine(CoRun(RunCancellationToken));
        }

        void Awake()
        {
            _staticObjects.Messenger.GameOver.AddListener(this.LevelGameOver);
            _staticObjects.Messenger.GameStarted.AddListener(this.LevelGameStarted);
        }

        /// <summary>
        /// Simplified version of a CancellationToken.
        /// Used to Cancel an executing Orchestration
        /// </summary>
        public class CancellationToken
        {
            public bool Cancel = false;
        }

        /// <summary>
        /// Status of an orchestration
        /// </summary>
        public enum StatusEnum
        {
            NotStarted,
            Running,
            Done
        }
    }
}
