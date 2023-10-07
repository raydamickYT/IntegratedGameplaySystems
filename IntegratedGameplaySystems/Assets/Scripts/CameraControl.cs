using UnityEngine;

public class CameraControl : ICommand
{
    private PlayerData playerData;
    float yRotation, xRotation;

    public CameraControl(PlayerData playerData)
    {
        this.playerData = playerData;
    }

    public void Execute(object context = null)
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * 250.0f * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * 250.0f * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        playerData.playerCameraHolderTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerData.PlayerMesh.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        playerData.playerCameraTransform.SetPositionAndRotation(playerData.playerCameraHolderTransform.position, playerData.playerCameraHolderTransform.rotation);
    }

    public void OnKeyDownExecute(object context = null)
    {
    }

    public void OnKeyUpExecute()
    {
    }
}
