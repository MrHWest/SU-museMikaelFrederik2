using DIKUArcade.Entities;
using DIKUArcade.Math;
using Galaga_Exercise_3.MovementStrategy;

namespace Exercise3ny.MovementStrategy {
    public class NoMove : IMovementStrategy {
        public void MoveEnemy(Enemy enemy) {
            enemy.Shape.AsDynamicShape().Direction = new Vec2F(0.0f, 0.0f);
            enemy.Shape.AsDynamicShape().Move();
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Entity enemy in enemies) {
                MoveEnemy(enemy as Enemy);

            }
        }
    }
}