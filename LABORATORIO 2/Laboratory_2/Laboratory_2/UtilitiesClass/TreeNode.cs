using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratory_2.UtilitiesClass
{
    public class TreeNode<T>
    {
        private T value;

        public TreeNode<T> padre;
        public TreeNode<T> left;
        public TreeNode<T> right;

        public T Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public TreeNode<T> GetPadre()
        {
            return this.padre;
        }

        public void SetPadre(TreeNode<T> Padre)
        {
            this.padre = Padre;
        }

        public TreeNode<T> GetLeft()
        {
            return left;
        }

        public void SetLeft(TreeNode<T> left)
        {
            this.left = left;
        }

        public TreeNode<T> GetRight()
        {
            return right;
        }

        public void SetRight(TreeNode<T> right)
        {
            this.right = right;
        }

    }
}