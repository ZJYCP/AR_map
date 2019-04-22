using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

	// Use this for initialization

    GameObject HelpBoard;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        HelpBoard=GameObject.Find("Canvas1/HelpBoard");
        HelpBoard.SetActive(false);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //当点击“开始游戏”，进入游戏场景
    public void StartButtonClick()
    {
        //audio.clip = mov.audioClip;
        //audio.Play();
        //加载游戏场景
        SceneManager.LoadScene("Load");
    }

    //当点击“游戏规则”，出现游戏规则框
    public void ShowHelpBoard()
    {

        HelpBoard.SetActive(true);
    }
    // 关闭关注
    public void CloseHelpBorad(){
        HelpBoard.SetActive(false);
    }
    //当点击“退出游戏”，退出游戏场景（导出才能看得见）
    public void TuiButtonClick()
    {
        Application.Quit();
    }

}
