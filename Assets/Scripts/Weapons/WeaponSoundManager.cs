using BulletHellJam2022.Assets.Scripts.Managers.SceneManagement;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Weapons
{
    /// <summary>
    /// Class that manages the sounds produced by a weapon
    /// </summary>
    public class WeaponSoundManager : MyMonoBehaviour
    {
        /// <summary>
        /// Sound reproduced on fire.
        /// </summary>
        [SerializeField]
        private Sound FireSound;

        /// <summary>
        /// Instance of the current SceneManager
        /// </summary>
        private SceneManager SceneManager;

        private void Start()
        {
            var sceneManagerGo = GameObject.FindGameObjectWithTag("SceneManager");
            SceneManager = sceneManagerGo?.GetComponent<SceneManager>();

            if(SceneManager == null)
            {
                throw new Exception("SceneManager not found");
            }
        }

        /// <summary>
        /// Play the fire sound
        /// </summary>
        public void PlayFireSound()
        {
            SceneManager.PlaySound(FireSound);
        }
    }
}
