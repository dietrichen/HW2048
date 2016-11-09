using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject YouWonText;
	public GameObject GameOverText;
	public Text GameOverScoreText;
	public GameObject GameOverPanel;

	private Block[,] AllBlocks = new Block[4, 4];
	private readonly List<Block[]> columns = new List<Block[]>();
	private readonly List<Block[]> rows = new List<Block[]>();
	private List<Block> EmptyBlocks = new List<Block>();

	// Use this for initialization
	void Start()
	{
		Block[] AllBlocksOneDim = GameObject.FindObjectsOfType<Block>();
		foreach (Block t in AllBlocksOneDim)
		{
			t.Number = 0;
			AllBlocks[t.indRow, t.indCol] = t;
			EmptyBlocks.Add(t);
		}
		columns.Add(new Block[] { AllBlocks[0, 0], AllBlocks[1, 0], AllBlocks[2, 0], AllBlocks[3, 0] });
		columns.Add(new Block[] { AllBlocks[0, 1], AllBlocks[1, 1], AllBlocks[2, 1], AllBlocks[3, 1] });
		columns.Add(new Block[] { AllBlocks[0, 2], AllBlocks[1, 2], AllBlocks[2, 2], AllBlocks[3, 2] });
		columns.Add(new Block[] { AllBlocks[0, 3], AllBlocks[1, 3], AllBlocks[2, 3], AllBlocks[3, 3] });

		rows.Add(new Block[] { AllBlocks[0, 0], AllBlocks[0, 1], AllBlocks[0, 2], AllBlocks[0, 3] });
		rows.Add(new Block[] { AllBlocks[1, 0], AllBlocks[1, 1], AllBlocks[1, 2], AllBlocks[1, 3] });
		rows.Add(new Block[] { AllBlocks[2, 0], AllBlocks[2, 1], AllBlocks[2, 2], AllBlocks[2, 3] });
		rows.Add(new Block[] { AllBlocks[3, 0], AllBlocks[3, 1], AllBlocks[3, 2], AllBlocks[3, 3] });

		Generate();
		Generate();
	}
	private void YouWon()
	{
		GameOverText.SetActive(false);
		YouWonText.SetActive(true);
		GameOverScoreText.text = ScoreTracker.Instance.Score.ToString();
		GameOverPanel.SetActive(true);
	}

	private void GameOver()
	{
		GameOverScoreText.text = ScoreTracker.Instance.Score.ToString();
		GameOverPanel.SetActive(true);
	}

	bool CanMove()
	{
		if (EmptyBlocks.Count > 0)
			return true;
		else
		{

			//Check Columns
			for (int i = 0; i < columns.Count; i++)
				for (int j = 0; j < rows.Count - 1; j++)
					if (AllBlocks[j, i].Number == AllBlocks[j + 1, i].Number)
						return true;

			//Check Rows
			for (int i = 0; i < rows.Count; i++)
				for (int j = 0; j < columns.Count - 1; j++)
					if (AllBlocks[i, j].Number == AllBlocks[i, j + 1].Number)
						return true;
		}
		return false;


	}

	public void NewGameButtonHandler()
	{
		SceneManager.LoadScene("Scene");
	}

	bool MakeOneMoveDownIndex(Block[] LineOfBlocks)
	{
		for (int i = 0; i < LineOfBlocks.Length - 1; i++)
		{
			//MOVEBLOCK
			if (LineOfBlocks[i].Number == 0 && LineOfBlocks[i + 1].Number != 0)
			{
				LineOfBlocks[i].Number = LineOfBlocks[i + 1].Number;
				LineOfBlocks[i + 1].Number = 0;
				return true;
			}
			//MERGE BLOCK
			if (LineOfBlocks[i].Number != 0 && LineOfBlocks[i].Number == LineOfBlocks[i + 1].Number &&
				LineOfBlocks[i].mergedThisTurn == false && LineOfBlocks[i + 1].mergedThisTurn == false)
			{
				LineOfBlocks[i].Number *= 2;
				LineOfBlocks[i + 1].Number = 0;
				LineOfBlocks[i].mergedThisTurn = true;
				ScoreTracker.Instance.Score += LineOfBlocks[i].Number;
				if (LineOfBlocks[i].Number == 2048)
					YouWon();
				return true;
			}

		}
		return false;
	}

	bool MakeOneMoveUpIndex(Block[] LineOfBlocks)
	{
		for (int i = LineOfBlocks.Length - 1; i > 0; i--)
		{
			//MOVEBLOCK
			if (LineOfBlocks[i].Number == 0 && LineOfBlocks[i - 1].Number != 0)
			{
				LineOfBlocks[i].Number = LineOfBlocks[i - 1].Number;
				LineOfBlocks[i - 1].Number = 0;
				return true;
			}
			//MERGE BLOCK
			if (LineOfBlocks[i].Number != 0 && LineOfBlocks[i].Number == LineOfBlocks[i - 1].Number &&
				LineOfBlocks[i].mergedThisTurn == false && LineOfBlocks[i - 1].mergedThisTurn == false)
			{
				LineOfBlocks[i].Number *= 2;
				LineOfBlocks[i - 1].Number = 0;
				LineOfBlocks[i].mergedThisTurn = true;
				ScoreTracker.Instance.Score += LineOfBlocks[i].Number;
				if (LineOfBlocks[i].Number == 2048)
					YouWon();
				return true;
			}
		}
		return false;
	}

	void Generate()
	{
		if (EmptyBlocks.Count > 0)
		{
			int indexForNewNumber = Random.Range(0, EmptyBlocks.Count);
			int randomNum = Random.Range(0, 10);
			if (randomNum == 0)
			{
				EmptyBlocks[indexForNewNumber].Number = 4;
			}
			else
				EmptyBlocks[indexForNewNumber].Number = 2;
			EmptyBlocks.RemoveAt(indexForNewNumber);
		}
	}

	private void ResetMergedFlags()
	{
		foreach (Block t in AllBlocks)
		{
			t.mergedThisTurn = false;
		}
	}

	private void UpdateEmptyBlocks()
	{
		EmptyBlocks.Clear();
		foreach (Block t in AllBlocks)
			if (t.Number == 0)
				EmptyBlocks.Add(t);
	}

	public void Move(MoveDirection md)
	{
		Debug.Log(md + " move.");
		bool moveMade = false;

		ResetMergedFlags();
		for (int i = 0; i < rows.Count; i++)
		{
			switch (md)
			{
				case MoveDirection.Down:
					while (MakeOneMoveUpIndex(columns[i]))
					{
						moveMade = true;
					}
					break;

				case MoveDirection.Up:
					while (MakeOneMoveDownIndex(columns[i]))
					{
						moveMade = true;
					}
					break;
				case MoveDirection.Left:
					while (MakeOneMoveDownIndex(rows[i]))
					{
						moveMade = true;
					}
					break;
				case MoveDirection.Right:
					while (MakeOneMoveUpIndex(rows[i]))
					{
						moveMade = true;
					}
					break;
			}
		}
		if (moveMade)
		{
			UpdateEmptyBlocks();
			Generate();

			if (!CanMove())
			{
				GameOver();
			}
		}
	}
}
