using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Начальная комната
    /// </summary>
    public class StartRoom : Room
    {
        public StartRoom(RoomPreferences reference) : base(reference)
        {
        }

        public override void InitializeSpawners()
        {
            base.InitializeSpawners();
            Player.Instance.transform.position = (Vector2)Parameters.GetRandomEmptyPosition(FloorPositionList);
        }
    }
}
