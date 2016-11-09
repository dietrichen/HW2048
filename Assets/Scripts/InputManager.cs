//Name: Eugene Dietrich
//Inst: Dr. Burns
//Crs:	CSC 496
//Ass: 	HW4
//File:	InputManager.cs

using UnityEngine;

public enum MoveDirection
{
	Left,
	Right,
	Up,
	Down
}

public class InputManager : MonoBehaviour
{
	GameManager gm;

	void Awake()
	{
		gm = FindObjectOfType<GameManager>();
		Debug.Log("Awake");
	}
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			// move right
			gm.Move(MoveDirection.Right);
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			// move left
			gm.Move(MoveDirection.Left);
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			// move up
			gm.Move(MoveDirection.Up);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			// move down
			gm.Move(MoveDirection.Down);
		}

	}
}

