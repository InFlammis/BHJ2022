using BulletHellJam2022.Assets.Scripts.MessageBroker;
using BulletHellJam2022.Assets.Scripts.MessageBroker.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Managers.HealthManagement
{
    public class HealthManager : 
        IHealthManager
    {
        /// <inheritdoc/>
        public int MaxHealth { get; set; }

        /// <inheritdoc/>
        private int _health;

        /// <inheritdoc/>
        public int Health
        {
            get => _health;
            set
            {
                _health = value;

                PublishHealthLevelChanged(this, _Target, _health, MaxHealth);

                if (_health <= 0)
                {
                    PublishHasDied(this, _Target);
                }
            }
        }
        
        /// <inheritdoc/>
        public bool IsInvulnerable { get; set; }
        
        /// <inheritdoc/>
        public bool IsDead { get; protected set;
        }

        public IMessenger Messenger { get; private set; }

        /// <inheritdoc/>
        public void Heal(int byValue)
        {
            var newHealthValue =  Health + byValue;
            if (newHealthValue > MaxHealth)
            {
                newHealthValue = MaxHealth;
            }

            Health = newHealthValue;

        }

        /// <inheritdoc/>
        public void Heal()
        {
            Heal(MaxHealth);
            IsDead = false;
        }

        /// <inheritdoc/>
        public void Damage(int byValue)
        {
            if (IsInvulnerable) return;

            var newHealthValue = Health - byValue;
            if (newHealthValue < 0)
            {
                newHealthValue = 0;
            }

            Health = newHealthValue;
        }

        /// <inheritdoc/>
        public void Kill()
        {
            Damage(Health);
        }

        public void PublishHasDied(object publisher, string target)
        {
            (Messenger as IHealthManagerEventsPublisher).PublishHasDied(this, target);
        }

        public void PublishHealthLevelChanged(object publisher, string target, int healthLevel, int maxHealthLevel)
        {
            (Messenger as IHealthManagerEventsPublisher).PublishHealthLevelChanged(this, target, healthLevel, maxHealthLevel);
        }

        private string _Target;
        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="health">Initial health level</param>
        /// <param name="maxHealth">Maximum health level</param>
        /// <param name="isInvulnerable">It is invulnerable at start</param>
        public HealthManager(string target, int health, int maxHealth, bool isInvulnerable)
        {
            this.Messenger = GameObject.FindObjectOfType<Messenger>();
            this._Target = target;
            this.MaxHealth = maxHealth;
            this.Health = health;
            this.IsInvulnerable = isInvulnerable;
        }

    }
}
