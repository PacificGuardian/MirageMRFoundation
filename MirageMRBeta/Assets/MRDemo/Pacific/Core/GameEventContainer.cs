
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void StageChange();

public class GameEventContainer : MonoBehaviour{
private bool TimeEnded = false;
public static event StageChange start;
public static event StageChange stageChange;
public static event StageChange end;
public static event StageChange TimeEnd;
public static int QuestionNumber = -1;
public static int TotalQs = -1;
public static QuestionsBase QuestionsCFG;
private static void OnStageChange(){
    if(QuestionsCFG != null)
    {
        TotalQs = QuestionsCFG.QNA.Length;
        if(TotalQs != -1)
        {
        if(QuestionNumber == -1)
        start.Invoke();
        else if(QuestionNumber < TotalQs)
        stageChange.Invoke();
        else
        end.Invoke();
        if(QuestionNumber < TotalQs)
        QuestionNumber++;
        }
    }

}
public static void AdvanceStage(){
    OnStageChange();
    if(QuestionNumber <= TotalQs)
    Debug.Log("Question now advanced to " + QuestionNumber + " out of " + TotalQs);
}

private void Update(){
    if(TimerBar.tmrt <= 0 && !TimeEnded){
        TimeEnd.Invoke();
        TimeEnded = true;
    }
}

}