public interface ICommand
{
    public void Execute(); // Perform the action
    void Undo(); // Undo the action (optional)
}