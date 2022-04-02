using BulletHellJam2022.Assets.Scripts.Managers.SceneManagement;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Player
{
    /// <summary>
    /// Class that manages the sounds produced by the player
    /// </summary>
    public class PlayerSoundManager : MyMonoBehaviour
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
        /// Sound reproduced when the enemy collects a power-up
        /// </summary>
        [SerializeField]
        private Sound PowerUpSound;

        /// <summary>
        /// Reference to the SceneManager instance
        /// </summary>
        public SceneManager SceneManager;

        /// <summary>
        /// Play the Move sound
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

        /// <summary>
        /// Play the Power-up sound
        /// </summary>
        public void PlayPowerUpSound()
        {
            SceneManager.PlaySound(PowerUpSound);
        }
    }
}
