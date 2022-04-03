using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IMenuEventsPublisher, IMenuEventsMessenger { }
    public partial class Messenger
    {
        [SerializeField] private StartGame _Menu_StartGame = new StartGame();
        [SerializeField] private QuitGame _Menu_QuitGame = new QuitGame();
        [SerializeField] private OpenCredits _Menu_OpenCredits = new OpenCredits();
        [SerializeField] private OpenHelp _Menu_OpenHelp = new OpenHelp();
        [SerializeField] private BackToMain _Menu_BackToMain = new BackToMain();
        [SerializeField] private ResumeGame _Menu_ResumeGame = new ResumeGame();
        [SerializeField] private PauseGame _Menu_PauseGame = new PauseGame();
        [SerializeField] private QuitCurrentGame _Menu_QuitCurrentGame = new QuitCurrentGame();

        StartGame IMenuEventsMessenger.StartGame => _Menu_StartGame;
        QuitGame IMenuEventsMessenger.QuitGame => _Menu_QuitGame;
        OpenCredits IMenuEventsMessenger.OpenCredits => _Menu_OpenCredits;
        OpenHelp IMenuEventsMessenger.OpenHelp => _Menu_OpenHelp;
        BackToMain IMenuEventsMessenger.BackToMain => _Menu_BackToMain;
        ResumeGame IMenuEventsMessenger.ResumeGame => _Menu_ResumeGame;
        PauseGame IMenuEventsMessenger.PauseGame => _Menu_PauseGame;
        QuitCurrentGame IMenuEventsMessenger.QuitCurrentGame => _Menu_QuitCurrentGame;

        void IMenuEventsPublisher.PublishStartGame(object publisher, string target)
        {
            _Menu_StartGame.Invoke(publisher, target);
        }
        void IMenuEventsPublisher.PublishQuitGame(object publisher, string target)
        {
            _Menu_QuitGame.Invoke(publisher, target);
        }
        void IMenuEventsPublisher.PublishOpenCreditsMenu(object publisher, string target)
        {
            _Menu_OpenCredits.Invoke(publisher, target);
        }
        void IMenuEventsPublisher.PublishOpenHelpMenu(object publisher, string target)
        {
            _Menu_OpenHelp.Invoke(publisher, target);
        }
        void IMenuEventsPublisher.PublishBackToMain(object publisher, string target)
        {
            _Menu_BackToMain.Invoke(publisher, target);
        }
        void IMenuEventsPublisher.PublishPauseGame(object publisher, string target)
        {
            _Menu_PauseGame.Invoke(publisher, target);
        }
        void IMenuEventsPublisher.PublishResumeGame(object publisher, string target)
        {
            _Menu_ResumeGame.Invoke(publisher, target);
        }
        void IMenuEventsPublisher.PublishQuitCurrentGame(object publisher, string target)
        {
            _Menu_QuitCurrentGame.Invoke(publisher, target);
        }
    }
}
