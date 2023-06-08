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

    class ATM_User{

        private string keyID;

        private string password;
        private string userName;

        private double cashBalance;
        private double creditCardBalance;

        public ATM_User(string name,string pass,string id){
            this.userName=name;
            this.password=pass;
            this.keyID=id;
            this.cashBalance=0;
            this.creditCardBalance=1000;
        }
        public void SetID(string id){
            this.keyID=id;
        }

        public void SetUserName(string name){
            this.userName=name;
        }

        public void SetCash(double money){
            this.cashBalance=money;
        }

         public void PW(string pw){
            this.password=pw;
        }

        public string GetPW(){
            return this.PW;
        }
        public string UserName(){
            return this.userName;
        }

        public string GetID(){
            return this.keyID;
        }

         public double GetCash(){
            return this.cashBalance;
        }

    }

    class ATM_UserList{
        List<ATM_User> atm_users=new List<ATM_User>();

        public void AddUser(string name,string pw){
            string keys=KeyGenerator(atm_users);
            atm_users.Add(new ATM_User(name,pw,keys));
        }

        public string FindUser(string key){
            return atm_users.Find(x=>x.GetID()=key);
        }

        public  void UpdateCash(string key,double money){

            var result=from r in atm_users where r.GetID()==key select r;
            result.First().SetCash(money);
        }

                /* UPDATE uygulama Örneği
                public static void UpdateHeroesInfo_CASH(string ID,int cash)
            {
                var result=from r in startingHeroesList where r.userID==ID select r;

                result.First().heroCash = cash;
            }
                */

        private string KeyGenerator(List<ATM_User> atm){
            int counter=atm_users.Count;
            string key="";

            if(counter<10){
                key=String.Concat("000",counter+1);
            }else if(counter>=10 && counter<100){
                key=String.Concat("00",counter+1);
            }else if(counter>=100 && counter<1000){
                key=String.Concat("0",counter+1);

            }else{
                key=(counter+1).ToString();
            }

            return key;
        }
    }
}
