using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Компонент отвечающий за процедурную генерацию подземелья
    /// </summary>
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Tilemap Renderer")]
        [SerializeField] private TilemapVisualizer _dungeonVisualizer;

        [Header("Dungeon Parameters")]
        [SerializeField] private Vector2Int _dungeonSize = new Vector2Int(50, 50);
        [SerializeField] [Min(3)] private int _roomsCount = 3;
        [SerializeField] [Range(1, 10)] private int _roomOffset = 1;

        [Header("Rooms Parameters")]
        [SerializeField] private List<RoomPreferences> _defaultRoomReferenceList = new List<RoomPreferences>();
        [SerializeField] private RoomPreferences _startRoomReference = new RoomPreferences();
        [SerializeField] private RoomPreferences _finishRoomReference = new RoomPreferences();


        private List<Room> _preparedRooms = new List<Room>();

        private HashSet<Vector2Int> _floorPositions = new HashSet<Vector2Int>();
        private HashSet<Vector2Int> _wallPositions = new HashSet<Vector2Int>();
        private HashSet<Vector2Int> _corridorPositions = new HashSet<Vector2Int>();

        private void Start()
        {
            if (_defaultRoomReferenceList.Count == 0)
                return;

            GenerateDungeonFloor();
            GenerateDungeonWalls();

            InitializeDungeonSpawners();

            VisualizeDungeonTiles();
        }

        private void OnDestroy()
        {
            _dungeonVisualizer.Clear();

            ResetCachedValues();

            Debug.Log("dfd");
        }

        /// <summary>
        /// Генерирует пол подземелья
        /// </summary>
        private void GenerateDungeonFloor()
        {
            BuildDungeonRooms();
            _preparedRooms.ForEach(room => _floorPositions.UnionWith(room.FloorPositionList));

            BuildDungeonCorridors();
            _floorPositions.UnionWith(_corridorPositions);
        }

        /// <summary>
        /// Генерирует стены подземелья
        /// </summary>
        private void GenerateDungeonWalls()
        {
            foreach (var position in _floorPositions)
                foreach (var direction in Direction2D.FullDirectionList)
                {
                    var wallPos = Vector2Int.RoundToInt(position + direction);
                    if (!_floorPositions.Contains(wallPos))
                        _wallPositions.Add(wallPos);
                }
        }

        /// <summary>
        /// Инициализирует спавнеры
        /// </summary>
        private void InitializeDungeonSpawners()
        {
            _preparedRooms.ForEach(room => room.InitializeSpawners());
        }

        /// <summary>
        /// Визуализирует подземелье
        /// </summary>
        private void VisualizeDungeonTiles()
        {
            var pos = _preparedRooms.Find(s => s is FinishRoom).SpawnPosition;
            var finishPositions = new List<Vector2Int>() { pos + Vector2Int.left, pos + Vector2Int.right };

            _wallPositions.UnionWith(finishPositions);
            finishPositions.Add(pos);

            _dungeonVisualizer.VisualizeFloorTiles(_floorPositions);
            _dungeonVisualizer.VisualizeWallTiles(_wallPositions);
            _dungeonVisualizer.VisualizeFinishTiles(finishPositions);
        }

        /// <summary>
        /// Генерирует комнаты подземелье
        /// </summary>
        private void BuildDungeonRooms()
        {
            var offset = new Vector2Int(_roomOffset, _roomOffset);

            var preparedRooms = new List<Room>() { new StartRoom(_startRoomReference) };

            for (int i = 1; i < _roomsCount - 1; i++)
            {
                var randomIndex = Random.Range(0, _defaultRoomReferenceList.Count);
                preparedRooms.Add(new DefaultRoom(_defaultRoomReferenceList[randomIndex]));
            }

            preparedRooms.Add(new FinishRoom(_finishRoomReference));

            // Спавним комнаты
            var initedRooms = new List<Room>();
            foreach (var prepRoom in preparedRooms)
            {
                int maxX = _dungeonSize.x - _roomOffset;
                int maxY = _dungeonSize.y - _roomOffset;

                var room = prepRoom;
                SpawnRoom(ref room, maxX, maxY);

                int debugIndex = 0;
                while (initedRooms.Exists(s => s.RoomOverlaped(room, _roomOffset)))
                {
                    SpawnRoom(ref room, maxX, maxY);
                    if (debugIndex++ >= 250) break;
                }

                initedRooms.Add(room);
            }

            _preparedRooms = initedRooms;
        }

        /// <summary>
        /// Получает список координат для пола коридоров
        /// </summary>
        private void BuildDungeonCorridors()
        {
            var roomCenters = new List<Vector2Int>();
            _preparedRooms.ForEach(s => roomCenters.Add(s.SpawnPosition));

            var currnetCenter = roomCenters[Random.Range(0, roomCenters.Count)];
            roomCenters.Remove(currnetCenter);

            while (roomCenters.Count > 0)
            {
                var closestCenter = FindClosestRoomCenter(currnetCenter, roomCenters);

                _corridorPositions.UnionWith(CreateCorridor(currnetCenter, closestCenter));

                currnetCenter = closestCenter;
                roomCenters.Remove(closestCenter);
            }
        }

        /// <summary>
        /// Спавнит комнату
        /// </summary>
        /// <param name="room">Комната для спавна</param>
        /// <param name="maxX">Максимальная ширина комнаты</param>
        /// <param name="maxY">Максимальная высота комнаты</param>
        private void SpawnRoom(ref Room room, int maxX, int maxY)
        {
            room.SetPosition(Random.Range(0, maxX), Random.Range(0, maxY));
            room.InitializeFloorPositions();
        }

        /// <returns>
        /// Пол коридора между центрами двух комнат
        /// </returns>
        /// <param name="currnetCenter">Текущий центр комнаты</param>
        /// <param name="destinationCenter">Центр нужной комнаты</param>
        private HashSet<Vector2Int> CreateCorridor(Vector2Int currnetCenter, Vector2Int destinationCenter)
        {
            var corridorPositions = new HashSet<Vector2Int>() { currnetCenter };

            corridorPositions.UnionWith(DrawCorridorDirection(ref currnetCenter, currnetCenter.y, destinationCenter.y, Vector2Int.up));
            corridorPositions.UnionWith(DrawCorridorDirection(ref currnetCenter, currnetCenter.x, destinationCenter.x, Vector2Int.right));

            return corridorPositions;
            
            HashSet<Vector2Int> DrawCorridorDirection(ref Vector2Int position, int current, int destination, Vector2Int increase)
            {
                var corridorResult = new HashSet<Vector2Int>();

                while (current != destination)
                    corridorResult.Add(GetCorridorDirection(destination, ref current, ref position, increase));

                return corridorResult;
            }

            Vector2Int GetCorridorDirection(int destination, ref int current, ref Vector2Int position, Vector2Int increase)
            {
                if (destination > current)
                    ChangePosition(1, increase, ref current, ref position);
                else if (destination < current)
                    ChangePosition(-1, -increase, ref current, ref position);

                return position;

                void ChangePosition(int increment, Vector2Int increase, ref int current, ref Vector2Int position)
                {
                    current += increment;
                    position += increase;
                }
            }
        }

        /// <returns>
        /// Ближайший центр до текущего центра
        /// </returns>
        /// <param name="currnetRoomCenter">Текущий центр комнаты</param>
        /// <param name="roomCenters">Список всех центров комнат</param>
        private Vector2Int FindClosestRoomCenter(Vector2Int currnetRoomCenter, List<Vector2Int> roomCenters)
        {
            var closestPoint = Vector2Int.zero;
            float currentDistance = int.MaxValue;

            foreach (var position in roomCenters)
            {
                var distance = Vector2Int.Distance(position, currnetRoomCenter);
                if (distance < currentDistance)
                {
                    currentDistance = distance;
                    closestPoint = position;
                }
            }

            return closestPoint;
        }

        /// <summary>
        /// Сбрасывает закешированные координаты пола и стен, и готовые комнаты
        /// </summary>
        private void ResetCachedValues()
        {
            _preparedRooms = new List<Room>();
            _floorPositions = new HashSet<Vector2Int>();
            _wallPositions = new HashSet<Vector2Int>();
            _corridorPositions = new HashSet<Vector2Int>();
    }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)_dungeonSize / 2, (Vector2)_dungeonSize);
            Gizmos.color = Color.green;
            if (_preparedRooms != null)
                foreach (var room in _preparedRooms)
                    if (room.FloorPositionList != null && room.FloorPositionList.Count > 0)
                        Gizmos.DrawWireCube((Vector3Int)(room.Min + room.Size / 2), (Vector3Int)room.Size);
        }
    }
}
