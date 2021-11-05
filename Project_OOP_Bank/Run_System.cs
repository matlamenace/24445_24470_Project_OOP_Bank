using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_OOP_Bank
{
    class Run_syst
    {
        public Run_syst() 
        {
            Console.Clear();
            ClientList clientlist = new ClientList();
            MainMenu mainmenu = new MainMenu(clientlist);
        }
        

    }
}
