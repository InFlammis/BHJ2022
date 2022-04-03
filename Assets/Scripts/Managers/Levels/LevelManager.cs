using BulletHellJam2022.Assets.Scripts.Managers.SceneManagement;
using BulletHellJam2022.Assets.Scripts.Player;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletHellJam2022.Assets.Scripts.Managers.Levels
{
    public abstract class LevelManager : 
        SceneManager, 
        ILevelManager
    {

        [SerializeField] protected LevelSceneSoundSettingsSO _soundSettings;

        public IPlayerControllerCore PlayerControllerCore { get; set; }

        public virtual void Move(InputAction.CallbackContext context){}

        public virtual void DisablePlayerInput(){}

        public virtual void EnablePlayerInput(){}

        public virtual void OnStart() 
        {
            var player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                throw new NullReferenceException("Player object not found");
            }

            this.PlayerControllerCore = player.GetComponent<IPlayerController>().Core;

            StaticObjects.Messenger.PublishPlayMusic(this, null, _soundSettings.BackgroundMusic);
        }

        public virtual void OnAwake() { }
    }
}
