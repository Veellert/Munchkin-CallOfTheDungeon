using UnityEngine;

namespace Assets.DungeonGeneration
{
    /// <summary>
    /// Финальная комната
    /// </summary>
    public class FinishRoom : Room
    {
        public FinishRoom(RoomPreferences reference) : base(reference)
        {
        }

        public override void InitializeSpawners()
        {
            base.InitializeSpawners();

            var indicatorPosition = new Vector2(SpawnPosition.x + new TileHalf(), SpawnPosition.y + new TileHalf());
            
            new MinimapIndicator(indicatorPosition, eMinimapIndicator.Finish).SetIndicator();
            // CHEAT
            CheatHandler.SetFinishPosition(SpawnPosition);
        }
    }
}
