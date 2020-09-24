using UnityEngine;
using UnityEngine.UI;

public class DetailsManager : MonoBehaviour
{
    public LevelScriptableObject levelScriptableObject;
    [SerializeField] private Text levelName;

    private bool seeDetails;

    public bool SeeDetails
    {
        get => seeDetails;
        set => seeDetails = value;
    }
    
    public void Update()
    {
        if(levelScriptableObject && seeDetails)
        {
            seeDetails = false;
            levelName.text = levelScriptableObject.levelName;
        }
    }

    /// <summary>
    /// PlayBtn in Details
    /// </summary>
    /// <param name="sceneLoader"></param>
    public void Play(SceneLoader sceneLoader)
    {
        if (sceneLoader)
        {
            sceneLoader.LoadScene(levelScriptableObject.sceneName);
        }
    }
}
