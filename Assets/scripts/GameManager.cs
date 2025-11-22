using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameStates currentState;
    public GameStates previousState;
    public enum GameStates
    {
        gameSetup,
        openGameState,
        stageResolve,
        drawState
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
