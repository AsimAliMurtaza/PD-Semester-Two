using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessApp
{
    internal class Program
    {
        static List<Product> productData = new List<Product>();
        static List<User> userData = new List<User>();

        static int choice = 0;
        static int feedbackCount = 0;
        static string[] feedback = new string[20];

        static void Main(string[] args)
        {
            readUserDataFromFile();
            readProductDataFromFile();
            User returnedUser;

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
                    returnedUser = signInInput();

                    if (returnedUser.roles == "admin" || returnedUser.roles == "Admin")
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
                    if (returnedUser.roles == "customer" || returnedUser.roles == "Customer")
                    {
                        string name = returnedUser.usernames;
                        while (choice != 9)
                        {
                            Console.Clear();
                            header();
                            customerMenu();
                            choice = returnOpt();

                            if (choice == 1)
                            {
                                Console.Clear();
                                header();
                                viewList();
                                addProductCustomerInput(name);
                            }
                            if (choice == 2)
                            {
                                Console.Clear();
                                header();
                                removeProductCustomerInput(name);
                            }
                            if (choice == 3)
                            {
                                Console.Clear();
                                header();
                                viewCart(name);
                            }
                            if (choice == 4)
                            {
                                Console.Clear();
                                header();
                                viewList();
                            }
                            if (choice == 5)
                            {
                                Console.Clear();
                                header();
                                viewCart(name);
                                checkoutInput(name);
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
            bool isSignedUp;
            bool isComma;
            bool isEmail;

            Console.Write("Enter Username: ");
            string usernames = Console.ReadLine();
            Console.Write("Enter password: ");
            string passwords = Console.ReadLine();
            Console.Write("Enter Email: ");
            string emails = Console.ReadLine();
            Console.Write("Enter Roles(Admin for now): ");
            string roles = Console.ReadLine();

            isComma = checkComma(usernames);

            if(isComma)
            {
                isEmail = checkEmail(emails);
                if(isEmail)
                {
                    User signUpData = new User(usernames, passwords, emails, roles);
                    isSignedUp = signUpData.signUpStore(ref signUpData, userData);

                    if (isSignedUp)
                    {
                        saveUserDataIntoFile();
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
        static User signInInput()
        {
            string usernames;
            string passwords;

            Console.Write("Enter username: ");
            usernames = Console.ReadLine();
            Console.Write("Enter password: ");
            passwords = Console.ReadLine();

            User signInData = new User(usernames, passwords);
            User user = signInData.signIn(usernames, passwords, userData);
            return user;
        }

        static void addProductInput()
        {
            string productIDAdmin;
            string productName;
            string productQuantityAdmin;
            string productPriceAdmin;

            Console.Write("Enter Product ID: ");
            productIDAdmin = Console.ReadLine();
            Console.Write("Enter Product Name: ");
            productName = Console.ReadLine();
            Console.Write("Enter Product Quantity: ");
            productQuantityAdmin = Console.ReadLine();
            Console.Write("Enter Product Price: ");
            productPriceAdmin = Console.ReadLine();

            Product prod = new Product(IDValidationCheck(productIDAdmin),productName, PriceValidationCheck(productPriceAdmin), QuantityValidationCheck(productQuantityAdmin));

            bool isStored = prod.addProductStore(ref prod, productData);
            if (isStored)
            {
                Console.Write("Product Added Successfully!");
                Console.ReadKey();
            }
        }
        static void deleteProductInput()
        {
            Console.Write("Enter Product ID to Delete: ");
            int productID = int.Parse(Console.ReadLine());

            Product prod = new Product(productID);
            bool isDeleted = prod.deleteProduct(ref productID, productData);

            if (isDeleted)
            {
                saveProductDataIntoFile();
                Console.WriteLine("Item deleted Successfully!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Item does not exist!");
                Console.ReadKey();
            }
        }

        static void updateProductPriceInput()
        {
            Console.Write("Enter Product ID: ");
            int productID = int.Parse(Console.ReadLine());

            for (int i = 0; i < productData.Count; i++)
            {
                if (productID == productData[i].productID)
                {
                    Console.WriteLine("Product Name: {0}", productData[i].productName);
                    Console.WriteLine("Product Previous Price: {0}", productData[i].productPrice);
                }
            }

            Console.Write("Enter New Price: ");
            float productPrice = float.Parse(Console.ReadLine());

            Product prod = new Product(productID);
            bool isUpdated = prod.updateProductPrice(ref productID, ref productPrice, productData);

            if (isUpdated)
            {
                saveProductDataIntoFile();
                Console.WriteLine("Price Updated Successfully!");
                Console.ReadKey();
            }
            else if (!isUpdated)
            {
                Console.WriteLine("Price could not be updated!");
                Console.ReadKey();
            }
        }
        static void updateProductQuantityInput()
        {
            Console.Write("Enter Product ID: ");
            int productID = int.Parse(Console.ReadLine());

            for (int i = 0; i < productData.Count; i++)
            {
                if (productID == productData[i].productID)
                {
                    Console.WriteLine("Product Name: {0}", productData[i].productName);
                    Console.WriteLine("Product Previous Quantity: {0}", productData[i].productQuantity);
                }
            }

            Console.Write("Enter New Quantity: ");
            int productQuantity = int.Parse(Console.ReadLine());

            Product prod = new Product(productID);
            bool isUpdated = prod.updateProductQuantity(ref productID, ref productQuantity, productData);

            if (isUpdated)
            {
                saveProductDataIntoFile();
                Console.WriteLine("Quantity Updated Successfully!");
                Console.ReadKey();
            }
            else if (!isUpdated)
            {
                Console.WriteLine("Quantity could not be updated!");
                Console.ReadKey();
            }
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
                    User readData = new User(parseData(record, 1), parseData(record, 2), parseData(record, 3), parseData(record, 4));
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
                    Product products = new Product(int.Parse(parseData(record, 1)), parseData(record, 2), int.Parse(parseData(record, 3)), int.Parse(parseData(record, 4)));
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

        static void addProductCustomerInput(string name)
        {
            string productName;
            string productQuantity;

            Console.Write("Enter Product Name: ");
            productName = Console.ReadLine();
            productName = productName.ToLower();
            Console.Write("Enter quantity of Product: ");
            productQuantity = Console.ReadLine();

            bool isNameValid = checkName(productName);
            int productQuantityInt = checkQuantity(productQuantity);
            if(isNameValid)
            {
                if(productQuantityInt != 0)
                {
                    for(int i = 0; i<productData.Count; i++)
                    {
                        if(productName == productData[i].productName)
                        {
                            foreach(var customer in userData)
                            {
                                if(customer.usernames == name)
                                {
                                    customer.addProductCart(productData[i]);
                                    for(int j = 0; j < customer.products.Count; j++)
                                    {
                                        if (productData[i].productName == customer.products[j].productName)
                                        {
                                            customer.products[j].productQuantity = productQuantityInt;
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
        static void removeProductCustomerInput(string name)
        {
            Console.Write("Enter Product Index: ");
            int index = int.Parse(Console.ReadLine());

            foreach (var customer in userData)
            {
                if (customer.usernames == name)
                {
                    customer.removeProductCart(productData[index]);
                    break;
                }
            }
        }

        static bool checkName(string nameItem)
        {
            for (int i = 0; i < productData.Count; i++)
            {
                if (nameItem == productData[i].productName)
                {
                    return true;
                }
            }
            return false;
        }
        static void viewCart(string name)
        {
            Console.WriteLine("\t\t Items in Cart");
            Console.WriteLine("ProductID \tProduct Name \tProduct Quantity");
            foreach(var i in userData)
            {
                if(i.usernames == name)
                {
                    foreach(var prod in i.products)
                    {
                        Console.WriteLine(prod.productID + "\t\t" + prod.productName + "\t\t" + prod.productQuantity);
                    }
                }
            }
            Console.ReadKey();
        }

        static void checkoutInput(string name)
        {
            Console.WriteLine("Prepare Bill? (Y/N): ");
            char yesOrNo = char.Parse(Console.ReadLine());

            if(yesOrNo == 'Y' || yesOrNo == 'y')
            {
                prepareBill(name);
            }
            else if(yesOrNo == 'n' || yesOrNo == 'N')
            {
                foreach(var i in userData)
                {
                    if(i.usernames == name)
                    {
                        i.products.Clear();
                    }
                }
            }    
            else { Console.WriteLine("Invalid Input!"); }
        }
        static void prepareBill(string name)
        {
            float totalAmount = 0;
            Console.WriteLine("\tTotal Bill");
            Console.WriteLine("Name\tQuantity\tPrice");
            foreach (var i in userData)
            {
                if (i.usernames == name)
                {
                    foreach(var prod in i.products)
                    {
                        Console.WriteLine(prod.productName + "\t" + prod.productQuantity + "\t\t" + prod.productPrice);
                        totalAmount = totalAmount + (prod.productPrice * prod.productQuantity);
                    }
                }
            }
            float discount = totalAmount * 0.05F;          // 5% fix discount.
            float payableAmount = totalAmount - discount; // payable amount after discount
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Total amount: " + totalAmount);
            Console.WriteLine("Discount: "+ discount);
            Console.WriteLine("Payable: "+ payableAmount);
            Console.ReadKey();
        }

        static int checkQuantity(string QuantityStr)
        {
            bool temp = false;
            int Quantity;

            for (int i = 0; i < QuantityStr.Length; i++)
            {
                if (QuantityStr[i] >= 48 && QuantityStr[i] <= 57)
                {
                    temp = true;
                }
            }
            if (temp)
            {
                Quantity = int.Parse(QuantityStr);
                return Quantity;
            }
            return 0;
        }
    }
}