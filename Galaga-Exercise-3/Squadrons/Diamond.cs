using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Exercise3ny.Squadrons {
    public class Diamond {
             
        //Setter added to allow for enemy deletion
        public EntityContainer<Enemy> enemies { get; set; }
        public int MaxEnemies { get; }

        public Diamond() {
            this.MaxEnemies = 11;
            this.enemies = new EntityContainer<Enemy>();
        }

        public void CreateEnemies(List<Image> enemyStrides) {
            
            enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(0.4f, 0.8f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(0.6f, 0.8f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(0.5f, 0.9f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(0.5f, 0.8f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(0.5f, 0.7f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
        }
    }
}