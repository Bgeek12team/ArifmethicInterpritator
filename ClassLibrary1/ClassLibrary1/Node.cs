namespace ClassLibrary1;
/// <summary>
/// Представляет узел в структуре данных бинарного дерева.
/// </summary>
/// <typeparam name="T">Тип значения, хранящегося в узле.</typeparam>
public class Node<T>
{
    /// <summary>
    /// Получает или задает значение, хранящееся в узле.
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Получает или задает левый дочерний узел текущего узла.
    /// </summary>
    public Node<T> Left { get; set; }

    /// <summary>
    /// Получает или задает правый дочерний узел текущего узла.
    /// </summary>
    public Node<T> Right { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса Node с указанным значением.
    /// </summary>
    /// <param name="value">Значение, которое будет храниться в узле.</param>
    public Node(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }

    public void ForEach(Action<Node<T>> action)
    {
        action(this);
        if (Left != null)
            Left.ForEach(action);
        if (Right != null)
            Right.ForEach(action);
    }
}