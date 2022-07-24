using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InFlammis.Victoria.Assets.Scripts.Managers.GameManagement.StateMachine
{
    /// <summary>
    /// Base state for Game Manager
    /// When the GameManager enters a new state, the state is created and added to the State Stack.
    /// When the State enters the State Stack, the OnEnter method should be invoked
    /// When the State is at the top of the State Stack, it is activated, and the OnActivate method should be called
    /// When the State is in the stack, but not at the top of the stack, it is deactivated, the OnDeactivate method should be called
    /// When the State exits the State Stack, the OnExit method should be called
    /// <example>
    /// When the player starts playing, the Play state is created and added to the State Stack.
    /// It is at the top of the stack, therefore activated
    /// In order, OnEnter and OnActivate are invoked.
    /// If the player presses the Escape button, the GameManager passes to Pause state.
    /// Here, the new Pause state is created and added to the State Stack.
    /// The Play state is deactivated, no longer being at the top of the stack, and its OnDeactivated method is invoked,
    /// The Pause state is added to the State Stack, and its OnEnter and OnActivate methods are invoked, being it now at the top of the stack.
    /// If the player presses the Escape button once more, the GameManager returns to the Play state, by
    /// Removing the Pause state from the stack. This causes the Pause state to have OnDeactivate and OnExit methods invoked
    /// The Play state now returns to the top of the stack and its OnActivate method is invoked
    /// </example>
    /// </summary>
    [Serializable]
    public abstract class State
    {
        public IGameManager GameManager { get; private set; }

        public IUnitySceneManagerWrapper SceneManagerWrapper { get; private set; }

        public StateStateEnum StateState { get; protected set; } = StateStateEnum.NotInStack;

        public State(IGameManager gameManager, IUnitySceneManagerWrapper sceneManagerWrapper)
        {
            GameManager = gameManager;
            SceneManagerWrapper = sceneManagerWrapper;
        }
        
        public virtual void OnEnter()
        {
            StateState = StateStateEnum.InStack;

            SceneManagerWrapper.SceneLoaded += SceneLoaded;
            SceneManagerWrapper.SceneUnloaded += SceneUnloaded;
        }

        public virtual void OnExit()
        {
            StateState = StateStateEnum.NotInStack;

            SceneManagerWrapper.SceneLoaded -= SceneLoaded;
            SceneManagerWrapper.SceneUnloaded -= SceneUnloaded;
        }

        public virtual void OnActivate()
        {
            StateState = StateStateEnum.Activated;
        }

        public virtual void OnDeactivate()
        {
            StateState = StateStateEnum.InStack;
        }

        public virtual void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
        }

        public virtual void SceneUnloaded(Scene scene)
        {
        }

        public virtual void PauseResumeGame()
        {
        }
    }

    /// <summary>
    /// State of a state
    /// </summary>
    public enum StateStateEnum{
        NotInStack,
        InStack,
        Activated,
    }
}
