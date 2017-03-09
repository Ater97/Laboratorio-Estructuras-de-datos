using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratory_2.UtilitiesClass
{
    public class MyBinaryTree<T> where T : IComparable<T>
    {
        private int count;
        private TreeNode<T> root;

        public MyBinaryTree()
        {
            count = 0;
            root = null;
        }

        private void SetRoot(TreeNode<T> root)
        {
            this.root = root;
        }

        //Sobrecarga del método add, de forma recursiva
        private void Add(TreeNode<T> value, TreeNode<T> root)
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
                if (value.Value.CompareTo(root.Value) == 1)
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

        public void Add(T x)
        {
            TreeNode<T> newElement = new TreeNode<T>();
            newElement.Value = x;
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

        public void Eliminate(T x)
        {

        }

        private bool EliminateCase1(TreeNode<T> node)
        {
            return false;
        }

        private bool EliminateCase2(TreeNode<T> node)
        {
            return false;
        }

        private bool EliminateCase3(TreeNode<T> node)
        {
            return false;
        }
    }
}