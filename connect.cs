using UnityEngine;
using System.Collections;

public class connect : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string strurl = "http://localhost/Test.php";
    // 用户的名字
    string playName = " ";
    // 用户的得分
    string playScores = " ";
    void OnGUI()
    {
        // 要插入的数据
        playName = GUI.TextField(new Rect(0, 0, 100, 40), playName, 25);
        playScores = GUI.TextField(new Rect(120, 0, 100, 40), playScores, 25);
        if (GUI.Button(new Rect(40, 100, 100, 40), "Sumbit"))
        {
            int scoresValue = 0;
            // 把string 类型的playScores 转换为 int 的 scoresValue
            if (int.TryParse(playScores, out scoresValue))
            {
                StartCoroutine(UploadData(playName, scoresValue));
            }
        }
    }
    IEnumerator UploadData(string strName, int scoresValue)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", strName);
        form.AddField("scores", scoresValue);
        WWW hspost = new WWW(strurl, form);
        yield return hspost;
        print(" UploadData== " + hspost.text);
    }
}
// Use this for initialization
