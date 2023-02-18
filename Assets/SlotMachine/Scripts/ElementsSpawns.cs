using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class ElementsSpawns : MonoBehaviour
{
    public static ElementsSpawns Intance;
    public List<GameObject> Objects = new List<GameObject>();
    public int ControllerNumber;


    public GameObject[] SpawnsObjects;
    public GameObject[] SpawnsPosition;

    public GameObject ChildObject;
    public GameObject GamePanel;

    private void Start() => Intance = this;

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator ElementsSpawnsController(int objectIndexNumber)
    {
        int maxLoop = 13;
        for (int i = 0; i < maxLoop; i++)
        {
            int randomObjeIndex = Random.Range(0, SpawnsObjects.Length);
            GameObject gbj = Instantiate(SpawnsObjects[randomObjeIndex], SpawnsPosition[objectIndexNumber].transform.position,
                SpawnsPosition[0].transform.rotation);
            gbj.transform.SetParent(ChildObject.gameObject.transform);
            gbj.transform.localScale = new Vector3(1, 1);
            yield return new WaitForSeconds(0.3f);
            if (i == maxLoop - 3)
            {
                gbj.GetComponent<ObjectMovement>().ActiveSpawns = true;
                gbj.name = "StopDown";
                Objects.Add(gbj);
            }
            else if (i == maxLoop - 2)
            {
                gbj.GetComponent<ObjectMovement>().ActiveSpawns = true;
                gbj.name = "StopCenter";
                Objects.Add(gbj);
            }
            else if (i == maxLoop - 1)
            {
                gbj.GetComponent<ObjectMovement>().ActiveSpawns = true;
                gbj.name = "StopUp";
                Objects.Add(gbj);
            }
            else
                gbj.GetComponent<ObjectMovement>().ActiveSpawns = false;
        }

        if (objectIndexNumber == 2)
            MiniGameController();
    }

    public void MiniGameController()
    {
        int Firstcounter = 0;
        int twocounter = 0;
        int treecounter = 0;
        ObjectsCountController(Objects.Where(m => m.name == "StopDown").ToList(), ref Firstcounter);
        ObjectsCountController(Objects.Where(m => m.name == "StopCenter").ToList(), ref twocounter);
        ObjectsCountController(Objects.Where(m => m.name == "StopUp").ToList(), ref treecounter);

        if (Firstcounter != 3 && twocounter != 3 && treecounter != 3)
            EarningsTransactions.Instance.DefaultEvent();
        else
            EarningsTransactions.Instance.WinnerEvent(EarningsTransactions.Instance.GlobalMoney);
    }

    private void ObjectsCountController(List<GameObject> gbj, ref int counter)
    {
        foreach (var down in gbj)
            if (down.GetComponent<ObjectMovement>().BetCounter == gbj[0].GetComponent<ObjectMovement>().BetCounter)
                counter++;

        if (counter >= 3)
            EarningsTransactions.Instance.GlobalMoney += (int)gbj[0].GetComponent<ObjectMovement>()
                .BetCounter;
    }

    private void Spawn()
    {
        StartCoroutine(ElementsSpawnsController(0));
        StartCoroutine(ElementsSpawnsController(1));
        StartCoroutine(ElementsSpawnsController(2));
    }

    public void ButtonStart()
    {
        if (!EarningsTransactions.Instance.ActiveMiniGame && EarningsTransactions.Instance.StartGameControl() == true)
        {
            foreach (GameObject item in Objects)
            {
                item.AddComponent<Rigidbody2D>();
                item.GetComponent<ObjectMovement>().ActiveSpawns = false;
            }
            Objects.Clear();
            Spawn();
        }
    }

    public void ActivePanel(bool active) => GamePanel.SetActive(active);
}