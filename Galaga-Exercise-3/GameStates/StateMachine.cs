using System;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using Galaga_Exercise_3.GameStates;

namespace Exercise3ny.GameStates {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }

        public StateMachine() {
        
            ActiveState = MainMenu.GetInstance();
            GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        }

        public void SwitchState(StateTransformer.GameStateType stateType) {
            switch (stateType) {
            case StateTransformer.GameStateType.GameRunning:
                if (ActiveState == MainMenu.GetInstance()) {
                    ActiveState = GameRunning.GetInstance0();
                } else {
                    ActiveState = GameRunning.GetInstance();
                }
                break;
            case StateTransformer.GameStateType.GamePaused:
                ActiveState = GamePaused.GetInstance();
                break;
            case StateTransformer.GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(stateType), stateType, null);
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {

            if (eventType == GameEventType.GameStateEvent) {
                
                switch (gameEvent.Parameter1) {
                case "GAME_RUNNING": 
                    SwitchState((StateTransformer.TransformStringToState(gameEvent.Parameter1)));
                    break;
                case "GAME_PAUSED": 
                    SwitchState((StateTransformer.GameStateType.GamePaused));
                    break;

                case "GAME_MAINMENU":
                    SwitchState((StateTransformer.GameStateType.MainMenu));
                    break; 

                }

            }
            else if (eventType == GameEventType.InputEvent) {
                switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    ActiveState.HandleKeyEvent(gameEvent.Parameter1,gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    ActiveState.HandleKeyEvent(gameEvent.Parameter1,gameEvent.Message);
                    break;
                }
            }
            }
        }
    }

