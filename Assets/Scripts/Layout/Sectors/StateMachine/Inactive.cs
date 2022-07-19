using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InFlammis.Victoria.Assets.Scripts.Layout.Sectors
{
    public partial class Sector
    {
        public class InactiveState : State
        {
            private Sector _sector;

            public InactiveState(Sector sector)
            {
                this._sector = sector;
            }

            public override void OnEnter()
            {
                base.OnEnter();

                _sector.areas.NorthAa?.SetActive(false);
                _sector.areas.SouthAa?.SetActive(false);
                _sector.areas.NorthNaa?.SetActive(false);
                _sector.areas.SouthNaa?.SetActive(false);
                _sector.areas.StainColliders?.SetActive(false);
                _sector.areas.SectorCollider.enabled = false;
            }
        }
    }
}
