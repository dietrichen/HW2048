//Name: Eugene Dietrich
//Inst: Dr. Burns
//Crs:	CSC 496
//Ass: 	HW4
//File:	ScoreTracker.cs

using UnityEngine;
using UnityEngine.UI;


public class ScoreTracker : MonoBehaviour
{

	int score;
	public static ScoreTracker Instance;
	public Text ScoreText;
	public Text HighScoreText;

	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
			ScoreText.text = score.ToString();

			if (PlayerPrefs.GetInt("HighScore") < score)
			{
				PlayerPrefs.SetInt("HighScore", score);
				HighScoreText.text = score.ToString();

			}
		}
	}
	void Awake()
	{
		Instance = this;
		if (!PlayerPrefs.HasKey("HighScore"))
			PlayerPrefs.SetInt("HighScore", 0);

		ScoreText.text = "0";
		HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
	}


}
