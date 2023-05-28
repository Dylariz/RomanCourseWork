namespace RomanCourseWork
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T>? Next { get; internal set; }
        public Node<T>? Previous { get; internal set; }

        internal Node(T item, Node<T>? next, Node<T>? previous)
        {
            Value = item;
            Next = next;
            Previous = previous;
        }
    }
}