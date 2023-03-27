using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Marker Set")]
public class MarkerSet : ScriptableObject
{
    public GameObject[] SmallMarkerObjects;
    public GameObject[] MediumMarkerObjects;
    public GameObject[] LargeMarkerObjects;
}
