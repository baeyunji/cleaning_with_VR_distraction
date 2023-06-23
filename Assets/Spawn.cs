using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{


    [Header("mapping control")]
    public bool wind;
   

    public GameObject[] prefabs; //찍어낼 게임 오브젝트를 넣어요
                              
    private BoxCollider area;    //박스콜라이더의 사이즈를 가져오기 위함
    public int count = 100;      //찍어낼 게임 오브젝트 갯수

    private List<GameObject> gameObject = new List<GameObject>();

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }

     private void spawns()
    {
        int selection = Random.Range(0, prefabs.Length);
        
        GameObject selectedPrefab = prefabs[selection];
        
        Vector3 spawnPos = GetRandomPosition();//랜덤위치함수
        
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        gameObject.Add(instance);
    }


    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            area = GetComponent<BoxCollider>();

            for (int i = 0; i < count; ++i)//count 수 만큼 생성한다
            {
                spawns();//생성 + 스폰위치를 포함하는 함수
            }

            area.enabled = false;

        }

        if (wind == true)
        {
            area = GetComponent<BoxCollider>();

            for (int i = 0; i < count; ++i)//count 수 만큼 생성한다
            {
                spawns();//생성 + 스폰위치를 포함하는 함수
            }

            area.enabled = false;

            wind = false;

        }
    }


     
}
