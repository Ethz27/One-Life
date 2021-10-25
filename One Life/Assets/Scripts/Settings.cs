using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public RenderPipelineAsset[] qualityLevels;
	public TMP_Dropdown dropdown;
	
	public Slider volumeSlider;
	
	// Start is called before the first frame update
    void Start()
    {
        dropdown.value = QualitySettings.GetQualityLevel();
		
		if(PlayerPrefs.HasKey("VOL")) 
		{
			volumeSlider.value = PlayerPrefs.GetFloat("VOL");
		}
		else
		{
			PlayerPrefs.SetFloat("VOL", 1f);
			volumeSlider.value = PlayerPrefs.GetFloat("VOL");
		}
		
		if(PlayerPrefs.HasKey("Quality")) 
		{
			dropdown.value = PlayerPrefs.GetInt("Quality");
		}
		else
		{
			PlayerPrefs.SetInt("Quality", 0);
			dropdown.value = PlayerPrefs.GetInt("Quality");
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void ChangeLevel (int value)
	{
		QualitySettings.SetQualityLevel(value);
		QualitySettings.renderPipeline = qualityLevels[value];
		PlayerPrefs.SetInt("Quality", value);
	}
	
	public void ChangeVolume()
	{
		AudioListener.volume = volumeSlider.value;
		PlayerPrefs.SetFloat("VOL", volumeSlider.value);
	}
	
	public void Quit()
	{
		Application.Quit();
	}
	
	public void OpenLink()
	{
		Application.OpenURL("https://www.youtube.com/channel/UCtbUHwVj7BFUH4ymg2gTv1w");
	}
	
}
