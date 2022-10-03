using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestionContainer : MonoBehaviour
{
    [HideInInspector]
    public string Question;
    public string CorrectAnswer;
    public int Deviation = 0;
    private int CorrectAnswerID (int Amount){
        return Random.Range(0,Amount);
    }
    public string[] Answers(int Amount, string type){
        if(type == "String")
        return GenerateStrings(Amount);
        else if(type == "Number")
        return GenerateNumbers(Amount);
        else
        {
            Debug.Log("Invalid Answer Type");
        return null;
        }
    }

    private string[] GenerateNumbers(int Amount){
        List<string> temp = new();
        int tempint = 0 ;
        for(int i = 0; i < Amount; i ++){
            //Debug.Log("Attempting to parse " + CorrectAnswer);
            tempint = int.Parse(CorrectAnswer);
            while(temp.Count < i + 1){
                tempint += Random.Range(-Deviation, Deviation);
                if(!temp.Contains(tempint.ToString()))
                    if(tempint.ToString() != CorrectAnswer)
                        temp.Add(tempint.ToString());
            }
        }
        temp[CorrectAnswerID(Amount)] = CorrectAnswer;
        return temp.ToArray();
    }

    private string[] GenerateStrings(int Amount){
    List<int> seed = new List<int>();
    List<string> export = new List<string>();
    string[] Wordlist = GetComponent<WordContainer>().Words;
    int tempint = 0;
    for(int i = 0; i < Amount; i++){
        while(seed.Count < i + 1){
            tempint = Random.Range(0,Wordlist.Length);
            if(!seed.Contains(tempint))
                seed.Add(tempint);
        }
        export.Add(Wordlist[seed[i]]);
    }
    export[CorrectAnswerID(Amount)] = CorrectAnswer;
    return export.ToArray();
    }
    
}
