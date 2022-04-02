using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus
{
    public class CreditsMenuManagerCore : ICreditsMenuManager
    {
        /// <inheritdoc/>
        public event EventHandler BackEvent;

        /// <inheritdoc/>
        public event EventHandler<Sound> PlaySoundEvent;

        public readonly IMyMonoBehaviour Parent;

        public CreditsMenuManagerCore(IMyMonoBehaviour parent)
        {
            Parent = parent;
        }

        /// <inheritdoc/>
        public void OnAwake() { }

        /// <inheritdoc/>
        public void OnStart()
        {
            //Debug.Log($"Credits menu opened");
        }

        /// <inheritdoc/>
        public void BackToMainMenu()
        {
            BackEvent?.Invoke(this, new EventArgs());
        }
    }
}
