using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(QuestionContainer))]
[RequireComponent(typeof(GameEventContainer))]
[RequireComponent(typeof(WordContainer))]
[RequireComponent(typeof(EventRegistry))]
[RequireComponent(typeof(Starting))]
[RequireComponent(typeof(Ending))]
public class Init : MonoBehaviour
{

    private static Init _singleton;
        public static Init Singleton
        {
            get => _singleton;
            private set
            {
                if (_singleton == null)
                    _singleton = value;
                else if (_singleton != value)
                {
                    Debug.Log($"{nameof(Init)} instance already exists, destroying object!");
                    Destroy(value);
                }
            }
        }
    public QuestionsBase QuestionsCFG;
    [SerializeField] GameObject Parent;
    [SerializeField] GameObject BookPrefab;
    [SerializeField] Transform Firept;
    [SerializeField] TextMeshProUGUI QText;
    public GameObject CamParent;
    public int FireAngleMod = -30;
    public float Delay = 0.2f;
    private GameObject[] BookList;
    private QuestionContainer container;
    public QuestionContainer Container => container;
    public GameObject PPar => Parent;
    public GameObject Rabbit;
    [SerializeField] 
    private GameObject[] Altar;
    public GameObject CurAltar;
    public EffectBase effBase;
    public float GameTime;
    [Tooltip("BGM is currently edited to fit 2 minutes")]
    public AudioClip BGM;
    [SerializeField]
    private float spawnDelay;
    public float time = 0;
    private void Awake(){
        Singleton = this;
    }
    private void Start(){
        GameEventContainer.QuestionsCFG = QuestionsCFG;
        container = GetComponent<QuestionContainer>();
        //Skip startup for debugging purposes
        GameEventContainer.AdvanceStage();
        
    }

    //Spawnbooks returns an array to keep track of current books
    //It also initiates the stages because theres no reason not to
    private GameObject[] SpawnBooks(QuestionsBase Base){
        int XMod = 0;
        List<GameObject> LocalHolder = new List<GameObject>();
        int QuestionNumber = GameEventContainer.QuestionNumber;
        int internalNum = QuestionNumber -= 1;
        int BookAmount = Base.QNA[internalNum].TotalCount;
        //Pushing variables to container
        container.Question = Base.QNA[internalNum].Question;
        container.CorrectAnswer = Base.QNA[internalNum].Answer;
        string[] tempans = container.Answers(BookAmount, Base.QNA[internalNum].Type);
        //float ShootMe = 0f;
        
        for(int i = 0; i < BookAmount; i++)
        {
            //Randomise base power and angle
            XMod = Random.Range(-10, 10);
            Firept.rotation = Quaternion.Euler(FireAngleMod + XMod, angle(), 0);

            GameObject Book = Instantiate(BookPrefab, Firept.position, Firept.rotation);
            Book.transform.SetParent(Parent.transform);
            Rigidbody tempforce = Book.GetComponent<Rigidbody>();


            if(tempforce == null)
            tempforce = Book.AddComponent<Rigidbody>();


            tempforce.AddForce(transform.forward * power(), ForceMode.Impulse);
            Book.GetComponentInChildren<TextMeshProUGUI>().text = tempans[i];
            LocalHolder.Add(Book);
            //Answers
            AnswersContainer anscon = Book.AddComponent<AnswersContainer>();
            anscon.Answer = tempans[i];
            if(anscon.Answer == container.CorrectAnswer)
            anscon.Correct = true;
            //ShootMe = 0;
            
        }
        
        QText.text = Base.QNA[internalNum].Question;
        CurAltar = Altar[internalNum];

        CurAltar.AddComponent<AltarTrig>();
        //Local list of books for resetting and resummoning
        return LocalHolder.ToArray();
    }
    int power(){
        return Random.Range(90,170);
    }

    int angle(){   
        return Random.Range(40,140);
    }
    public void BookSwap(){
        if(CurAltar != null){
        Destroy(CurAltar.GetComponent<AltarTrig>());
        Destroy(CurAltar.GetComponent<Outline>());
        }
        if(BookList != null){
        Animations.BookSwap();
        for(int i = 0; i < BookList.Length; i++){
                Destroy(BookList[i]);
        }
     }
        StartCoroutine(PrivBook());
    }
    IEnumerator PrivBook(){
        yield return new WaitForSeconds(spawnDelay);
        BookList = SpawnBooks(QuestionsCFG);
        yield return null;
    }

    public void BookStop(){
        Destroy(CurAltar.GetComponent<AltarTrig>());
        Destroy(CurAltar.GetComponent<Outline>());
        if(BookList != null)
        for(int i = 0; i < BookList.Length; i++){
                Destroy(BookList[i]);
            }
    }

    #region ButtonTest
    void StageChangeSample(){
        GameEventContainer.AdvanceStage();
    }

    public void OnStageClick(){
        GameEventContainer.AdvanceStage();
    }
    #endregion
}
