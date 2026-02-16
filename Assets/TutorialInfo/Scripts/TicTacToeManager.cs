using UnityEngine;
using TMPro;

public class TicTacToeManager : MonoBehaviour
{

    public TMP_Text text;
    public Tile[] tiles;
    public enum Turn
    {
        Cricle,
        Cross
    }
    public Turn turn = Turn.Cricle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = "It's now " + turn.ToString() + " turn";
    }

    public void UpdateTurn()
    {
        switch (turn)
        {
            default:
                return;
            case (Turn.Cricle):
                turn = Turn.Cross;
                text.text = "It's now " + turn.ToString() + " turn";
                CheckGameState();
                return;
            case (Turn.Cross):
                turn = Turn.Cricle;
                text.text = "It's now " + turn.ToString() + " turn";
                CheckGameState();
                return;
        }
    }

    public void CheckGameState()
    {
        Tile.state[,] board = new Tile.state[3, 3];

        // Wypełniamy tablicę stanami z tiles
        foreach (Tile tile in tiles)
        {
            board[tile.indexA-1, tile.indexB-1] = tile.GetState();
        }

        // Sprawdzenie wierszy
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != Tile.state.Empty &&
                board[i, 0] == board[i, 1] &&
                board[i, 1] == board[i, 2])
            {
                EndGame(board[i, 0]);
                return;
            }
        }

        // Sprawdzenie kolumn
        for (int i = 0; i < 3; i++)
        {
            if (board[0, i] != Tile.state.Empty &&
                board[0, i] == board[1, i] &&
                board[1, i] == board[2, i])
            {
                EndGame(board[0, i]);
                return;
            }
        }

        // Przekątna 1
        if (board[0, 0] != Tile.state.Empty &&
            board[0, 0] == board[1, 1] &&
            board[1, 1] == board[2, 2])
        {
            EndGame(board[0, 0]);
            return;
        }

        // Przekątna 2
        if (board[0, 2] != Tile.state.Empty &&
            board[0, 2] == board[1, 1] &&
            board[1, 1] == board[2, 0])
        {
            EndGame(board[0, 2]);
            return;
        }

        // Sprawdzenie remisu
        bool isDraw = true;
        foreach (Tile tile in tiles)
        {
            if (tile.GetState() == Tile.state.Empty)
            {
                isDraw = false;
                break;
            }
        }

        if (isDraw)
        {
            text.text = "Draw!";
            Debug.Log("Draw!");
        }
    }

    private void EndGame(Tile.state winner)
    {
        text.text = winner.ToString() + " wins!";
        Debug.Log(winner.ToString() + " wins!");

        // opcjonalnie: wyłączenie dalszej gry
        foreach (Tile tile in tiles)
        {
            tile.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
