using InFlammis.Victoria.Assets.Scripts.Managers.SoundManagement;
using InFlammis.Victoria.Assets.Scripts.MessageBroker.Events;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.MessageBroker
{
    public partial interface IMessenger : IAudioEventsPublisher, IAudioEventsMessenger { }
    public partial class Messenger
    {
        [SerializeField] private PlaySound _PlaySound = new PlaySound();
        [SerializeField] private PlayMusic _PlayMusic = new PlayMusic();

        PlaySound IAudioEventsMessenger.PlaySound => _PlaySound;
        PlayMusic IAudioEventsMessenger.PlayMusic => _PlayMusic;

        void IAudioEventsPublisher.PublishPlaySound(object publisher, string target, Sound sound)
        {
            _PlaySound.Invoke(publisher, target, sound);
        }

        void IAudioEventsPublisher.PublishPlayMusic(object publisher, string target, Sound sound)
        {
            _PlayMusic.Invoke(publisher, target, sound);
        }
    }
}
