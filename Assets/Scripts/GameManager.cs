﻿//Name: Eugene Dietrich
//Inst: Dr. Burns
//Crs:	CSC 496
//Ass: 	HW4
//File:	GameManager.cs

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject YouWonText;
	public GameObject GameOverText;
	public Text GameOverScoreText;
	public GameObject GameOverPanel;

	Tile[,] AllTiles = new Tile[4, 4];
	readonly List<Tile[]> columns = new List<Tile[]>();
	readonly List<Tile[]> rows = new List<Tile[]>();
	List<Tile> EmptyTiles = new List<Tile>();

	// Use this for initialization
	void Start()
	{
		Tile[] AllTilesOneDim = FindObjectsOfType<Tile>();
		foreach (Tile t in AllTilesOneDim)
		{
			t.Number = 0;
			AllTiles[t.indRow, t.indCol] = t;
			EmptyTiles.Add(t);
		}
		columns.Add(new Tile[] { AllTiles[0, 0], AllTiles[1, 0], AllTiles[2, 0], AllTiles[3, 0] });
		columns.Add(new Tile[] { AllTiles[0, 1], AllTiles[1, 1], AllTiles[2, 1], AllTiles[3, 1] });
		columns.Add(new Tile[] { AllTiles[0, 2], AllTiles[1, 2], AllTiles[2, 2], AllTiles[3, 2] });
		columns.Add(new Tile[] { AllTiles[0, 3], AllTiles[1, 3], AllTiles[2, 3], AllTiles[3, 3] });

		rows.Add(new Tile[] { AllTiles[0, 0], AllTiles[0, 1], AllTiles[0, 2], AllTiles[0, 3] });
		rows.Add(new Tile[] { AllTiles[1, 0], AllTiles[1, 1], AllTiles[1, 2], AllTiles[1, 3] });
		rows.Add(new Tile[] { AllTiles[2, 0], AllTiles[2, 1], AllTiles[2, 2], AllTiles[2, 3] });
		rows.Add(new Tile[] { AllTiles[3, 0], AllTiles[3, 1], AllTiles[3, 2], AllTiles[3, 3] });

		Generate();
		Generate();
	}
	void YouWon()
	{
		GameOverText.SetActive(false);
		YouWonText.SetActive(true);
		GameOverScoreText.text = ScoreTracker.Instance.Score.ToString();
		GameOverPanel.SetActive(true);
	}

	void GameOver()
	{

		GameOverScoreText.text = ScoreTracker.Instance.Score.ToString();
		GameOverPanel.SetActive(true);
		YouWonText.SetActive(false);
	}

	bool CanMove()
	{
		if (EmptyTiles.Count > 0)
			return true;
		else
		{

			//Check Columns
			for (int i = 0; i < columns.Count; i++)
				for (int j = 0; j < rows.Count - 1; j++)
					if (AllTiles[j, i].Number == AllTiles[j + 1, i].Number)
						return true;

			//Check Rows
			for (int i = 0; i < rows.Count; i++)
				for (int j = 0; j < columns.Count - 1; j++)
					if (AllTiles[i, j].Number == AllTiles[i, j + 1].Number)
						return true;
		}
		return false;


	}

	public void NewGameButtonHandler()
	{
		SceneManager.LoadScene("Scene");
	}

	bool MakeOneMoveDownIndex(Tile[] LineOfTiles)
	{
		for (int i = 0; i < LineOfTiles.Length - 1; i++)
		{
			//MOVEBLOCK
			if (LineOfTiles[i].Number == 0 && LineOfTiles[i + 1].Number != 0)
			{
				LineOfTiles[i].Number = LineOfTiles[i + 1].Number;
				LineOfTiles[i + 1].Number = 0;
				return true;
			}
			//MERGE BLOCK
			if (LineOfTiles[i].Number != 0 && LineOfTiles[i].Number == LineOfTiles[i + 1].Number &&
				LineOfTiles[i].mergedThisTurn == false && LineOfTiles[i + 1].mergedThisTurn == false)
			{
				LineOfTiles[i].Number *= 2;
				LineOfTiles[i + 1].Number = 0;
				LineOfTiles[i].mergedThisTurn = true;
				ScoreTracker.Instance.Score += LineOfTiles[i].Number;
				if (LineOfTiles[i].Number == 2048)
					YouWon();
				return true;
			}

		}
		return false;
	}

	bool MakeOneMoveUpIndex(Tile[] LineOfTiles)
	{
		for (int i = LineOfTiles.Length - 1; i > 0; i--)
		{
			//MOVEBLOCK
			if (LineOfTiles[i].Number == 0 && LineOfTiles[i - 1].Number != 0)
			{
				LineOfTiles[i].Number = LineOfTiles[i - 1].Number;
				LineOfTiles[i - 1].Number = 0;
				return true;
			}
			//MERGE BLOCK
			if (LineOfTiles[i].Number != 0 && LineOfTiles[i].Number == LineOfTiles[i - 1].Number &&
				LineOfTiles[i].mergedThisTurn == false && LineOfTiles[i - 1].mergedThisTurn == false)
			{
				LineOfTiles[i].Number *= 2;
				LineOfTiles[i - 1].Number = 0;
				LineOfTiles[i].mergedThisTurn = true;
				ScoreTracker.Instance.Score += LineOfTiles[i].Number;
				if (LineOfTiles[i].Number == 2048)
					YouWon();
				return true;
			}
		}
		return false;
	}

	void Generate()
	{
		if (EmptyTiles.Count > 0)
		{
			int indexForNewNumber = Random.Range(0, EmptyTiles.Count);
			int randomNum = Random.Range(0, 10);
			if (randomNum == 0)
			{
				EmptyTiles[indexForNewNumber].Number = 4;
			}
			else
				EmptyTiles[indexForNewNumber].Number = 2;
			EmptyTiles.RemoveAt(indexForNewNumber);
		}
	}

	void ResetMergedFlags()
	{
		foreach (Tile t in AllTiles)
		{
			t.mergedThisTurn = false;
		}
	}

	void UpdateEmptyTiles()
	{
		EmptyTiles.Clear();
		foreach (Tile t in AllTiles)
			if (t.Number == 0)
				EmptyTiles.Add(t);
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
			UpdateEmptyTiles();
			Generate();

			if (!CanMove())
			{
				GameOver();
			}
		}
	}
}
