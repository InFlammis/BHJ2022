using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Pause
{
    public class PauseMenuManager : MenuManager, IPauseMenuManager
    {
        public event EventHandler ResumeGameEvent;

        public event EventHandler QuitCurrentGameEvent;

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

            Core.ResumeGameEvent += (sender, args) => ResumeGameEvent?.Invoke(sender, args);
            Core.QuitCurrentGameEvent += (sender, args) => QuitCurrentGameEvent?.Invoke(sender, args);
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
