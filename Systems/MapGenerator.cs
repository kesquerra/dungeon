using Dungeon.Core;
using RogueSharp;
using RLNET;
using System;
using System.Linq;

namespace Dungeon.Systems {
    public class MapGenerator {
        private readonly int _width;
        private readonly int _height;
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;

        private readonly DungeonMap _map;

        public MapGenerator(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize) {
            _width = width;
            _height = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;
            _map = new DungeonMap();
        }

        public DungeonMap CreateMap() {
            _map.Initialize(_width, _height);

            for (int i=_maxRooms; i>0; i--) {
                int roomWidth = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomXPosition = Game.Random.Next(0, _width - roomWidth -1);
                int roomYPosition = Game.Random.Next(0, _height - roomHeight - 1);

                var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);
                bool newRoomIntersects = _map.Rooms.Any(room => newRoom.Intersects(room));

                if (!newRoomIntersects) {
                    _map.Rooms.Add(newRoom);
                }
            }
            foreach (Rectangle room in _map.Rooms) {
                CreateRoom(room);
            }

            for (int r=1; r<_map.Rooms.Count; r++) {
                int prevRoomCenterX = _map.Rooms[r-1].Center.X;
                int prevRoomCenterY = _map.Rooms[r-1].Center.Y;
                int curRoomCenterX = _map.Rooms[r].Center.X;
                int curRoomCenterY = _map.Rooms[r].Center.Y;

                if (Game.Random.Next(1, 2) == 1) {
                    CreateHorizontalTunnel(prevRoomCenterX, curRoomCenterX, prevRoomCenterY);
                    CreateVerticalTunnel(prevRoomCenterY, curRoomCenterY, curRoomCenterX);
                } else {
                    CreateVerticalTunnel(prevRoomCenterY, curRoomCenterY, prevRoomCenterX);
                    CreateHorizontalTunnel(prevRoomCenterX, curRoomCenterX, curRoomCenterY);
                }
            }
            PlacePlayer();
            return _map;

        }

        private void CreateRoom(Rectangle room) {
            for (int x=room.Left+1; x<room.Right; x++) {
                for (int y=room.Top+1; y<room.Bottom; y++) {
                    _map.SetCellProperties(x, y, true, true, true);
                }
            }
        }

        private void PlacePlayer() {
            Player player = Game.Player;
            if (player == null) {
                player = new Player();
            }

            player.X = _map.Rooms[0].Center.X;
            player.Y = _map.Rooms[0].Center.Y;

            _map.AddPlayer(player);
        }

        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPos) {
            for (int x=Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++) {
                _map.SetCellProperties(x, yPos, true, true);
            }
        }

        private void CreateVerticalTunnel(int yStart, int yEnd, int xPos) {
            for (int y=Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++) {
                _map.SetCellProperties(xPos, y, true, true);
            }
        }
    }
}