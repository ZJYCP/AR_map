using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIcontrol : MonoBehaviour {
	[SerializeField]
	
	Transform canvasTransform;
	Button returnBtn;
	Button closeBoard;

	Button soundbtn;
	GameObject textdiv;

	// AudioSource music;

	 AudioSource Audio3;
    AudioSource BgMusic3;

	public GameObject board;

	public static int soundstate=-1;  //-1 初始、1
		
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		// _instance=this;
		//3r4
		// board=gameObject.Find("board");
		var music=GameObject.Find("BgMusic").GetComponents(typeof(AudioSource));
		Audio3=(AudioSource)music[0];
		BgMusic3=(AudioSource)music[1];
		returnBtn=canvasTransform.Find("Return").GetComponent<Button>();
		closeBoard=canvasTransform.Find("board/Panel").GetComponent<Button>();
		soundbtn=canvasTransform.Find("music").GetComponent<Button>();

		// textdiv=GameObject.Find("textdiv");
	}
	// Use this for initialization

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (!Audio3.isPlaying&&BgMusic3.isPlaying){
			BgMusic3.Stop();
			GameObject.Find("music/ball/pause").SetActive(false);
			GameObject.Find("music/ball/play").SetActive(false);
			GameObject.Find("music/ball/again").SetActive(true);
			soundstate=2;
			Debug.Log("放完了");
		}
	}
	void Start () {
		// if (soundstate)
		// {
		// 	GameObject.Find("music/ball/pause").SetActive(true);
		// 	GameObject.Find("music/ball/play").SetActive(false);
		// 	GameObject.Find("music/ball/again").SetActive(false);
		// }else
		// {
		// 	GameObject.Find("music/ball/mute").SetActive(true);
		// 	GameObject.Find("music/ball/spaker").SetActive(false);
			
		// }

		// textdiv.SetActive(false);
		returnBtn.onClick.AddListener(ReturnBtnClick);
		closeBoard.onClick.AddListener(closeclick);
		soundbtn.onClick.AddListener(soundclick);
	}

	void ReturnBtnClick(){
		SceneManager.LoadScene("UI");
	}

	void soundclick(){
		if (soundstate==1)
		{
			Audio3.Pause();
			BgMusic3.Pause();
			GameObject.Find("music/ball/pause").SetActive(false);
			GameObject.Find("music/ball/play").SetActive(true);
			soundstate=0;
		}else if(soundstate==0)
		{
			Audio3.Play();
			BgMusic3.Play();
			GameObject.Find("music/ball/play").SetActive(false);
			GameObject.Find("music/ball/pause").SetActive(true);
			soundstate=1;
		}else if(soundstate==2)
		{
			Audio3.Play();
			BgMusic3.Play();
			
			GameObject.Find("music/ball/play").SetActive(false);
			GameObject.Find("music/ball/pause").SetActive(true);
			GameObject.Find("music/ball/again").SetActive(false);
			soundstate=1;
		}
		{
			
		}
	}
	public static void get_img(){
		soundstate=1;
		GameObject.Find("music/ball/again").SetActive(false);
		GameObject.Find("music/ball/play").SetActive(false);
		GameObject.Find("music/ball/pause").SetActive(true);
	}
	public static void lost_img(){
		GameObject.Find("music/ball/again").SetActive(false);
		GameObject.Find("music/ball/pause").SetActive(false);
		GameObject.Find("music/ball/play").SetActive(true);
	}
	void closeclick(){
		Debug.Log("关闭面板");
		board.SetActive(false);
		// if(textdiv.activeSelf){
		// 	textdiv.SetActive(false);
		// 	Debug.Log("t");
		// }
		// else
		// {
		// 	textdiv.SetActive(true);
		// 	Debug.Log("f");
		// }
	}
}
