using RLNET;
using RogueSharp;
using System.Collections.Generic;

namespace Dungeon.Core {
    public class DungeonMap : Map {
        public List<Rectangle> Rooms;

        public DungeonMap() {
            Rooms = new List<Rectangle>();
        }

        public void Draw(RLConsole mapConsole) {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells()) {
                SetConsoleSymbolForCell(mapConsole, cell);
            }
        }

        private void SetConsoleSymbolForCell(RLConsole console, Cell cell) {
            if (!cell.IsExplored) {
                return;
            }

            if (IsInFov(cell.X, cell.Y)) {
                if (cell.IsWalkable) {
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                } else {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            } else {
                if (cell.IsWalkable) {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                } else {
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }

        public void UpdatePlayerFieldOfView() {
            Player player = Game.Player;
            ComputeFov(player.X, player.Y, player.Awareness, true);
            foreach(Cell cell in GetAllCells()) {
                if (IsInFov(cell.X, cell.Y)) {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }

        public bool SetActorPosition(Actor actor, int x, int y) {
            if (GetCell(x, y).IsWalkable) {
                SetIsWalkable(actor.X, actor.Y, true);
                actor.X = x;
                actor.Y = y;

                SetIsWalkable(actor.X, actor.Y, false);

                if (actor is Player) {
                    UpdatePlayerFieldOfView();
                }
                return true;
            }
            return false;
        }

        public void SetIsWalkable(int x, int y, bool isWalkable) {
            ICell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        public void AddPlayer(Player player) {
            Game.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            UpdatePlayerFieldOfView();
        }
    }
}