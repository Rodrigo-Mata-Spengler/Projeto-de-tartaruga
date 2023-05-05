using UnityEngine;

[CreateAssetMenu(menuName = "level/Conetions")]

public class Conections : ScriptableObject
{
    public static Conections activeConetion { get; set; }

    public static bool wasConetion { get; set; } = false;
}
