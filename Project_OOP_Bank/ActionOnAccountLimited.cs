using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Project_OOP_Bank
{
    class ActionOnAccountLimited : ActionOnAccounts //This class heritates from the ActionOnAccount abtract class, in this class there are only methods that only an employee can access
    {
        
        public static void DeleteClient(ClientList clientlist) //This method is the first called when we need to delete a client, it automatically call DeleteClientFromFileList(clientlist, dictionnaryKey), DeleteClientFiles(clientlist, dictionnaryKey), and DeleteClientFromDictionary(clientlist, dictionnaryKey) to supress the client from every data we have
        {
            Console.Clear();
            Console.Write("Type the first name of who you want to delete : ");
            string fname = Console.ReadLine();
            Console.Clear();
            Console.Write("Type the last name of who you want to delete : ");
            string lname = Console.ReadLine();
            string dictionnaryKey = fname + lname;
            DeleteClientFromFileList(clientlist, dictionnaryKey); //this method delete the client from ClientFileList.txt
            DeleteClientFiles(clientlist, dictionnaryKey); //this method delete the client's files savings and current
            DeleteClientFromDictionary(clientlist, dictionnaryKey); //this method delete the client from the dictionnary
        }
        public static void DeleteClientFromDictionary(ClientList clientlist, string dictionaryKey) 
        {
            string clientnumber = "";
            //This condition is simply here to be able to have the client number that is going to be removed in a variable and to keep it after it was removed in order to confirm the suppression to the employee
            clientnumber = clientlist.Dictionary[dictionaryKey];
            //Dictionnary.Remove try to remove the item specified in the parameters, and return true if he succeded, false if he couldn't do it
            if (clientlist.Dictionary.Remove(dictionaryKey) == false) //This condition is here to react if Dictionnary.Remove didn't succeded, it would mean that the key specified by the employee doesn't exist
            {
                Console.WriteLine("The removal was unsuccessfull, type on the keyboard to try again");
                Console.ReadKey();
                Console.Clear();
                DeleteClient(clientlist);

            }

            //Confirmation of success
            Console.WriteLine($"The client {clientnumber} was removed from all list, type on the keyboard to continue");
            Console.ReadKey();
            Console.Clear();
        }
        public static void DeleteClientFromFileList(ClientList clientlist, string name) //this method delete the client from ClientFileList.txt
        {
            Console.Clear();
            if (clientlist.Dictionary.ContainsKey(name) != true) //test if the name that the employee entered is right
            {
                Console.WriteLine("The client's name doesn't exist in our files, please try again");
                Thread.Sleep(1500);
                Console.Clear();
                DeleteClient(clientlist); //if not we start again the main delete method to get the right names
            }
            string ANDnumber = clientlist.Dictionary[name];
            //Here we are placing every lines in the file into a list<string> which will allow us to delete the lines that interest us 
            List<string> clientList = File.ReadAllLines("AccountFileList.txt").ToList();
            int rank = 0;
            for (int i = 0; i < clientList.Count(); i++)
            {
                if (clientList[i].Contains(ANDnumber))
                {
                    rank = i;
                }
            }
            clientList.RemoveAt(rank);
            File.WriteAllLines("AccountFileList.txt", clientList);
        }
        public static void DeleteClientFiles(ClientList clientlist,string name) //this method delete the client's files savings and current
        {
            string ANDnumber = clientlist.Dictionary[name];
            File.Delete(ANDnumber + "-savings.txt");
            File.Delete(ANDnumber + "-current.txt");
        }
        public static void NewClient(ClientList clientList) //Method to add a new client 
        {
            Console.Clear();
            //Information about the client
            Console.Write("Type the client's first name : ");
            string firstname = Console.ReadLine();
            firstname=Char.ToUpper(firstname[0])+firstname.Remove(0,1);
            Console.Write("Type the client's family name : ");
            string familyname = Console.ReadLine();
            familyname=Char.ToUpper(familyname[0])+familyname.Remove(0,1);
            //We create a new client file that will automatically create a saving and a current file, add the client to the dictionary and the file clientlist
            ClientFile newclient = new ClientFile(firstname, familyname, clientList);
            Console.WriteLine($"{firstname} {familyname} was added to the clients list, type to continue.");
            Console.ReadKey();
            Console.Clear();
        }
        public static void Transaction(ClientList clientlist) //Create a transaction but for an employee (the method for client is on ActionOnAccounts class)
        {
            Console.Clear();
            //Client's infos
            Console.Write("Enter  the client's first name : ");
            string clientFirstName = Console.ReadLine();
            Console.Write("Enter the client's family name : ");
            string clientFamName = Console.ReadLine();
            string clientname = clientFirstName + clientFamName;
            Console.Write("On which account do you want to do the transaction ? Type S for saving and C for current : ");
            string account = Console.ReadLine();
            bool goodentry = account == "S" || account == "C"; //goodentry is false if the employee entered something else than S or C
            if (clientlist.Dictionary.ContainsKey(clientname) && goodentry) //checking that the client's informations entered by the employee exist, and that goodentry is true
            {
                if (account == "S") { account = "-savings"; }
                if (account == "C") { account = "-current"; }

                string filename = clientlist.Dictionary[clientname]; //we use the dictionnary to find the filename
                string balance = "";
                try
                {
                    StreamReader sr = new StreamReader(filename + account + ".txt");//Open a streamreader
                    //The streamreader read the last line, wich correspond to the client's balance
                    while (sr.EndOfStream == false)
                    {
                        balance = sr.ReadLine();
                    }
                    sr.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("The program can't read the file" + e.Message);
                    //If the stream doesn't work, we start again the method
                    Transaction(clientlist);
                }
                Console.WriteLine($"The maximum withdrawal you can make is {balance} euros");
                Console.Write("What is the amount you want to apply on the account (don't forget to use '-' if you withdraw) : ");
                //We calculate the new amount
                double amount = Convert.ToDouble(Console.ReadLine());
                double newbalance = Convert.ToDouble(balance) + amount;
                if (newbalance < 0) //checking that after the transaction, the balance won't be negative
                {
                    Console.Clear();
                    Console.WriteLine("Client can't have a negative balance, type to continue");
                    Console.ReadKey();
                    Console.Clear();
                    Transaction(clientlist);
                }
                try
                {
                    StreamWriter sw = new StreamWriter(filename + account + ".txt", true); //Open a streamwriter to write the transaction on the savings or current file
                    if (amount < 0)
                    {
                        sw.WriteLine('\n' + Convert.ToString(DateTime.Now) + " withdraw " + amount + '\n' + newbalance);//if withdraw
                    }
                    if (amount > 0)
                    {
                        sw.WriteLine('\n' + Convert.ToString(DateTime.Now) + " deposit " + amount + '\n' + newbalance);//if deposit
                    }
                    sw.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("The program can't write on the file" + e.Message + " type on the keyboard to continue");
                    Console.ReadKey();
                    Transaction(clientlist);
                }
            }
            else //if what entered the employee at the begininning is wrong
            {
                Console.Clear();
                Console.WriteLine("The client's informations are wrong or you didn't choose the right account.");
                Thread.Sleep(1200);
                Transaction(clientlist);
            }
        }
    }
}
