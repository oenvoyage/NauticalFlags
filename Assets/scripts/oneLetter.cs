using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class oneLetter : MonoBehaviour
{
    public int index;
    public Animator anim;
    public TextMesh hint;

    public GameObject[] alphabet;

    private bool isHintShown;

    // Start is called before the first frame update
    void Start()
    {
        isHintShown = false;
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void startDestroying()
    {

        // Debug.Log("starting kill himself " + gameObject.name);

        StartCoroutine("killHimself"); 
        
    }

    private IEnumerator killHimself()
    {
        yield return new WaitForSeconds(index * 0.1f);
        anim.SetTrigger("outro");
        Destroy(gameObject,0.5f);
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse Touched : " + gameObject.name);
        toggleHint();
    }

    public void toggleHint()
    {
        if (isHintShown == true)
        {
            hideHint();
        }
        else
        {
           showHint();
        }
    }
    public void hideHint()
    {
        isHintShown = false;
        anim.SetTrigger("hideHint");

    }


    public void showHint()
    {
        isHintShown = true;
        anim.SetTrigger("showHint");

    }





    public void setLetter(char letter)
    {
        hint.text = letter.ToString();
        // Debug.Log("searching for " + letter.ToString());
        foreach (GameObject ob in alphabet)
        {
            // Debug.Log(ob.name);
            if (ob.name == letter.ToString())
            {
                // Debug.Log("FOUND");
                ob.SetActive(true);

            }
            else
            {
                ob.SetActive(false);
            }
        }
    }
    
}
