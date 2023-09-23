using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApp
{
    internal class Program
    {
        static List<Data> userData = new List<Data>();
        static List<Product> productData = new List<Product>();

        static int choice = 0;
        static int feedbackCount = 0;
        static string[] feedback = new string[20];

        static void Main(string[] args)
        {
            readUserDataFromFile();
            readProductDataFromFile();
            string returnedRole;

            while (choice != 3)
            {
                Console.Clear();
                header();
                menu();
                choice = returnOpt();

                if (choice == 2)
                {
                    Console.Clear();
                    header();
                    signUpInput();
                }

                else if (choice == 1)
                {
                    Console.Clear();
                    header();
                    returnedRole = signInInput();

                    if (returnedRole == "admin" || returnedRole == "Admin")
                    {
                        while (choice != 8)
                        {
                            Console.Clear();
                            header();
                            adminMenu();
                            choice = returnOpt();

                            if (choice == 1)
                            {
                                Console.Clear();
                                header();
                                addProductInput();
                            }
                            else if (choice == 2)
                            {
                                Console.Clear();
                                header();
                                viewList();
                                deleteProductInput();
                            }
                            else if (choice == 3)
                            {
                                Console.Clear();
                                header();
                                viewList();
                                updateProductPriceInput();
                            }
                            else if (choice == 4)
                            {
                                Console.Clear();
                                header();
                                viewList();
                                updateProductQuantityInput();
                            }
                            else if (choice == 5)
                            {
                                Console.Clear();
                                header();
                                viewList();
                            }
                            else if (choice == 6)
                            {
                                Console.Clear();
                                header();
                                viewRegisteredUsers();
                            }
                            else if (choice == 7)
                            {
                                Console.Clear();
                                header();
                                viewFeedbacks();
                            }
                        }
                    }
                }
            }
        }
        static void header()
        {
            Console.WriteLine("***********************************************************************************************************");
            Console.WriteLine("**************************************   GROCERY MANAGEMENT SYSTEM   **************************************");
            Console.WriteLine("***********************************************************************************************************");
            Console.WriteLine("___________________________________________________________________________________________________________");
        }
        static int returnOpt()
        {
           string option;
           int optionReturn = 0; ;
           Console.Write("Enter a choice: ");
           option = Console.ReadLine();

            for (int i = 0; i < option.Length; i++)
            {
                if (option[i] >= 48 && option[i] <= 57)
                {
                   optionReturn = int.Parse(option);
                }
                else
                {
                    Console.WriteLine("Enter a valid choice!");
                    Console.ReadKey();
                    break;
                }
            }
            return optionReturn;
        }
        static void menu()
        {
            Console.WriteLine("1. \t Sign In.");
            Console.WriteLine("2. \t Sign Up.");
            Console.WriteLine("3. \t Exit.");
        }
        static void adminMenu()
        {
            Console.WriteLine("1. \t Add a New Product.");
            Console.WriteLine("2. \t Delete a Product.");
            Console.WriteLine("3. \t Update Price of a Product.");
            Console.WriteLine("4. \t Update Quantity of a Product.");
            Console.WriteLine("5. \t View List of all items in Stock.");
            Console.WriteLine("6. \t Check Registered Users.");
            Console.WriteLine("7. \t View Customers' Feedbacks.");
            Console.WriteLine("8. \t Exit to Login Menu.");
        }
        static void customerMenu()
        {
            Console.WriteLine("1. \t Add a Product to Cart");
            Console.WriteLine("2. \t Remove a Product from Cart.");
            Console.WriteLine("3. \t View the Cart.");
            Console.WriteLine("4. \t View List of all Available Items.");
            Console.WriteLine("5. \t Checkout.");
            Console.WriteLine("6. \t Change your Password.");
            Console.WriteLine("7. \t Delete your Account.");
            Console.WriteLine("8. \t Give Feedback. ");
            Console.WriteLine("9. \t Exit to Login Menu.");
        }

        static void signUpInput()
        {
            Data signUpData = new Data();
            bool isSignedUp;
            bool isComma;
            bool isEmail;

            Console.Write("Enter Username: ");
            signUpData.usernames = Console.ReadLine();
            Console.Write("Enter password: ");
            signUpData.passwords = Console.ReadLine();
            Console.Write("Enter Email: ");
            signUpData.emails = Console.ReadLine();
            Console.Write("Enter Roles(Admin for now): ");
            signUpData.roles = Console.ReadLine();

            isComma = checkComma(signUpData.usernames);

            if(isComma)
            {
                isEmail = checkEmail(signUpData.emails);
                if(isEmail)
                {
                    isSignedUp = signUpStore(ref signUpData);
                    if (isSignedUp)
                    {
                        Console.WriteLine("Signed Up successfully!");
                        Console.ReadKey();
                    }
                    else if (!isSignedUp) 
                    {
                        Console.WriteLine("User limit exceeded!");
                        Console.ReadKey();
                    }
                }
                else if(!isEmail)
                {
                    Console.WriteLine("Enter a valid Email address!");
                    Console.ReadKey();
                }
            }
            else if(!isComma)
            {
                Console.WriteLine("Comma not allowed!");
                Console.ReadKey();
            }
        }
        static bool signUpStore(ref Data signUpData)
        {
            if (userData.Count < 10)
            {
                userData.Add(signUpData);
                saveUserDataIntoFile();
                return true;
            }
            else
            {
                return false;
            }
        }
        static string signInInput()
        {
            string usernames;
            string passwords;
            string isRole;

            Console.Write("Enter username: ");
            usernames = Console.ReadLine();
            Console.Write("Enter password: ");
            passwords = Console.ReadLine();

            isRole = signIn(usernames, passwords);
            if (isRole == "Admin" || isRole == "admin" || isRole == "Customer" || isRole == "customer")
            {
                return isRole;
            }
            else
            {
                Console.Write("User does not exist!");
                return isRole;
                Console.ReadKey();
            }

        }
        static string signIn(string username, string password)
        {
            for (int i = 0; i < userData.Count; i++)
            {
                if (username == userData[i].usernames && password == userData[i].passwords) 
                {
                    return userData[i].roles;
                }
            }
            return "";
        }

        static void addProductInput()
        {
            Product prod = new Product();
            string productIDAdmin;
            string productQuantityAdmin;
            string productPriceAdmin;

            Console.Write("Enter Product ID: ");
            productIDAdmin = Console.ReadLine();
            Console.Write("Enter Product Name: ");
            prod.productName = Console.ReadLine();
            Console.Write("Enter Product Quantity: ");
            productQuantityAdmin = Console.ReadLine();
            Console.Write("Enter Product Price: ");
            productPriceAdmin = Console.ReadLine();

            prod.productID = IDValidationCheck(productIDAdmin);
            prod.productPrice = PriceValidationCheck(productPriceAdmin);
            prod.productQuantity = QuantityValidationCheck(productQuantityAdmin);

            bool isStored = addProductStore(ref prod);
            if (isStored)
            {
                Console.Write("Product Added Successfully!");
                Console.ReadKey();
            }
        }
        static bool addProductStore(ref Product prod)
        {
            bool isStored = false;
            if (prod.productID != 0 && prod.productQuantity != 0 && prod.productPrice != 0) 
            {
                if (productData.Count < 50) 
                {
                    productData.Add(prod);
                    saveProductDataIntoFile();
                    isStored = true;
                }
            }
            return isStored;
        }
        static void deleteProductInput()
        {
            Product prod = new Product();

            Console.Write("Enter Product ID to Delete: ");
            prod.productID = int.Parse(Console.ReadLine());

            bool isDeleted = deleteProduct(ref prod);
            if (isDeleted)
            {
                Console.WriteLine("Item deleted Successfully!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Item does not exist!");
                Console.ReadKey();
            }
        }
        static bool deleteProduct(ref Product prod)
        {
            bool isFound = false;

            if (prod.productID != 0)
            {
                int idx = 0;
                for (int i = 0; i < productData.Count; i++)
                {
                    if (prod.productID == productData[i].productID)
                    {
                        idx = i;
                        isFound = true;
                    }
                }

                for (int i = idx; i < productData.Count; i++)
                {
                    productData.RemoveAt(i);
                    saveProductDataIntoFile();
                    break;
                }
            }
            return isFound;
        }
        static void updateProductPriceInput()
        {
            Product prod = new Product();

            Console.Write("Enter Product ID: ");
            prod.productID = int.Parse(Console.ReadLine());

            for (int i = 0; i < productData.Count; i++)
            {
                if (prod.productID == productData[i].productID)
                {
                    Console.WriteLine("Product Name: {0}", productData[i].productName);
                    Console.WriteLine("Product Previous Price: {0}", productData[i].productPrice);
                }
            }

            Console.Write("Enter New Price: ");
            prod.productPrice = int.Parse(Console.ReadLine());

            bool isUpdated = updateProductPrice(ref prod);
            if (isUpdated)
            {
                Console.WriteLine("Price Updated Successfully!");
                Console.ReadKey();
            }
            else if (!isUpdated)
            {
                Console.WriteLine("Price could not be updated!");
                Console.ReadKey();
            }
        }
        static bool updateProductPrice(ref Product prod)
        {
            bool isUpdated = false;

            if (prod.productPrice != 0)
            {
                for (int i = 0; i < productData.Count; i++)
                {
                    if (prod.productID == productData[i].productID)
                    {
                        productData[i].productPrice = prod.productPrice;
                        saveProductDataIntoFile();
                        isUpdated = true;
                    }
                }
            }
            return isUpdated;
        }
        static void updateProductQuantityInput()
        {
            Product prod = new Product();

            Console.Write("Enter Product ID: ");
            prod.productID = int.Parse(Console.ReadLine());

            for (int i = 0; i < productData.Count; i++)
            {
                if (prod.productID == productData[i].productID)
                {
                    Console.Write("Product Name: {0}", productData[i].productName);
                    Console.Write("Product Previous Quantity: {0}", productData[i].productQuantity);
                }
            }

            Console.Write("Enter New Quantity: ");
            prod.productQuantity = int.Parse(Console.ReadLine());

            bool isUpdated = updateProductQuantity(ref prod);
            if (isUpdated)
            {
                Console.WriteLine("Quantity Updated Successfully!");
                Console.ReadKey();
            }
            else if (!isUpdated)
            {
                Console.WriteLine("Quantity could not be updated!");
                Console.ReadKey();
            }
        }
        static bool updateProductQuantity(ref Product prod)
        {
            bool isUpdated = false;

            if (prod.productID != 0)
            {
                for (int i = 0; i < productData.Count; i++)
                {
                    if (prod.productID == productData[i].productID)
                    {
                        productData[i].productQuantity = prod.productQuantity;
                        saveProductDataIntoFile();
                        isUpdated = true;
                    }
                }
            }
            return isUpdated;
        }
        static void viewList()
        {
            Console.WriteLine("Product ID \t Product Name \t Product Quantity \t Product Price");
            for (int i = 0; i < productData.Count; i++)
            {
                Console.WriteLine(productData[i].productID.ToString() + "\t\t" + " " + productData[i].productName + "\t\t" + " " + productData[i].productQuantity.ToString() + "\t\t\t" + " " + productData[i].productPrice.ToString());
            }
            Console.ReadKey();
        }
        static void viewRegisteredUsers()
        {
            int count = 0;
            for (int i = 0; i < userData.Count; i++)
            {
                if (userData[i].roles == "customer" || userData[i].roles == "Customer")
                {
                    Console.WriteLine("Username: {0}", userData[i].usernames);
                    Console.WriteLine("Role: {0}", userData[i].roles);
                    count++;
                }
            }
            Console.WriteLine("Total Users Registered: {0} ", count);
            Console.ReadKey();
        }
        static void viewFeedbacks()
        {
            Console.WriteLine("FEEDBACKS!");
            for (int i = 0; i < feedbackCount; i++)
            {
                Console.WriteLine(i + 1 + "\t" + feedback[i]);
            }
            Console.ReadKey();
        }

        static void saveUserDataIntoFile()
        {
            string path = "D:\\UET BS-CS\\SEMESTER 02\\PD\\Week1\\userData.txt";
            StreamWriter userDataFile = new StreamWriter(path,false);
            for (int i = 0; i < userData.Count; i++)
            {
                userDataFile.WriteLine(userData[i].usernames + "," + userData[i].passwords + "," + userData[i].emails + "," + userData[i].roles);
                userDataFile.Flush();
            }
            userDataFile.Close();
        }
        static void readUserDataFromFile()
        {
            string path = "D:\\UET BS-CS\\SEMESTER 02\\PD\\Week1\\userData.txt";
            string record;

            if (File.Exists(path))
            {
                StreamReader userDataFile = new StreamReader(path);
                while ((record = userDataFile.ReadLine()) != null)
                {
                    Data readData = new Data();
                    readData.usernames = parseData(record, 1);
                    readData.passwords = parseData(record, 2);
                    readData.emails = parseData(record, 3);
                    readData.roles = parseData(record, 4);
                    userData.Add(readData);

                    if (userData.Count >= 10)
                    {
                        break;
                    }
                }
                userDataFile.Close();
            }
            else
            {
                Console.WriteLine("User does not exist!");
                Console.ReadKey();
            }
        }
        static string parseData(string record, int count)
        {
            int commaCount = 1;
            string item = "";

            for (int i = 0; i < record.Length; i++)
            {
                if (record[i] == ',')
                {
                    commaCount++;
                }
                else if (commaCount == count)
                {
                    item = item + record[i];
                }
            }
            return item;
        }
        static void saveProductDataIntoFile()
        {
            string path = "D:\\UET BS-CS\\SEMESTER 02\\PD\\Week1\\productData.txt";
            StreamWriter productDataFile = new StreamWriter(path);

            for (int i = 0; i < productData.Count; i++)
            {
                productDataFile.WriteLine(productData[i].productID + "," + productData[i].productName + "," + productData[i].productQuantity + "," + productData[i].productPrice);
                productDataFile.Flush();
            }
            productDataFile.Close();
        }
        static void readProductDataFromFile()
        {
            string path = "D:\\UET BS-CS\\SEMESTER 02\\PD\\Week1\\productData.txt";
            string record;

            if (File.Exists(path))
            {
                StreamReader productDataFile = new StreamReader(path);
                while ((record = productDataFile.ReadLine()) != null)
                {
                    Product products = new Product();
                    products.productID = int.Parse(parseData(record, 1));
                    products.productName = parseData(record, 2);
                    products.productQuantity = int.Parse(parseData(record, 3));
                    products.productPrice = int.Parse(parseData(record, 4));
                    productData.Add(products);
                    if (productData.Count >= 50)
                    {
                        break;
                    }
                }
                productDataFile.Close();
            }
            else
            {
                Console.WriteLine("Product does not exist!");
                Console.ReadKey();
            }
        }

        static int IDValidationCheck(string productIDAdmin)
        {
            int intID = 0;
            bool temp = false;

            for (int i = 0; i < productIDAdmin.Length; i++)
            {
                if (productIDAdmin[i] >= 48 && productIDAdmin[i] <= 57)
                {
                    temp = true;
                }
            }

            if (temp)
            {
                intID = int.Parse(productIDAdmin);
            }
            else if (!temp)
            {
                Console.WriteLine("Enter a valid ID!");
                Console.ReadKey();
            }
            return intID;
        }
        static int QuantityValidationCheck(string productQuantityAdmin)
        {
            int intQuantity = 0;
            bool temp = false;

            for (int i = 0; i < productQuantityAdmin.Length; i++)
            {
                if (productQuantityAdmin[i] >= 48 && productQuantityAdmin[i] <= 57)
                {
                    temp = true;
                }
            }

            if (temp)
            {
                intQuantity = int.Parse(productQuantityAdmin);
            }
            else if (!temp)
            {
                Console.WriteLine("Enter a valid Quantity!");
                Console.ReadKey();
            }
            return intQuantity;
        }
        static int PriceValidationCheck(string productPriceAdmin)
        {
            int intPrice = 0;
            bool temp = false;

            for (int i = 0; i < productPriceAdmin.Length; i++)
            {
                if (productPriceAdmin[i] >= 48 && productPriceAdmin[i] <= 57)
                {
                    temp = true;
                }
            }

            if (temp)
            {
                intPrice = int.Parse(productPriceAdmin);
            }
            else if (!temp)
            {
                Console.WriteLine("Enter a valid Price!");
                Console.ReadKey();
            }
            return intPrice;
        }
        static bool checkComma(string usernames)
        {
            bool temp = true;

            for (int i = 0; i < usernames.Length; i++)
            {
                if (usernames[i] == 44)
                {
                    temp = false;
                }
            }
            return temp;
        }
        static bool checkEmail(string emails)
        {
            bool isEmail = false;

            for (int i = 0; i < emails.Length; i++)
            {
                if (emails[i] == 64 && emails[emails.Length - 1] == 'm' && emails[emails.Length - 2] == 'o' && emails[emails.Length - 3] == 'c' && emails[emails.Length - 4] == '.')
                {
                    isEmail = true;
                    break;
                }
            }
            return isEmail;
        }
    }
}