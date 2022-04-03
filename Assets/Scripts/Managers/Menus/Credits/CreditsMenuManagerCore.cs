using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Credits
{
    public class CreditsMenuManagerCore : ICreditsMenuManager
    {
        /// <inheritdoc/>
        public event EventHandler BackEvent;

        public readonly ICreditsMenuManager Parent;

        public StaticObjectsSO StaticObjects => Parent.StaticObjects;

        public CreditsMenuManagerCore(ICreditsMenuManager parent)
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
        }
    }
}
