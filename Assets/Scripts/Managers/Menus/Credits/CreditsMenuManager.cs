using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Credits
{
    public class CreditsMenuManager : MenuManager, ICreditsMenuManager
    {
        public event EventHandler BackEvent;

        public ICreditsMenuManager Core { get; protected set; }

        void Awake()
        {
            Core = new CreditsMenuManagerCore(this);

            OnAwake();
        }

        void Start()
        {
            OnStart();
            StaticObjects.Messenger.PublishPlayMusic(this, null, _soundSettings.BackgroundMusic);
        }

        public void OnAwake()
        {
            Core.OnAwake();
            Core.BackEvent += (sender, args) => BackEvent?.Invoke(sender, args);
        }

        public void OnStart()
        {
            Core.OnStart();
        }

        public void BackToMainMenu()
        {
            StaticObjects.Messenger.PublishBackToMain(this, null);
        }

        public override void PlaySound(Sound sound)
        {
            StaticObjects.Messenger.PublishPlaySound(this, null, sound);
        }
    }
}
