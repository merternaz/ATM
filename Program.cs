using System;
using System.Collections.Generic;

namespace structures
{
    
    class Program
    {
        
        static void Main(string[] args)
        {

            Console.WriteLine("Lutfen yapmak istediğiniz işlemi seçiniz :");
                int x=Convert.ToInt32(Console.ReadLine());
            switch(x){
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;


            }

        }

        public static enum ATM_Fonks
        {
            ParaCek,ParaYatir,BorcOde
        } 
            
        
    }
}
