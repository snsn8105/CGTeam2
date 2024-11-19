using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 데이터 저장
// 1. 저장할 데이터가 존재해야함
// 2. 데이터를 제이슨으로 변환
// 3. 제이슨을 외부에 저장
// 데이터 불러오기
// 1. 외부에 저장된 제이슨 가져옴
// 2. 제이슨을 데이터 형태로 변환
// 3. 불러온 데이터를 사용

public class PlayerData
{
    // 이름, 
}
public class DataManager : MonoBehaviour
{
    //singleton
    public static DataManager instance;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
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
