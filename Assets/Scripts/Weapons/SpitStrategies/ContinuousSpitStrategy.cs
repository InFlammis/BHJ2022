using System;
using System.Collections;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons
{
    public class ContinuousSpitStrategy : SpitStrategy
    {
        public override event Action<Vector3, Quaternion> SpitEvent;
        public override event Action<float> BeginSpitEvent;
        public override event Action<float> EndSpitEvent;

        protected override void RaiseBeginSpitEvent()
        {
            this.BeginSpitEvent?.Invoke(Time.time);
        }

        protected override void RaiseEndSpitEvent()
        {
            this.EndSpitEvent?.Invoke(Time.time);
        }

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