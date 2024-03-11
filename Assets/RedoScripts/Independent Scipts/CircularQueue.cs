using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class CircularQueue<T>
    {
        private T[] array;
        private int capacity;
        private int count;
        private int head; // Index of the front of queue element
        private int tail; // Index of the rear of queue element

        //constructor for circular queue
        public CircularQueue(int capacity)
        {
            //circular queue must be atleast 1 long
            this.capacity = Mathf.Max(1, capacity);
            array = new T[this.capacity];
            count = 0;
            head = 0;
            tail = -1;
        }

        public int Count { get { return count; } }

        public bool IsEmpty { get { return count == 0; } }

        public T GetFrontItem()
        {
            return array[head];
        }

        public void Shift(int num)
        {
            head = (head + num) % count;
            tail = (tail + num) % count;
        }

        public bool Enqueue(T item)
        {
            if (count == capacity)
                return false; // Queue is full and so enqueue failed

            tail = (tail + 1) % capacity; // Wraps around if necessary
            array[tail] = item;

            count++;
            return true; // Enqueue is successful

        }

        public bool Dequeue(out T item)
        {
            if (count == 0)
            {
                item = default(T);
                return false; // Queue is empty and so dequeue failed
            }

            head = (head + 1) % capacity; // Wraps around if necessary
            item = array[head];

            count--;
            return true; // Dequeue is successful
        }

        public bool Peek(out T item)
        {
            if (count == 0)
            {
                item = default(T);
                return false; // Queue is empty and so peek failed
            }

            item = array[head];
            return true; // Peek is successful
        }
    }
}

