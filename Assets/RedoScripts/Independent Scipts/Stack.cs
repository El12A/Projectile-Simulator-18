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

        //constructor for the Stack
        public Stack(int capacity)
        {
            // make the capacity whatever the user enters but obviosuly with a minimum of 1
            this.capacity = Mathf.Max(1, capacity);
            stackArray = new T[this.capacity];
            topOfStack = -1;
            count = 0;

        }
        // returns the count of the stack (how many elements are in the stack)
        public int GetCount()
        {
            return count;
        }

        // this method is for looking at the top of the stack and returning error exception if stack is empty
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

        // this function is for adding items to the top of the stack it will also resize it if currently full
        // the resize is especially handy for the timestamp stack implementation as it is likely the user will have the projectile in motion for longer then the initial length call
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

        // this method will remove and return the top element of the stack
        // it will throw error if the stack is empty though
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
        //clears all  the elements inside of the stack and resets the count and the topOfStack pointer
        public void Clear()
        {
            topOfStack = -1;
            count = 0;
            Array.Clear(stackArray, 0, stackArray.Length);
        }
    }
}
