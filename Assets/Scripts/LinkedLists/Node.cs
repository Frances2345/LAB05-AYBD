

public class Node<T>
{
    private T value = default;

    private Node<T> next = null;
    private Node<T> prev = null;

    public Node(T value)
    {
        this.value = value;
    }
    public void SetNext(Node<T> next)
    {
        this.next = next;
    }
    public void SetPrev(Node<T> prev)
    {
        this.prev = prev;
    }
    public Node<T> Next => next;
    public Node<T> Prev => prev;
    public T Value => value;
}