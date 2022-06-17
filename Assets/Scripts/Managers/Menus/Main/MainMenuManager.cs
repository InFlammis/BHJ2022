using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using System;

namespace InFlammis.Victoria.Assets.Scripts.Managers.Menus.Main
{
    public class MainMenuManager : MenuManager, IMainMenuManager
    {
        public IMainMenuManager Core { get; protected set; }

        void Awake()
        {
            Core = new MainMenuManagerCore(this);

            OnAwake();
        }
        void Start()
        {
            OnStart();
        }

        public void OnStart()
        {
            Core.OnStart();
            StaticObjects.Messenger.PublishPlayMusic(this, null, _soundSettings.BackgroundMusic);
        }

        public void OnAwake()
        {
            Core.OnAwake();
        }

        public void StartGame()
        {
            Core.StartGame();
            StaticObjects.Messenger.PublishStartGame(this, null);
        }

        public void QuitGame()
        { 
            Core.QuitGame();
            StaticObjects.Messenger.PublishQuitGame(this, null);
        }

        public override void PlaySound(Sound sound)
        {
            StaticObjects.Messenger.PublishPlaySound(this, null, sound);
        }

        public void ShowCredits()
        {
            Core.ShowCredits();
            StaticObjects.Messenger.PublishOpenCreditsMenu(this, null);
        }

        public void ShowHelp()
        {
            Core.ShowHelp();
            StaticObjects.Messenger.PublishOpenHelpMenu(this, null);
        }
    }
}
