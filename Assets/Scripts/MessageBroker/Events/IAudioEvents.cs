using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using System;
using UnityEngine.Events;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker.Events
{
    [Serializable] public class PlaySound : UnityEvent<object, string, Sound> { }
    [Serializable] public class PlayMusic : UnityEvent<object, string, Sound> { }


    public interface IAudioEventsPublisher
    {
        void PublishPlaySound(object publisher, string target, Sound sound);
        void PublishPlayMusic(object publisher, string target, Sound sound);
    }

    public interface IAudioEventsMessenger
    {
        PlaySound PlaySound { get; }
        PlayMusic PlayMusic { get; }
    }
}
