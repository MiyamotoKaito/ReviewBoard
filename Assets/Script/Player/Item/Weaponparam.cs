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
    public string WeaponName;

    [SerializeField] int Weapondamage;
    [SerializeField] GameObject WeaponpPrefab;
    [SerializeField] GameObject WeaponHitPrefab;
}