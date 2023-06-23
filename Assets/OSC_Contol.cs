using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSC_Contol : MonoBehaviour
{



    public UnityEngine.UI.Text maintext;


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


    public AudioSource source;
    public AudioClip clip;

    public Spawn spawn;


    public GameObject player;

    [Header("mapping object")]
    public GameObject windObject;
    public GameObject windObjectGhost;
    public GameObject drillObject;
    public GameObject tempObject;
    public GameObject keyObject;
    public GameObject touchObject;

    [Header("box with button")]
    public GameObject boxWithButton;


    [Header("mapping sound")]
    public AudioSource windAudio;
    public AudioSource fireAudio;
    public AudioSource touchAudio;

    // switch on/off distraction integration
    // can also toggle through editor for testing
    [Header("mapping control")]
    public bool wind;
    public bool temp;
    public bool touch;
    public bool key;
    public bool drill;

    [Header("door control")]
    public GameObject doorObject;
    public GameObject doorObjectGhost;


    [Header("key control")]
    public bool keyBox;

    public bool door;


    private bool wind_first = false;

    private bool last_drill = false;
    private bool last_wind = false;
    private bool last_temp = false;
    private bool last_key = false;
    private bool last_touch = false;

    private bool last_door = false;
    private bool last_keyBox;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // upon receive sensor result, turn on/off the sensor integration
    

    }

    // Update is called once per frame
    void Update()
    {

        if (key == true)
        {
            SetkeyActive();
        }

        if (key == false)
        {
            SetkeyInactive();
        }

        if (drill == true && last_drill == false)
        {
            SetdrillActive();
            last_drill = true;
        }
        if (drill == false && last_drill == true)
        {
            SetdrillInactive();
            last_drill = false;
        }

        if (temp == true && last_temp == false)
        {
            SetTempActive();
            fireAudio.Play();

            last_temp = true;
        }

        if (temp == false && last_temp == true)
        {
            SetTempInactive();
            fireAudio.Stop();

            last_temp = false;
        }

        if (wind == true)
        {
            SetWindActive();
            windAudio.Play();
           

            last_wind = true;

        }

        if (wind == false)
        {
            SetWindInactive();
            windAudio.Stop();

            last_wind = false;

        }

        if (touch == true && last_touch == false)
        {
            SetTouchActive();
            // touchAudio.Play();

            last_touch = true;
        }

        if (touch == false && last_touch == true)
        {
            SetTouchInactive();
            // touchAudio.Stop();

            last_touch = false;

        }

        if (door == true && last_door == false)
        {
            SetDoorActive();
            last_door = true;
        }

        if (door == false && last_door == true)
        {
            SetDoorInactive();
            last_door = false;
        }

        if (keyBox == true && last_keyBox == false)
        {
            SetKeyboxActive();
            last_keyBox = true;
        }

        if (keyBox == false && last_keyBox == true)
        {
            SetKeyboxInctive();
            last_keyBox = false;
        }

    }


    void SetdrillActive()
    {
        drillObject.SetActive(true);
    }

    void SetdrillInactive()
    {
        drillObject.SetActive(false);
    }

    void SetkeyActive()
    {
        keyObject.SetActive(true);
    }

    void SetkeyInactive()
    {
        keyObject.SetActive(false);
    }

    void SetWindActive()
    {

        area = GetComponent<BoxCollider>();

        for (int i = 0; i < count; ++i)//count 수 만큼 생성한다
        {
            spawns();//생성 + 스폰위치를 포함하는 함수
        }

        area.enabled = false;

    }

    void SetWindInactive()
    {
        if (wind_first == false)
        {
           
        }
        else
        {
         
        }

    }

    void SetTempActive()
    {
        tempObject.SetActive(true);
    }

    void SetTempInactive()
    {
        tempObject.SetActive(false);
    }

    void SetTouchActive()
    {
        touchObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.167f, player.transform.position.z);
        // touchObject.SetActive(true);
        // touchAudio.Play();

        StartCoroutine(TouchWaiter());
    }

    void SetTouchInactive()
    {
        touchObject.SetActive(false);
        touchAudio.Stop();
    }

    void SetDoorActive()
    {
        doorObject.SetActive(true);
        doorObject.GetComponent<Animator>().enabled = true;
        doorObjectGhost.SetActive(false);

    }

    void SetDoorInactive()
    {
        doorObject.SetActive(false);
        doorObjectGhost.SetActive(true);
    }

    void SetKeyboxActive()
    {
        boxWithButton.SetActive(true);
    }

    void SetKeyboxInctive()
    {
        boxWithButton.SetActive(false);
    }

    IEnumerator TouchWaiter()
    {

        touchObject.SetActive(true);
        touchAudio.Play();

        //Wait for 2 seconds
        yield return new WaitForSeconds(2);

        touchObject.SetActive(false);
    }

    // Handle OSC communication to talk to sensors
    // Dont't revise this part
    void OnReceiveKey(OscMessage message)
    {
        float state = message.GetInt(0);

        if (state == 0)
        {
            key = false;
        }

        if (state == 1)
        {
            key = true;
        }
    }

    void OnReceiveDrill(OscMessage message)
    {
        float state = message.GetInt(0);

        if (state == 0)
        {
            drill = false;
        }

        if (state == 1)
        {
            drill = true;
        }
    }

    void OnReceiveWind(OscMessage message)
    {
        float state = message.GetInt(0);

        if (state == 0)
        {
            wind = false;
        }

        if (state == 1)
        {
            wind = true;
        }
    }

    void OnReceiveTemp(OscMessage message)
    {
        float state = message.GetInt(0);

        if (state == 0)
        {
            temp = false;
        }

        if (state == 1)
        {
            temp = true;
        }
    }

    void OnReceiveTouch(OscMessage message)
    {
        float state = message.GetInt(0);

        if (state == 0)
        {
            touch = false;
        }

        if (state == 1)
        {
            touch = true;
        }
    }

}
