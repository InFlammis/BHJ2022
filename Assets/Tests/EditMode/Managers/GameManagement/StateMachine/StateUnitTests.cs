using InFlammis.Victoria.Assets.Scripts;
using InFlammis.Victoria.Assets.Scripts.Managers.GameManagement;
using InFlammis.Victoria.Assets.Scripts.Managers.GameManagement.StateMachine;
using Moq;
using NUnit.Framework;

namespace FightshipArena.Assets.Tests.EditMode.Managers.GameManagement.StateMachine
{
    [TestFixture]
    public class StateUnitTests
    {
        [Test]
        public void Constructor_assigns_input_parameters_to_properties()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            stateMock.CallBase = true;

            //act
            var state = stateMock.Object;
            
            //assert
            Assert.That(state.GameManager, Is.SameAs(gameManagerCore));
            Assert.That(state.SceneManagerWrapper, Is.SameAs(sceneManagerWrapper));

        }

        [Test]
        public void OnConstruction_StateState_is_initialised_to_NotInStack()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            stateMock.CallBase = true;

            //act
            var state = stateMock.Object;

            //assert
            Assert.That(state.StateState, Is.EqualTo(StateStateEnum.NotInStack));
        }


        [Test]
        public void OnEnter_set_stateState_to_InStack()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            stateMock.CallBase = true;

            var state = stateMock.Object;

            //act
            state.OnEnter();

            //assert
            Assert.That(state.StateState, Is.EqualTo(StateStateEnum.InStack));
        }

        [Test]
        public void OnEnter_attaches_event_listeners_to_sceneManagerWrapper()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();

            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            stateMock.CallBase = true;

            var state = stateMock.Object;

            sceneManagerWrapperMock.SetupAdd(x => x.SceneLoaded += state.SceneLoaded);
            sceneManagerWrapperMock.SetupAdd(x => x.SceneUnloaded += state.SceneUnloaded);

            //act
            state.OnEnter();

            //assert
            sceneManagerWrapperMock.VerifyAdd(x=>x.SceneLoaded += state.SceneLoaded, Times.Once);
            sceneManagerWrapperMock.VerifyAdd(x => x.SceneUnloaded += state.SceneUnloaded, Times.Once);
        }
        [Test]
        public void OnExit_set_stateState_to_NotInStack()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            stateMock.CallBase = true;

            var state = stateMock.Object;

            //act
            state.OnExit();

            //assert
            Assert.That(state.StateState, Is.EqualTo(StateStateEnum.NotInStack));
        }

        [Test]
        public void OnExit_removes_event_listeners_from_sceneManagerWrapper()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();

            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            stateMock.CallBase = true;

            var state = stateMock.Object;

            sceneManagerWrapperMock.SetupRemove(x => x.SceneLoaded -= state.SceneLoaded);
            sceneManagerWrapperMock.SetupRemove(x => x.SceneUnloaded -= state.SceneUnloaded);

            //act
            state.OnExit();

            //assert
            sceneManagerWrapperMock.VerifyRemove(x => x.SceneLoaded -= state.SceneLoaded, Times.Once);
            sceneManagerWrapperMock.VerifyRemove(x => x.SceneUnloaded -= state.SceneUnloaded, Times.Once);
        }

        [Test]
        public void OnActivate_set_stateState_to_Activated()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            stateMock.CallBase = true;

            var state = stateMock.Object;

            //act
            state.OnActivate();

            //assert
            Assert.That(state.StateState, Is.EqualTo(StateStateEnum.Activated));
        }

        [Test]
        public void OnDectivate_set_stateState_to_InStack()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            stateMock.CallBase = true;

            var state = stateMock.Object;

            //act
            state.OnDeactivate();

            //assert
            Assert.That(state.StateState, Is.EqualTo(StateStateEnum.InStack));
        }
    }


}