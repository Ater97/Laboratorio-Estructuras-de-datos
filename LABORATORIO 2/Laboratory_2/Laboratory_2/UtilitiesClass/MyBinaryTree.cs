using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratory_2.UtilitiesClass
{
    public class MyBinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public delegate int Compare<E>(T a, E b);

        //Contador de elementos
        private int count;
        //Nodo Raiz del árbol
        private TreeNode<T> root;

        /// <summary>
        /// Constructor del árbol binario.
        /// </summary>
        public MyBinaryTree()
        {
            count = 0;
            root = null;
        }

        /// <summary>
        /// Verifica si el árbol se encuentra vacio o no.
        /// </summary>
        /// <returns>Retorna True o False.</returns>
        public bool Empty()
        {
            if (root == null)
            {
                return true;
            }

            return false;
        }

        public int GetCount()
        {
            return this.count;
        }

        private void SetRoot(TreeNode<T> root)
        {
            this.root = root;
        }

        #region Add Element

        /// <summary>
        /// Sobrecarga publica de la función agregar, agrega un elemento al
        /// árbol binario.
        /// </summary>
        /// <param name="x"> Elemento a agregar del tipo T. </param>
        public void Add(T x)
        {
            TreeNode<T> newElement = new TreeNode<T>();
            newElement.Value = x;
            this.Add(newElement, root);
        }

        /// <summary>
        /// Sobrecarga privada de la función agregar, compara y decide donde debe
        /// ser agregado el elemento ingresado, lo hace de manera recursiva.
        /// </summary>
        /// <param name="value"> Nuevo nodo del árbol. </param>
        /// <param name="root"> Nodo raiz del árbol. </param>
        private void Add(TreeNode<T> value, TreeNode<T> root)
        {
            if (root == null)
            {
                /*
                 * Primer caso, en el que insertamos el primer elemento 
                 * se inserta como raiz de todo el árbol.
                 */

                this.SetRoot(value);
                this.root.SetPadre(null);
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
                        root.GetLeft().SetPadre(root);
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
                        root.GetRight().SetPadre(root);
                        count++;
                    }
                    else
                    {
                        Add(value, root.GetRight());
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
            if(Node != null)
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
                if(TempNode != null)
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