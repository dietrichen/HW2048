using UnityEngine;
using System.Collections;

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

	void Awake ()
	{
		Instance = this;
	}
}