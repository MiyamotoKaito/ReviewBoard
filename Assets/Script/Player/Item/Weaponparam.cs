using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData")]
public class WeaponparamAsset : ScriptableObject
{
    public List<Weaponparam> WeaponsparamList = new List<Weaponparam>();
}

[System.Serializable]
public class Weaponparam
{
    public string WeaponName = "Punch";

    [SerializeField] int Weapondamage = 10;
    [SerializeField] GameObject WeaponpPrefab;
    [SerializeField] GameObject WeaponHitPrefab;
}