using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Imsg
{
    ///// <summary>
    ///// ∑¢ÀÕ»Œ“‚
    ///// </summary>
    ///// <param name="data"></param>
    //void SendMsg(MyCommand myCommand ,object data);
    void RecieveMsg(string command,object data);
    void Bind();
    void RemoveBind();

}

public class MyMsgCenter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
