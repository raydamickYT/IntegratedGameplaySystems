using UnityEngine;

public class CameraControl : ICommand
{
    private ActorData playerData;
    float yRotation, xRotation;

    public CameraControl(ActorData playerData)
    {
        this.playerData = playerData;
    }

    public void Execute(object context = null)
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * playerData.CameraSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * playerData.CameraSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        playerData.playerCameraHolderTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerData.ActorMesh.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        playerData.playerCameraTransform.SetPositionAndRotation(playerData.playerCameraHolderTransform.position, playerData.playerCameraHolderTransform.rotation);
    }

    public void OnKeyDownExecute()
    {
    }

    public void OnKeyUpExecute()
    {
    }
}
