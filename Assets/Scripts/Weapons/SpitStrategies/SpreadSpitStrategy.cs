using System;
using System.Collections;
using UnityEngine;

namespace InFlammis.Victoria.Assets.Scripts.Weapons.SpitStrategies
{
    public class SpreadSpitStrategy : SpitStrategy
    {
        public override event Action<Vector3, Quaternion> SpitEvent;

        protected override IEnumerator Spit()
        {
            var delta = 1.0f / InitSettings.RateOfSpit;

            if(InitSettings.BurstSize == 1)
            {
                this.SpitEvent?.Invoke(Vector2.zero, Quaternion.identity);
                yield break;
            }

            var wedge = InitSettings.BurstWidth / (InitSettings.BurstSize - 1);

            var demiAngle = InitSettings.BurstWidth / 2;

            for (float i = -demiAngle; i <= demiAngle; i += wedge)
            {
                var rotation = Quaternion.AngleAxis(i, new Vector3(0, 0, 1));
                this.SpitEvent?.Invoke(Vector2.zero, rotation);
            }

            yield break;
        }
    }
}
