using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	
	string playLevel = "MainMenu";
	
	private SceneFader sceneFader;
	private void Awake()
	{
	
		Application.targetFrameRate = 60;
	
	}

	private void Start()
    {
		sceneFader = FindObjectOfType<SceneFader>();
    }
    public void Menu ()
	{
		sceneFader.FadeTo(playLevel);
	}
	public void Play(string LevelNumber)
	{
		sceneFader.FadeTo(LevelNumber);
	}
	public void Tutorial()
	{
		sceneFader.FadeTo("tutorial");
	}
	public void Quit ()
	{
		Debug.Log("Exciting...");
		Application.Quit();
	}

}
