using System;
using BulletHellJam2022.Assets.Scripts.Managers.Menus;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace FightshipArena.Assets.Tests.EditMode.Managers.Menus
{
    public class PauseMenuManagerUnitTests
    {
        [Test]
        public void OnStart_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<IPauseMenuManager>();
            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<PauseMenuManagerMock>();
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
            var coreMock = new Mock<IPauseMenuManager>();
            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<PauseMenuManagerMock>();
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
            var coreMock = new Mock<IPauseMenuManager>();
            coreMock.SetupAdd(x => x.ResumeGameEvent += It.IsAny<EventHandler>());
            coreMock.SetupAdd(x => x.QuitCurrentGameEvent += It.IsAny<EventHandler>());

            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<PauseMenuManagerMock>();
            levelManager.SetCore(core);

            //act
            levelManager.OnAwake();

            //assert
            coreMock.VerifyAdd(x => x.ResumeGameEvent += It.IsAny<EventHandler>());
            coreMock.VerifyAdd(x => x.QuitCurrentGameEvent += It.IsAny<EventHandler>());

        }

        [Test]
        public void Raise_ResumeGameEvent_if_core_does()
        {
            //arrange
            var coreMock = new Mock<IPauseMenuManager>();
            coreMock.SetupAdd(x => x.QuitCurrentGameEvent += It.IsAny<EventHandler>());
            coreMock.SetupAdd(x => x.ResumeGameEvent += It.IsAny<EventHandler>());

            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<PauseMenuManagerMock>();
            levelManager.SetCore(core);

            levelManager.OnAwake();

            var eventRaised = false;

            levelManager.ResumeGameEvent += (sender, args) =>
            {
                eventRaised = true;
            };

            //act
            coreMock.Raise(x => x.ResumeGameEvent += null, new EventArgs());

            //assert
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        public void Raise_QuitCurrentGameEvent_if_core_does()
        {
            //arrange
            var coreMock = new Mock<IPauseMenuManager>();
            coreMock.SetupAdd(x => x.QuitCurrentGameEvent += It.IsAny<EventHandler>());
            coreMock.SetupAdd(x => x.ResumeGameEvent += It.IsAny<EventHandler>());

            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<PauseMenuManagerMock>();
            levelManager.SetCore(core);

            levelManager.OnAwake();

            var eventRaised = false;

            levelManager.QuitCurrentGameEvent += (sender, args) =>
            {
                eventRaised = true;
            };

            //act
            coreMock.Raise(x => x.QuitCurrentGameEvent += null, new EventArgs());

            //assert
            Assert.That(eventRaised, Is.True);
        }

        [Test]
        public void ResumeGame_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<IPauseMenuManager>();
            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<PauseMenuManagerMock>();
            levelManager.SetCore(core);

            //act
            levelManager.ResumeGame();

            //assert
            coreMock.Verify(x => x.ResumeGame(), Times.Once);
        }

        [Test]
        public void QuitCurrentGame_pass_through_to_Core()
        {
            //arrange
            var coreMock = new Mock<IPauseMenuManager>();
            var core = coreMock.Object;

            var gameObject = new GameObject("MenuManager");
            var levelManager = gameObject.AddComponent<PauseMenuManagerMock>();
            levelManager.SetCore(core);

            //act
            levelManager.QuitCurrentGame();

            //assert
            coreMock.Verify(x => x.QuitCurrentGame(), Times.Once);
        }

    }

    public class PauseMenuManagerMock : PauseMenuManager
    {
        public void SetCore(IPauseMenuManager core)
        {
            this.Core = core;
        }
    }
}
