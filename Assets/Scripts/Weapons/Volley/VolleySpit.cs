using InFlammis.Victoria.Assets.Scripts.Enemies;
using InFlammis.Victoria.Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons.Volley
{
    public class VolleySpit : SpitBase
    {
        private Vector3 _velocity = Vector3.zero;

        private Vector3 startPosition;

        void Start()
        {
            InitCheck();
            SetInitSettings();

            this.transform.localScale = Vector3.one * InitSettings.Scale;
            _velocity = transform.up * this.InitSettings.Speed;

            startPosition = transform.position;

            StartCoroutine(CheckForDestruction());
        }
        void FixedUpdate()
        {
            this.transform.position += _velocity;
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Enemy")
            {
                var enemy = col.gameObject.GetComponent<EnemyController>();
                enemy.HealthManager.Damage(InitSettings.Damage);
            }
            else if (col.gameObject.tag == "Player")
            {
                var obj = col.gameObject.GetComponent<PlayerController>();
                obj.HealthManager.Damage(InitSettings.Damage);
            }

            IsDestroyed = true;
            GameObject.Destroy(this.GameObject);
        }

        IEnumerator CheckForDestruction()
        {
            yield return new WaitUntil(() => Vector3.Distance(startPosition, transform.position) > InitSettings.Distance);

            StaticObjects.Messenger.PublishSpitHasDied(this.GameObject, null);
            Destroy(this.GameObject);
        }
    }
}