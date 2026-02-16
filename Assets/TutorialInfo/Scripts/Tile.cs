using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour
{
    public TicTacToeManager ticTacToeManager;

    public enum state
    {
        Circle,
        Cross,
        Empty
    }

    public int indexA;
    public int indexB;
    private TMP_Text textState; 

    public state TileState = Tile.state.Empty;

    public state GetState()
    {
        return TileState;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textState = GetComponentInChildren<TMP_Text>();
    }


    public void OnClick()
    {
        Debug.Log("Clicked on tile:["+indexA+","+indexB+"]...");
        switch (TileState) {
            case state.Empty:
                var turn = ticTacToeManager.turn;
                if(turn == TicTacToeManager.Turn.Cricle)
                {
                    TileState = state.Circle;
                    textState.text = "O";
                    Debug.Log("... and set to Circle");
                }
                else
                {
                    TileState = state.Cross;
                    textState.text = "X";
                    Debug.Log("... and set to Cross");
                }
                ticTacToeManager.UpdateTurn();
                return;
            default:
                Debug.Log("... but the tile already has been set to "+TileState);
                return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
