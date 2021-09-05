using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "Custom/Characters/Monsters")]
public class MonsterParameters : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [HideInInspector] public GameObject model;
    public new string name = "Монстр безымянный";
    [Range(1, 20)] public int lvl = 1;
    public bool isBoss; 

    public void Instantiate(Vector3 position)
    {
        model = Instantiate(_prefab, position, Quaternion.identity);
        model.name = name;
    }
}
