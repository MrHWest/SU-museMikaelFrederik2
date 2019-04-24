using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Exercise3ny {
    public class PlayerShot : Entity {
        
        public PlayerShot(DynamicShape shape, IBaseImage image) : base(shape, image) {
            
            shape.Direction = new Vec2F(0.0f,0.01f);
        }
    }
}