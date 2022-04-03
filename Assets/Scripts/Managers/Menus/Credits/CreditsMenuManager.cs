using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Credits
{
    public class CreditsMenuManager : MenuManager, ICreditsMenuManager
    {
        /// <inheritdoc/>
        public event EventHandler BackEvent;

        ///// <inheritdoc/>
        //public event EventHandler<Sound> PlaySoundEvent;

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
        }

        /// <inheritdoc/>
        public void BackToMainMenu()
        {
            BackEvent?.Invoke(this, new EventArgs());
        }

        /// <inheritdoc/>
        public override void PlaySound(Sound sound)
        {
            StaticObjects.Messenger.PublishPlaySound(this, null, sound);
        }
    }
}
