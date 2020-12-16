using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMole : MonoBehaviour
{
    public static SpawnMole instance;
    public GameObject Mole;
    public Transform spawnPoints;
    public Transform player;
    public List<Vector3> spawnPointList = new List<Vector3>();
    public List<GameObject> MoleRespawns = new List<GameObject>();

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        foreach (Transform item in spawnPoints)
        {
            spawnPointList.Add(item.localPosition);
        }
        InvokeRepeating("GetMole", 1f, 2f);
    }

    void Update()
    {
        
    }

    void GetMole(){
        if(spawnPointList.Count > 0){
            Vector3 randPos = spawnPointList[Random.Range(0, spawnPointList.Count)];
            spawnPointList.Remove(randPos);
            GameObject currentMole = Instantiate(Mole, randPos, Quaternion.identity, transform);
            currentMole.transform.LookAt(new Vector3(player.position.x, currentMole.transform.position.y, player.position.z));
            currentMole.SetActive(true);
            MoleRespawns.Add(currentMole);
        }
    }

    public void DestroyMole(GameObject mole){
        spawnPointList.Add(mole.transform.localPosition);
        MoleRespawns.Remove(mole);
        Destroy(mole);
    }
}
