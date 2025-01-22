using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject fuelPrefab;
    public Slider fuelSlider;
    public List<GameObject> RoadPrefab;
    public GameObject playerobj;
    public GameObject playUI;
    public GameObject endUI;
    public Player player;
    public float createTime = 3f;
    private float fallingSpeed = 8f;
    public float runningSpeed = 10f;
    private float startRoadPos = 30f;
    private float endRoadPos = -15f;
    
    private Fuel fuel;

    private bool _repeat = true;
    public bool bRepeat
    {
        get => _repeat;
        set
        {
            _repeat = value;
            if (_repeat)
            {
                StartCoroutine(RepeatCreate());
            }
            else
            {
                StopAllCoroutines();
                playUI.SetActive(false);
                endUI.SetActive(true);
            }
        }
    }

    void Awake()
    {
        player = playerobj.GetComponent<Player>();
        fuelSlider.maxValue = player.fuel;
        
    }

    void Start()
    {
        StartCoroutine(RepeatCreate());
    }

    void Update()
    {
        if (player.fuel < 0)
        {
            
            return;
        }
        fuelSlider.value = player.fuel;
        foreach (GameObject roadPrefab in RoadPrefab)
        {
            roadPrefab.transform.position += Vector3.back * runningSpeed * Time.deltaTime;
            if (roadPrefab.transform.position.z < endRoadPos)
            {
                roadPrefab.transform.position =
                    new Vector3(roadPrefab.transform.position.x, roadPrefab.transform.position.y, startRoadPos);
            }
        }
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator RepeatCreate()
    {
        if (player.fuel < 0)
        {
            bRepeat = false;
        }
        Vector3 createPos = new Vector3(Random.Range(-4f,4f),0 ,15f);
        var fuelObj = Instantiate(fuelPrefab, createPos, Quaternion.identity);
        fuel = fuelObj.GetComponent<Fuel>();
        fuel.fallingSpeed = fallingSpeed;
        
        yield return new WaitForSeconds(createTime);
        
        StartCoroutine(RepeatCreate());
    }
}
