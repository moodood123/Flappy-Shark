using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSelectionData", menuName = "Data/CharacterSelectionData")]
public class CharacterSelectionData : ScriptableObject
{
    [field: SerializeField] public List<CharacterSelection> Selections { get; private set; } = new List<CharacterSelection>();
}

[Serializable]
public class CharacterSelection
{
    [field: SerializeField] public string SelectionName { get; private set; }
    [field: SerializeField] public string SelectionDescription { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
}