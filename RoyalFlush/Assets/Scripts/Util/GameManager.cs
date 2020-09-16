using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerControlls pc;
    private bool saveOnLoad;
    public DropHandler dh;
    public TempAnimationHandler tah;
    public MapHandler mh;
    public ScoreAndShopHandler sash;
    public XPHandler xph;
    public int score;
    public List<GameObject> queuedItems;
    public bool startingLevel;
    public int cachedGold;
    //remember to set this when spawning enemies
    public int numEnemies;
    // Start is called before the first frame update
    void Start()
    {
        //change this when spawning is done
        numEnemies = 0;
        DontDestroyOnLoad(this.gameObject);
    }

    public void drop(Vector3 pos)
    {
        dh.drop(pos);
    }

    public void death(Vector3 pos, int n)
    {
        drop(pos);
        numEnemies -= 1;
        tah.explosion(pos);
        xph.Spawn(pos, n);
    }

    public void DeleteSave()
    {
        SaveSystem.DeleteSave();
    }

    public void StartGame()
    {
        if (SaveSystem.CheckSave())
        {
            saveOnLoad = false;
            startingLevel = true;
            newScene(1);
        }
        else
        {
            saveOnLoad = true;
            newScene(1);
        }
    }

    public void NextLevel()
    {
        StartGame();
    }
    public bool Buy(ShopItemButton item)
    {
        if (cachedGold > dh.possibleItemsDict[item.name].GetComponent<Item>().price)
        {
            cachedGold -= dh.possibleItemsDict[item.name].GetComponent<Item>().price;
            QueueItem(dh.possibleItemsDict[item.name]);
            return true;
        }
        return false;
    }
    public int GetGold()
    {
        return cachedGold;
    }
    public void QueueItem(GameObject item)
    {
        queuedItems.Add(item);
    }

    public void EndLevel()
    {
        pc.gold = pc.gold * (sash.Score(pc.i.hand) + 1);
        SaveSystem.SavePlayer(pc);
        cachedGold = pc.gold;
        newScene(2);
        //score = sash.Score(pc.i.hand);
    }

    public void newScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);

        if (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            StartCoroutine("waitForSceneLoad", sceneNumber);
        }
    }

    IEnumerator waitForSceneLoad(int sceneNumber)
    {
        while (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            yield return null;
        }

        // Do anything after proper scene has been loaded
        if (SceneManager.GetActiveScene().buildIndex == sceneNumber)
        {
            if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                //Debug.Log(SceneManager.GetActiveScene().buildIndex);
                //initialize player
                pc = GameObject.FindWithTag("Player").GetComponent<PlayerControlls>();
                if (saveOnLoad)
                {
                    mh.CreateNewMap();
                    pc.seed = new int[] { mh.tl, mh.tc, mh.tr, mh.ml, mh.mc, mh.mr, mh.bl, mh.bc, mh.br };
                    SaveSystem.SavePlayer(pc);
                    cachedGold = pc.gold;
                }
                else
                {
                    PlayerData data = SaveSystem.LoadPlayer();
                    if (startingLevel)
                        data.position = new float[3] { -10, -10, 0 };
                    pc.LoadFromSave(data);
                    mh.SetSeed(pc.seed[0], pc.seed[1], pc.seed[2], pc.seed[3], pc.seed[4], pc.seed[5], pc.seed[6], pc.seed[7], pc.seed[8]);
                    mh.CreateMap();
                    mh.SetAStarGrids();
                    foreach (GameObject item in queuedItems)
                    {
                        item.GetComponent<Item>().Pickup(pc);
                        pc.gold = cachedGold;
                    }
                    queuedItems = new List<GameObject>();
                    
                }
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                sash.Stock();
            }
        }
    }

}
