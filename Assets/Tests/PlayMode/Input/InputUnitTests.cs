using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletHellJam2022.Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace FightshipArena.Assets.Tests.PlayMode.Input
{
    [TestFixture]
    public class InputUnitTests
    {

        private Keyboard ResetAndReturnKeyboard()
        {
            var keyboard = InputSystem.GetDevice<Keyboard>();

            InputSystem.RemoveDevice(keyboard);

            return InputSystem.AddDevice<Keyboard>();
        }


        [Test]
        [TestCase("a", "Keyboard", "Move", "left")]
        [TestCase("w", "Keyboard", "Move", "up")]
        [TestCase("d", "Keyboard", "Move", "right")]
        [TestCase("s", "Keyboard", "Move", "down")]
        public void Keypress_triggers_Move_actions(string key, string device, string action, string name)
        {
            InputTestFixture input = new InputTestFixture();
            //arrange
            var keyboard = ResetAndReturnKeyboard();

            var playerActions = new PlayerActionAsset();

            var playerGO = new GameObject("Player");
            playerGO.SetActive(false);
            playerGO.AddComponent<PlayerController>();

            var playerInput = playerGO.AddComponent<PlayerInput>();
            playerInput.actions = playerActions.asset;

            playerInput.actions.Enable();

            var button = (ButtonControl)keyboard[key];

            //act
            input.Press(button);

            //assert
            var actionObj = playerActions.asset.FindAction(action);
            Assert.That(actionObj.triggered, Is.True);

            var binding = actionObj.bindings.SingleOrDefault(x => x.path == $"<{device}>/{key}" && x.name == name);
            Assert.That(binding.id, Is.Not.EqualTo(Guid.Empty));
        }

        [TestCase("j", "Keyboard", "Fire", null)]
        [TestCase("k", "Keyboard", "Fire Alt", null)]
        [TestCase("l", "Keyboard", "Open Selection Menu", null)]
        public void Keypress_triggers_other_actions(string key, string device, string action, string name)
        {
            InputTestFixture input = new InputTestFixture();
            //arrange
            var keyboard = ResetAndReturnKeyboard();

            var playerActions = new PlayerActionAsset();

            var playerGO = new GameObject("Player");
            playerGO.SetActive(false);
            playerGO.AddComponent<PlayerController>();

            var playerInput = playerGO.AddComponent<PlayerInput>();
            playerInput.actions = playerActions.asset;

            playerInput.actions.Enable();

            var button = (ButtonControl)keyboard[key];

            //act
            input.Press(button);

            //assert
            var actionObj = playerActions.asset.FindAction(action);
            Assert.That(actionObj.triggered, Is.True);

            var binding = actionObj.bindings.SingleOrDefault(x => x.path == $"<{device}>/{key}" && x.name == name);
            Assert.That(binding.id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
