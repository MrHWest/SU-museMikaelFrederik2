using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Exercise3ny {
    
    public class Enemy : Entity {
        private IGameState game;
        public Vec2F StartPosition { get; private set; }
        public Enemy(IGameState game, Shape shape, ImageStride image) : base(shape, image) {
            this.StartPosition = shape.Position;
        }
    }
}