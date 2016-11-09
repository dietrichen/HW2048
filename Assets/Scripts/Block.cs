using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
	public bool mergedThisTurn = false;

	public int indRow;
	public int indCol;

	public int Number
	{
		get
		{
			return number;
		}
		set
		{
			number = value;
			if (number == 0)
				SetEmpty();
			else
			{
				ApplyStyle(number);
				SetVisible();
			}
		}

	}

	private int number;
	private Text BlockText;
	private Image BlockImage;

	void Awake()
	{
		BlockText = GetComponentInChildren<Text>();
		BlockImage = transform.Find("NumberedBlock").GetComponent<Image>();
	}

	void ApplyStyleFromHolder(int index)
	{
		BlockText.text = BlockStyle.Instance.Blocks[index].Number.ToString();
		BlockText.color = BlockStyle.Instance.Blocks[index].TextColor;
		BlockImage.color = BlockStyle.Instance.Blocks[index].BlockColor;
	}

	void ApplyStyle(int num)
	{
		switch (num)
		{
			case 2:
				ApplyStyleFromHolder(0);
				break;
			case 4:
				ApplyStyleFromHolder(1);
				break;
			case 8:
				ApplyStyleFromHolder(2);
				break;
			case 16:
				ApplyStyleFromHolder(3);
				break;
			case 32:
				ApplyStyleFromHolder(4);
				break;
			case 64:
				ApplyStyleFromHolder(5);
				break;
			case 128:
				ApplyStyleFromHolder(6);
				break;
			case 256:
				ApplyStyleFromHolder(7);
				break;
			case 512:
				ApplyStyleFromHolder(8);
				break;
			case 1024:
				ApplyStyleFromHolder(9);
				break;
			case 2048:
				ApplyStyleFromHolder(10);
				break;
			default:
				Debug.LogError("Check passed numbers");
				break;
		}

	}

	private void SetVisible()
	{
		BlockImage.enabled = true;
		BlockText.enabled = true;
	}

	private void SetEmpty()
	{
		BlockImage.enabled = false;
		BlockText.enabled = false;

	}
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
