using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BulletHellJam2022.Assets.Scripts.Managers.GameManagement;
using BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine;
using BulletHellJam2022.Assets.Scripts.Managers.Menus;
using BulletHellJam2022.Assets.Scripts.Managers.SoundManagement;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using UnityEngine.SceneManagement;

namespace FightshipArena.Assets.Tests.EditMode.Managers.GameManagement.StateMachine
{
    public class InitUnitTests
    {
        [Test]
        public void OnEnter_calls_LoadSceneAsync_on_SceneManagerWrapper()
        {
            //arrange
            var soundManagerMock = new Mock<ISoundManager>();

            var gameManagerCoreMock = new Mock<IGameManager>();
            gameManagerCoreMock.Setup(x=>x.SoundManager).Returns(soundManagerMock.Object);
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<Init>(gameManagerCore, sceneManagerWrapper);
            stateMock.Setup(x => x.OnEnter()).CallBase();

            var state = stateMock.Object;
            
            //act
            state.OnEnter();

            //assert
            sceneManagerWrapperMock.Verify(x=>x.LoadSceneAsync(state._sceneName, LoadSceneMode.Additive), Times.Once);
        }

        [Test]
        public void OnExit_calls_UnloadSceneAsync_on_SceneManagerWrapper()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<Init>(gameManagerCore, sceneManagerWrapper);
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

            var mainMenuManagerMock = new Mock<IMainMenuManager>();

            var mainMenuManager = mainMenuManagerMock.Object;

            var stateMock = new Mock<Init>(gameManagerCore, sceneManagerWrapper);
            stateMock.Setup(x => x.SceneLoaded(It.IsAny<Scene>(), It.IsAny<LoadSceneMode>())).CallBase();
            stateMock
                .Protected()
                .Setup<IMainMenuManager>("GetMenuManagerFromScene", ItExpr.IsAny<Scene>())
                .Returns(mainMenuManager);

                
            var state = stateMock.Object;

            var scene = new Scene();

            mainMenuManagerMock.SetupAdd(x => x.StartGameEvent += It.IsAny<EventHandler>());
            mainMenuManagerMock.SetupAdd(x => x.QuitGameEvent += It.IsAny<EventHandler>());


            //act
            state.SceneLoaded(scene, LoadSceneMode.Additive);

            //assert
            mainMenuManagerMock.VerifyAdd(x=>x.StartGameEvent += It.IsAny<EventHandler>(), Times.Once);
            mainMenuManagerMock.VerifyAdd(x => x.QuitGameEvent += It.IsAny<EventHandler>(), Times.Once);
        }

        [Test]
        public void StartGameEventHandler_raises_PlayGameEvent()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var state = new InitMock(gameManagerCore, sceneManagerWrapper);
            var eventCalled = false;

            state.PlayGameEvent += (sender, args) => { eventCalled = true; };

            //act
            state.StartGameEventHandler_Caller();

            //assert
            Assert.That(eventCalled, Is.True);

        }

        [Test]
        public void QuitGameEventHandler_raises_QuitGameEvent()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var state = new InitMock(gameManagerCore, sceneManagerWrapper);
            
            var eventCalled = false;

            state.QuitGameEvent += (sender, args) => { eventCalled = true; };
            //act

            state.QuitGameEventHandler_Caller();

            //assert
            Assert.That(eventCalled, Is.True);
        }


        public class InitMock : Init
        {
            public InitMock(IGameManager gameManager, IUnitySceneManagerWrapper sceneManagerWrapper) : base(gameManager, sceneManagerWrapper)
            {
            }

            public void StartGameEventHandler_Caller()
            {
                StartGameEventHandler(null, new EventArgs());
            }

            public void QuitGameEventHandler_Caller()
            {
                QuitGameEventHandler(null, new EventArgs());
            }

        }
    }
}
