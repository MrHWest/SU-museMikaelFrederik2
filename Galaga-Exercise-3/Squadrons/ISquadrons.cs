using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Exercise3ny.Squadrons {
    public interface ISquadrons {
        EntityContainer<Enemy> enemies { get; }
        int MaxEnemies { get; }
        
        void CreateEnemies(List<Image> enemyStrides);
    }
}