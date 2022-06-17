using System;

namespace InFlammis.Victoria.Assets.Scripts.Managers.Menus.Help
{
    public class HelpMenuManagerCore : IHelpMenuManager
    {
        public readonly IHelpMenuManager Parent;
        public StaticObjectsSO StaticObjects => Parent.StaticObjects;


        public HelpMenuManagerCore(IHelpMenuManager parent)
        {
            Parent = parent;
        }

        public void BackToMainMenu()
        {
        }

        public void OnAwake() { }

        public void OnStart()
        {
        }
    }
}
