public interface ICommand
{
    void Execute();  // Perform the action
    void Undo();     // Undo the action (optional)
}