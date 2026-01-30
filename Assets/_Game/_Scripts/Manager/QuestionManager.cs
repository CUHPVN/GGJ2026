using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{

    public bool[] playerHave = new bool[5];
    public Question question;
    public struct Question
    {
        public bool isHighest;
        public int type;
    }
    public void GenQuestion()
    {
        List<int> tmp = new List<int>();
        for(int i=0; i<playerHave.Length; i++)
        {
            if(!playerHave[i])
            {
                tmp.Add(i);
            }
        }
        Question res= new Question();
        res.isHighest = (Random.Range(0, 2)==1)? true:false;
        res.type = tmp[Random.Range(0, tmp.Count)];
        question = res;
    }
    public EndBattleState IsPlayerWin(int[] playerRes, int[] enemyRes) 
    {
        int playCnt = 0;
        foreach(int i in playerRes)
        {
            if(i == question.type) playCnt++;
        }
        int enemCnt = 0;
        foreach (int i in enemyRes)
        {
            if (i == question.type) enemCnt++;
        }
        if (playCnt == enemCnt) return EndBattleState.Draw;
        if (question.isHighest)
        {
            if (playCnt > enemCnt) return EndBattleState.Win;
            else return EndBattleState.Lose;
        }
        else
        {
            if (playCnt > enemCnt)
            return EndBattleState.Lose;
            else return EndBattleState.Win;
        }
    }
}
    public enum EndBattleState
    {
        Win,
        Lose,
        Draw,
    }
