using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class Stack<T>
    {
        private int topOfStack;
        private T[] stackArray;
        private int count;
        private int capacity;

        //constructor for Stack
        public Stack(int capacity)
        {
            // make the capacity whatever the user enters but obviosuly with a minimum of 1
            this.capacity = Mathf.Max(1, capacity);
            stackArray = new T[this.capacity];
            topOfStack = -1;
            count = 0;
        }
        public int GetCount()
        {
            return count;
        }
        public T Peek()
        {
            // check if stack is empty if so throw empty stack error
            if (topOfStack == -1)
            {
                throw new InvalidOperationException("Stack is currently empty");
            }
            // else if stack is not empty return the top item
            return stackArray[topOfStack];
        }
        public void Push(T item)
        {
            // if stack is full then resize the array making it twice as big.
            if (count == capacity)
            {
                Array.Resize(ref stackArray, count * 2); 
            }
            topOfStack++;
            count++;
            stackArray[topOfStack] = item;
        }
        public T Pop()
        {
            // check if stack is empty if so throw empty stack error
            if (topOfStack == -1)
            {
                throw new InvalidOperationException("Stack is currently empty");
            }
            // else get the top of stack element out of stack and return it aswell as making the next element underneath it now the top of stack
            T item = stackArray[topOfStack];
            topOfStack--;
            count--;
            return item;
        }
        //clear all elements in stack and reset count and topOfStack pointer
        public void Clear()
        {
            topOfStack = -1; 
            count = 0; 
            Array.Clear(stackArray, 0, stackArray.Length); 
        }

    }
}

