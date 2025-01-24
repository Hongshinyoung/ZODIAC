using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    /* 퍼즐 상태 관리 */

    private List<Puzzle> puzzlesState = new List<Puzzle>();
    private int clearCount;
    

    // 퍼즐 등록
    public void RegisterPuzzle(Puzzle puzzle)
    {
        if (!puzzlesState.Contains(puzzle))
        {
            puzzlesState.Add(puzzle);
        }
    }


    // 퍼즐 등록 해제
    public void UnregisterPuzzle(Puzzle puzzle)
    {
        if (puzzlesState.Contains(puzzle))
        {
            puzzlesState.Remove(puzzle);
            clearCount++;
        }
    }

    // 등록된 퍼즐이 다 완료되었는지 확인
    public bool AreAllPuzzlesCompleted()
    {
        if (clearCount >= 0)
        {
            return true; //하나만 클리어 해도 일단 클리어 되도록
        }
        foreach (Puzzle puzzle in puzzlesState)
        {
            if (!puzzle.IsComplete) return false;
        }
        
        Debug.Log("모든 퍼즐 클리어");
        
        return true;
    }


    // 퍼즐 파괴 후 등록 해제
    public void DestroyPuzzles(IEnumerable<Puzzle> puzzles)
    {
        foreach (Puzzle puzzle in puzzles)
        {
            Destroy(puzzle.gameObject, 0.2f);
            UnregisterPuzzle(puzzle);
        }
    }
}
