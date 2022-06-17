using System;

namespace InFlammis.Victoria.Assets.Scripts.Enemies.Infantry.StateMachine
{
    /// <summary>
    /// Abstract State for an Infantry enemy
    /// </summary>
    public abstract class InfantryState : IInfantryState{

        public virtual void Move() { }

        public virtual void Rotate() { }

        public virtual void OnEnter()
        {             
        }

        public virtual void OnExit()
        {
        }

        public abstract event Action<IInfantryState> ChangeState;

        public InfantryControllerCore Parent { get; set; }

        public StateFactory Factory { get; set; }
    }
}