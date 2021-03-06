using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga_Exercise_2.Squadrons {
    public interface ISquadrons {
        EntityContainer<Enemy> enemies { get; }
        int MaxEnemies { get; }
        
        void CreateEnemies(List<Image> enemyStrides);
    }
}