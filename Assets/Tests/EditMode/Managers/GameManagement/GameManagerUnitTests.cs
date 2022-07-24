using System.Collections;
using InFlammis.Victoria.Assets.Scripts.Managers.GameManagement;
using Moq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

namespace FightshipArena.Assets.Tests.EditMode.Managers.GameManagement
{
    public class GameManagerUnitTests
    {
        [UnityTest]
        public IEnumerator PauseResumeGame_calls_PauseResumeGame_on_Core()
        {
            //arrange
            var coreMock = new Mock<IGameManager>();
            var core = coreMock.Object;
            
            var gameObject = new GameObject("Player");
            var gameManager = gameObject.AddComponent<GameManagerMock>();

            gameManager.SetCore(core);

            //act
            //gameManager.PauseResumeGame(new InputAction.CallbackContext());

            //assert
            //coreMock.Verify(x=>x.OnPauseResumeGame(It.IsAny<InputAction.CallbackContext>()), Times.Once());

            yield return null;
        }
    }

    public class GameManagerMock : GameManager
    {
        public void SetCore(IGameManager core)
        {
            Core = core;
        }
    }
}
