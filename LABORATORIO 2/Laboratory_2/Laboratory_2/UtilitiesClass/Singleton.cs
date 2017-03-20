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
        public MyBinaryTree<ProductModel> ProductsBinaryTree = new MyBinaryTree<ProductModel>();
        public MyBinaryTreeAVL<BillsModel> BillsBinaryTree = new MyBinaryTreeAVL<BillsModel>();
        public string TempBillId = "";
        public string[] paths = { "", "" };
        public bool[] flags = { false, false, false };
    }
}