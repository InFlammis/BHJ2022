using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InFlammis.Victoria.Assets.Scripts.Managers.GameManagement.StateMachine
{
    /// <summary>
    /// This is a Stack for states.
    /// Evolution of a normal StateMachine only based on one state at a time, this pattern allows multiple states at the same time.
    /// The new state can replace the previous one, or be pushed at the top of the stack. The previous active state is then pushed back in the Stack.
    /// When the current state is done, it is pulled out of the stack and the previous stack become active once again.
    /// </summary>
    public class StateStack
    {
        /// <summary>
        /// Event raised when a State is being popped out of the stack
        /// </summary>
        public virtual event EventHandler<State> PoppingStateEvent;

        /// <summary>
        /// Event raised when a new State is being pushed into the stack
        /// </summary>
        public virtual event EventHandler<State> PushingStateEvent;

        /// <summary>
        /// The stack instance
        /// </summary>
        protected IStack<State> _stack = new Stack<State>();

        /// <summary>
        /// Expose the Peek method from the stack instance.  <see cref="Stack{T}.Peek(T)"></see>
        /// </summary>
        /// <returns></returns>
        public virtual State Peek() => _stack.Peek();

        /// <summary>
        /// Decorator for the Pop method of the stack instance.  <see cref="Stack{T}.Pop(T)"></see>
        /// </summary>
        /// <returns></returns>
        public virtual State Pop()
        {
            var state = _stack.Pop(); //throws InvalidOperationException if stack is empty
            state.OnDeactivate();
            state.OnExit();

            RaisePoppingStateEvent(state);

            if (_stack.Count > 0)
            {
                _stack.Peek().OnActivate();
            }

            return state;
        }

        /// <summary>
        /// Decorator for the Push method of the stack instance. <see cref="Stack{T}.Push(T)"></see>
        /// </summary>
        /// <param name="state">New state to push into the stack</param>
        public virtual void Push(State state)
        {
            if (_stack.Count > 0)
            {
                _stack.Peek().OnDeactivate();
            }

            _stack.Push(state);

            RaisePushingStateEvent(state);

            state.OnEnter();
            state.OnActivate();
        }

        /// <summary>
        /// Clear the stack
        /// </summary>
        public virtual void Clear()
        {
            while (_stack.Count > 0)
            {
                var state = Pop();
            }
        }

        /// <summary>
        /// Raise a PoppingStateEvent
        /// </summary>
        /// <param name="state">The Event popped</param>
        protected void RaisePoppingStateEvent(State state) => PoppingStateEvent?.Invoke(this, state);

        /// <summary>
        /// Raise a PushingStateEvent
        /// </summary>
        /// <param name="state">The event pushed</param>
        protected void RaisePushingStateEvent(State state) => PushingStateEvent?.Invoke(this, state);
    }
}
