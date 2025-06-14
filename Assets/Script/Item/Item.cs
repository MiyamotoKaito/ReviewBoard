using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData")]
public class Item : ScriptableObject
{
    public GameObject Cursor;
    public GameObject attack;
    public int damage;

}
