using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ItensInInventory")]
public class ItensInMenu : ScriptableObject
{
    public string ItemName;
    [TextAreaAttribute]
    public string ItemDescription;
}
