using BulletHellJam2022.Assets.Scripts.Managers.SceneManagement;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Enemies
{
    /// <summary>
    /// Class that manages the sounds produced by an enemy
    /// </summary>
    public class EnemySoundManager : MyMonoBehaviour
    {
        /// <summary>
        /// Sound reproduced on move.
        /// </summary>
        [SerializeField]
        private Sound MoveSound;

        /// <summary>
        /// Sound reproduced on Explode
        /// </summary>
        [SerializeField]
        private Sound ExplodeSound;

        /// <summary>
        /// Sound reproduced when the enemy is hit
        /// </summary>
        [SerializeField]
        private Sound HitSound;

        /// <summary>
        /// Instance of the current SceneManager
        /// </summary>
        public SceneManager SceneManager;

        /// <summary>
        /// Play the move sound
        /// </summary>
        public void PlayMoveSound()
        {
            SceneManager.PlaySound(MoveSound);
        }
        
        /// <summary>
        /// Play the Explode sound
        /// </summary>
        public void PlayExplodeSound()
        {
            SceneManager.PlaySound(ExplodeSound);
        }

        /// <summary>
        /// Play the Hit sound
        /// </summary>
        public void PlayHitSound()
        {
            SceneManager.PlaySound(HitSound);
        }
    }
}
