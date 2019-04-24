using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Exercise3ny;
using Exercise3ny.Squadrons;


namespace Galaga_Exercise_3.Squadrons {
    public class Triangle : ISquadrons {
        
        //Setter added to allow for enemy deletion
        public EntityContainer<Enemy> enemies { get; set; }
        public int MaxEnemies { get; }
        public List<Enemy> listenemy;


        public Triangle() {
            this.MaxEnemies = 3;
            this.enemies = new EntityContainer<Enemy>(MaxEnemies);
            listenemy=new List<Enemy>();

        }
        public void CreateEnemies(List<Image> enemyStrides) {
            enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(0.6f, 0.9f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(0.4f, 0.9f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(0.5f, 0.9f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
            
            enemies.AddDynamicEntity(new Enemy(new DynamicShape(new Vec2F(0.5f, 0.8f),
                new Vec2F(0.1f, 0.1f)), new ImageStride(80, enemyStrides)));
        }
    }
}
