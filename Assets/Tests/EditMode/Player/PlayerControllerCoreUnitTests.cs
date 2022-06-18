using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InFlammis.Victoria.Assets.Scripts;
using InFlammis.Victoria.Assets.Scripts.Managers.HealthManagement;
using InFlammis.Victoria.Assets.Scripts.Player;
using InFlammis.Victoria.Assets.Scripts.Weapons;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace FightshipArena.Assets.Tests.EditMode.Player
{
    public class PlayerControllerCoreUnitTests
    {
        [Test]
        public void Constructor_initializes_parent_and_transform()
        {
            //arrange
            var gameObject = new GameObject();
            var healthManagerMock = new Mock<IHealthManager>();
            var healthManager = healthManagerMock.Object;

            var weaponGO = new GameObject();
            var weapon = weaponGO.AddComponent<Spitter>();

            var monoBehaviourMock = new Mock<IPlayerController>();
            monoBehaviourMock
                .SetupGet(x => x.GameObject)
                .Returns(gameObject);
            monoBehaviourMock
                .SetupGet(x => x.HealthManager)
                .Returns(healthManager);
            monoBehaviourMock
                .SetupGet(x => x.Weapons)
                .Returns(new[] { weapon });

            var monoBehaviour = monoBehaviourMock.Object;


            //act
            var core = new PlayerControllerCore(monoBehaviour);

            //assert
            Assert.That(core.Parent, Is.SameAs(monoBehaviour));
            Assert.That(core.Transform, Is.SameAs(gameObject.transform));
        }

        //[Test, Ignore("Needs review")]
        //public void Move_adds_movement_to_position()
        //{
        //    //arrange
        //    var gameObject = new GameObject();
        //    gameObject.AddComponent<Rigidbody2D>();
        //    var healthManagerMock = new Mock<IHealthManager>();
        //    var healthManager = healthManagerMock.Object;

        //    var weaponGO = new GameObject();
        //    var weapon = weaponGO.AddComponent<MultiCannon>();

        //    var monoBehaviourMock = new Mock<IPlayerController>();
        //    monoBehaviourMock
        //        .SetupGet(x => x.GameObject)
        //        .Returns(gameObject);
        //    monoBehaviourMock
        //        .SetupGet(x => x.HealthManager)
        //        .Returns(healthManager);
        //    monoBehaviourMock
        //        .SetupGet(x => x.Weapons)
        //        .Returns(new[] { weapon });
        //    monoBehaviourMock
        //        .SetupGet(x => x.InitSettings)
        //        .Returns(new PlayerSettings());

        //    var monoBehaviour = monoBehaviourMock.Object;

        //    var core = new PlayerControllerCore(monoBehaviour);

        //    core.PlayerInput = Vector3.one;

        //    //act
        //    core.Move();

        //    //assert
        //    Assert.That(gameObject.transform.position, Is.EqualTo(Vector3.one));

        //}

    }
}
