using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InFlammis.Victoria.Assets.Scripts.Layout.Sectors.StateMachine
{
    public partial class StateFactory : IStateFactory
    {
        private readonly State awakenState;
        private readonly State activeState;
        private readonly State inactiveState;

        public StateFactory(Sector sector)
        {
            this.awakenState = new Sector.AwakenState(sector);
            this.activeState = new Sector.ActiveState(sector);
            this.inactiveState = new Sector.InactiveState(sector);
        }
        public State Awaken => awakenState;
        public State Active => activeState;
        public State Inactive => inactiveState;
    }
}
