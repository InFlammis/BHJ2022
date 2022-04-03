using System;
using BulletHellJam2022.Assets.Scripts.Managers.Menus;
using BulletHellJam2022.Assets.Scripts.Managers.Menus.Main;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace FightshipArena.Assets.Tests.EditMode.Managers.Menus
{
    public class MainMenuManagerUnitTests
    {
        [Test]
        public void OnStart_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<IMainMenuManager>();
            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<MainMenuManagerMock>();
            levelManager.SetCore(core);

            //act
            levelManager.OnStart();

            //assert
            coreMock.Verify(x => x.OnStart(), Times.Once);
        }

        [Test]
        public void OnAwake_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<IMainMenuManager>();
            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<MainMenuManagerMock>();
            levelManager.SetCore(core);

            //act
            levelManager.OnAwake();

            //assert
            coreMock.Verify(x => x.OnAwake(), Times.Once);
        }

        [Test]
        public void OnAwake_sets_eventHandlers_for_Core_events()
        {
            //arrange
            var coreMock = new Mock<IMainMenuManager>();
            coreMock.SetupAdd(x=>x.QuitGameEvent += It.IsAny<EventHandler>());
            coreMock.SetupAdd(x => x.StartGameEvent += It.IsAny<EventHandler>());

            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<MainMenuManagerMock>();
            levelManager.SetCore(core);

            //act
            levelManager.OnAwake();

            //assert
            coreMock.VerifyAdd(x => x.StartGameEvent += It.IsAny<EventHandler>());
            coreMock.VerifyAdd(x => x.QuitGameEvent += It.IsAny<EventHandler>());

        }

        [Test]
        public void Raise_StartGameEvent_if_core_does()
        {
            //arrange
            var coreMock = new Mock<IMainMenuManager>();
            coreMock.SetupAdd(x => x.QuitGameEvent += It.IsAny<EventHandler>());
            coreMock.SetupAdd(x => x.StartGameEvent += It.IsAny<EventHandler>());

            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<MainMenuManagerMock>();
            levelManager.SetCore(core);

            levelManager.OnAwake();

            var eventRaised = false;

            levelManager.StartGameEvent += (sender, args) => 
            { 
                eventRaised = true; 
            };

            //act
            coreMock.Raise(x => x.StartGameEvent += null, new EventArgs());

            //assert
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        public void Raise_QuitGameEvent_if_core_does()
        {
            //arrange
            var coreMock = new Mock<IMainMenuManager>();
            coreMock.SetupAdd(x => x.QuitGameEvent += It.IsAny<EventHandler>());
            coreMock.SetupAdd(x => x.StartGameEvent += It.IsAny<EventHandler>());

            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<MainMenuManagerMock>();
            levelManager.SetCore(core);

            levelManager.OnAwake();

            var eventRaised = false;

            levelManager.QuitGameEvent += (sender, args) =>
            {
                eventRaised = true;
            };

            //act
            coreMock.Raise(x => x.QuitGameEvent += null, new EventArgs());

            //assert
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        public void StartGame_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<IMainMenuManager>();
            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<MainMenuManagerMock>();
            levelManager.SetCore(core);

            //act
            levelManager.StartGame();

            //assert
            coreMock.Verify(x => x.StartGame(), Times.Once);
        }

        [Test]
        public void QuitGame_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<IMainMenuManager>();
            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<MainMenuManagerMock>();
            levelManager.SetCore(core);

            //act
            levelManager.QuitGame();

            //assert
            coreMock.Verify(x => x.QuitGame(), Times.Once);
        }

    }

    public class MainMenuManagerMock : MainMenuManager
    {
        public void SetCore(IMainMenuManager core)
        {
            this.Core = core;
        }
    }
}
