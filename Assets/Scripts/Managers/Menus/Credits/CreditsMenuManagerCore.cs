using System;

namespace InFlammis.Victoria.Assets.Scripts.Managers.Menus.Credits
{
    public class CreditsMenuManagerCore : ICreditsMenuManager
    {
        public readonly ICreditsMenuManager Parent;

        public StaticObjectsSO StaticObjects => Parent.StaticObjects;

        public CreditsMenuManagerCore(ICreditsMenuManager parent)
        {
            Parent = parent;
        }

        public void OnAwake() { }

        public void OnStart()
        {
        }

        public void BackToMainMenu()
        {
        }
    }
}
