using DIKUArcade.Entities;
using Exercise3ny;

namespace Galaga_Exercise_3.MovementStrategy {
        public interface IMovementStrategy {
            void MoveEnemy(Enemy enemy);
            void MoveEnemies(EntityContainer<Enemy> enemies);
        }
    }

