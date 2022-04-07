using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Help
{
    public class HelpMenuManager : MenuManager, IHelpMenuManager
    {
        public IHelpMenuManager Core { get; protected set; }

        void Awake()
        {
            Core = new HelpMenuManagerCore(this);

            OnAwake();
        }

        void Start()
        {
            OnStart();
        }

        public void BackToMainMenu()
        {
            StaticObjects.Messenger.PublishBackToMain(this, null);
        }

        public void OnAwake()
        {
            Core.OnAwake();
        }

        public void OnStart()
        {
            Core.OnStart();
            StaticObjects.Messenger.PublishPlayMusic(this, null, _soundSettings.BackgroundMusic);

        }

        public override void PlaySound(Sound sound)
        {
            StaticObjects.Messenger.PublishPlaySound(this, null, sound);
        }
    }
}
