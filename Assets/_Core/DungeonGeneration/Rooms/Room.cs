using System.Collections.Generic;
using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Комната родитель
    /// </summary>
    public abstract class Room
    {
        public RoomPreferences Parameters { get; private set; }
        public List<Vector2Int> FloorPositionList { get; private set; }

        public Vector2Int SpawnPosition { get; private set; }

        public Vector2Int Max { get; private set; }
        public Vector2Int Min { get; private set; }

        public Vector2Int Size => Max - Min;

        public Room(RoomPreferences reference)
        {
            Parameters = reference;
            FloorPositionList = new List<Vector2Int>();
        }

        /// <summary>
        /// Устанавливает точку для спавна
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public void SetPosition(int x, int y)
        {
            SetPosition(new Vector2Int(x, y));
        }
        /// <summary>
        /// Устанавливает точку для спавна
        /// </summary>
        /// <param name="position">Точка для спавна</param>
        public void SetPosition(Vector2Int position)
        {
            SpawnPosition = position;
        }

        /// <summary>
        /// Инициализация координат пола комнаты
        /// </summary>
        public void InitializeFloorPositions()
        {
            FloorPositionList = Parameters.GetRoomFloor(SpawnPosition);

            Max = Direction2D.CompareMax(FloorPositionList);
            Min = Direction2D.CompareMin(FloorPositionList);
        }

        /// <summary>
        /// Инициализирует спавнеры
        /// </summary>
        public virtual void InitializeSpawners()
        {
            FloorPositionList = Parameters.InitSpawners(FloorPositionList);
        }

        /// <returns>
        /// Перекрывает ли комната другую комнату
        /// </returns>
        /// <param name="room">Комната которую надо проверить</param>
        /// <param name="offset">Отступ</param>
        public bool RoomOverlaped(Room room, int offset)
        {
            foreach (var pos in FloorPositionList)
                if (room.FloorPositionList.Exists(s => CompairPositions(s, pos) ||
                  CompairPositions(s, pos, offset) || CompairPositions(s, pos, -offset)))
                    return true;

            return false;

            bool CompairPositions(Vector2Int pos1, Vector2Int pos2, int pos1Offset = 0)
            {
                var pos = new Vector2Int(pos1.x + pos1Offset, pos1.y + pos1Offset);

                return Compair(pos.x, pos2.x) && Compair(pos.y, pos2.y);
            }
            bool Compair(int value1, int value2) => value1 == value2;
        }
    }
}
