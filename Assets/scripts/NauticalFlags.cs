using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NauticalFlags : MonoBehaviour
{
    public Transform letterSpawner;
    public GameObject letterPrefab;
    public Animator cameraAnimator;
    public InputField wordList;
    public string[] words;

    private string word;

    // Start is called before the first frame update
    void Start()
    {

        word = "alphabet";

        wordList.text = "beta,racing,test,boat,sailing,marina,ouchy,bateau,maison,lausanne,blender,nautique,blob,cinema,redbull,avion,glenans," +
            "olivier,lausanne,ouchy,macbook,helice,danger,natation,plage,mer,tilt,golf,house,home,mat,voile,navire,navy,yankee,whisky,zulu,tanguy," +
            "bravo,minotaure,zelda,zingaro,race,stop,more,less,mouth,hand,main,geneve,montreux,vidy,ordinateur,mecanique,technique,artistique,apprentissage," +
            "alphabet,bretagne,letters,help,fun,sos,bus,car,eau,camping,orange,punk,funk,vert,bleu,drapeau,livre,aeroport,port,mer,bretagne,nc,phoque," +
            "chat,chien,camera,ph,ch";

       // wordList.text = "beta,racingff,verylongtettesttest";

        newWord();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private string getRandomWord()
    {

       words = wordList.text.Split(',');


        int rand = Random.Range(0, words.Length);
        string newWord = words[rand];

        Debug.Log("Hidden word  = " + newWord);
        return newWord;
    }

    public void newWord()
    {
        removeWord();
        word = getRandomWord();
        StartCoroutine(generateWord(word));
        if (word.Length <6)
        {
            cameraAnimator.SetTrigger("zoom");
        }
        else if (word.Length > 8)
        {
            cameraAnimator.SetTrigger("wide");
        }
        else
        {
            cameraAnimator.SetTrigger("normal");

        }
    }

    public void showAnswer()
    {
        foreach (Transform child in letterSpawner.transform)
        {
            // Debug.Log(child.gameObject.name);
            child.gameObject.GetComponent<oneLetter>().showHint();
        }
    }

    public void removeWord()
    {
        // foreach (Transform child in letterSpawner.GetComponentsInChildren<Transform>())
        foreach (Transform child in letterSpawner.transform)
        {
            // Debug.Log(child.gameObject.name);
            child.gameObject.GetComponent<oneLetter>().startDestroying();
        }
    }


    public IEnumerator generateWord(string word = "nothing")
    {
        // Debug.Log("I am now shuffling to this word : " + word);
        word = word.ToUpper();
        int index = 0;
        foreach (var letter in word)
        {
            index += 1;
            // Debug.Log("spawning : " + letter.ToString());
            GameObject newLetter = Instantiate(letterPrefab,letterSpawner);

            newLetter.GetComponent<oneLetter>().setLetter(letter);
            newLetter.GetComponent<oneLetter>().index = index;
            newLetter.name = letter.ToString();
            newLetter.transform.position = newLetter.transform.position + new Vector3(0.0f, -2.5f * index, 0.0f);
            yield return new WaitForSeconds(0.2f);

        }
    }
}
