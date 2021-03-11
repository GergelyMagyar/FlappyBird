using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class BirdAgent : Agent
{
    [SerializeField] private GameControllerScript _gameController;
    [SerializeField] private bool _trainingMode;

    public override void Initialize()
    {
        
    }

    public override void OnEpisodeBegin()
    {
        _gameController.StartGame(Mode.AI);
    }

    //vectorAction[0]: jumping (+1 -> jump, 0, -1 -> no jump)
    public override void OnActionReceived(float[] vectorAction)
    {
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
    }

}
