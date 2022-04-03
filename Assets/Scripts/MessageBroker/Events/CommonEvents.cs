using System;
using UnityEngine.Events;

namespace BulletHellJam2022.Assets.Scripts.MessageBroker.Events
{
    [Serializable] public class HasDied : UnityEvent<object, string> { }
    [Serializable] public class HealthLevelChanged : UnityEvent<object, string, int, int> { }

}
