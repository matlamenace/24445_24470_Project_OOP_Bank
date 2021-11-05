using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_OOP_Bank
{
    class ClientList //this class is instatiated when the program start to run. It create a dictionnary 
                     //that will store all the AND numbers (the key for a specific AND is the client's 
                     //first name and the client's family name attached). The method also create a text
                     //file where every client's info will be stored
                     
    {
        Dictionary<string, string> dictionary;
        public ClientList()
        {
            this.dictionary = new Dictionary<string, string>();
            CreateAccountFileList();

        }
        public Dictionary<string, string> Dictionary
        {
            get{ return dictionary; }
            set { dictionary = value; }
        }
        private void CreateAccountFileList() //Method that create the text file AccountFileList where all clients are stored
        {
            try
            {
                StreamWriter sw = new StreamWriter("AccountFileList" + ".txt");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("The account file list couldn't be created   Exception: " + e.Message);
            }
        }
    }
}
