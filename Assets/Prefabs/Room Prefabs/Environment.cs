using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Environment")]

public class Environment : ScriptableObject
{
    public GameObject[] iTiles; //I Shaped Tiles (2 Exits)
    public GameObject[] tTiles; //T Shaped Tiles (3 Exits)
    public GameObject[] hTiles; //Hub Tiles (4 Exits)
    public GameObject[] eTiles; //End Tiles (1 Exits)
    public GameObject[] cTiles; //Corner Tiles (2 Exits)
    public GameObject[] bossTile; //BossTiles

    public int m_minTiles, m_maxTiles;

    public float m_iChance, m_tChance, m_hChance, m_eChance, m_cChance;
}
