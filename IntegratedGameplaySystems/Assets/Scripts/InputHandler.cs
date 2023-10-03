using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute(KeyCode key, object context = null);
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
                keyCommand.Command.Execute(keyCommand.Key, keyCommand.Context);
            }
        }
    }

    public KeyCommand BindInputToCommand(KeyCode keyCode, ICommand command, object context = null)
    {
        KeyCommand keyCommand = new KeyCommand()
        {
            Key = keyCode,
            Command = command,
            Context = context
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
    public KeyCode Key;
    public ICommand Command;
    public object Context;
}

public class MovementContext
{
    public Vector3 Direction { get; set; }
}



