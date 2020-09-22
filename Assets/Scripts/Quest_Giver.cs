using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Giver : MonoBehaviour
{
    public bool Assigned_Quest;
    public bool Helped;
    public int how_many_quests;
    [SerializeField]
    private string[] quest_script_name;
    public GameObject quest;
    private Quest Quest;
    public string quest_name_desc;
    public Text descTxt;
    public Text isQuestComplete;
    public RectTransform descTransform;
    public bool showedQuest = false;
    public int questNum;
    void Start()
    {
        questNum = Random.Range(0, how_many_quests);
        Talk_to_Quest_Giver();
        //descTransform.anchoredPosition = new Vector2(0, 300);
        //showedQuest = false;
    }

    void Update()
    {
        Debug.Log("always updating" + quest.GetComponent<Quest>().Quest_Description);
        quest_name_desc = Quest.Quest_Description;
        descTxt.text = quest_name_desc;

        if (!showedQuest)
        {
            StartCoroutine(Show_Quest());
        }
        else
        {
            StartCoroutine(Hide_Quest());
        }
    }

    public void Talk_to_Quest_Giver()
    {
        if(!Assigned_Quest && !Helped)
        {
            Assign_Quest();
            
        }
        else if(Assigned_Quest && !Helped)
        {
            Giver_Checks_Quest();
        }
        
    }

    void Assign_Quest()
    {
        Assigned_Quest = true;
        Quest = (Quest)quest.AddComponent(System.Type.GetType(quest_script_name[questNum]));
        isQuestComplete.text = "Quest Incomplete";
    }

    void Giver_Checks_Quest()
    {
        if (Quest.Quest_Completed)
        {
            isQuestComplete.text = "Quest Complete";
            Quest.Give_Reward();
            Helped = true;
            Assigned_Quest = false;
        }
    }

    public void Forfeit_Quest()
    {
        Assigned_Quest = false;
        Helped = false;
    }
    IEnumerator Show_Quest()
    {
        descTransform.anchoredPosition = new Vector2(0, 300);
        yield return new WaitForSeconds(1f);
        descTransform.anchoredPosition = Vector2.Lerp(descTransform.anchoredPosition, new Vector2(0, -75), 5f * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        showedQuest = true;

    }

    IEnumerator Hide_Quest()
    {
        yield return new WaitForSeconds(1f);
        descTransform.anchoredPosition = Vector2.Lerp(descTransform.anchoredPosition, new Vector2(0, 300), 5f * Time.deltaTime);
    }

    public void Move_Quest_Text()
    {
        descTransform.anchoredPosition = Vector2.Lerp(descTransform.anchoredPosition, new Vector2(0, 0), 5f * Time.deltaTime);
    }
}
