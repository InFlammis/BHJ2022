using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Help
{
    public class HelpMenuManagerCore : IHelpMenuManager
    {
        /// <inheritdoc/>
        public event EventHandler BackEvent;

        /// <inheritdoc/>
        public event EventHandler<Sound> PlaySoundEvent;

        public readonly IMyMonoBehaviour Parent;

        public HelpMenuManagerCore(IMyMonoBehaviour parent)
        {
            Parent = parent;
        }

        /// <inheritdoc/>
        public void BackToMainMenu()
        {
            BackEvent?.Invoke(this, new EventArgs());
        }

        /// <inheritdoc/>
        public void OnAwake() { }

        /// <inheritdoc/>
        public void OnStart()
        {
            //Debug.Log($"Credits menu opened");
        }
    }
}
