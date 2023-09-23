using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp
{
    internal class Product
    {
        public int productID;
        public string productName;
        public int productPrice;
        public int productQuantity;

        public Product(int ID, string n, int p, int q) 
        {
            productID = ID;
            productName = n;
            productPrice = p;
            productQuantity = q;
        }
        public Product(int ID)
        {
            productID = ID;
        }

        public bool addProductStore(ref Product prod, List<Product> productData)
        {
            bool isStored = false;
            if (prod.productID != 0 && prod.productQuantity != 0 && prod.productPrice != 0)
            {
                if (productData.Count < 50)
                {
                    productData.Add(prod);
                    isStored = true;
                }
            }
            return isStored;
        }
        public bool deleteProduct(ref int ID, List<Product> prod)
        {
            bool isFound = false;
            int count = 0;

            if (ID != 0)
            {
                int idx = 0;
                foreach (var i in prod)
                {
                    if (ID == i.productID)
                    {
                        idx = i.productID;
                        isFound = true;
                        break;
                    }
                    count++;
                }
                if (isFound)
                {
                    prod.RemoveAt(count);
                }
            }
            return isFound;
        }
        public bool updateProductPrice(ref int ID, ref int productPrice, List<Product> productData)
        {
            bool isUpdated = false;

            if (productPrice != 0)
            {
                for (int i = 0; i < productData.Count; i++)
                {
                    if (ID == productData[i].productID)
                    {
                        productData[i].productPrice = productPrice;
                        isUpdated = true;
                    }
                }
            }
            return isUpdated;
        }
        public bool updateProductQuantity(ref int ID, ref int productQuantity, List<Product> productData)
        {
            bool isUpdated = false;

            if (productID != 0)
            {
                for (int i = 0; i < productData.Count; i++)
                {
                    if (productID == productData[i].productID)
                    {
                        productData[i].productQuantity = productQuantity;
                        isUpdated = true;
                    }
                }
            }
            return isUpdated;
        }
    }
}
