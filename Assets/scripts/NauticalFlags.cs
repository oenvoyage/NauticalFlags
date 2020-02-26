using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NauticalFlags : MonoBehaviour
{
    public Transform letterSpawner;
    public GameObject letterPrefab;
    public Animator cameraAnimator;
    public Button newWordButton;
    public Button showAnswerButton;


    public Toggle vocabularyAlphabet;
    public Toggle vocabularyEnglish;
    public Toggle vocabularyFrench;

    public string[] words;


    public string word;
    public string previousWord;
    private string wordList;

    // Start is called before the first frame update
    void Start()
    {


        // wordList.text = "beta,racingff,verylongtettesttest";
        readPreferences();
        generateWordList();
        newWord();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private string getRandomWord()
    {
        int rand = Random.Range(0, words.Length);
        string newWord = words[rand];

        while (newWord == previousWord)   // if word is same as previous we do it one more time
        {
            Debug.Log("oops same word as before");
            rand = Random.Range(0, words.Length);
            newWord = words[rand];
        }
        previousWord = newWord;

        Debug.Log("Hidden word  = " + newWord);
        return newWord;
    }

    public void generateWordList()
    {
        wordList = "a";
        /*        "beta,racing,test,boat,sailing,marina,ouchy,bateau,maison,lausanne,blender,nautique,blob,cinema,redbull,avion,glenans," +
            "olivier,lausanne,ouchy,macbook,helice,danger,natation,plage,mer,tilt,golf,house,home,mat,voile,navire,navy,yankee,whisky,zulu,tanguy," +
            "bravo,minotaure,zelda,zingaro,race,stop,more,less,mouth,hand,main,geneve,montreux,vidy,ordinateur,mecanique,technique,artistique,apprentissage," +
            "alphabet,bretagne,letters,help,fun,sos,bus,car,eau,camping,orange,punk,funk,vert,bleu,drapeau,livre,aeroport,port,mer,bretagne,nc,phoque," +
            "chat,chien,camera,ph,ch"
            */

        if (vocabularyAlphabet.GetComponent<Toggle>().isOn == true)
        {
            wordList += ",b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";

        }
        if (vocabularyEnglish.GetComponent<Toggle>().isOn == true)
        {
            wordList += ",beta,racing,test,boat,sailing,marina,blob,cinema,redbull,airplane," +
            "macbook,helicopter,danger,swimming,beach,sea,tilt,golf,house,home,material,sailing,navy,yankee,whisky,zulu,tanguy," +
            "bravo,zelda,zingaro,race,stop,more,less,mouth,hand,main,mecanical,technical,beauty,learning," +
            "alphabet,letters,help,fun,sos,bus,car,water,camping,orange,punk,funk,sail,livre,aiport,port,starboard,nc,antarctica," +
            "dog,cat,camera,ph,ch";

        }
        if (vocabularyFrench.GetComponent<Toggle>().isOn == true)
        {
            wordList += ",ouchy,bateau,maison,lausanne,blender,nautique,blob,cinema,redbull,avion,glenans," +
            "olivier,lausanne,ouchy,helice,danger,natation,plage,mer,voilier,voile,navire," +
            "hand,main,geneve,montreux,vidy,ordinateur,mecanique,technique,artistique,apprentissage," +
            "alphabet,bretagne,help,fun,sos,bus,car,eau,vert,bleu,drapeau,livre,aeroport,port,mer,bretagne,nc,phoque," +
            "chat,chien,photographie";

        }

        // we check if wordlist is empty
        if (wordList.Length ==1)
        {
            wordList = "nautical,alphabet";
        }

        words = wordList.Split(',');

    }
    public void newWord()
    {
        removeWord();
        word = getRandomWord();
        StartCoroutine(generateWord(word));
        if (word.Length < 2)
        {
            cameraAnimator.SetTrigger("macro");
        }
        else if (word.Length < 6)
        {
            cameraAnimator.SetTrigger("zoom");
        }
        else if (word.Length > 9)
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

    public void savePreferences()
    {
        PlayerPrefsX.SetBool("VocAlpha", vocabularyAlphabet.isOn);
        PlayerPrefsX.SetBool("VocFrench", vocabularyFrench.isOn);
        PlayerPrefsX.SetBool("VocEnglish", vocabularyEnglish.isOn);
    }

    public void readPreferences()
    {
        vocabularyAlphabet.isOn = PlayerPrefsX.GetBool("VocAlpha", vocabularyAlphabet.isOn);
        vocabularyFrench.isOn =  PlayerPrefsX.GetBool("VocFrench", vocabularyFrench.isOn);
        vocabularyEnglish.isOn = PlayerPrefsX.GetBool("VocEnglish", vocabularyEnglish.isOn);
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

        // reactivate the New Word button at tend of animation.
        newWordButton.interactable = true;
        showAnswerButton.interactable = true;

    }
}
