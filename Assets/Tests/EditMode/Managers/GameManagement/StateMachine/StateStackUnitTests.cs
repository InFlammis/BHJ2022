using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using BulletHellJam2022.Assets.Scripts.Managers.GameManagement;
using BulletHellJam2022.Assets.Scripts.Managers.GameManagement.StateMachine;
using Moq;
using NUnit.Framework;

namespace FightshipArena.Assets.Tests.EditMode.Managers.GameManagement.StateMachine
{
    [TestFixture]
    public class StateStackUnitTests
    {
        [Test]
        public void OnConstruction_initialize__stack_property()
        {
            //arrange
            //act
            var stateStack = new StateStackMock();

            //assert
            Assert.That(stateStack.Stack, Is.Not.Null);
        }

        [Test]
        public void Peek_calls_Peek_on_internal_stack()
        {
            //arrange
            var stackMock = new Mock<IStack<State>>();

            var stateStack = new StateStackMock();
            stateStack.Stack = stackMock.Object;

            //act
            stateStack.Peek();

            //arrange
            stackMock.Verify(x=>x.Peek(), Times.Once);
        }

        [Test]
        public void Pop_calls_Pop_on_internal_stack()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var state = new Mock<State>(gameManagerCore, sceneManagerWrapper).Object;

            var stackMock = new Mock<IStack<State>>();
            stackMock
                .Setup(x => x.Pop())
                .Returns(state);

            var stateStack = new StateStackMock();
            stateStack.Stack = stackMock.Object;

            //act
            var returnState = stateStack.Pop();

            //assert
            Assert.That(returnState, Is.SameAs(state));
            stackMock.Verify(x=>x.Pop(), Times.Once);
        }

        [Test]
        public void Pop_calls_OnDeactivate_and_OnExit_on_state_popped_out()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            var state = stateMock.Object;

            var stackMock = new Mock<IStack<State>>();
            stackMock
                .Setup(x => x.Pop())
                .Returns(state);

            var stateStack = new StateStackMock();
            stateStack.Stack = stackMock.Object;

            //act
            var returnState = stateStack.Pop();

            //assert
            stateMock.Verify(x => x.OnDeactivate(), Times.Once);
            stateMock.Verify(x => x.OnExit(), Times.Once);
        }

        [Test]
        public void Pop_invokes_PoppingStateEvent_event()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            var state = stateMock.Object;

            var stackMock = new Mock<IStack<State>>();
            stackMock
                .Setup(x => x.Pop())
                .Returns(state);

            var stateStack = new StateStackMock();
            stateStack.Stack = stackMock.Object;

            //values to assert - These values are set if the event handler is invoked
            bool poppingStateEventInvoked = false;
            State stateInEvent = null;

            //Event handler to check that the event is invoked
            stateStack.PoppingStateEvent += (sender, state1) =>
            {
                poppingStateEventInvoked = true;
                stateInEvent = state1;
            };

            //act
            var returnState = stateStack.Pop();

            //assert
            Assert.That(poppingStateEventInvoked, Is.True);
            Assert.That(stateInEvent, Is.SameAs(state));
        }

        [Test]
        public void Pop_calls_OnActivate_on_new_top_of_stack_state()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateFromPopMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            var stateFromPop = stateFromPopMock.Object;

            var stateFromPeekMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            var stateFromPeek = stateFromPeekMock.Object;

            var stackMock = new Mock<IStack<State>>();
            stackMock
                .Setup(x => x.Pop())
                .Returns(stateFromPop);
            stackMock
                .Setup(x => x.Peek())
                .Returns(stateFromPeek);

            stackMock.SetupGet(x => x.Count).Returns(1);

            var stateStackMock = new Mock<StateStackMock>();
            stateStackMock.Setup(x => x.Pop()).CallBase();

            var stateStack = stateStackMock.Object;

            stateStack.Stack = stackMock.Object;

            //act
            var returnState = stateStack.Pop();

            //assert
            stateFromPeekMock.Verify(x=>x.OnActivate(), Times.Once);
        }

        [Test]
        public void Push_calls_OnDeactivate_on_top_of_stack_state()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateToPushMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            var stateToPush = stateToPushMock.Object;

            var stateFromPeekMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            var stateFromPeek = stateFromPeekMock.Object;

            var stackMock = new Mock<IStack<State>>();

            //This makes the if block to be executed
            stackMock.SetupGet(x => x.Count).Returns(1);

            stackMock
                .Setup(x => x.Peek())
                .Returns(stateFromPeek);

            var stateStackMock = new Mock<StateStackMock>();
            stateStackMock.Setup(x => x.Pop()).CallBase();
            stateStackMock.Setup(x => x.Push(It.IsAny<State>())).CallBase();
            var stateStack = stateStackMock.Object;

            stateStack.Stack = stackMock.Object;

            //act
            stateStack.Push(stateToPush);

            //assert
            stateFromPeekMock.Verify(x=>x.OnDeactivate(), Times.Once);
        }

        [Test]
        public void Push_calls_Push_on_internal_stack()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateToPushMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            var stateToPush = stateToPushMock.Object;

            var stackMock = new Mock<IStack<State>>();

            var stateStack = new StateStackMock();
            stateStack.Stack = stackMock.Object;

            //act
            stateStack.Push(stateToPush);

            //assert
            stackMock.Verify(x => x.Push(stateToPush), Times.Once);
        }

        [Test]
        public void Push_invokes_OnEnter_and_OnActivate_on_state_pushed_in()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateToPushMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            var stateToPush = stateToPushMock.Object;

            var stackMock = new Mock<IStack<State>>();

            var stateStack = new StateStackMock();
            stateStack.Stack = stackMock.Object;

            //act
            stateStack.Push(stateToPush);

            //assert
            stateToPushMock.Verify(x => x.OnEnter(), Times.Once);
            stateToPushMock.Verify(x => x.OnActivate(), Times.Once);
        }

        [Test]
        public void Push_invokes_PushingStateEvent_event()
        {
            //arrange
            var gameManagerCoreMock = new Mock<IGameManager>();
            var gameManagerCore = gameManagerCoreMock.Object;

            var sceneManagerWrapperMock = new Mock<IUnitySceneManagerWrapper>();
            var sceneManagerWrapper = sceneManagerWrapperMock.Object;

            var stateToPushMock = new Mock<State>(gameManagerCore, sceneManagerWrapper);
            var stateToPush = stateToPushMock.Object;

            var stackMock = new Mock<IStack<State>>();

            var stateStack = new StateStackMock();
            stateStack.Stack = stackMock.Object;

            //values to assert - These values are set if the event handler is invoked
            bool pushingStateEventInvoked = false;
            State stateInEvent = null;

            //Event handler to check that the event is invoked
            stateStack.PushingStateEvent += (sender, state) =>
            {
                pushingStateEventInvoked = true;
                stateInEvent = state;
            };

            //act
            stateStack.Push(stateToPush);

            //assert
            Assert.That(pushingStateEventInvoked, Is.True);
            Assert.That(stateInEvent, Is.SameAs(stateToPush));
        }

        [Test]
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(10)]
        public void Clear_calls_pop_as_many_times_as_the_stack_count(int iterations)
        {
            //arrange
            var stackMock = new Mock<IStack<State>>();
            var counter = iterations;
            stackMock.SetupGet(x=>x.Count)
                .Returns(() => counter--);

            var stateStackMock = new Mock<StateStackMock>();
            stateStackMock.Setup(x => x.Clear()).CallBase();

            var stateStack = stateStackMock.Object;
            stateStack.Stack = stackMock.Object;

            //act
            stateStack.Clear();

            //Assert
            stateStackMock.Verify(x=>x.Pop(), Times.Exactly(iterations));
        }


        public class StateStackMock : StateStack
        {
            public IStack<State> Stack
            {
                get => _stack;
                set => _stack = value;
            }
        }
    }
}