using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class BaseItem : ScriptableObject
{
    public new string name;
    public Sprite ingameIcon;
    public Sprite inventoryIcon;
    public GameObject prefab;
}
