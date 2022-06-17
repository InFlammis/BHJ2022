using InFlammis.Victoria.Assets.Scripts.Player;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.PowerUps.HealthCharger
{
    /// <summary>
    /// Power-up of type HealthCharger. When collected by the Player, increases its health level by the carried amount.
    /// </summary>
    public class HealthCharger : PowerUpBase
    {
        /// <summary>
        /// Manage collision with player and transfer its value to player's healthManager
        /// </summary>
        /// <param name="collision"></param>
        void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            _staticObjects.Messenger.PublishHealthCollected(this, "Player", (int)InitSettings.Value);
            GameObject.Destroy(this.GameObject);
        }
    }
}
