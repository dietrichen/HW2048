using UnityEngine;
using System.Collections;

[System.Serializable]
public class BlockDisplay
{
	public int Number;
	public Color32 BlockColor;
	public Color32 TextColor;
}

public class BlockStyle : MonoBehaviour
{

	public static BlockStyle Instance;
	public BlockDisplay[] Blocks;

	void Awake()
	{
		Instance = this;
	}
}