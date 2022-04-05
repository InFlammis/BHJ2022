﻿namespace BulletHellJam2022.Assets.Scripts.Enemies.Triangle.StateMachine
{
    public interface ITriangleState : IEnemyState<ITriangleState>
    {
        TriangleControllerCore Parent { get; set; }
        StateFactory Factory { get; set; }

    }
}
