using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MySingle<ObjectPool>
{
    private Dictionary<string, List<GameObject>> _objectDict;
    private Dictionary<string, GameObject> _prefabDict;
    /// <summary>
    /// 对象池
    /// </summary>

    private Dictionary<string, List<GameObject>> ObjectDic
    {
        get
        {

            if (_objectDict == null) _objectDict = new Dictionary<string, List<GameObject>>();
            return _objectDict;
        }
        set
        {
            _objectDict = value;
        }

    }

    /// <summary>

    /// 预设体字典

    /// </summary>

    private Dictionary<string, GameObject> PrefabDict

    {

        get

        {

            if (_prefabDict == null) _prefabDict = new Dictionary<string, GameObject>();

            return _prefabDict;

        }

        set

        {

            _prefabDict = value;

        }

    }

    /// <summary>

    /// 记录预设体字典

    /// </summary>

    /// <param name="obj"></param>

    public void SetPrefab(GameObject obj)
    {
        if (PrefabDict.ContainsKey(obj.name)) return;
        PrefabDict.Add(obj.name, obj);
    }

    /// <summary>

    /// 从对象池中获取对象

    /// </summary>

    /// <param name="objName"></param>

    /// <returns></returns>

    public GameObject GetObject(string objName, Transform parent = null)

    {

        if (parent == null) parent = transform;

        GameObject result = null;

        if (ObjectDic.ContainsKey(objName))

        {

            if (ObjectDic[objName].Count > 0)

            {

                result = ObjectDic[objName][0];

                result.transform.SetParent(parent);

                ObjectDic[objName].RemoveAt(0);

                return result;

            }

        }

        GameObject prefab = null;

        if (PrefabDict.ContainsKey(objName))
        {
            prefab = PrefabDict[objName];
        }

        else return null;
        result = Instantiate(prefab, parent);

        result.name = objName;

        return result;

    }

    /// <summary>

    /// 回收对象到对象池

    /// </summary>

    /// <param name="objName"></param>

    public void RecycleObj(GameObject obj, Transform parent = null)

    {
        if (parent == null) parent = transform;

        obj.SetActive(false);

        obj.transform.SetParent(parent);

        if (ObjectDic.ContainsKey(obj.name))
        {
            ObjectDic[obj.name].Add(obj);
        }
        else
        {
            ObjectDic.Add(obj.name, new List<GameObject>() { obj });

        }

    }

    /// <summary>

    /// 回收对象到对象池

    /// delay为0时，协程回收UI可能存在更新不及时的闪烁

    /// </summary>

    /// <param name="objName"></param>

    public void RecycleObjDelay(GameObject obj, float delay = 0, Transform parent = null)
    {
        StartCoroutine(RecycleObj(obj, delay, parent));
    }

    private IEnumerator RecycleObj(GameObject obj, float delay = 0, Transform parent = null)

    {

        yield return new WaitForSeconds(delay);

        if (parent == null) parent = transform;

        obj.SetActive(false);

        obj.transform.SetParent(parent);

        if (ObjectDic.ContainsKey(obj.name))

        {

            ObjectDic[obj.name].Add(obj);

        }

        else

        {

            ObjectDic.Add(obj.name, new List<GameObject>() { obj });

        }

    }
}
