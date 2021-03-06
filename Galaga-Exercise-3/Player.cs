using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Galaga_Exercise_3;
using Galaga_Exercise_3.GameStates;

namespace Exercise3ny {
    
        public class Player : IGameEventProcessor<object> {
        public Entity Entity { get; private set;} 
        private IGameState game;
        
        
        public Player(IGameState game, DynamicShape shape, IBaseImage image)  {
            this.game = game;
            Entity= new Entity(shape,image);
        }

        private void Direction(Vec2F dir) {
            this.Entity.Shape.AsDynamicShape().Direction = dir;
        }
        
        
        public void Right() {
            Direction(new Vec2F(0.01f, 0.00f));
        }
        public void Left() {
            Direction(new Vec2F(-0.01f,0.00f));
        }
        public void Release() {
            Direction(new Vec2F(0.00f,0.00f));
        }

        public void Move() {
            //our player will only move left and right, so we only check X values
            if(this.Entity.Shape.Position.X > 0.01 && this.Entity.Shape.Position.X < 1-this.Entity.Shape.Extent.X) {
                this.Entity.Shape.Move(this.Entity.Shape.AsDynamicShape().Direction);
            }
            else if(this.Entity.Shape.Position.X <= 0 + this.Entity.Shape.Extent.X && this.Entity.Shape.AsDynamicShape().Direction.X > 0) {
                this.Entity.Shape.Move(this.Entity.Shape.AsDynamicShape().Direction);
            }
            else if(this.Entity.Shape.Position.X >= 1-this.Entity.Shape.Extent.X && this.Entity.Shape.AsDynamicShape().Direction.X < 0) {
                this.Entity.Shape.Move(this.Entity.Shape.AsDynamicShape().Direction);
            }
        }

        Image x = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        public void Shoot() {
            GameRunning.playerShots.Add(new PlayerShot(new DynamicShape(new Vec2F((this.Entity.Shape.Position.X+0.045f), this.Entity.Shape.Position.Y+this.Entity.Shape.Extent.Y),
                new Vec2F(0.008f,0.027f)),x));
        }

        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
             if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                case "LEFT":
                    Left();
                    break;
                case "RIGHT":
                    Right();
                    break;
                case "RELEASE":
                    Release();
                    break;
                }
            } 
        }
    }
}
    