using System;
using System.Collections;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    public sealed class Continuous : SpitStrategy
    {
        public override event Action<Vector3, Quaternion> SpitEvent;

        protected override IEnumerator Spit()
        {
            var delta = 1.0f / InitSettings.RateOfSpit;

            while (true)
            {
                this.SpitEvent?.Invoke(Vector2.zero, Quaternion.identity);

                yield return new WaitForSeconds(delta);
            }
        }
    }
}