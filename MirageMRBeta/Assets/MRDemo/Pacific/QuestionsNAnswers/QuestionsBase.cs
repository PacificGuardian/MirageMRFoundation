using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "QuestionPreset", menuName = "JungleCfg/QuestionsCfg", order = 2)]
public class QuestionsBase : ScriptableObject
{
    [System.Serializable]
    public struct QuestionTemplate{
        public string Question;
        public string Answer;
        //Type of question for randomised wrong anwsers if i can do that in time
        //done
        [Tooltip("Either input (String) or (Number))")]public string Type;
        //Amount of books which will spawn 
        public int TotalCount;
    }
    public QuestionTemplate[] QNA;
}
