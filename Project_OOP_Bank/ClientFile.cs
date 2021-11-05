using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Project_OOP_Bank
{
    class ClientFile //class that is instatiated by the method NewClient on the ActionOnAccountLimited class,
                     //it creates a new client which means creating new savings and current accounts, and adding
                     //the client to the dictionnary and the AccountFileList file
    {
        string filename;
        string clientfrstname;
        string clientfamname;
        string Pin;
        public ClientFile(string clientfrstname,string clientfamname, ClientList clientlist) 
        {
            this.clientfrstname = clientfrstname;
            this.clientfamname = clientfamname;
            this.filename = AutoCreateFileName();
            AutoCreateFile(filename); 
            this.Pin = string.Concat(filename[6], filename[7], filename[9], filename[10]);
            AddToClientsList(clientlist);
        }
        private string AutoCreateFileName() //This method return the client's AND number 
        {
            string filename = "";
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string indexFrstName = Convert.ToString(alphabet.IndexOf(Char.ToLower(clientfrstname[0])) + 1);
            if (indexFrstName.Length == 1) { indexFrstName = "0" + indexFrstName; }
            string indexFamName = Convert.ToString(alphabet.IndexOf(Char.ToLower(clientfamname[0])) + 1);
            if (indexFamName.Length == 1) { indexFamName = "0" + indexFamName; }
            string namelength = Convert.ToString(clientfamname.Length + clientfrstname.Length);

            if (namelength.Length == 1) { namelength = "0" + namelength; }
            filename = filename.Insert(0, $"{ clientfrstname[0]}{ clientfamname[0]}" + "-" + namelength + "-" + indexFrstName + "-" + indexFamName);


            return filename;
        }
        private void AutoCreateFile(string filename) //this method create the savings and current bank file
        {

            try
            {
                StreamWriter sw = new StreamWriter(filename +"-current"+ ".txt");
                sw.WriteLine(0);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Current account couldn't be created   Exception: " + e.Message);
            }
            try
            {
                StreamWriter sw = new StreamWriter(filename + "-savings" + ".txt");
                sw.WriteLine(0);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Savings account couldn't be created   Exception: " + e.Message);
            }
            
        }
        private void AddToClientsList(ClientList clientlist) //this method add the client to the AccountFileList text file and to the dictionary in ClientList
        {
            try
            {
                StreamWriter sw = new StreamWriter("AccountFileList" + ".txt", true);
                sw.WriteLine(filename + " " + clientfrstname + " " + clientfamname); //add to AccountFileList.text
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("The new client file  couldn't be added to the clients list   Exception: " + e.Message);
            }

            Console.WriteLine(Pin);
            clientlist.Dictionary.Add(clientfrstname+clientfamname, filename); //add to dictionary
            

           

        }
        
    }
}
