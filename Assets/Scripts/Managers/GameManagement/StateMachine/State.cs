using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine
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
    public abstract class State
    {
        /// <summary>
        /// Raise a PauseGameEvent, for when a Pause menu is requested to open
        /// </summary>
        public abstract event EventHandler PauseGameEvent;
        /// <summary>
        /// Raise a ResumeGameEvent, for when to Resume from a Pause menu 
        /// </summary>
        public abstract event EventHandler ResumeGameEvent;
        /// <summary>
        /// Raise a PlayGameEvent, to start Playing a new game
        /// </summary>
        public abstract event EventHandler PlayGameEvent;
        /// <summary>
        /// Raise a QuitCurrentGameEvent, for quitting for the current game
        /// </summary>
        public abstract event EventHandler QuitCurrentGameEvent;
        /// <summary>
        /// Raise a QuitGameEvent, for closing up the game and return to the OS
        /// </summary>
        public abstract event EventHandler QuitGameEvent;
        /// <summary>
        /// Raise a CreditsEvent, for when a Credit menu is requested to open
        /// </summary>
        public abstract event EventHandler CreditsEvent;
        /// <summary>
        /// Raise a HelpEvent, for when a Help menu is requested to open
        /// </summary>
        public abstract event EventHandler HelpEvent;
        /// <summary>
        /// Raise a BackToMainMenuEvent, for when to return to the Main Menu
        /// </summary>
        public abstract event EventHandler BackToMainMenuEvent;

        /// <summary>
        /// Reference to the GameManager instance
        /// </summary>
        public IGameManager GameManager { get; private set; }

        /// <summary>
        /// Reference to the IUnitySceneManagerWrapper instance
        /// </summary>
        public IUnitySceneManagerWrapper SceneManagerWrapper { get; private set; }

        /// <summary>
        /// State of the state
        /// </summary>
        public StateStateEnum StateState { get; protected set; } = StateStateEnum.NotInStack;

        /// <summary>
        /// Create an instance of the State class
        /// </summary>
        /// <param name="gameManager">Reference to the <see cref="IGameManager"/></param>
        /// <param name="sceneManagerWrapper">Reference to the <see cref="IUnitySceneManagerWrapper"/></param>
        public State(IGameManager gameManager, IUnitySceneManagerWrapper sceneManagerWrapper)
        {
            GameManager = gameManager;
            SceneManagerWrapper = sceneManagerWrapper;
        }
        
        /// <summary>
        /// Fires when the State is added to the State Stack
        /// </summary>
        public virtual void OnEnter()
        {
            //Debug.Log($"State {this.GetType().Name}: OnEnter");
            StateState = StateStateEnum.InStack;

            SceneManagerWrapper.SceneLoaded += SceneLoaded;
            SceneManagerWrapper.SceneUnloaded += SceneUnloaded;
        }

        /// <summary>
        /// Fires when the State is removed from the State Stack
        /// </summary>
        public virtual void OnExit()
        {
            //Debug.Log($"State {this.GetType().Name}: OnExit");
            StateState = StateStateEnum.NotInStack;

            SceneManagerWrapper.SceneLoaded -= SceneLoaded;
            SceneManagerWrapper.SceneUnloaded -= SceneUnloaded;
        }

        /// <summary>
        /// Fires when the State is Activated
        /// </summary>
        public virtual void OnActivate()
        {
            //Debug.Log($"State {this.GetType().Name}: OnActivate");
            StateState = StateStateEnum.Activated;
        }

        /// <summary>
        /// Fires when the State is deactivated 
        /// </summary>
        public virtual void OnDeactivate()
        {
            //Debug.Log($"State {this.GetType().Name}: OnDeactivate");
            StateState = StateStateEnum.InStack;
        }

        /// <summary>
        /// Called when a scene is loaded
        /// </summary>
        /// <param name="scene">the new scene loaded</param>
        /// <param name="loadSceneMode">The load mode</param>
        public virtual void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            //Debug.Log($"State {this.GetType().Name}: SceneLoaded: {scene.name}");

        }

        /// <summary>
        /// Called when a scene is unloaded
        /// </summary>
        /// <param name="scene"></param>
        public virtual void SceneUnloaded(Scene scene)
        {
            //Debug.Log($"State {this.GetType().Name}: SceneUnloaded: {scene.name}");
        }

        /// <summary>
        /// Manage Pause/Resume state
        /// </summary>
        public virtual void PauseResumeGame()
        {
            //Debug.Log($"State {this.GetType().Name}: Pause/Resume game");
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
