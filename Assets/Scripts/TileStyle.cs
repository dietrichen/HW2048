//Name: Eugene Dietrich
//Inst: Dr. Burns
//Crs:	CSC 496
//Ass: 	HW4
//File:	TitleStyle.cs

using UnityEngine;

[System.Serializable]
public class TileDisplay
{
	public int Number;
	public Color32 TileColor;
	public Color32 TextColor;
}

public class TileStyle : MonoBehaviour
{

	public static TileStyle Instance;
	public TileDisplay[] Tiles;

	void Awake()
	{
		Instance = this;
	}
}