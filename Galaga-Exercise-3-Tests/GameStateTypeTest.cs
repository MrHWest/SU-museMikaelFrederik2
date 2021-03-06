﻿using System;
using Exercise3ny.GameStates;
using NUnit.Framework;

namespace Galaga_Exercise_3_Tests {
    [TestFixture]
    public class GameStateTypeTests {
        [Test]
        public void TestTransformStateToStringGAME_MAINMENU() {
            Assert.AreSame(StateTransformer.TransformStateToString(
                StateTransformer.GameStateType.MainMenu),"GAME_MAINMENU");
        }
        [Test]
        public void TestTransformStateToStringGAME_PAUSED() {

            Assert.AreSame(StateTransformer.TransformStateToString(
                StateTransformer.GameStateType.GamePaused),"GAME_PAUSED");
        }
        [Test]
        public void TestTransformStateToStringGAME_RUNNING() {

            Assert.AreSame(StateTransformer.TransformStateToString(
                StateTransformer.GameStateType.GameRunning),"GAME_RUNNING");
        }
        [Test]
        public void TestTransformStringToStateGAME_MAINMENU() {

            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_MAINMENU"),
                StateTransformer.GameStateType.MainMenu);
        }
        [Test]
        public void TestTransformStringToStateGAME_PAUSED() {

            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_PAUSED"),
                StateTransformer.GameStateType.GamePaused);
        }
        [Test]
        public void TestTransformStringToStateGAME_RUNNING() {

            Assert.AreEqual(StateTransformer.TransformStringToState("GAME_RUNNING"),
                StateTransformer.GameStateType.GameRunning);
        }
    }
}