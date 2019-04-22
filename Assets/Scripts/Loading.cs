using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;


public class Loading : MonoBehaviour
{

    AsyncOperation async;
    public Slider m_pProgress;//滑动条
    private int progress = 0;
    //public Text proText;//数值文本
    // private float loadingSpeed = 1;
    // private float targetvalue;

    void Start()
    {
        // m_pProgress.value=0.0f;
        StartCoroutine(LoadScenes());
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    // void Update()
    // {
    //     targetvalue = async.progress;

    //     if (async.progress >= 0.9f)
    //     {
    //         targetvalue = 1.0f;
    //     }
    //     if (targetvalue != m_pProgress.value)
    //     {

    //         m_pProgress.value = Mathf.Lerp(m_pProgress.value, targetvalue, Time.deltaTime * loadingSpeed);
    //         if (Mathf.Abs(m_pProgress.value - targetvalue) < 0.02f)
    //         {
    //             m_pProgress.value = targetvalue;

    //         }
    //     }

    //     if((int)(m_pProgress.value*100)==100){
    //         async.allowSceneActivation = true;
    //     }
    // }
    IEnumerator LoadScenes()
    {
        int nDisPlayProgress = 0;
        async = SceneManager.LoadSceneAsync("arscene");//更换要加载的场景名字！！！！！！！！！！
        async.allowSceneActivation = false;
        // yield return async;

        while (async.progress < 0.9f)
        {
            progress = (int)async.progress * 100;
            while (nDisPlayProgress < progress)
            {
                ++nDisPlayProgress;
                m_pProgress.value = (float)nDisPlayProgress / 100;
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
        progress = 100;
        while (nDisPlayProgress < progress)
        {
            ++nDisPlayProgress;
            //    proText.text = "Loading " + nDisPlayProgress + "%";//实时更新进度百分比的文本显示
            m_pProgress.value = (float)nDisPlayProgress / 100;
            yield return new WaitForEndOfFrame();
        }
        async.allowSceneActivation = true;

    }
}
