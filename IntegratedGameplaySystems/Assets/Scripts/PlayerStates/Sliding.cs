using UnityEngine;

public class Sliding : ICommand
{
    private ActorData playerData;

    public Sliding(ActorData _playerData)
    {
        playerData = _playerData;
    }

    public void Execute(object context = null)
    {
        if (context is MovementContext movementContext)
        {
            //Downward Force.
            playerData.ActorRigidBody.AddRelativeForce(movementContext.Direction.normalized * 5.0f, ForceMode.Force);
        }
    }

    public void OnKeyDownExecute(object context = null)
    {
        playerData.ActorMesh.transform.localScale = new Vector3(1, 0.5f, 1);
        playerData.CurrentMoveSpeed += playerData.SlideSpeedIncrease;
    }

    public void OnKeyUpExecute()
    {
        playerData.CurrentMoveSpeed -= playerData.SlideSpeedIncrease;
        playerData.ActorMesh.transform.localScale = Vector3.one;
    }
}