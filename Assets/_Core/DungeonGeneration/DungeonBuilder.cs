using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.DungeonGeneration
{
    public class DungeonBuilder : MonoBehaviour
    {
        [Header("Tilemap Renderer")]
        [SerializeField] private TilemapVisualizer _dungeonVisualizer;

        [Header("Room Reference")]
        [SerializeField] private RoomBuilder _roomTarget;

        [Header("Dungeon Parameters")]
        [SerializeField] [Min(3)] private int _roomsCount = 3;
        [SerializeField] [Range(1, 5)] private int _roomOffset = 1;

        [Header("Rooms Parameters")]
        [SerializeField] private List<RoomPreferences> _defaultRoomPreferences = new List<RoomPreferences>();
        [SerializeField] private RoomPreferences _startRoomPreference = new RoomPreferences();
        [SerializeField] private RoomPreferences _finishRoomPreference = new RoomPreferences();

        private void Start()
        {
            var preparedRooms = CreateRooms();
            var totalFloor = ConnectRooms(preparedRooms);

            VisualizeRooms(totalFloor, preparedRooms.Last());

            Player.Instance.transform.position = preparedRooms.First().Center + new Vector2(0.5f, 0.5f);
        }

        private List<RoomBuilder> CreateRooms()
        {
            var roomList = new List<RoomBuilder>();

            roomList.Add(CreateRoom(Vector2Int.zero, _startRoomPreference));

            for (int i = 1; i < _roomsCount - 1; i++)
                roomList.Add(CreateRoom(Vector2Int.zero, _defaultRoomPreferences[Random.Range(0, _defaultRoomPreferences.Count)]));

            roomList.Add(CreateRoom(Vector2Int.zero, _finishRoomPreference));

            return ReArrangeRooms(roomList);
        }

        private RoomBuilder CreateRoom(Vector2Int startPosition, RoomPreferences roomPreference)
        {
            var roomObject = Instantiate(_roomTarget.gameObject, (Vector2)startPosition, Quaternion.identity);
            var room = roomObject.GetComponent<RoomBuilder>();
            var roomParameters = roomPreference.GetParameters();

            room.CreateShape(startPosition, roomParameters._pathIterations, roomParameters._pathLength, roomParameters._isRandomEachIteration);
            room.InitializeSpawners(roomPreference.GetSpawners());

            return room;
        }

        private List<RoomBuilder> ReArrangeRooms(List<RoomBuilder> preparedRooms)
        {
            var roomList = new List<RoomBuilder>();
            Vector2Int startPosition = Vector2Int.zero;

            for (int i = 0; i < preparedRooms.Count; i++)
            {
                roomList.Add(preparedRooms[i].ArrangeOn(startPosition));

                if (i + 1 < preparedRooms.Count)
                    startPosition = CreateStartPosition(roomList, preparedRooms[i + 1]);
            }

            return roomList;
        }

        private Vector2Int CreateStartPosition(List<RoomBuilder> existRooms, RoomBuilder nextRoom)
        {
            var lastRoom = existRooms.Last();

            Vector2Int roomDirection = Direction2D.GetRandomStandartDirection().Rounded();
            var startPosition = lastRoom.GetBound(roomDirection) + nextRoom.Size * (roomDirection * _roomOffset);

            while (existRooms.Exists(s => s.IsOverlaped(startPosition)))
            {
                roomDirection = Direction2D.GetRandomStandartDirection().Rounded();
                startPosition = lastRoom.GetBound(roomDirection) + nextRoom.Size * (roomDirection * _roomOffset);
            }

            return startPosition;
        }

        private HashSet<Vector2Int> ConnectRooms(List<RoomBuilder> rooms)
        {
            var result = new HashSet<Vector2Int>();

            RoomBuilder lastRoom = null;
            foreach (var room in rooms)
            {
                room.UnionWith(ref result);
                if (lastRoom != null)
                    result.UnionWith(CreateCorridor(room, lastRoom));

                lastRoom = room;
            }

            return result;
        }

        private HashSet<Vector2Int> CreateCorridor(RoomBuilder currentRoom, RoomBuilder lastRoom)
        {
            var corridorFloor = new HashSet<Vector2Int>() { currentRoom.Center };
            var currentPosition = corridorFloor.Last();

            while (currentPosition.x != lastRoom.Center.x)
                DrawCorridorTile(currentPosition.x, lastRoom.Center.x, Vector2Int.right);

            while (currentPosition.y != lastRoom.Center.y)
                DrawCorridorTile(currentPosition.y, lastRoom.Center.y, Vector2Int.up);

            void DrawCorridorTile(int current, int destination, Vector2Int increment)
            {
                if (current > destination)
                    increment *= -1;

                corridorFloor.Add(currentPosition + increment);
                currentPosition = corridorFloor.Last();
            }

            return corridorFloor;
        }

        private HashSet<Vector2Int> CreateWalls(HashSet<Vector2Int> floorPositions)
        {
            var result = new HashSet<Vector2Int>();

            foreach (var position in floorPositions)
                foreach (var direction in Direction2D.FullDirectionList)
                {
                    var wallPos = (position + direction).Rounded();
                    if (!floorPositions.Contains(wallPos))
                        result.Add(wallPos);
                }

            return result;
        }

        private void VisualizeRooms(HashSet<Vector2Int> floorPositions, RoomBuilder finishRoom)
        {
            var wallPositions = CreateWalls(floorPositions);

            var pos = finishRoom.Center;
            var finishPositions = new List<Vector2Int>() { pos + Vector2Int.left, pos + Vector2Int.right };

            wallPositions.UnionWith(finishPositions);
            finishPositions.Add(pos);

            new MinimapIndicator(pos + new Vector2(0.5f, 0.5f), eMinimapIndicator.Finish).SetIndicator();
            // CHEAT
            CheatHandler.SetFinishPosition(pos);

            _dungeonVisualizer.VisualizeFloorTiles(floorPositions);
            _dungeonVisualizer.VisualizeWallTiles(wallPositions);
            _dungeonVisualizer.VisualizeFinishTiles(finishPositions);
        }
    }
}
