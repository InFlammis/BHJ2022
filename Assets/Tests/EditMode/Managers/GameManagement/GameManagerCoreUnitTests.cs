using System;
using BulletHellJam2022.Assets.Scripts;
using BulletHellJam2022.Assets.Scripts.Managers.GameManagement;
using BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FightshipArena.Assets.Tests.EditMode.Managers.GameManagement
{
    public class GameManagerCoreUnitTests
    {
        [Test]
        public void Constructor_initializes_parent()
        {
            //arrange
            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var monoBehaviourMock = new Mock<IMyMonoBehaviour>();
            monoBehaviourMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = monoBehaviourMock
            //    .As<IGameManager>()
            //    .Setup(x=>x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);

            var monoBehaviour = monoBehaviourMock.Object;

            //act
            var core = new GameManagerCore(monoBehaviour);

            //assert
            Assert.That(core.Parent, Is.SameAs(monoBehaviour));
        }

        [Test]
        public void OnAwake_attaches_eventListeners_to_stateStack()
        {
            //arrange
            var stateStackMock = new StateStackMock();
            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);

            var parent = parentMock.Object;

            var coreMock = new Mock<GameManagerCoreMock>(parent as IMyMonoBehaviour);

            var core = coreMock.Object;
            core.StateStack = stateStackMock;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(core, sceneManagerWrapper);
            var state = stateMock.Object;

            //act
            core.OnAwake();

            stateStackMock.RaisePoppingStateEvent_Caller(state);
            stateStackMock.RaisePushingStateEvent_Caller(state);

            //assert
            coreMock.Protected().Verify("StateStack_PoppingStateEvent", Times.Once(), ItExpr.IsAny<object>(), state);
            coreMock.Protected().Verify("StateStack_PushingStateEvent", Times.Once(), ItExpr.IsAny<object>(), state);
        }

        [Test]
        public void OnStart_calls_PushState()
        {
            //arrange
            var stateStackMock = new StateStackMock();

            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var parent = parentMock.Object;

            var coreMock = new Mock<GameManagerCoreMock>(parent);

            var core = coreMock.Object;
            core.StateStack = stateStackMock;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(core, sceneManagerWrapper);
            var state = stateMock.Object;

            //act
            core.OnStart();

            //assert
            coreMock.Protected().Verify("PushState", Times.Once(), ItExpr.IsAny<Init>());
        }

        [Test]
        public void OnAwake_subscribes_events_on_StackState()
        {
            //arrange
            var stateStackMock = new Mock<StateStack>();
            stateStackMock.SetupAdd(m => m.PoppingStateEvent += It.IsAny<EventHandler<State>>());
            stateStackMock.SetupAdd(m => m.PushingStateEvent += It.IsAny<EventHandler<State>>());

            var stateStack = stateStackMock.Object;

            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var parent = parentMock.Object;

            var core = new GameManagerCoreMock(parent);

            core.StateStack = stateStack;

            //act
            core.OnAwake();
            //assert

            stateStackMock.VerifyAdd(m => m.PoppingStateEvent += It.IsAny<EventHandler<State>>(), Times.Once);
            stateStackMock.VerifyAdd(m => m.PushingStateEvent += It.IsAny<EventHandler<State>>(), Times.Once);
        }

        [Test]
        public void StateStack_PushingStateEvent_subscribes_events_on_state()
        {
            //arrange

            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var gameManager = parentMock.Object;

            var core = new GameManagerCoreMock(gameManager);

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(core, sceneManagerWrapper);
            stateMock.SetupAdd(m => m.PauseGameEvent += (sender, args) => { });
            stateMock.SetupAdd(m => m.PlayGameEvent += (sender, args) => { });
            stateMock.SetupAdd(m => m.ResumeGameEvent += (sender, args) => { });
            stateMock.SetupAdd(m => m.QuitCurrentGameEvent += (sender, args) => { });
            stateMock.SetupAdd(m => m.QuitGameEvent += (sender, args) => { });

            var state = stateMock.Object;

            //act
            core.StateStack_PushingStateEvent_Caller(null, state);

            //assert
            stateMock.VerifyAdd(m => m.PauseGameEvent += It.IsAny<EventHandler>(), Times.Once);
            stateMock.VerifyAdd(m => m.PlayGameEvent += It.IsAny<EventHandler>(), Times.Once);
            stateMock.VerifyAdd(m => m.ResumeGameEvent += It.IsAny<EventHandler>(), Times.Once);
            stateMock.VerifyAdd(m => m.QuitCurrentGameEvent += It.IsAny<EventHandler>(), Times.Once);
            stateMock.VerifyAdd(m => m.QuitGameEvent += It.IsAny<EventHandler>(), Times.Once);
        }

        [Test]
        public void StateStack_PoppingStateEvent_unsubscribes_events_on_state()
        {
            //arrange

            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var gameManager = parentMock.Object;
            var core = new GameManagerCoreMock(gameManager);

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(core, sceneManagerWrapper);
            stateMock.SetupRemove(m => m.PauseGameEvent -= It.IsAny<EventHandler>());
            stateMock.SetupRemove(m => m.PlayGameEvent -= It.IsAny<EventHandler>());
            stateMock.SetupRemove(m => m.ResumeGameEvent -= It.IsAny<EventHandler>());
            stateMock.SetupRemove(m => m.QuitCurrentGameEvent -= It.IsAny<EventHandler>());
            stateMock.SetupRemove(m => m.QuitGameEvent -= It.IsAny<EventHandler>());

            var state = stateMock.Object;

            //act
            core.StateStack_PoppingStateEvent_Caller(null, state);

            //assert
            stateMock.VerifyRemove(m => m.PauseGameEvent -= It.IsAny<EventHandler>(), Times.Once);
            stateMock.VerifyRemove(m => m.PlayGameEvent -= It.IsAny<EventHandler>(), Times.Once);
            stateMock.VerifyRemove(m => m.ResumeGameEvent -= It.IsAny<EventHandler>(), Times.Once);
            stateMock.VerifyRemove(m => m.QuitCurrentGameEvent -= It.IsAny<EventHandler>(), Times.Once);
            stateMock.VerifyRemove(m => m.QuitGameEvent -= It.IsAny<EventHandler>(), Times.Once);
        }

        [Test]
        public void ReplaceState_calls_PopState_andPushState()
        {
            //arrange
            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var gameManager = parentMock.Object;
            var coreMock = new Mock<GameManagerCoreMock>(gameManager);
            coreMock
                .Setup(x => x.ReplaceState_Caller(It.IsAny<State>()))
                .CallBase();

            coreMock
                .Protected()
                .Setup("ReplaceState", ItExpr.IsAny<State>())
                .CallBase();

            var core = coreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(core, sceneManagerWrapper);
            var state = stateMock.Object;

            //act
            core.ReplaceState_Caller(state);

            //assert
            coreMock.Protected().Verify("PopState", Times.Once());
            coreMock.Protected().Verify("PushState", Times.Once(), state);

        }

        [Test]
        public void PushState_calls_Push_on_StateStack()
        {
            //arrange
            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var gameManager = parentMock.Object;
            var stateStackMock = new Mock<StateStack>();

            var stateStack = stateStackMock.Object;

            var core = new GameManagerCoreMock(gameManager);
            core.StateStack = stateStack;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(core, sceneManagerWrapper);
            var state = stateMock.Object;

            //act
            core.PushState_Caller(state);

            //assert
            stateStackMock.Verify(x=>x.Push(state), Times.Once);
        }

        [Test]
        public void PopState_calls_Pop_on_StateStack()
        {
            //arrange
            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var gameManager = parentMock.Object;

            var stateStackMock = new Mock<StateStack>();

            var stateStack = stateStackMock.Object;

            var core = new GameManagerCoreMock(gameManager);
            core.StateStack = stateStack;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(core, sceneManagerWrapper);
            var state = stateMock.Object;

            //act
            core.PopState_Caller();

            //assert
            stateStackMock.Verify(x => x.Pop(), Times.Once);
        }

        [Test]
        public void State_PauseGameEvent_calls_PushState()
        {
            //arrange
            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var gameManager = parentMock.Object;
            var coreMock = new Mock<GameManagerCoreMock>(gameManager);
            coreMock
                .Setup(x => x.State_PauseGameEvent_Caller(It.IsAny<object>(), It.IsAny<EventArgs>()))
                .CallBase();
            coreMock
                .Protected()
                .Setup("State_PauseGameEvent", ItExpr.IsAny<object>(), ItExpr.IsAny<EventArgs>())
                .CallBase();

            var core = coreMock.Object;
            
            //act
            core.State_PauseGameEvent_Caller(null, null);

            //assert
            coreMock.Protected().Verify("PushState", Times.Once(), ItExpr.IsAny<Pause>());
        }

        [Test]
        public void State_ResumeGameEvent_calls_PopState()
        {
            //arrange
            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var gameManager = parentMock.Object;

            var coreMock = new Mock<GameManagerCoreMock>(gameManager);
            coreMock
                .Setup(x => x.State_ResumeGameEvent_Caller(It.IsAny<object>(), It.IsAny<EventArgs>()))
                .CallBase();
            coreMock
                .Protected()
                .Setup("State_ResumeGameEvent", ItExpr.IsAny<object>(), ItExpr.IsAny<EventArgs>())
                .CallBase();

            var core = coreMock.Object;

            //act
            core.State_ResumeGameEvent_Caller(null, null);

            //assert
            coreMock.Protected().Verify("PopState", Times.Once());
        }

        [Test]
        public void State_PlayGameEvent_calls_PopState()
        {
            //arrange
            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var gameManager = parentMock.Object;
            var coreMock = new Mock<GameManagerCoreMock>(gameManager);
            coreMock
                .Setup(x => x.State_PlayGameEvent_Caller(It.IsAny<object>(), It.IsAny<EventArgs>()))
                .CallBase();
            coreMock
                .Protected()
                .Setup("State_PlayGameEvent", ItExpr.IsAny<object>(), ItExpr.IsAny<EventArgs>())
                .CallBase();

            var core = coreMock.Object;

            //act
            core.State_PlayGameEvent_Caller(null, null);

            //assert
            coreMock.Protected().Verify("ReplaceState", Times.Once(), ItExpr.IsAny<Play>());
        }

        [Test]
        public void State_QuitCurrentGameEvent_calls_Clear_on_StateStack_and_PushState()
        {
            //arrange
            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var gameManager = parentMock.Object;

            var stateStackMock = new Mock<StateStack>();
            var stateStack = stateStackMock.Object;

            var coreMock = new Mock<GameManagerCoreMock>(gameManager);
            coreMock
                .Setup(x => x.State_QuitCurrentGameEvent_Caller(It.IsAny<object>(), It.IsAny<EventArgs>()))
                .CallBase();
            coreMock
                .Protected()
                .Setup("State_QuitCurrentGameEvent", ItExpr.IsAny<object>(), ItExpr.IsAny<EventArgs>())
                .CallBase();

            var core = coreMock.Object;
            core.StateStack = stateStack;

            //act
            core.State_QuitCurrentGameEvent_Caller(null, null);

            //assert
            stateStackMock.Verify(x=>x.Clear(), Times.Once);
            coreMock.Protected().Verify("PushState", Times.Once(), ItExpr.IsAny<Init>());
        }

        [Test]
        public void State_QuitGameEvent_calls_PushState()
        {
            //arrange
            var soundManagerMock = new Mock<ISoundManager>();

            var gameObject = new GameObject();
            var parentMock = new Mock<IMyMonoBehaviour>();
            parentMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);

            //var mbAsGameManager = parentMock
            //    .As<IGameManager>()
            //    .Setup(x => x.StaticObjects.SoundManager)
            //    .Returns(soundManagerMock.Object);
            var gameManager = parentMock.Object;

            var coreMock = new Mock<GameManagerCoreMock>(gameManager);
            coreMock
                .Setup(x => x.State_QuitGameEvent_Caller(It.IsAny<object>(), It.IsAny<EventArgs>()))
                .CallBase();
            coreMock
                .Protected()
                .Setup("State_QuitGameEvent", ItExpr.IsAny<object>(), ItExpr.IsAny<EventArgs>())
                .CallBase();

            var core = coreMock.Object;

            //act
            core.State_QuitGameEvent_Caller(null, null);

            //assert
            coreMock.Protected().Verify("PushState", Times.Once(), ItExpr.IsAny<Quit>());
        }

        public class GameManagerCoreMock : GameManagerCore
        {
            public StateStack StateStack
            {
                get => _stateStack;
                set => _stateStack = value;
            }

            public GameManagerCoreMock(IMyMonoBehaviour parent) : base(parent) { }

            public void StateStack_PushingStateEvent_Caller(object sender, State state)
            {
                StateStack_PushingStateEvent(sender, state);
            }

            public void StateStack_PoppingStateEvent_Caller(object sender, State state)
            {
                StateStack_PoppingStateEvent(sender, state);
            }

            public virtual void ReplaceState_Caller(State state)
            {
                ReplaceState(state);
            }

            public virtual void PushState_Caller(State state)
            {
                PushState(state);
            }

            public virtual void PopState_Caller()
            {
                PopState();
            }

            public virtual void State_PauseGameEvent_Caller(object sender, EventArgs e)
            {
                State_PauseGameEvent(sender, e);
            }
            public virtual void State_ResumeGameEvent_Caller(object sender, EventArgs e)
            {
                State_ResumeGameEvent(sender, e);
            }
            public virtual void State_PlayGameEvent_Caller(object sender, EventArgs e)
            {
                State_PlayGameEvent(sender, e);
            }
            public virtual void State_QuitCurrentGameEvent_Caller(object sender, EventArgs e)
            {
                State_QuitCurrentGameEvent(sender, e);
            }
            public virtual void State_QuitGameEvent_Caller(object sender, EventArgs e)
            {
                State_QuitGameEvent(sender, e);
            }
        }


        public class StateStackMock : StateStack
        {
            public void RaisePoppingStateEvent_Caller(State state) => RaisePoppingStateEvent(state);
            public void RaisePushingStateEvent_Caller(State state) => RaisePushingStateEvent(state);
        }

        //public class StateMock : State
        //{
        //    public void RaisePauseGameEvent() => PauseGameEvent?.Invoke(this, new EventArgs());
        //    public void RaiseResumeGameEvent() => ResumeGameEvent?.Invoke(this, new EventArgs());
        //    public void RaisePlayGameEvent() => PlayGameEvent?.Invoke(this, new EventArgs());
        //    public void RaiseQuitCurrentGameEvent() => QuitCurrentGameEvent?.Invoke(this, new EventArgs());
        //    public void RaiseQuitGameEvent() => QuitGameEvent?.Invoke(this, new EventArgs());

        //    public StateMock(IGameManagerCore gameManager) : base(gameManager)
        //    {
        //    }

        //    public override event EventHandler ResumeGameEvent;
        //    public override event EventHandler PlayGameEvent;
        //    public override event EventHandler QuitCurrentGameEvent;
        //    public override event EventHandler PauseGameEvent;
        //    public override event EventHandler QuitGameEvent;
        //}
    }
}
