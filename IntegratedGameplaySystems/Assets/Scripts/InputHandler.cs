using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public interface ICommand
{
    void Execute(object context = null);
    void OnKeyDownExecute();
    void OnKeyUpExecute();
}

public class InputHandler
{
    private List<KeyCommand> keyCommands = new List<KeyCommand>();

    public void HandleInput()
    {
        foreach (var keyCommand in keyCommands)
        {
            if (keyCommand.IsMouseControll)
            {
                keyCommand.Command.Execute();
            }
            else
            {
                CheckForKeyInput(keyCommand);
            }
        }
    }

    private void CheckForKeyInput(KeyCommand keyCommand)
    {
        if (Input.GetKeyDown(keyCommand.Key))
        {
            keyCommand.Pressed = true;
            keyCommand.Command.OnKeyDownExecute();
        }
        if (Input.GetKeyUp(keyCommand.Key))
        {
            keyCommand.Pressed = false;
            keyCommand.Command.OnKeyUpExecute();
        }
        if (Input.GetKey(keyCommand.Key))
        {
            keyCommand.Command.Execute(keyCommand.Context);
        }
    }

    public KeyCommand BindInputToCommand(ICommand command, KeyCode keyCode = KeyCode.None, object context = null, bool isMouseControl = false)
    {
        KeyCommand keyCommand = new()
        {
            Key = keyCode,
            Command = command,
            Context = context,
            IsMouseControll = isMouseControl
        };

        keyCommands.Add(keyCommand);

        return keyCommand;
    }

    public void UnBindInput(KeyCode keyCode)
    {
        var items = keyCommands.FindAll(x => x.Key == keyCode);
        items.ForEach(x => keyCommands.Remove(x));
    }
}

public class KeyCommand
{
    public bool Pressed;
    public bool IsMouseControll = false;
    public KeyCode Key;
    public ICommand Command;
    public object Context;
}

public class MovementContext
{
    public Vector3 Direction { get; set; }
}