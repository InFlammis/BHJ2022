using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using System;

namespace InFlammis.Victoria.Assets.Scripts.Managers.Menus.Pause
{
    public class PauseMenuManager : MenuManager, IPauseMenuManager
    {
        public IPauseMenuManager Core { get; protected set; }

        void Awake()
        {
            Core = new PauseMenuManagerCore(this);

            OnAwake();
        }

        void Start()
        {
            OnStart();
        }

        public void OnStart()
        {
            Core.OnStart();
            StaticObjects.Messenger.PublishPlayMusic(this, null, _soundSettings.BackgroundMusic);
        }

        public void OnAwake()
        {
            Core.OnAwake();
        }

        public void ResumeGame()
        {
            StaticObjects.Messenger.PublishResumeGame(this, null);
        }

        public void QuitCurrentGame()
        {
            StaticObjects.Messenger.PublishQuitCurrentGame(this, null);
        }

        public override void PlaySound(Sound sound)
        {
            StaticObjects.Messenger.PublishPlaySound(this, null, sound);
        }
    }
}
