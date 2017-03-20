using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratory_2.UtilitiesClass
{
    public class MyBinaryTreeAVL<T> : IEnumerable<T> where T : IComparable<T>
    {
        public delegate int Compare<E>(T a, E b);

        private int count;
        private TreeNode<T> root;

        public MyBinaryTreeAVL()
        {
            count = 0;
            root = null;
        }

        public int GetCount()
        {
            return this.count;
        }

        private void SetRoot(TreeNode<T> root)
        {
            this.root = root;
        }

        #region Add Balanced
        public void Add(T x)
        {
            TreeNode<T> newElement = new TreeNode<T>();
            newElement.Value = x;
            this.Add(newElement, this.root);
        }

        private void Add(TreeNode<T> newElement, TreeNode<T> root)
        {
            if (root == null)
            {
                this.SetRoot(newElement);
                count++;
                this.root.SetPadre(null);
            }
            else
            {
                TreeNode<T> node = this.root;

                while (node != null)
                {
                    int compare = newElement.Value.CompareTo(node.Value);

                    if (compare < 0)
                    {
                        TreeNode<T> left = node.GetLeft();

                        if (left == null)
                        {
                            node.SetLeft(newElement);
                            node.GetLeft().SetPadre(node);
                            count++;
                            InsertBalance(node, 1);
                            return;
                        }
                        else
                        {
                            node = left;
                        }
                    }
                    else
                    {
                        if (compare > 0)
                        {
                            TreeNode<T> right = node.GetRight();

                            if (right == null)
                            {
                                node.SetRight(newElement);
                                node.GetRight().SetPadre(node);
                                count++;
                                InsertBalance(node, -1);
                                return;
                            }
                            else
                            {
                                node = right;
                            }
                        }
                        else
                        {
                            node.Value = newElement.Value;
                            return;
                        }
                    }
                }
            }
        }

        private void InsertBalance(TreeNode<T> node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 0)
                {
                    return;
                }
                else if (balance == 2)
                {
                    if (node.GetLeft().Balance == 1)
                    {
                        RotateRight(node);
                    }
                    else
                    {
                        RotateLeftRight(node);
                    }

                    return;
                }
                else if (balance == -2)
                {
                    if (node.GetRight().Balance == -1)
                    {
                        RotateLeft(node);
                    }
                    else
                    {
                        RotateRightLeft(node);
                    }

                    return;
                }

                TreeNode<T> parent = node.GetPadre();

                if (parent != null)
                {
                    balance = parent.GetLeft() == node ? 1 : -1;
                }

                node = parent;
            }
        }

        private TreeNode<T> RotateRight(TreeNode<T> node)
        {
            TreeNode<T> left = node.GetLeft();
            TreeNode<T> leftRight = left.GetRight();
            TreeNode<T> parent = node.GetPadre();

            left.SetPadre(parent);
            left.SetRight(node);
            node.SetLeft(leftRight);
            node.SetPadre(left);


            if (leftRight != null)
            {
                leftRight.SetPadre(node);
            }

            if (node == this.root)
            {
                this.root = left;
            }
            else if (parent.GetLeft() == node)
            {
                parent.SetLeft(left);
            }
            else
            {
                parent.SetRight(left);
            }

            left.Balance++;
            node.Balance = -left.Balance;

            return left;
        }

        private TreeNode<T> RotateLeft(TreeNode<T> node)
        {
            TreeNode<T> right = node.GetRight();
            TreeNode<T> rightLeft = right.GetLeft();
            TreeNode<T> parent = node.GetPadre();

            right.SetPadre(parent);
            right.SetLeft(node);
            node.SetRight(rightLeft);
            node.SetPadre(right);


            if (rightLeft != null)
            {
                rightLeft.SetPadre(node);
            }

            if (node == this.root)
            {
                this.root = right;
            }
            else if (parent.GetRight() == node)
            {
                parent.SetRight(right);
            }
            else
            {
                parent.SetLeft(right);
            }

            right.Balance++;
            node.Balance = -right.Balance;

            return right;
        }

        private TreeNode<T> RotateRightLeft(TreeNode<T> node)
        {
            TreeNode<T> right = node.GetRight();
            TreeNode<T> rightLeft = right.GetLeft();
            TreeNode<T> parent = node.GetPadre();
            TreeNode<T> rightLeftLeft = rightLeft.GetLeft();
            TreeNode<T> rightLeftRight = rightLeft.GetRight();

            rightLeft.SetPadre(parent);
            node.SetRight(rightLeftLeft);
            right.SetLeft(rightLeftRight);
            rightLeft.SetRight(right);
            rightLeft.SetLeft(node);
            right.SetPadre(rightLeft);
            node.SetPadre(rightLeft);

            if (rightLeftLeft != null)
            {
                rightLeftLeft.SetPadre(node);
            }

            if (rightLeftRight != null)
            {
                rightLeftRight.SetPadre(right);
            }

            if (node == this.root)
            {
                SetRoot(rightLeft);
            }
            else if (parent.GetRight() == node)
            {
                parent.SetRight(rightLeft);
            }
            else
            {
                parent.SetLeft(rightLeft);
            }

            if (rightLeft.Balance == 1)
            {
                node.Balance = 0;
                right.Balance = -1;
            }
            else if (rightLeft.Balance == 0)
            {
                node.Balance = 0;
                right.Balance = 0;
            }
            else
            {
                node.Balance = 1;
                right.Balance = 0;
            }

            rightLeft.Balance = 0;

            return rightLeft;
        }

        private TreeNode<T> RotateLeftRight(TreeNode<T> node)
        {
            TreeNode<T> left = node.GetLeft();
            TreeNode<T> leftRight = left.GetRight();
            TreeNode<T> parent = node.GetPadre();
            TreeNode<T> leftRightRight = leftRight.GetRight();
            TreeNode<T> leftRightLeft = leftRight.GetLeft();

            leftRight.SetPadre(parent);
            node.SetLeft(leftRightRight);
            left.SetRight(leftRightLeft);
            leftRight.SetLeft(left);
            leftRight.SetRight(node);
            left.SetPadre(leftRight);
            node.SetPadre(leftRight);

            if (leftRightRight != null)
            {
                leftRightRight.SetPadre(node);
            }

            if (leftRightLeft != null)
            {
                leftRightLeft.SetPadre(left);
            }

            if (node == this.root)
            {
                SetRoot(leftRight);
            }
            else if (parent.GetLeft() == node)
            {
                parent.SetLeft(leftRight);
            }
            else
            {
                parent.SetRight(leftRight);
            }

            if (leftRight.Balance == -1)
            {
                node.Balance = 0;
                left.Balance = 1;
            }
            else if (leftRight.Balance == 0)
            {
                node.Balance = 0;
                left.Balance = 0;
            }
            else
            {
                node.Balance = -1;
                left.Balance = 0;
            }

            leftRight.Balance = 0;

            return leftRight;


        }

        #endregion

        #region Edit Element

        public bool Edit<E>(Compare<E> compare, E element, T Item)
        {
            return EditElement(root, compare, element, Item);
        }

        /// <summary>
        /// Función que permite realizar cambios en el nodo deseado.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="node"> Raiz del árbol. </param>
        /// <param name="condicion"> Delegado que determina como buscar el nodo. </param>
        /// <param name="element"> Parametro por el cual se busca el nodo. </param>
        /// <param name="Item"> Elemento ya editado. </param>
        /// <returns> True/False dependiento si pudo hacer la edición o no. </returns>
        private bool EditElement<E>(TreeNode<T> node, Compare<E> condicion, E element, T Item)
        {
            if (condicion(node.Value, element) == 0)
            {
                node.Value = Item;
                return true;
            }
            else
            {
                if (condicion(node.Value, element) > 0)
                {
                    if (node.GetLeft() == null)
                    {
                        return false;
                    }
                    else
                    {
                        return EditElement(node.GetLeft(), condicion, element, Item);
                    }
                }
                else
                {
                    if (node.GetRight() == null)
                    {
                        return false;
                    }
                    else
                    {
                        return EditElement(node.GetRight(), condicion, element, Item);
                    }
                }
            }
        }

        #endregion

        #region Search Element

        /// <summary>
        /// Sobrecarga pública de la función buscar, recibe un delegado para saber
        /// de que forma se va a buscar dentro del árbol.
        /// </summary>
        /// <typeparam name="E"> Tipo de dato del parametro de busqueda. </typeparam>
        /// <param name="compare"> Delegado que define la comparación. </param>
        /// <param name="element"> Parametro que se buscara en el árbol. </param>
        /// <returns> Retorna true or false, dependiendo si encontro o no el elemento. </returns>
        public TreeNode<T> Search<E>(Compare<E> compare, E element)
        {
            if (root == null)
            {
                return null;
            }
            else
            {
                return SearchNode(root, compare, element);
            }
        }

        /// <summary>
        /// Sobre carga privada de la función buscar, realiza la busqueda dentro del árbol
        /// haciendo comparaciones hasta encontrar el elemento que se busca.
        /// </summary>
        /// <typeparam name="E"> Tipo de dato del parametro de busqueda. </typeparam>
        /// <param name="node"> Nodo raiz que le toca recorrer. </param>
        /// <param name="condicion"> Delegado que define la comparación. </param>
        /// <param name="element"> Parametro que se buscara en el árbol. </param>
        /// <returns> Retorna true or false, dependiendo si encontro o no el elemento. </returns>
        private TreeNode<T> SearchNode<E>(TreeNode<T> node, Compare<E> condicion, E element)
        {
            if (condicion(node.Value, element) == 0)
            {
                return node;
            }
            else
            {
                if (condicion(node.Value, element) > 0)
                {
                    if (node.GetLeft() == null)
                    {
                        return null;
                    }
                    else
                    {
                        return SearchNode(node.GetLeft(), condicion, element);
                    }
                }
                else
                {
                    if (node.GetRight() == null)
                    {
                        return null;
                    }
                    else
                    {
                        return SearchNode(node.GetRight(), condicion, element);
                    }
                }
            }
        }

        /// <summary>
        /// Realiza la busqueda del elemento derecho mas a la izquierda.
        /// </summary>
        /// <param name="node"> Nodo actual. </param>
        /// <returns></returns>
        private TreeNode<T> RouteLeft(TreeNode<T> node)
        {
            if (node.GetLeft() != null)
            {
                return RouteLeft(node.GetLeft());
            }

            return node;
        }

        #endregion

        #region Eliminate Element

        public bool Eliminate(TreeNode<T> node)
        {
            bool NodeRight = node.GetRight() != null ? true : false;
            bool NodeLeft = node.GetLeft() != null ? true : false;

            /*En caso que el nodo que se desea eliminar este vacio.*/
            /*En caso que el elemento buscado no exista*/
            if (node == null)
            {
                return false;
            }

            /* Caso 1, el nodo a eliminar no posee hijos, ni derecho ni izquierdo..*/

            if (!NodeRight && !NodeLeft)
            {
                return EliminateCase1(node);
            }

            /*
             * Caso 2, el nodo a eliminar posee un hijo..
             * Posee el hijo derecho pero no el izquierdo...
             */

            if (NodeRight && !NodeLeft)
            {
                return EliminateCase2(node);
            }

            /*
             * Caso 2, el nodo a eliminar posee un hijo..
             * Posee el hijo izquierdo pero no el derecho...
             */

            if (!NodeRight && NodeLeft)
            {
                return EliminateCase2(node);
            }

            /*
             * Caso 3, el nodo a eliminar posee un hijo..
             * Posee el hijo derecho pero no el izquierdo...
             */

            if (NodeRight && NodeLeft)
            {
                return EliminateCase3(node);
            }

            return false;
        }

        /// <summary>
        /// Funcion que satisface el primer caso de la eliminación en arboles.
        /// Caso 1: El nodo a eliminar no posee hijos.
        /// </summary>
        /// <param name="node"> Nodo a eliminar. </param>
        /// <returns></returns>
        private bool EliminateCase1(TreeNode<T> node)
        {
            /*
             * Obtiene el padre del nodo a eliminar, luego obtiene los hijos de este padre
             * con el fin de tener referencia que nodo se va eliminar y de donde proviene.
             */

            //Hijo Izquierdo
            TreeNode<T> TempLeft = node.GetPadre().GetLeft();

            //Hijo Derecho
            TreeNode<T> TempRight = node.GetPadre().GetRight();

            /*
             * Verifica que nodo es el que se va a eliminar
             * y en base a eso obtiene sobre que lado (izquierdo/derecho)
             * va a realizar los cambios.
             */
            if (TempLeft == node)
            {
                /*
                 * En base al padre del nodo que se elimino,
                 * el hijo ahora apunta a null.
                 */
                node.GetPadre().SetLeft(null);
                count--;
                return true;
            }

            if (TempRight == node)
            {
                /*
                * En base al padre del nodo que se elimino,
                * el hijo ahora apunta a null.
                */
                node.GetPadre().SetRight(null);
                count--;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Función que satisface el segundo caso de la eliminación en arboles
        /// Caso 2: El nodo a eliminar posee un hijo (izquierdo/derecho).
        /// </summary>
        /// <param name="node"> Nodo a eliminar </param>
        /// <returns></returns>
        private bool EliminateCase2(TreeNode<T> node)
        {
            /*
             * Obtiene el padre del nodo a eliminar, luego obtiene los hijos de este padre
             * con el fin de tener referencia que nodo se va eliminar y de donde proviene.
             */
            // Hijo izquierdo
            TreeNode<T> TempLeft = node.GetPadre().GetLeft();
            // Hijo derecho
            TreeNode<T> TempRight = node.GetPadre().GetRight();

            /* Obtiene el hijo existente del nodo que se va a eliminar.*/
            TreeNode<T> current = node.GetLeft() != null ? node.GetLeft() : node.GetRight();

            if (TempLeft == node)
            {
                node.GetPadre().SetLeft(current);

                /* Elimina todas las referencias hacia el nodo */
                current.SetPadre(node.GetPadre());
                node.SetRight(null);
                node.SetLeft(null);
                count--;
                return true;
            }

            if (TempRight == node)
            {
                node.GetPadre().SetRight(current);

                /* Eliminando todas las referencias hacia el nodo */
                current.SetPadre(node.GetPadre());
                node.SetRight(null);
                node.SetLeft(null);
                count--;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Función que satisface el tercer caso de la eliminación en arboles
        /// Caso 3: El nodo a eliminar posee los dos hijos.
        /// </summary>
        /// <param name="node"> Nodo a eliminar. </param>
        /// <returns></returns>
        private bool EliminateCase3(TreeNode<T> node)
        {
            TreeNode<T> NodeMoreLeft = RouteLeft(node.GetRight());

            if (NodeMoreLeft != null)
            {
                /*
                 * Reemplazamos el valor del nodo que queremos eliminar por el nodo que encontramos 
                 */
                node.Value = NodeMoreLeft.Value;
                /* 
                 * Eliminar este nodo de las formas que conocemos ( caso 1, caso 2 ) 
                 */
                Eliminate(NodeMoreLeft);
                return true;
            }

            return false;
        }

        #endregion

        #region Tours of the Tree

        /// <summary>
        /// Recorrido del árbol de foma Pre-Orden
        /// </summary>
        /// <returns> Retorna una lista que contendra el orden encontrado. </returns>
        public List<T> PreOrden()
        {
            List<T> preOrden = new List<T>();
            PreOrden(root, ref preOrden);
            return preOrden;
        }


        /// <summary>
        /// Recorrido del árbol de fomra Post-Orden.
        /// </summary>
        /// <returns> Retorna una lista que contendra el orden encontrado. </returns>
        public List<T> PostOrden()
        {
            List<T> postOrden = new List<T>();
            PostOrden(root, ref postOrden);
            return postOrden;
        }

        /// <summary>
        /// Recorrido del árbol de forma In-Orden.
        /// </summary>
        /// <returns> Retorna una lista que contendra el orden encontrado. </returns>
        public List<T> InOrden()
        {
            List<T> inOrden = new List<T>();
            InOrden(root, ref inOrden);
            return inOrden;
        }

        /// <summary>
        /// Sobrecarga privada de la función de Pre-Orden se encarga de llenar 
        /// la lista recibida en el orden definido.
        /// </summary>
        /// <param name="node"> Nodo actual. </param>
        /// <param name="list"> Lista que contendra los elementos. </param>
        private void PreOrden(TreeNode<T> node, ref List<T> list)
        {
            if (node != null)
            {
                list.Add(node.Value);
                PreOrden(node.GetLeft(), ref list);
                PreOrden(node.GetRight(), ref list);
            }
        }

        /// <summary>
        /// Sobrecarga privada de la función de PostOrden se encarga de llenar
        /// la lista recibida en el orden definido.
        /// </summary>
        /// <param name="node"> Nodo actual. </param>
        /// <param name="list"> Lista que contendra los elementos. </param>
        private void PostOrden(TreeNode<T> node, ref List<T> list)
        {
            if (node != null)
            {
                PostOrden(node.GetLeft(), ref list);
                PostOrden(node.GetRight(), ref list);
                list.Add(node.Value);
            }
        }

        /// <summary>
        /// Sobrecarga privada de la función de InOrden se encarga de llenar
        /// la lista recibida en el orden definido.
        /// </summary>
        /// <param name="node"> Nodo actual. </param>
        /// <param name="list"> Lista que contendra los elementos. </param>
        private void InOrden(TreeNode<T> node, ref List<T> list)
        {
            if (node != null)
            {
                InOrden(node.GetLeft(), ref list);
                list.Add(node.Value);
                InOrden(node.GetRight(), ref list);
            }
        }
        #endregion

        #region IEnumerable<T> Members

        private IEnumerable<TreeNode<T>> Traversal(TreeNode<T> Node)
        {
            if (Node != null)
            {
                if (Node.GetLeft() != null)
                {
                    foreach (TreeNode<T> LeftNode in Traversal(Node.GetLeft()))
                        yield return LeftNode;
                }

                yield return Node;

                if (Node.GetRight() != null)
                {
                    foreach (TreeNode<T> RightNode in Traversal(Node.GetRight()))
                        yield return RightNode;
                }
            }
            else
            {
                yield return null;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (TreeNode<T> TempNode in Traversal(root))
            {
                if (TempNode != null)
                {
                    yield return TempNode.Value;
                }
                else
                {
                    yield return default(T);
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (TreeNode<T> TempNode in Traversal(root))
            {
                yield return TempNode.Value;
            }
        }

        #endregion


    }
}