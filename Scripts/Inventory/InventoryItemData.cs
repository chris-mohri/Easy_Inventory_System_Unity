using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="InventoryItemData")]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
    public int maxStack;

    public string type; //weapon/helmet/etc.
    public string description; // flavor text
    //public List<int> stats; // important info like ATK + 12 [ATK, DEF, MAG DEF, AGL, CRT DMG, CRT %]
    
    /**
    public int ATK;
    public int DEF;
    public int MAG_DEF;
    public int AGL;
    public int LCK;
    public int CRT_DMG;
    public int CRT_RATE;
    **/

    //public int stack;

}
