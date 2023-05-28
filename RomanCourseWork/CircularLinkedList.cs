using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RomanCourseWork
{
public sealed class CircularLinkedList<T> : ICollection<T>
{
    private readonly IEqualityComparer<T> _comparer;

    public Node<T>? Tail { get; private set; }
    public Node<T>? Head { get; private set; }
    public int Count { get; private set; }
    
    public CircularLinkedList(IEnumerable<T>? collection, IEqualityComparer<T> comparer)
    {
        _comparer = comparer;
        if (collection != null)
        {
            var enumerable = collection.ToList();
            foreach (var item in enumerable)
                AddToTail(item);
            Count = enumerable.Count();
        }
    }
    
    public CircularLinkedList() : this(null, EqualityComparer<T>.Default) { }

    public Node<T> this[int index]
    {
        get
        {
            if (index >= Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            

            Node<T> node = Head ?? throw new InvalidOperationException();
            for (int i = 0; i < index; i++)
                node = node!.Next!;
            return node;
        }
    }

    public void AddToTail(T item)
    {
        if (Head == null || Tail == null)
        {
            AddFirstItem(item);
        }
        else
        {
            Node<T> newNode = new Node<T>(item, Head, Tail);
            Tail.Next = newNode;
            newNode.Next = Head;
            newNode.Previous = Tail;
            Head.Previous = newNode;
            Tail = newNode;
        }

        ++Count;
    }
    
    public void AddToHead(T item)
    {
        if (Head == null || Tail == null)
            AddFirstItem(item);
        else
        {
            Node<T> newNode = new Node<T>(item, Head, Tail);
            Head.Previous = newNode;
            newNode.Next = Head;
            newNode.Previous = Tail;
            Tail.Next = newNode;
            Head = newNode;
        }

        ++Count;
    }

    private void AddFirstItem(T item)
    {
        Head = new Node<T>(item, null, null);
        Tail = Head;
        Head.Next = Tail;
        Head.Previous = Tail;
    }
    

    public void AddAfter(Node<T> node, T item)
    {
        if (node == null || node.Next == null)
            throw new ArgumentNullException(nameof(node));
        if (Head == null)
            throw new NullReferenceException(nameof(Head));
        if (FindNode(Head, node.Value) != node)
            throw new InvalidOperationException("Node doesn't belong to this list");

        Node<T> newNode = new Node<T>(item, node.Next, node);
        newNode.Next!.Previous = newNode;
        node.Next = newNode;

        if (node == Tail)
            Tail = newNode;
        ++Count;
    }

    public void AddAfter(T existingItem, T newItem)
    {
        Node<T>? node = Find(existingItem);
        if (node == null)
            throw new ArgumentException("existingItem doesn't exist in the list");
        AddAfter(node, newItem);
    }

    public void AddBefore(Node<T> node, T item)
    {
        if (node == null || node.Previous == null)
            throw new ArgumentNullException(nameof(node));
        if (Head == null)
            throw new NullReferenceException(nameof(Head));
        Node<T>? temp = FindNode(Head, node.Value);
        if (temp != node)
            throw new InvalidOperationException("Node doesn't belong to this list");

        Node<T> newNode = new Node<T>(item, node, node.Previous);
        node.Previous.Next = newNode;
        node.Previous = newNode;

        if (node == Head)
            Head = newNode;
        ++Count;
    }

    public void AddBefore(T existingItem, T newItem)
    {
        Node<T>? node = Find(existingItem);
        if (node == null)
            throw new ArgumentException("existingItem doesn't exist in the list");
        AddBefore(node, newItem);
    }

    public Node<T>? Find(T item)
    {
        return FindNode(Head ?? throw new InvalidOperationException(), item);
    }

    public bool Remove(T item)
    {
        Node<T>? nodeToRemove = Find(item);
        if (nodeToRemove != null)
            return RemoveNode(nodeToRemove);
        return false;
    }
    
    private bool RemoveNode(Node<T>? nodeToRemove)
    {
        if (nodeToRemove == null || nodeToRemove.Previous == null || nodeToRemove.Next == null)
            throw new ArgumentNullException(nameof(nodeToRemove));
        
        Node<T> previous = nodeToRemove.Previous;
        previous.Next = nodeToRemove.Next;
        nodeToRemove.Next.Previous = nodeToRemove.Previous;

        if (Head == nodeToRemove)
            Head = nodeToRemove.Next;
        else if (Tail == nodeToRemove)
            Tail = Tail.Previous;

        --Count;
        return true;
    }

    public void RemoveAll(T item)
    {
        bool removed;
        do
        {
            removed = Remove(item);
        } while (removed);
    }

    public void Clear()
    {
        Head = null;
        Tail = null;
        Count = 0;
    }

    public bool RemoveHead()
    {
        return RemoveNode(Head);
    }

    public bool RemoveTail()
    {
        return RemoveNode(Tail);
    }

    private Node<T>? FindNode(Node<T> node, T valueToCompare)
    {
        Node<T>? result = null;
        if (_comparer.Equals(node.Value, valueToCompare))
            result = node;
        else if (result == null && node.Next != Head)
            result = FindNode(node.Next!, valueToCompare);
        return result;
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (Head == null)
            throw new NullReferenceException(nameof(Head));
        Node<T> current = Head;
        if (current != null)
        {
            do
            {
                yield return current!.Value;
                current = current.Next!;
            } while (current != Head);
        }
    }

    public IEnumerator<T> GetReverseEnumerator()
    {
        if (Tail == null)
            throw new NullReferenceException(nameof(Head));
        Node<T> current = Tail;
        if (current != null)
        {
            do
            {
                yield return current!.Value;
                current = current.Previous!;
            } while (current != Tail);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Contains(T item)
    {
        return Find(item) is not null;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        if (arrayIndex < 0 || arrayIndex > array.Length)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));

        Node<T> node = Head ?? throw new NullReferenceException(nameof(Head));
        do
        {
            array[arrayIndex++] = node!.Value;
            node = node.Next!;
        } while (node != Head);
    }

    public void SortFromHeadToTailBy<TKey>(Func<T, TKey> keySelector, bool descending)
    {
        Node<T>? currentNode = Head;
        if (currentNode is null)
        {
            throw new NullReferenceException();
        }
        
        for (int i = 1; i < Count; i++)
        {
            for (int j = 0; j < Count - i; j++)
            {
                if (Comparer<TKey>.Default.Compare(keySelector(this[j].Value), keySelector(this[j + 1].Value)) == (descending ? -1 : 1))
                {
                    (this[j].Value, this[j + 1].Value) = (this[j + 1].Value, this[j].Value);
                }
            }
        }
    }

    bool ICollection<T>.IsReadOnly => false;

    void ICollection<T>.Add(T item)
    {
        AddToTail(item);
    }
}
}