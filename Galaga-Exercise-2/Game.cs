using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using Galaga_Exercise_1;
using Galaga_Exercise_1.MovementStrategy;
using Galaga_Exercise_2.MovementStrategy;
using Galaga_Exercise_2.Squadrons;

namespace Galaga_Exercise_2 {
    public class Game : IGameEventProcessor<object> {
        private Window win;
        private DIKUArcade.Timers.GameTimer gameTimer;
        private Player player;
        private GameEventBus<object> eventBus;

        public List<Image> enemyStrides;

        public List<Enemy> enemies;

        private List<Image> explosionStrides;

        private AnimationContainer explosions;

        private Score score;

        private Triangle t;
        private Square s;
        private Diamond d;

        private Down down;
        private ZigZagDown zzdown;
        public List<PlayerShot> playerShots { get; private set; }

        public Game() {
            win = new Window("Window-name", 500, 500);
            gameTimer = new GameTimer(60, 60);
            t = new Triangle(this);
            s = new Square(this);
            d = new Diamond(this);
            down = new Down();
            zzdown = new ZigZagDown(0.0003f, 0.05f, 0.045f);

            player = new Player(this, new DynamicShape(new Vec2F(0.45f, 0.1f),
                new Vec2F(0.1f, 0.1f)), new Image(Path.Combine("Assets", "Images",
                "Player.png")));
            explosionStrides = ImageStride.CreateStrides(8,
                Path.Combine("Assets", "Images", "Explosion.png"));
            explosions = new AnimationContainer(500);

            score = new Score(new Vec2F(0.8f, 0.7f),
                new Vec2F(0.2f, 0.2f));

            eventBus = new GameEventBus<object>();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.InputEvent, // key press / key release
                GameEventType.WindowEvent, // messages to the window
                GameEventType.PlayerEvent, // Move the player
            });
            win.RegisterEventBus(eventBus);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            eventBus.Subscribe(GameEventType.PlayerEvent, this);

            enemyStrides = ImageStride.CreateStrides(4,
                Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemies = new List<Enemy>();
            AddEnemies();

            playerShots = new List<PlayerShot>();
        }

        private int explosionLength = 500;

        public void AddExplosion(float posX, float posY,
            float extentX, float extentY) {
            explosions.AddAnimation(
                new StationaryShape(posX, posY, extentX, extentY), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }

        public void AddEnemies() {
            d.CreateEnemies(enemyStrides);
        }

        public void IterateShots() {
            foreach (var shot in playerShots) {
                shot.Shape.Move();
                if (shot.Shape.Position.Y > 1.0f) {
                    shot.DeleteEntity();
                }
                
                foreach (Enemy enemy in d.enemies) {
                    if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape)
                        .Collision) {
                        score.AddPoint();
                        explosions.RenderAnimations();
                        AddExplosion(enemy.Shape.Position.X, enemy.Shape.Position.Y,
                            enemy.Shape.Extent.X, enemy.Shape.Extent.Y);

                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                    }
                }
            }

            EntityContainer<Enemy> newEnemies = new EntityContainer<Enemy>();
            foreach (Enemy enemy in d.enemies) {
                if (!enemy.IsDeleted()) {
                    newEnemies.AddDynamicEntity(enemy);
                }
            }

            d.enemies = newEnemies;

            List<PlayerShot> newPlayerShots = new List<PlayerShot>();
            foreach (PlayerShot shot in playerShots) {
                if (!shot.IsDeleted()) {
                    newPlayerShots.Add(shot);
                }
            }

            playerShots = newPlayerShots;
        }

        public void GameLoop() {
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    // Update game logic here
                    player.Move();
                    win.PollEvents();
                    eventBus.ProcessEvents();
                    IterateShots();
                    zzdown.MoveEnemies(d.enemies);
                    
                }

                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    // Render gameplay entities here
                    d.enemies.RenderEntities();
                    s.enemies.RenderEntities();
                    t.enemies.RenderEntities();
                    player.Entity.RenderEntity();
                    score.RenderScore();
                    explosions.RenderAnimations();
                    foreach (Enemy item in enemies) {
                        item.RenderEntity();
                    }

                    foreach (PlayerShot shot in playerShots) {
                        shot.RenderEntity();
                    }

                    win.SwapBuffers();
                }

                if (gameTimer.ShouldReset()) {
                    // 1 second has passed - display last captured ups and fps
                    win.Title = "Galaga | UPS: " + gameTimer.CapturedUpdates +
                                ", FPS: " + gameTimer.CapturedFrames;
                }
            }
        }

        private void KeyPress(string key) {
            switch (key) {
            case "KEY_ESCAPE":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.WindowEvent, this, "CLOSE_WINDOW",
                        "", ""));
                break;
            case "KEY_SPACE":
                player.Shoot();
                break;
            case "KEY_A": case "KEY_D": case "KEY_LEFT": case "KEY_RIGHT":
                player.ProcessEvent(GameEventType.PlayerEvent,GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.PlayerEvent, this,
                    key == "KEY_A" || key == "KEY_LEFT" ? "LEFT" : "RIGHT",
                    "", ""));
                
                break;
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
            case "KEY_A": case "KEY_D": case "KEY_LEFT": case "KEY_RIGHT":
                player.ProcessEvent(GameEventType.PlayerEvent,
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.PlayerEvent, this, "RELEASE",
                        "", ""));
                break;
            case "KEY_SPACE":
                break;
            }
        }

        public void ProcessEvent(GameEventType eventType,
            GameEvent<object> gameEvent) {
            if (eventType == GameEventType.WindowEvent) {
                switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    win.CloseWindow();
                    break;
                }
                
            } else if (eventType == GameEventType.InputEvent) {
                switch (gameEvent.Parameter1) {
                case "KEY_PRESS":
                    KeyPress(gameEvent.Message);
                    break;
                case "KEY_RELEASE":
                    KeyRelease(gameEvent.Message);
                    break;
                }
            }
            
            }
        }
    }
