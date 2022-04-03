using BulletHellJam2022.Assets.Scripts.Managers.OrchestrationManagement;
using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //[SerializeField] private Messenger _messenger;
        //public IMessenger Messenger => _messenger;

        [SerializeField] private StaticObjectsSO _staticObjects;

        public StaticObjectsSO StaticObjects => _staticObjects;
        /// <summary>
        /// Collection of waves of enemies to spawn
        /// </summary>
        public Wave[] Waves;

        /// <summary>
        /// The cancellation token
        /// </summary>
        private CancellationToken RunCancellationToken;

        ///// <inheritdoc/>
        //public event Action<int> SendScore;

        /// <summary>
        /// Delay between two consecutive waves
        /// </summary>
        public float DelayBetweenWaves = 1.0f;

        /// <summary>
        /// Delay before starting the next wave
        /// </summary>
        public float DelayBeforeStart = 1.0f;

        /// <summary>
        /// Delay after the previous wave is done
        /// </summary>
        public float DelayAfterEnd = 1.0f;

        /// <summary>
        /// Status of the Orchestration
        /// </summary>
        public StatusEnum Status { get; private set; } = StatusEnum.NotStarted;

        /// <summary>
        /// CoRoutine that manages the execution
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public IEnumerator CoRun(CancellationToken cancellationToken)
        {
            _staticObjects.Messenger.PublishOrchestrationStarted(this, null);

            Status = StatusEnum.Running;

            yield return new WaitForSeconds(DelayBeforeStart);
            foreach(var wave in Waves)
            {
                if(cancellationToken.Cancel == true)
                {
                    _staticObjects.Messenger.PublishOrchestrationCancelled(this, null);
                    yield break;
                }
                wave.Run(this, cancellationToken);

                yield return new WaitUntil(() => wave.Status == StatusEnum.Done);

                yield return new WaitForSeconds(DelayBetweenWaves);
            }

            yield return new WaitForSeconds(DelayAfterEnd);

            Status = StatusEnum.Done;

            _staticObjects.Messenger.PublishOrchestrationComplete(this, null);
        }

        public void LevelGameOver(object publisher, string target)
        {
            RunCancellationToken.Cancel = true; ;
        }

        public void LevelGameStarted(object publisher, string target)
        {
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
