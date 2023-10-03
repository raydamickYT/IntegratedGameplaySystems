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
            if (Input.GetKeyDown(keyCommand.key))
            {
                keyCommand.command.OnKeyDownExecute();
            }
            if (Input.GetKeyUp(keyCommand.key))
            {
                keyCommand.command.OnKeyUpExecute();
            }
            if (Input.GetKey(keyCommand.key))
            {
                keyCommand.command.Execute(keyCommand.key, keyCommand.context);
            }
        }
    }

    public void BindInputToCommand(KeyCode keyCode, ICommand command, object context = null)
    {
        keyCommands.Add(new KeyCommand()
        {
            key = keyCode,
            command = command,
            context = context
        });
    }

    public void UnBindInput(KeyCode keyCode)
    {
        var items = keyCommands.FindAll(x => x.key == keyCode);
        items.ForEach(x => keyCommands.Remove(x));
    }
}

public class KeyCommand
{
    public KeyCode key;
    public ICommand command;
    public object context;
}

public class MovementContext
{
    public Vector3 Direction { get; set; }
}



