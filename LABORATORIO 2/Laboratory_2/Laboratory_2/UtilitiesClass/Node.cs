using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratory_2.UtilitiesClass
{
    public class Node<T> : IComparable<T>
    {
        public T data { get; set; }

        public Node<T> left { get; set; }
        public Node<T> right { get; set; }

        public Node(T data)
        {
            this.data = data;
            
            left = null;
            right = null;

        }

        public int CompareTo(T other)
        {
            throw new NotImplementedException();
        }
    }

}