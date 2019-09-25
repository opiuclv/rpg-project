using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadTXT : MonoBehaviour {


    private string Mytxt;       //用来存放文本内容

    // Use this for initialization
    void Start()
    {
        Mytxt = ReadFile("C:\\Users\\Eniac\\Desktop\\Data.txt", 1);  //txt文件的绝对路径
        print(Mytxt);           //输出验证

    }
    // Update is called once per frame
    void Update()
    {
    }

    //按路径读取txt文本的内容，第一个参数是路径名，第二个参数是第几行，返回值是sring[]数组
    string ReadFile(string PathName, int linenumber)
    {
        string[] strs = File.ReadAllLines(PathName);//读取txt文本的内容，返回sring数组的元素是每行内容
        if (linenumber == 0)
        {
            return "";
        }
        else
        {
            return strs[linenumber - 1];   //返回第linenumber行内容
        }
    }
}
