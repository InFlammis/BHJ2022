using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Help
{
    public class HelpMenuManager : MenuManager, IHelpMenuManager
    {
        /// <inheritdoc/>
        public event EventHandler BackEvent;
        /// <inheritdoc/>
        public event EventHandler<Sound> PlaySoundEvent;

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
            BackEvent?.Invoke(this, new EventArgs());
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
        public override void PlaySound(Sound sound)
        {
            PlaySoundEvent?.Invoke(this, sound);
        }
    }
}
