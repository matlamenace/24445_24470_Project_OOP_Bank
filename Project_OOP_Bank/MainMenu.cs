using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Project_OOP_Bank
{
    class MainMenu //instantiated by the Run_System class, MainMenu is for instantiating the right plateform for the user (employee's or client's)
    {
        public MainMenu(ClientList clientlist) 
        {
            Console.Clear();  
            string entryIdentifier = EntryIdentifier();
            Instantiator(entryIdentifier, clientlist);
        }
        private string EntryIdentifier() //method that define if the user is an employee or a client
        {
            for (; ; ) 
            {
                Console.WriteLine("If you have administrators rights, type 'E'" + '\n' + "If you are a client, type 'C'");
                string entryIdentifier = Console.ReadLine();
                if (entryIdentifier == "E" || entryIdentifier == "C") { return entryIdentifier; } //the method return "E" for employee or "C" for client

                else { Console.WriteLine("Wrong entry, please try again"); }
                Thread.Sleep(1000);
            }
        }
        private void Instantiator(string entryIdentifier, ClientList clientList) //Method that instantiate the class EmployeePlateform if the user is an employee or ClientPlateform if the user is a client
        {
            if (entryIdentifier == "E") { EmployeePlateform employee = new EmployeePlateform(clientList); }
            if (entryIdentifier == "C") { ClientPlateforme client = new ClientPlateforme(clientList); }
        }
    }

}
