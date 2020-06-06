using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private static CommandManager _instance;
    private List<ICommand> _commandBuffer = new List<ICommand>();
    private bool _executingPlay;

    public static CommandManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The CommandManager is NULL");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    //create a method to "add" commands to the command buffer

    public void AddCommand(ICommand command)
    {
        //var cubes = GameObject.FindGameObjectsWithTag("CommandMngr");

        if (command != null && !_executingPlay)
            _commandBuffer.Add(command);

    }

    //crate a play routine triggered by a play method that0s going to play back all the commands 1 second delay

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        Debug.Log("Playing...");
        _executingPlay = true;

        foreach (var command in _commandBuffer)
        {
            command.Execute();
            yield return new WaitForEndOfFrame();
        }

        _executingPlay = false;
        Debug.Log("Finished");
    }

    //create a rewsing routine triggered by a rewind method that's going play in reverse, with a 1 second delay

    public void Rewind()
    {
        StartCoroutine(RewindRoutine());
    }

    IEnumerator RewindRoutine()
    {
        _executingPlay = true;
        Debug.Log("Rewind.");

        foreach (var command in Enumerable.Reverse(_commandBuffer))
        {
            command.Undue();
            yield return new WaitForEndOfFrame();
        }

        _executingPlay = false;

        Debug.Log("Finished.");
    }

    //Done = Finished with changing colors. Turn them all white

    public void Done()
    {
        var cubes = GameObject.FindGameObjectsWithTag("Cube");
        foreach (var cube in cubes)
        {
            cube.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
    //Reset - Clear the command buffer

    public void Reset()
    {
        _commandBuffer.Clear();
    }
}
