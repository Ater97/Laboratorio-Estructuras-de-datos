using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Laboratory_2.Models;

namespace Laboratory_2.UtilitiesClass
{
    public class Singleton
    {
        private static Singleton _instance;

        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Singleton();
                }
                return _instance;
            }
        }
        BinaryTree<ProductModel> ProductsBinaryTree = new BinaryTree<ProductModel>();

    }
}