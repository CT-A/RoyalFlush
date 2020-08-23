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

    //remember to set this when spawning enemies
    public int numEnemies;
    // Start is called before the first frame update
    void Start()
    {
        //change this when spawning is done
        numEnemies = 8;
        DontDestroyOnLoad(this.gameObject);
    }

    public void drop(Vector3 pos)
    {
        dh.drop(pos);
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
            newScene(1);
        }
        else
        {
            saveOnLoad = true;
            newScene(1);
        }
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
            //Debug.Log(SceneManager.GetActiveScene().buildIndex);
            //initialize player
            pc = GameObject.FindWithTag("Player").GetComponent<PlayerControlls>();
            if (saveOnLoad) SaveSystem.SavePlayer(pc);
            else
            {
                PlayerData data = SaveSystem.LoadPlayer();
                pc.LoadFromSave(data);
            }
        }
    }

}
