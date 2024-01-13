using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="world", menuName = "level")]
public class level : ScriptableObject
{
    [Header("Board Dimensions")]

    public int width;
    public int height;

    [Header("Starting Tiles")]

    public TileType[] boardLayout;
    
    [Header("Score")]
    public int scores;

    [Header("Availabel All Dots")]
    public GameObject[] dots;

  public GameRequirements endGameRequirements;
}
