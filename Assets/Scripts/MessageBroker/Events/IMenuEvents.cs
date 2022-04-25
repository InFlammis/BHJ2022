using System;
using UnityEngine.Events;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker.Events
{
    [Serializable] public class StartGame : UnityEvent<object, string> { }
    [Serializable] public class QuitGame : UnityEvent<object, string> { }
    [Serializable] public class OpenCredits : UnityEvent<object, string> { }
    [Serializable] public class OpenHelp : UnityEvent<object, string> { }
    [Serializable] public class BackToMain : UnityEvent<object, string> { }
    [Serializable] public class PauseGame : UnityEvent<object, string> { }
    [Serializable] public class ResumeGame : UnityEvent<object, string> { }
    [Serializable] public class QuitCurrentGame : UnityEvent<object, string> { }
    [Serializable] public class PreRollFinished : UnityEvent<object, string> { }



    public interface IMenuEventsPublisher
    {
        void PublishStartGame(object publisher, string target);
        void PublishQuitGame(object publisher, string target);
        void PublishOpenCreditsMenu(object publisher, string target);
        void PublishOpenHelpMenu(object publisher, string target);
        void PublishBackToMain(object publisher, string target);
        void PublishPauseGame(object publisher, string target);
        void PublishResumeGame(object publisher, string target);
        void PublishQuitCurrentGame(object publisher, string target);
        void PublishPreRollFinished(object publisher, string target);
    }

    public interface IMenuEventsMessenger
    {
        StartGame StartGame { get; }
        QuitGame QuitGame { get; }
        OpenCredits OpenCredits { get; }
        OpenHelp OpenHelp { get; }
        BackToMain BackToMain { get; }
        PauseGame PauseGame { get; }
        ResumeGame ResumeGame { get; }
        QuitCurrentGame QuitCurrentGame { get; }

        PreRollFinished PreRollFinished { get; }
    }
}
