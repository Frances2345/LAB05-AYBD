

using System;
using UnityEngine;

public class LinkedList<T> : MonoBehaviour
{
    public Node<T> head = null;
    public int Count;


    //->O(N)
    public virtual void Add(T value)
    {
        Node<T> tempNode = new(value);

        //-> Cuando no hay nuingun elemento en la lista
        if (head == null)
        {
            head = tempNode;
        }
        else
        {
            Node<T> Evaluator = head;

            while (Evaluator.Next != null)
            {
                Evaluator = Evaluator.Next;
            }

            Evaluator.SetNext(tempNode);

        }
        Count++;
    }

    //->O(n)
    public void RemoveLast()
    {

        Node<T> Evaluator = head;

        if (Count == 0)
        {
            Debug.Log("La lista esta vacia");
            return;
        }
        else if (Count == 1)
        {
            head = null;
            Count--;
        }
        else if (Count == 2)
        {
            head.SetNext(null);
            Count--;
        }
        else if (Count > 2)
        {
            while (Evaluator != null)
            {
                if (Evaluator.Next.Next == null)
                {
                    break;
                }

                Evaluator = Evaluator.Next;
            }

            Evaluator.SetNext(null);
            Count--;
        }

    }
    //-> O(1)
    public void RemoveFirst()
    {

        if (Count <= 1)
        {
            head = null;
            Count--;
            return;
        }

        Node<T> Evaluator = head.Next;
        head.SetNext(null);
        head = Evaluator;
        Count--;


    }


    // Recorrer
    public void Traverse(Action<Node<T>> action)
    {
        Node<T> Evaluator = head;
        while (Evaluator != null)
        {
            //  Debug.Log(Evaluator.Value);
            action(Evaluator);

            Evaluator = Evaluator.Next;
        }
    }
    public void RecursiveTraverse(Node<T> Evaluator, Action<Node<T>> action)
    {
        if (Evaluator == null)
        {
            Debug.Log("Terminas de recorrer la lista");
            return;
        }

        action(Evaluator);

        RecursiveTraverse(Evaluator.Next, action);
    }

}