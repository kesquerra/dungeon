using RLNET;
using Dungeon.Core;
using Dungeon.Systems;
using System;

namespace Dungeon
{
    public static class Game
    {

        private static bool _renderRequired = true;
        public static CommandSystem CommandSystem{get; private set;}
        // Main Screen Variables
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;
        public static RLRootConsole _rootConsole;

        // Map Screen Variables
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 48;
        private static RLConsole _mapConsole;

        // Message Screen Variables
        private static readonly int _msgWidth = 80;
        private static readonly int _msgHeight = 11;
        private static RLConsole _msgConsole;

        // Message Screen Variables
        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 70;
        private static RLConsole _statConsole;

        private static readonly int _invWidth = 80;
        private static readonly int _invHeight = 11;
        private static RLConsole _invConsole;

        public static Player Player {get; private set;}
        public static DungeonMap DungeonMap {get; private set;}
        
        public static void Main()
        {
            string fontFileName = "terminal8x8.png";
            string consoleTitle = "Roguelike";
            
            CommandSystem = new CommandSystem();
            // Init all consoles
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight, 8, 8, 3f, consoleTitle);
            _mapConsole = new RLConsole(_mapWidth, _mapHeight);
            _msgConsole = new RLConsole(_msgWidth, _msgHeight);
            _statConsole = new RLConsole(_statWidth, _statHeight);
            _invConsole = new RLConsole(_invWidth, _invHeight);

            _mapConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, Colors.FloorBackground);
            _mapConsole.Print(1, 1, "Map", Colors.TextHeading);

            _msgConsole.SetBackColor(0, 0, _msgWidth, _msgHeight, Swatch.DbDeepWater);
            _msgConsole.Print(1, 1, "Messages", Colors.TextHeading);

            _statConsole.SetBackColor(0, 0, _statWidth, _statHeight, Swatch.DbOldStone);
            _statConsole.Print(1, 1, "Stats", Colors.TextHeading);
            
            _invConsole.SetBackColor(0, 0, _invWidth, _invHeight, Swatch.DbWood);
            _invConsole.Print(1, 1, "Inventory", Colors.TextHeading);


            Player = new Player();
            MapGenerator mapGenerator = new MapGenerator(_mapWidth, _mapHeight);
            DungeonMap = mapGenerator.CreateMap();
            DungeonMap.UpdatePlayerFieldOfView();
            
            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;
            _rootConsole.Run();
        }

        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e) {
            bool didPlayerAct = false;
            RLKeyPress keyPress = _rootConsole.Keyboard.GetKeyPress();

            if ( keyPress != null ) {
                if ( keyPress.Key == RLKey.Up ) {
                    didPlayerAct = CommandSystem.MovePlayer( Direction.Up );
                }
                else if ( keyPress.Key == RLKey.Down ) {
                didPlayerAct = CommandSystem.MovePlayer( Direction.Down );
                }
                else if ( keyPress.Key == RLKey.Left )
                {
                didPlayerAct = CommandSystem.MovePlayer( Direction.Left );
                }
                else if ( keyPress.Key == RLKey.Right )
                {
                didPlayerAct = CommandSystem.MovePlayer( Direction.Right );
                }
                else if ( keyPress.Key == RLKey.Escape )
                {
                _rootConsole.Close();
                }
            }
            if (didPlayerAct) {
                _renderRequired = true;
            }
        }
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e) {
            RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight, _rootConsole, 0, _invHeight);
            RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight, _rootConsole, _mapWidth, 0);
            RLConsole.Blit(_msgConsole, 0, 0, _msgWidth, _msgHeight, _rootConsole, 0, _screenHeight - _msgHeight);
            RLConsole.Blit(_invConsole, 0, 0, _invWidth, _invHeight, _rootConsole, 0, 0);
            _rootConsole.Draw();

            if (_renderRequired) {
            
                DungeonMap.Draw(_mapConsole);
                Player.Draw(_mapConsole, DungeonMap);

                RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight, _rootConsole, 0, _invHeight);
                RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight, _rootConsole, _mapWidth, 0);
                RLConsole.Blit(_msgConsole, 0, 0, _msgWidth, _msgHeight, _rootConsole, 0, _screenHeight - _msgHeight);
                RLConsole.Blit(_invConsole, 0, 0, _invWidth, _invHeight, _rootConsole, 0, 0);
                _rootConsole.Draw();
                
                _renderRequired = false;
            }
        }

        
    }
}
