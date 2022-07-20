using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InFlammis.Victoria.Assets.Scripts.Layout.Sectors
{
    public partial class Sector
    {
        public class AwakenState : State
        {
            private Sector _sector;

            public AwakenState(Sector sector)
            {
                this._sector = sector;
            }

            public override void OnEnter()
            {
                base.OnEnter();

                _sector.areas.SetActivationAreasActive(true);
                _sector.areas.SetSectorColliderActive(true);
                _sector.areas.SetNeighbourActivationAreasActive(false);
                _sector.areas.SetStainCollidersActive(false);
            }
        }
    }
}
