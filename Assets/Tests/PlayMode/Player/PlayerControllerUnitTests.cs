using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletHellJam2022.Assets.Scripts.Player;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FightshipArena.Assets.Tests.PlayMode.Player
{
    [TestFixture]
    public class PlayerControllerUnitTests
    {
        //[UnityTest]
        //public IEnumerator Player_instantiate_core_on_Start()
        //{
        //    //arrange
        //    //act
        //    var gameObject = new GameObject("Player");
        //    var playerController = gameObject.AddComponent<PlayerController>();

        //    yield return null;

        //    //assert
        //    Assert.That(playerController.Core, Is.Not.Null);
        //}

        //[UnityTest]
        //public IEnumerator FixedUpdate_invokes_Move_on_Core()
        //{
        //    var playerGO = new GameObject("Player");
        //    var playerController = playerGO.AddComponent<PlayerController>();

        //    var playerControllerCoreMock = new Mock<IPlayerControllerCore>();

        //    playerControllerCoreMock
        //        .Setup(x => x.Move());

        //    var playerControllerCore = playerControllerCoreMock.Object;
        //    playerController.Core = playerControllerCore;


        //    yield return new WaitForFixedUpdate();

        //    //assert
        //    playerControllerCoreMock.Verify(x => x.Move(), Times.Once);
        //}
    }
}
