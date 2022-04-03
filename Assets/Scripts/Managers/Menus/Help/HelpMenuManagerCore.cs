using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;

namespace BulletHellJam2022.Assets.Scripts.Managers.Menus.Help
{
    public class HelpMenuManagerCore : IHelpMenuManager
    {
        /// <inheritdoc/>
        public event EventHandler BackEvent;

        ///// <inheritdoc/>
        //public event EventHandler<Sound> PlaySoundEvent;

        public readonly IHelpMenuManager Parent;
        public StaticObjectsSO StaticObjects => Parent.StaticObjects;


        public HelpMenuManagerCore(IHelpMenuManager parent)
        {
            Parent = parent;
        }

        /// <inheritdoc/>
        public void BackToMainMenu()
        {

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
