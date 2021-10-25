using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoad : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingBar;
    public TMP_Text loadingText;
	public GameObject uiPostProcessing;
	
	void Start()
	{
		loadingPanel.SetActive(false);
		if(uiPostProcessing != null)uiPostProcessing.SetActive(false);
	}
	
	public void LoadLevel (string levelName)
    {
        StartCoroutine(LoadSceneAsync(levelName));
    }

    IEnumerator LoadSceneAsync (string levelName )
    {
        loadingPanel.SetActive(true);
		if(uiPostProcessing != null)uiPostProcessing.SetActive(true);
		
		AsyncOperation op = SceneManager.LoadSceneAsync(levelName);

        while ( !op.isDone )
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            
            loadingBar.value = progress;
            loadingText.text = progress * 100f + "%";

            yield return null;
        }
    }
	
	
}
