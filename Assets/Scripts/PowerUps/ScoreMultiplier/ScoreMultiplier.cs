﻿using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.PowerUps.ScoreMultiplier
{
    /// <summary>
    /// Power-up of type ScoreMultiplier. When collected by the player, increases the Multiplier factor by the carried value.
    /// </summary>
    public class ScoreMultiplier : PowerUpBase
    {
        /// <summary>
        /// Manage collision with player and transfer its value
        /// </summary>
        /// <param name="collision"></param>
        void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            _staticObjects.Messenger.PublishScoreMultiplierCollected(this, "Player", (int)InitSettings.Value);

            GameObject.Destroy(this.GameObject);
        }
    }
}