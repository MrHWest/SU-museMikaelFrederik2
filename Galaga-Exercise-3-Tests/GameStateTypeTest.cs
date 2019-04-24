using System;
using Exercise3ny.GameStates;
using NUnit.Framework;

namespace Galaga_Exercise_3_Tests {
    [TestFixture]
    public class Tests {
        [Test]
        public void Test1() {
            Assert.AreSame(StateTransformer.TransformStateToString(
                StateTransformer.GameStateType.MainMenu),"GAME_MAINMENU");
        }
        [Test]
        public void Test2() {

            Assert.AreSame(StateTransformer.TransformStateToString(
                StateTransformer.GameStateType.GamePaused),"GAME_PAUSED");
        }
        [Test]
        public void Test3() {

            Assert.AreSame(StateTransformer.TransformStateToString(
                StateTransformer.GameStateType.GameRunning),"GAME_RUNNING");
        }
        [Test]
        public void Test4() {

            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_MAINMENU"),
                StateTransformer.GameStateType.MainMenu);
        }
        [Test]
        public void Test5() {

            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_PAUSED"),
                StateTransformer.GameStateType.GamePaused);
        }
        [Test]
        public void Test6() {

            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_RUNNING"),
                StateTransformer.GameStateType.GameRunning);
        }
    }
}