using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InFlammis.Victoria.Assets.Scripts.Layout.Sectors
{
    public abstract class State
    {
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}
