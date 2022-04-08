using BulletHellJam2022.Assets.Scripts.Enemies;
using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.OrchestrationManagement
{
    /// <summary>
    /// A sequence of enemy spawns.
    /// A wave contains a set of enemies to spawn, a set of spawn points.
    /// When executing, it randomly spawns the enemy throught the spawn points,
    /// based on the condition sets, until completion or the player's death.
    /// </summary>
    [Serializable]
    public class Wave
    {

        /// <summary>
        /// Collection of enemy types spawn in the wave
        /// </summary>
        public EnemyType[] EnemyTypes;

        /// <summary>
        /// Collection of spawn points where to spawn the enemies
        /// </summary>
        public GameObject[] SpawnPoints;

        /// <summary>
        /// How many enemies of any type will be in the scene at any moment
        /// </summary>
        public int MaxSimultaneousEnemiesSpawned;

        /// <summary>
        /// Delay between two consecutive spawns
        /// </summary>
        public float DelayBetweenSpawns = 1.0f;

        /// <summary>
        /// Delay before starting the current wave
        /// </summary>
        public float DelayBeforeStart = 1.0f;

        /// <summary>
        /// Delay after the previous wave si done
        /// </summary>
        public float DelayAfterEnd = 1.0f;

        /// <summary>
        /// How many enemies are in the scene at a specific moment
        /// </summary>
        [HideInInspector]public int CurrentSimultaneousEnemiesSpawned;

        /// <summary>
        /// Total enemies spawned at a specific moment
        /// </summary>
        [HideInInspector] public int TotEnemiesSpawned;

        /// <summary>
        /// Total enemies to spawn in the whole wave
        /// </summary>
        [HideInInspector] public int TotEnemiesToSpawn;

        /// <summary>
        /// Total enemies killed during the wave execution
        /// </summary>
        [HideInInspector] public int TotEnemiesKilled;

        /// <summary>
        /// Status of the execution
        /// </summary>
        public OrchestrationManager.StatusEnum Status { get; private set; } = OrchestrationManager.StatusEnum.NotStarted;

        public IMessenger Messenger { get; private set; }

        /// <summary>
        /// Start the execution of the Wave
        /// </summary>
        /// <param name="manager"></param>
        public void Run(OrchestrationManager manager, OrchestrationManager.CancellationToken cancellationToken)
        {
            Messenger = manager.StaticObjects.Messenger;
            (Messenger as IEnemyEventsMessenger).HasDied.AddListener(EnemyHasDied);

            manager.StartCoroutine(CoRun(manager, cancellationToken));
        }

        /// <summary>
        /// Stops the wave if it's running
        /// </summary>
        public void Stop()
        {
            (Messenger as IEnemyEventsMessenger).HasDied.RemoveListener(EnemyHasDied);
        }

        /// <summary>
        /// CoRoutine executing the wave's plan
        /// </summary>
        /// <param name="manager">Reference to the OrchestrationManager instance</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public IEnumerator CoRun(OrchestrationManager manager, OrchestrationManager.CancellationToken cancellationToken)
        {
            Status = OrchestrationManager.StatusEnum.Running;
            TotEnemiesSpawned = 0;
            TotEnemiesKilled = 0;
            TotEnemiesToSpawn = EnemyTypes.Sum(x => x.Settings.NumToSpawn);

            yield return new WaitForSeconds(DelayBeforeStart);

            while (TotEnemiesSpawned < TotEnemiesToSpawn)
            {
                yield return new WaitForSeconds(DelayBetweenSpawns);

                yield return new WaitUntil(() => CurrentSimultaneousEnemiesSpawned < MaxSimultaneousEnemiesSpawned || cancellationToken.Cancel == true);

                if(cancellationToken?.Cancel == true)
                {
                    Debug.Log("Wave cancelled");
                    break;
                }

                var nextEnemy = EnemyTypes.Where(x=>x.CurrentlySpawned < x.Settings.MaxNumOfSimultaneousSpawns).OrderBy(x => x.TotalSpawned / (float)x.Settings.NumToSpawn).FirstOrDefault();

                if (nextEnemy == null)
                    continue;

                var spawnPoint = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];

                var nextEnemySpawn = GameObject.Instantiate(nextEnemy.Settings.EnemyType, spawnPoint.transform.position, Quaternion.identity);
                UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(nextEnemySpawn, UnityEngine.SceneManagement.SceneManager.GetSceneAt(1));

                nextEnemy.TotalSpawned++;
                nextEnemy.CurrentlySpawned++;
                TotEnemiesSpawned++;
            }

            Debug.Log($"DelayAfterEnd Waiting for seconds {DelayAfterEnd}");
            yield return new WaitForSeconds(DelayAfterEnd);

            yield return new WaitUntil(() => TotEnemiesKilled == TotEnemiesToSpawn || cancellationToken.Cancel == true);

            Status = OrchestrationManager.StatusEnum.Done;
        }

        void EnemyHasDied(object publisher, string target)
        {
            var enemyController = publisher as IEnemyController;

            var enemyType = EnemyTypes.Single(x => x.Settings.EnemyTypeEnum == enemyController.InitSettings.EnemyType);
            enemyType.CurrentlySpawned--;
            TotEnemiesKilled++;
        }
    }
}
