using System;

namespace InFlammis.Victoria.Assets.Scripts.Managers.Menus.Main
{
    public class MainMenuManagerCore : IMainMenuManager
    {
        public readonly IMainMenuManager Parent;

        public StaticObjectsSO StaticObjects => Parent.StaticObjects;

        public MainMenuManagerCore(IMainMenuManager parent)
        {
            Parent = parent;
        }

        public void OnStart()
        {
        }

        public void OnAwake() { }

        public void StartGame()
        {
        }

        public void QuitGame()
        {
        }

        public void ShowCredits()
        {
        }

        public void ShowHelp()
        {
        }
    }
}
