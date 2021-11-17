using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Project_OOP_Bank
{
    class EmployeePlateform
    {
        string Pin = "A1234";
        public EmployeePlateform(ClientList clientList) 
        {
            if (PasswordCheck()) 
            {
                MenuForEmployee(clientList);
            }
        }
        private bool PasswordCheck() 
        {
            
            for(int i = 0; i < 3; i++) 
            {
                Console.Clear();
                Console.WriteLine("Enter your pin    "+$"You have {3-i} try left");
                if (Console.ReadLine() == this.Pin) 
                {
                    Console.WriteLine("Password Checked");
                    Thread.Sleep(3000);
                    return true;
                }
                Console.Clear();
                Console.WriteLine("Wrong password");
                Thread.Sleep(1500);
                

            }
            Console.Clear();
            Console.WriteLine("Your account is blocked");
            Thread.Sleep(3000);
            Run_syst run = new Run_syst();
            return false;
        }
        public static void MenuForEmployee(ClientList clientList) 
        {
            Console.Clear();
            Console.Clear();
            Console.WriteLine("If you want to see the clients list, type 'seefile'"+'\n'+"If you want to add a new client, type 'newclient',"+'\n'+"If you want to delete a client, type 'delete'"+'\n'+"If you want to add a transaction, type 'transaction'"+'\n'+"If you want to disconnect, type 'exit'");
            string employeeanswer = Console.ReadLine();
            if (employeeanswer != "seefile" && employeeanswer != "newclient" && employeeanswer != "delete" && employeeanswer != "transaction" && employeeanswer != "exit") 
            {
                Console.WriteLine("We can't guess what you want");
                MenuForEmployee(clientList);
            }
            if (employeeanswer == "seefile") 
            {
                
                Console.WriteLine("Here is the client list : " + '\n' + '\n');

                ActionOnAccountLimited.SeeFile("AccountFileList");
                Console.WriteLine("Type to continue");
                Console.ReadKey();
                Console.Clear();
                MenuForEmployee(clientList);
            }
            if (employeeanswer == "newclient") 
            { 
                ActionOnAccountLimited.NewClient(clientList);
                MenuForEmployee(clientList);
            }
            if (employeeanswer == "delete") 
            {
                ActionOnAccountLimited.DeleteClient(clientList);
                MenuForEmployee(clientList);
            }
            if (employeeanswer == "transaction") 
            {
                ActionOnAccountLimited.Transaction(clientList);
                MenuForEmployee(clientList);
            }
            if (employeeanswer == "exit") 
            {
                Console.Clear();
                MainMenu main = new MainMenu(clientList);
            }
        }
        
    }
}
