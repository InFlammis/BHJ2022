using System;
using BulletHellJam2022.Assets.Scripts.Managers.GameManagement;
using BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine;
using BulletHellJam2022.Assets.Scripts.Managers.Menus;
using BulletHellJam2022.Assets.Scripts.Managers.Menus.Pause;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using UnityEngine.SceneManagement;

namespace FightshipArena.Assets.Tests.EditMode.Managers.GameManagement.StateMachine
{
    [TestFixture]
    public class PauseUnitTests
    {
        [Test]
        public void OnEnter_calls_LoadSceneAsync_on_SceneManagerWrapper()
        {
            //arrange
            var soundManagerMock = new Mock<ISoundManager>();

            var gameManagerCoreMock = new Mock<IGameManager>();
            //gameManagerCoreMock.Setup(x => x.StaticObjects.SoundManager).Returns(soundManagerMock.Object);

            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<Pause>(gameManagerCore, sceneManagerWrapper);
            stateMock.Setup(x => x.OnEnter()).CallBase();

            var state = stateMock.Object;

            //act
            state.OnEnter();

            //assert
            sceneManagerWrapperMock.Verify(x => x.LoadSceneAsync(state._sceneName, LoadSceneMode.Additive), Times.Once);
        }

        [Test]
        public void OnExit_calls_UnloadSceneAsync_on_SceneManagerWrapper()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<Pause>(gameManagerCore, sceneManagerWrapper);
            stateMock.Setup(x => x.OnExit()).CallBase();

            var state = stateMock.Object;

            //act
            state.OnExit();

            //assert
            sceneManagerWrapperMock.Verify(x => x.UnloadSceneAsync(state._sceneName), Times.Once);
        }

        [Test]
        public void SceneLoaded_attaches_event_listeners_to_scenes_menuManager()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var mainMenuManagerMock = new Mock<IPauseMenuManager>();

            var mainMenuManager = mainMenuManagerMock.Object;

            var stateMock = new Mock<Pause>(gameManagerCore, sceneManagerWrapper);
            stateMock.Setup(x => x.SceneLoaded(It.IsAny<Scene>(), It.IsAny<LoadSceneMode>())).CallBase();
            stateMock
                .Protected()
                .Setup<IPauseMenuManager>("GetMenuManagerFromScene", ItExpr.IsAny<Scene>())
                .Returns(mainMenuManager);


            var state = stateMock.Object;

            var scene = new Scene();

            //mainMenuManagerMock.SetupAdd(x => x.ResumeGameEvent += It.IsAny<EventHandler>());
            //mainMenuManagerMock.SetupAdd(x => x.QuitCurrentGameEvent += It.IsAny<EventHandler>());


            //act
            state.SceneLoaded(scene, LoadSceneMode.Additive);

            //assert
            //mainMenuManagerMock.VerifyAdd(x => x.ResumeGameEvent += It.IsAny<EventHandler>(), Times.Once);
            //mainMenuManagerMock.VerifyAdd(x => x.QuitCurrentGameEvent += It.IsAny<EventHandler>(), Times.Once);
        }

        [Test]
        public void ResumeGameEventHandler_raises_ResumeGameEvent()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var state = new PauseMock(gameManagerCore, sceneManagerWrapper);
            var eventCalled = false;

            //state.ResumeGameEvent += (sender, args) => { eventCalled = true; };

            //act
            state.ResumeGameEventHandler_Caller();

            //assert
            Assert.That(eventCalled, Is.True);

        }

        [Test]
        public void QuitCurrentGameEventHandler_raises_QuitCurrentGameEvent()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var state = new PauseMock(gameManagerCore, sceneManagerWrapper);
            var eventCalled = false;

            //state.QuitCurrentGameEvent += (sender, args) => { eventCalled = true; };

            //act
            state.QuitCurrentGameEventHandler_Caller();

            //assert
            Assert.That(eventCalled, Is.True);

        }
        [Test]
        public void PauseResumeGame_raises_ResumeGameEvent()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var state = new Pause(gameManagerCore, sceneManagerWrapper);
            var eventCalled = false;

            //state.ResumeGameEvent += (sender, args) => { eventCalled = true; };

            //act
            state.PauseResumeGame();

            //assert
            Assert.That(eventCalled, Is.True);

        }

        public class PauseMock : Pause
        {
            public PauseMock(IGameManager gameManager, IUnitySceneManagerWrapper sceneManagerWrapper) : base(gameManager, sceneManagerWrapper)
            {
            }

            public void ResumeGameEventHandler_Caller()
            {
                //ResumeGameEventHandler(null, new EventArgs());
            }

            public void QuitCurrentGameEventHandler_Caller()
            {
                //QuitCurrentGameEventHandler(null, new EventArgs());
            }

        }

    }
}