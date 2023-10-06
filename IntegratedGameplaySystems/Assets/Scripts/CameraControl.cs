using UnityEngine;

public class CameraControl : ICommand
{
    private PlayerData playerData;
    float yRotation, xRotation;

    public CameraControl(PlayerData playerData)
    {
        this.playerData = playerData;
        playerData.playerCameraTransform.rotation = Quaternion.Euler(Vector3.zero);
        playerData.PlayerMesh.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void Execute(object context = null)
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * 100.0f * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * 100.0f * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        Mathf.Clamp(yRotation, -90, 90);

        playerData.playerCameraHolderTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerData.PlayerMesh.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        playerData.playerCameraTransform.SetPositionAndRotation(playerData.playerCameraHolderTransform.position, playerData.playerCameraHolderTransform.rotation);
    }

    public void OnKeyDownExecute()
    {
    }

    public void OnKeyUpExecute()
    {
    }
}
