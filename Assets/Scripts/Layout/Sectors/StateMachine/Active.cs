﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InFlammis.Victoria.Assets.Scripts.Layout.Sectors
{
    public partial class Sector
    {
        public class ActiveState : State
        {
            private Sector _sector;

            public ActiveState(Sector sector)
            {
                this._sector = sector;
            }

            public override void OnEnter()
            {
                base.OnEnter();
                _sector.areas.NorthAa.SetActive(true);
                _sector.areas.SouthAa.SetActive(true);
                _sector.areas.NorthNaa.SetActive(true);
                _sector.areas.SouthNaa.SetActive(true);
                _sector.areas.StainColliders.SetActive(true);
                _sector.areas.SectorCollider.enabled = true;
            }
        }
    }
}
