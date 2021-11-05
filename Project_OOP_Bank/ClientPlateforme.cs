using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Project_OOP_Bank
{
    class ClientPlateforme //instantiated by MainMenu class, here to propose to the client actions to do and to instantiate classes that will provide the actions
    {
        string firstname { get; set; }
        string familyname { get; set; }
        string accountAND { get; set; }
        string pin { get; set; }

        public ClientPlateforme(ClientList clientlist) 
        {
            if (Entry(clientlist)) 
            {
                MenuForClients(clientlist);
            }
        }
        private bool Entry(ClientList clientlist) //Asking the client his entry informations 
        {
            Console.Clear();
            Console.Write("Enter your first name : ");
            string firstname=Console.ReadLine();
            Console.Clear();
            Console.Write("Enter your family name : ");
            string familyname = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter your four digit PIN : ");
            string pin = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter your account AND : ");
            string accountAND = Console.ReadLine();
            Console.Clear();
            return EntryCheck(firstname, familyname,accountAND, clientlist,pin);
        }
        private bool EntryCheck(string firstname, string familyname, string accountAND, ClientList clientlist, string pin) //check if the informations entered by the client correspond to the informations that the bank have on file
        {
            if (clientlist.Dictionary.ContainsKey(firstname+familyname)==false) //check if the client's name correspond to a name that we have on the list
            {
                Console.WriteLine("Connexion didn't succeded, please try again"); Thread.Sleep(2000); Entry(clientlist); return false;
            }
            if (clientlist.Dictionary[firstname+familyname] == accountAND) //Checking if the AND that the client gave correspond to the name
            {
                this.firstname = firstname;
                this.familyname = familyname;
                this.pin = pin;
                this.accountAND = accountAND;
                Console.WriteLine("Connexion has succeded");
                Thread.Sleep(1000);
                return true;
            }
            else { Console.WriteLine("The informations you entered are not correct"); Entry(clientlist); return false; }
        }
        
        private void MenuForClients(ClientList clientlist) //This method is the main menu for client, there is a very similar method in EmployeePlateform
        {
            Console.Clear();
            Console.WriteLine("If you want to see your accounts, type 'seeaccount'" +'\n' +"If you want to add a transaction, type 'transaction'" + '\n' + "If you want to disconnect, type 'exit'");
            string clientanswer = Console.ReadLine().ToLower();
            if (clientanswer != "seeaccount" && clientanswer != "transaction" && clientanswer != "exit")//checking if the string the client entered correspond to a word we asked
            {
                Console.WriteLine("We can't guess what you want");
                MenuForClients(clientlist);
            }
            if (clientanswer == "seeaccount")
            {
                Console.WriteLine("Which account do you want to see ? Type 'current' or 'savings'");
                string clientanswer1 = Console.ReadLine();
                if (clientanswer1 != "current" && clientanswer1 != "savings") 
                {
                    Console.Clear();
                    Console.WriteLine("Please type 'current' or 'savings'");
                    Thread.Sleep(1500);
                    MenuForClients(clientlist);
                }
                Console.Clear();
                Console.WriteLine($"Here are your transactions on your {clientanswer1} : " + '\n' + '\n');
                ActionOnAccounts.SeeFile(accountAND + "-" + clientanswer1);
                Console.WriteLine("Type to continue");
                Console.ReadKey();
                Console.Clear();
                MenuForClients(clientlist);
            }
            if (clientanswer == "transaction")
            {
                ActionOnAccounts.Transaction(clientlist, firstname+familyname);
                MenuForClients(clientlist);
            }
            if (clientanswer == "exit")
            {
                Console.Clear();
                MainMenu main = new MainMenu(clientlist);
            }
        }
        

    }
}
