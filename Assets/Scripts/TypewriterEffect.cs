using UnityEngine;
using System.Collections;

public class TypewriterEffect : MonoBehaviour

    // ********************************************************這支script目前沒用到，但是這是是打字機效果的code原型，待之後有需要時使用
{
    /// <summary>
    /// 間隔時間
    /// </summary>
    private float letterPause = 0.2f;

    public AudioClip clip;

    private AudioSource source;
    /// <summary>
    /// 暫存中間值
    /// </summary>
    private string word;
    /// <summary>
    /// 要顯示的內容
    /// </summary>
    private LoadLeadStory theLS;
    private string text;

    void Start()
    {
        theLS = FindObjectOfType<LoadLeadStory>();
        text = theLS.dText.text ;
        source = GetComponent<AudioSource>();
        word = text;
        text = "";
        StartCoroutine(TypeText());
    }

    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Space) )
        {
            text = theLS.dText.text ;
            word = text;
            text = "";
            StartCoroutine(TypeText());
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 200, 200), "序章");
        GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 200), text);
    }

    /// <summary>
    /// 打字機效果
    /// </summary>
    /// <returns></returns>
    private IEnumerator TypeText()
    {
        foreach (char letter in word.ToCharArray())
        {
            text += letter;
            if(clip )
            {
                source.PlayOneShot(clip);
            }
            yield return new WaitForSeconds(letterPause);
        }
    }
}