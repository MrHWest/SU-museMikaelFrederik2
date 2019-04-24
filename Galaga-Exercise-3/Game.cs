using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Exercise3ny.GameStates;
using Galaga_Exercise_3.GameStates;

namespace Exercise3ny { 
    

    public class Game : IGameEventProcessor<object> {
        private Window win;
        private DIKUArcade.Timers.GameTimer gameTimer;
        public StateMachine stateMachine;


        public Game() {
            win = new Window("Window-name", 500, 500);
            gameTimer = new GameTimer(60, 60);
            stateMachine = new StateMachine();
            GalagaBus.GetBus().InitializeEventBus(new List<GameEventType>()
            {
                GameEventType.WindowEvent,
                GameEventType.InputEvent,
                GameEventType.GameStateEvent,
                GameEventType.PlayerEvent

            });
        

        GalagaBus.GetBus().Subscribe(GameEventType.WindowEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);

        win.RegisterEventBus(GalagaBus.GetBus());
        }

        public void GameLoop() {
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    win.PollEvents();
                    // Update game logic here
                    GalagaBus.GetBus().ProcessEvents();
                    stateMachine.ActiveState.UpdateGameLogic();
                    

                }

                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    // Render gameplay entities here
                    stateMachine.ActiveState.RenderState();

                    win.SwapBuffers();
                }

                if (gameTimer.ShouldReset()) {
                    // 1 second has passed - display last captured ups and fps
                    win.Title = "Galaga | UPS: " + gameTimer.CapturedUpdates +
                                ", FPS: " + gameTimer.CapturedFrames;
                }
            }
        }
       

        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    win.CloseWindow();
                    break;
                }
                
            }
            else if (eventType == GameEventType.InputEvent) {

                switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    stateMachine.ActiveState.HandleKeyEvent(gameEvent.Parameter1,gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    stateMachine.ActiveState.HandleKeyEvent(gameEvent.Parameter1,gameEvent.Message);
                    break;
                }
            }
            
        }
        }
        
    }
