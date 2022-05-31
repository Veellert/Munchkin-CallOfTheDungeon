using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.DungeonGeneration
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class RoomBuilder : MonoBehaviour
    {
        public Vector2Int Center { get; private set; }
        public Vector2Int Size { get; private set; }

        private BoxCollider2D _collider;
        private HashSet<Vector2Int> _floorPositions = new HashSet<Vector2Int>();
        private List<Spawner> _spawnerList = new List<Spawner>();

        private bool _isSpawnersInited;

        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            SetSize(Size);
        }

        public Vector2Int GetBound(Vector2Int direction) => Center + Size * direction;

        public bool IsOverlaped(Vector2Int position) => _floorPositions.ToList().Exists(s => s.x == position.x &&  s.y == position.y);

        public void UnionWith(ref HashSet<Vector2Int> floorPositions) => floorPositions.UnionWith(_floorPositions);

        public RoomBuilder ArrangeOn(Vector2Int center)
        {
            if (_floorPositions.Count != 0)
            {
                var difference = center - Center;
                Center += difference;

                var arrangedFloor = new HashSet<Vector2Int>();
                foreach (var position in _floorPositions)
                    arrangedFloor.Add(position + difference);

                _floorPositions = arrangedFloor;


                var horizontal = _floorPositions.OrderBy(s => s.x);
                var vertical = _floorPositions.OrderBy(s => s.y);

                var minX = new Vector2Int(horizontal.First().x, 0);
                var minY = new Vector2Int(0, vertical.First().y);

                var maxX = new Vector2Int(horizontal.Last().x, 0);
                var maxY = new Vector2Int(0, vertical.Last().y);

                transform.position = new Vector2(minX.x + Size.x, minY.y + Size.y);
            }

            return this;
        }

        public void CreateShape(Vector2Int startPosition, int floorHardness, int lineLength, bool _isRandomShape = false)
        {
            _floorPositions = new HashSet<Vector2Int>();
            Center = startPosition;

            var currentPosition = startPosition;
            for (int i = 0; i < floorHardness; i++)
            {
                _floorPositions.UnionWith(DrawFloorLines(currentPosition, lineLength));

                if (_isRandomShape)
                    currentPosition = _floorPositions.ElementAt(Random.Range(0, _floorPositions.Count));
            }

            var horizontal = _floorPositions.OrderBy(s => s.x);
            var vertical = _floorPositions.OrderBy(s => s.y);

            var minX = new Vector2Int(horizontal.First().x, 0);
            var minY = new Vector2Int(0, vertical.First().y);
            
            var maxX = new Vector2Int(horizontal.Last().x, 0);
            var maxY = new Vector2Int(0, vertical.Last().y);
            
            //var minX = horizontal.First().x;
            //var minY = vertical.First().y;
            
            //var maxX = horizontal.Last().x;
            //var maxY = vertical.Last().y;

            SetSize(new Vector2Int((int)Vector2.Distance(maxX, minX)/2, (int)Vector2.Distance(maxY, minY)/2));
        }

        private void SetSize(Vector2Int size)
        {
            Size = size;
            if (_collider)
                _collider.size = size * 2;
        }

        private HashSet<Vector2Int> DrawFloorLines(Vector2Int currentPosition, int lineLength)
        {
            var pathResult = new HashSet<Vector2Int> { currentPosition };

            Vector2Int prevPosition = currentPosition;
            for (int i = 0; i < lineLength; i++)
            {
                prevPosition = prevPosition + Direction2D.GetRandomStandartDirection().Rounded();
                pathResult.Add(prevPosition);
            }

            return pathResult;
        }

        private Vector2Int GetRandomEmptyPosition(HashSet<Vector2Int> spawnPositions)
        {
            return spawnPositions.ElementAt(Random.Range(0, spawnPositions.Count()));
        }

        public void InitializeSpawners(List<Spawner> spawners)
        {
            _spawnerList = spawners;
        }

        private void SpawnAll()
        {
            var totalPositions = new HashSet<Vector2Int>(_floorPositions);

            var exceptPositions = new HashSet<Vector2Int>() { Center };
            foreach (var direction in Direction2D.FullDirectionList)
                exceptPositions.Add(Center + direction.Rounded());

            var spawnPositions = new HashSet<Vector2Int>(exceptPositions);

            foreach (var spawner in _spawnerList)
            {
                totalPositions.ExceptWith(spawnPositions);
                spawnPositions.Add(GetRandomEmptyPosition(totalPositions));
                spawner.Spawn(spawnPositions.Last());
            }

            _isSpawnersInited = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isSpawnersInited)
                return;

            if(collision.CompareTag("Player"))
                SpawnAll();
        }
    }
}
