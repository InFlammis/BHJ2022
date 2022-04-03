using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Help
{
    public class HelpMenuManager : MenuManager, IHelpMenuManager
    {
        /// <inheritdoc/>
        public event EventHandler BackEvent;
        ///// <inheritdoc/>
        //public event EventHandler<Sound> PlaySoundEvent;

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

        /// <inheritdoc/>
        public void BackToMainMenu()
        {
            StaticObjects.Messenger.PublishBackToMain(this, null);

        }

        /// <inheritdoc/>
        public void OnAwake()
        {
            Core.OnAwake();
            Core.BackEvent += (sender, args) => BackEvent?.Invoke(sender, args);
        }

        /// <inheritdoc/>
        public void OnStart()
        {
            Core.OnStart();
            StaticObjects.Messenger.PublishPlayMusic(this, null, _soundSettings.BackgroundMusic);

        }

        /// <inheritdoc/>
        public override void PlaySound(Sound sound)
        {
            StaticObjects.Messenger.PublishPlaySound(this, null, sound);
        }
    }
}
