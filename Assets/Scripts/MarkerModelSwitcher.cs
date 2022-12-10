using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerModelSwitcher : MonoBehaviour
{
    [SerializeField]
    private ARObjectManager aRObjectManager;

    private int _pageNum = 0;

    public GameObject _arObj { get; set; }

    public void SwitchingObject(int pageNum, Vector3 pos, Quaternion rot)
    {
        if (_arObj != null) _arObj.SetActive(false);

        this._pageNum = pageNum;
        _arObj = aRObjectManager.GetArObjByNum(_pageNum);
        _arObj.SetActive(true);
        _arObj.transform.position = pos;
        _arObj.transform.rotation = rot;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
