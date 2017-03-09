using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratory_2.UtilitiesClass
{
    // public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    public class BinaryTree<T> : IComparable<T>
    {
        private Node<T> Root;
        static int count = 0;

        public BinaryTree()
        {
            Root = null;

        }
        public void Add(T Data)
        {
            Node<T> newNode = new Node<T>(Data);

            if (Root == null)
            {
                Root = newNode;
            }
            else
            {
                Insert(newNode, Root);
            }
            count++;
        }
        private void Insert(Node<T> item, Node<T> currentRoot)
        {
            int ComparedValue = currentRoot.CompareTo(item.data);

            if ( ComparedValue > 0)
            {
                if (currentRoot.right == null)
                {
                    currentRoot.right = item;
                }
                else
                {
                    Insert(item, currentRoot.right);
                }
            }
            else
            {
                if (currentRoot.left == null)
                {
                    currentRoot.left = item;
                }
                else
                {
                    Insert(item, currentRoot.left);
                }
            }
        }
        public int CompareTo(T item)
        {

            if (item == null) return 0;
            
            /*if (other.GetType() != GetType())
                return -1;
            return data.CompareTo(other.data);
            */
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(T obj)
        {
            throw new NotImplementedException();
        }

        /*  public int GetHashCode(Thing obj)
           {
               throw new NotImplementedException();
           }
           */

        /* IEnumerator IEnumerable.GetEnumerator()
         {
             throw new NotImplementedException();
         }
         */
    }
}