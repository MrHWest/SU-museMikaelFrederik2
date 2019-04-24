using System.Collections.Generic;
using DIKUArcade.EventBus;
using Exercise3ny;
using Exercise3ny.GameStates;
using NUnit.Framework;

namespace Galaga_Exercise_3_Tests {
    public class Test1 {
        [TestFixture]
        public class StateMachineTesting {
            private StateMachine stateMachine;
            private Game game;

            [SetUp]
            public void InitiateStateMachine() {
                DIKUArcade.Window.CreateOpenGLContext();
                stateMachine = new StateMachine();

                GalagaBus.GetBus().InitializeEventBus(new List<GameEventType>() {
                    GameEventType.WindowEvent,
                    GameEventType.InputEvent,
                    GameEventType.GameStateEvent,
                    GameEventType.PlayerEvent

                });
                GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, game);
                GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, game);
                GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, game);
                GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, game);


            }

            [Test]
            public void TestInitialState() {
                Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
            }

            [Test]
            public void TestEventGamePaused() {
                GalagaBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent,
                        this,
                        "CHANGE_STATE",
                        "GAME_PAUSED", ""));

                GalagaBus.GetBus().ProcessEventsSequentially();
                Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());

            }
        }
    }
}