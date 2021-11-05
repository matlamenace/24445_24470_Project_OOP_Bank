using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Project_OOP_Bank
{
    abstract class ActionOnAccounts //this class have the method the client and the employee can access, like doing seeing a account
    {

        public static void Transaction(ClientList clientlist, string clientname) //Create a transaction but for a client, there is a similar method in the ActionOnAccountLimited class wich allow transaction only for an employee.
        {
            Console.Clear();
            
            Console.Write("On which account do you want to do the transaction ? Type S for saving and C for current : ");
            string account = Console.ReadLine();
            bool goodentry = account == "S" || account == "C"; //goodentry is false if the employee entered something else than S or C
            if (account == "S") { account = "-savings"; }
            if (account == "C") { account = "-current"; }
            string filename = clientlist.Dictionary[clientname]; //we use the dictionnary to find the filename
            string balance = "";
            if (goodentry == false)
            {
                Console.WriteLine("Please type S or C");
                Thread.Sleep(1500);
                Transaction(clientlist, clientname);
            }

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
                Transaction(clientlist,clientname);
            }
            Console.WriteLine($"The maximum withdrawal you can make is {balance} euros");
            Console.Write("What is the amount you want to apply on the account (don't forget to use '-' if you withdraw) : ");
            //We calculate the new amount
            double amount = Convert.ToDouble(Console.ReadLine());
            double newbalance = Convert.ToDouble(balance) + amount;
            if (newbalance < 0) //checking that after the transaction, the balance won't be negative
            {
                Console.Clear();
                Console.WriteLine("You can't have a negative balance, type to continue");
                Console.ReadKey();
                Console.Clear();
                Transaction(clientlist,clientname);
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
                Transaction(clientlist,clientname);
            }
            
            
        }


        public static void SeeFile(string path) //the content of a file. This method used by an employee allow him to see the client's list, used by a client it would allow him to see his savings and current account
        {
            Console.Clear();

            try
            {
                StreamReader streamR = new StreamReader(path + ".txt");
                string textfile = "";
                textfile = streamR.ReadToEnd();
                streamR.Close();
                Console.WriteLine(textfile);


            }
            catch (Exception e)
            {
                Console.WriteLine("We have a problem connecting to the database, please try again. " + e.Message);
            }
        }
        
        
    }
}
