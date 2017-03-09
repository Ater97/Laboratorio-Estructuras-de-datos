using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Laboratory_2.UtilitiesClass
{
    public class MyBinaryTree<T>
    {

        public class TreeNode
        {
            public T value;
            public IComparable condition;

            public TreeNode left;
            public TreeNode right;


            public void SetCondition(IComparable z)
            {
                this.condition = z;
            }

            public IComparable GetCondition()
            {
                return this.condition;
            }

            public void SetValue(T value)
            {
                this.value = value;
            }

            public T GetValue()
            {
                return value;
            }

            public TreeNode GetLeft()
            {
                return left;
            }

            public void SetLeft(TreeNode left)
            {
                this.left = left;
            }

            public TreeNode GetRight()
            {
                return right;
            }

            public void SetRight(TreeNode right)
            {
                this.right = right;
            }

        }
        private int count;
        private TreeNode root;

        public MyBinaryTree()
        {
            count = 0;
            root = null;
        }

        private void SetRoot(TreeNode root)
        {
            this.root = root;
        }

        //Sobrecarga del método add, de forma recursiva
        private void Add(TreeNode value, TreeNode root, IComparable temp)
        {
            if (root == null)
            {
                /*
                 * Primer caso, en el que insertamos el primer elemento 
                 * se inserta como raiz de todo el árbol.
                 */

                this.SetRoot(value);
                count++;
            }
            else
            {
                if (temp.CompareTo(root.GetCondition()) < 0)
                {
                    /* 
                     * 5.- En caso de ser menor pasamos al Nodo de la IZQUIERDA del
                     * que acabamos de preguntar y repetimos desde el paso 2 
                     * partiendo del Nodo al que acabamos de visitar 
                     */
                    if (root.GetLeft() == null)
                    {
                        root.SetLeft(value);
                        count++;
                    }
                    else
                    {
                        Add(value, root.GetLeft(), temp);
                    }
                }
                else
                {
                    /* 
                     * 6.- En caso de ser mayor pasamos al Nodo de la DERECHA y tal
                     * cual hicimos con el caso anterior repetimos desde el paso 2
                     * partiendo de este nuevo Nodo.
                     */
                    if (root.GetRight() == null)
                    {
                        root.SetRight(value);
                        count++;
                    }
                    else
                    {
                        Add(value, root.GetRight(), temp);
                    }
                }
            }
        }

        //Sobrecarga del método add, de forma recursiva
        private void Add(TreeNode value, TreeNode root)
        {
            if (root == null)
            {
                /*
                 * Primer caso, en el que insertamos el primer elemento 
                 * se inserta como raiz de todo el árbol.
                 */

                this.SetRoot(value);
                count++;
            }
            else
            {
                if (Convert.ToInt32(value.GetValue()) > Convert.ToInt32(root.GetValue()))
                {
                    /* 
                     * 5.- En caso de ser menor pasamos al Nodo de la IZQUIERDA del
                     * que acabamos de preguntar y repetimos desde el paso 2 
                     * partiendo del Nodo al que acabamos de visitar 
                     */
                    if (root.GetLeft() == null)
                    {
                        root.SetLeft(value);
                        count++;
                    }
                    else
                    {
                        Add(value, root.GetLeft());
                    }
                }
                else
                {
                    /* 
                     * 6.- En caso de ser mayor pasamos al Nodo de la DERECHA y tal
                     * cual hicimos con el caso anterior repetimos desde el paso 2
                     * partiendo de este nuevo Nodo.
                     */
                    if (root.GetRight() == null)
                    {
                        root.SetRight(value);
                        count++;
                    }
                    else
                    {
                        Add(value, root.GetRight());
                    }
                }
            }
        }

        //Sobrecarga del método add
        public void Add(T x, IComparable temp)
        {
            TreeNode newElement = new TreeNode();
            newElement.value = x;
            newElement.condition = temp;

            this.Add(newElement, root, temp);
        }

        //Sobrecarga del método add
        public void Add(T x)
        {
            TreeNode newElement = new TreeNode();
            newElement.value = x;
            this.Add(newElement, root);
        }

        public bool Empty()
        {
            if (root == null)
            {
                return true;
            }

            return false;
        }


    }
}