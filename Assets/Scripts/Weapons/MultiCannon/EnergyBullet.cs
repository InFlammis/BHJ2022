using BulletHellJam2022.Assets.Scripts.Enemies;
using BulletHellJam2022.Assets.Scripts.Player;
using UnityEngine;

namespace BulletHellJam2022.Assets.Scripts.Weapons.MultiCannon
{
    public class EnergyBullet : BulletBase
    {
        private Vector3 _velocity = Vector3.zero;

        void Start()
        {
            this.transform.localScale = Vector3.one * InitSettings.Scale;
            _velocity = transform.up * this.InitSettings.Speed;

            Destroy(this.GameObject, 2);
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
    }
}
