namespace ClassLibrary1;
public class Node<T>(T value)
{
    public T Value { get; set; } = value;
    public Node<T> Left { get; set; } = null;
    public Node<T> Right { get; set; } = null;
}
