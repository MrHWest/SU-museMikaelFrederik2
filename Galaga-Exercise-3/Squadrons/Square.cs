using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Exercise3ny.Squadrons {
    public class Square {
          
        //Setter added to allow for enemy deletion
        public EntityContainer<Enemy> enemies { get; set; }
        public int MaxEnemies { get; }
        public List<Enemy> listenemy;

        public Square() {
            this.MaxEnemies = 9;
            this.enemies = new EntityContainer<Enemy>(MaxEnemies);
            listenemy=new List<Enemy>();
        }
        public void CreateEnemies(List<Image> enemyStrides) {
            for (int i = 1; i < 4; i++) {
                float xposition = i * 0.09f;
                enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(xposition, 0.9f),
                    new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
                
                enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(xposition, 0.8f),
                    new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
                
                
                enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(xposition, 0.7f),
                    new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            }
        }
    }
}