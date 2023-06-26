using System;
using System.Collections.Generic;
using System.IO;

namespace structures
{
    
    class Program
    {
        public static ATM_UserList aTM_User=new ATM_UserList();
        static void Main(string[] args)
        {
            //ATM_UserList aTM_User=new ATM_UserList();

            IO_FONKS.READ_USER(aTM_User);//kullanıcıları dosyadan çek
            bool autentication=false;
            
            /*
                ATM de LOGIN olacak. HEsap yoksa hesap açacak sonra tekrar ana menu ve işlem 
            */
            baslangıc:
            Console.WriteLine("Kullanıcı ID ve Password Giriniz...");
            Console.WriteLine("ID:");
            string id=Console.ReadLine().ToString();
            Console.WriteLine("Password:");
            string pw=Console.ReadLine().ToString();


            if(ATM_UserList.FindUserByID(id).GetPW()==pw && ATM_UserList.FindUserByPW(pw).GetID()==id){// listeden kullanıcı kontrolü
                autentication=true;
                Console.WriteLine("Giriş Başarılı :)) Hoşgeldiniz :: "+ATM_UserList.FindUserByID(id).GetUserName());
            }else{
                autentication=false;
                Console.WriteLine("Kullanıcı adı ve şifre hatalı !!");
                goto baslangıc;
            }


            islemler:
            Console.WriteLine("Lutfen yapmak istediğiniz işlemi seçiniz :");
            Console.WriteLine("0-Para Çekme / 1-Para Yatırma / 3-Gün Sonu Raporu/4-Kullanıcı Olustur/5-Kullanıcıları Gör");
                int x=Convert.ToInt32(Console.ReadLine());
            switch(x){
                case 0://Para Çekme
                   
                   Console.WriteLine("Çekmek istediğiniz miktarı giriniz :");
                   int cash_out=Convert.ToInt32(Console.ReadLine());
                   double currentCash=ATM_UserList.FindUserByID(id).GetCash();
                   ATM_UserList.FindUserByID(id).CheckOutCash((int)currentCash-cash_out);
                   aTM_User.UpdateCash(id,(int)currentCash-cash_out);
                   int bal=(int)currentCash-cash_out;
                   Console.WriteLine("Kasa:"+ATM_UserList.FindUserByID(id).GetCash());
                   IO_FONKS.LOG_REG_WR("Para çekme işlemi: "+DateTime.Now+"***"+ATM_UserList.FindUserByID(id).GetUserName()+
                   "***"+"out:"+cash_out+"***"+"balance:"+bal);
                    
                    break;
                    
                case 1://Para Yatırma
                    Console.WriteLine("Yatırılacak para miktarını giriniz :");
                    int cash_in=Convert.ToInt32(Console.ReadLine());//yatırılıacak miktar
                    double current=ATM_UserList.FindUserByID(id).GetCash();//listeden güncel bakiyeyi al

                    ATM_UserList.FindUserByID(id).CheckInCash((int)current+cash_in);//kullanıcı structure karşılığını güncelle
                    aTM_User.UpdateCash(id,(int)current+cash_in);//listeyi güncelle
                    int bal2=(int)current+cash_in;//güncel bakiye
                    IO_FONKS.LOG_REG_WR("Para yatırma işlemi: "+DateTime.Now+"***"+ATM_UserList.FindUserByID(id).GetUserName()+
                   "***"+"in:"+cash_in+"***"+"balance:"+bal2);
                    break;
                case 2://Rapor Bas
                    IO_FONKS.LOG_REG_WR("Rapor bastır: "+DateTime.Now);
                    break;
                case 3://Rapor Al
                    IO_FONKS.LOG_REG_RD();
                    break;
                case 4://Kullanıcı olustur
                    Console.WriteLine("Kullanıcı adı giriniz :");
                    string s=Console.ReadLine().ToString();
                    Console.WriteLine("Kullanıcı sifre giriniz :");
                    string p=Console.ReadLine().ToString();
                    
                    IO_FONKS.LOG_REG_WR_USER("Kullanıcı Olustur: "+DateTime.Now+"/"+s+":"+p);//dosyaya useri ekledi
                    aTM_User.AddUser(s,p);//listeyede ekledi
                    IO_FONKS.READ_USER(aTM_User);//dosyadaki user list tekrar okunup eşlenecek

                    break;
                case 5://Kullanıcı goster
                    IO_FONKS.READ_USER(aTM_User);
                    break;    


            }
            goto islemler;

        }

    

        public enum ATM_Fonks
        {
            ParaCek,ParaYatir,Borcode
        } 



            
        
    }

    class IO_FONKS{

        public static void WRITE_USER(string name,string pass,string id,double bal,double credit){
            string komut="Kullanıcı Olustur:";
           
            FileStream fs=new FileStream("C:\\Users\\MONSTER\\GitProject\\ATM\\users.txt",FileMode.Append,FileAccess.Write,FileShare.Read,4096,true);
            StreamWriter sw =new StreamWriter(fs);
            sw.WriteLine(name+","+pass+","+id+","+bal+","+credit);
            sw.Close();
        }

            public static void LOG_REG_WR(string log){
            string dosyaAdi="EOD"+DateTime.Today.ToShortDateString();    
            FileStream fs=new FileStream("C:\\Users\\MONSTER\\GitProject\\ATM\\"+dosyaAdi+".txt",FileMode.Append,FileAccess.Write,FileShare.Read,4096,true);
            StreamWriter sw =new StreamWriter(fs);
            sw.WriteLine(log);
            sw.Close();
        }

        public static void LOG_REG_WR_USER(string log){
            string komut="Kullanıcı Olustur:";
            string dosyaAdi="EOD"+DateTime.Today.ToShortDateString();
            FileStream fs=new FileStream("C:\\Users\\MONSTER\\GitProject\\ATM\\"+dosyaAdi+".txt",FileMode.Append,FileAccess.Write,FileShare.Read,4096,true);
            StreamWriter sw =new StreamWriter(fs);
            sw.WriteLine(log);
            sw.Close();
        }

        public static void LOG_REG_RD(){
           // FileStream fs=new FileStream("C:\\Users\\MONSTER\\GitProject\\ATM\\logReg.txt",FileMode.OpenOrCreate,FileAccess.Read,FileShare.Read,4096,true);
            string dosyaAdi="EOD"+DateTime.Today.ToShortDateString();
            StreamReader sr =new StreamReader("C:\\Users\\MONSTER\\GitProject\\ATM\\"+dosyaAdi+".txt");
            Console.WriteLine(sr.ReadToEnd());
            sr.Close();
            
        }
        
         public static void READ_USER(ATM_UserList staticUserList){
            ATM_UserList newUserList=new ATM_UserList();
           // FileStream fs=new FileStream("C:\\Users\\MONSTER\\GitProject\\ATM\\logReg.txt",FileMode.OpenOrCreate,FileAccess.Read,FileShare.Read,4096,true);
            StreamReader sr =new StreamReader("C:\\Users\\MONSTER\\GitProject\\ATM\\users.txt");
            //Console.WriteLine(sr.ReadToEnd());
            string ss=sr.ReadToEnd();
            string[] sL=ss.Split(",");
            int num=sL.Count()/5;// kaç kayıt var (satır bazlı)
            int counter=0,regCounter=0;
           
           
            for(int i=0;i<sL.Length;i++){
                

                if(i%5==0){//0-5-10-15-20---24.
                    if(i==0){
                        counter=0;
                    }else{
                        counter++;
                    }
                    
                    regCounter=counter*5;
                }

               newUserList.AddUser(sL[0+regCounter].ToString(),sL[1+regCounter].ToString(),sL[2+regCounter].ToString(),(double)Convert.ToDouble(sL[3+regCounter]),(double)Convert.ToDouble(sL[4+regCounter]));

            }

           // Console.WriteLine("kullanıcı:"+ATM_UserList.FindUserByPW("2211").GetUserName()); test komutu
          // newUserList.ShowUserList();
            staticUserList=newUserList;
            Console.WriteLine(ss);
            sr.Close();
        }

         public static void WRITE_USER(string name,string pass,string id){
            string komut="Kullanıcı Olustur:";
            double balance=0;
            FileStream fs=new FileStream("C:\\Users\\MONSTER\\GitProject\\ATM\\users.txt",FileMode.Append,FileAccess.Write,FileShare.Read,4096,true);
            StreamWriter sw =new StreamWriter(fs);
            sw.WriteLine(name+","+pass+","+id+","+balance);
            sw.Close();
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
            IO_FONKS.WRITE_USER(name,pass,id,this.cashBalance,this.creditCardBalance);//Dosyaya yazar
        }

         public ATM_User(string name,string pass,string id,double cash,double credit){
            this.userName=name;
            this.password=pass;
            this.keyID=id;
            this.cashBalance=cash;
            this.creditCardBalance=credit;
          //  IO_FONKS.WRITE_USER(name,pass,id,this.cashBalance,this.creditCardBalance);//Dosyaya yazar
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
            return this.password;
        }
        public string GetUserName(){
            return this.userName;
        }

        public string GetID(){
            return this.keyID;
        }

         public double GetCash(){
            return this.cashBalance;
        }

        public double CheckOutCash(int outCash){
           return this.cashBalance=this.cashBalance-outCash;


        }

        public double CheckInCash(int inCash){
           return this.cashBalance=this.cashBalance+inCash;


        }

    }

    class ATM_UserList{
        public static List<ATM_User> atm_users=new List<ATM_User>();

        public void AddUser(string name,string pw){ // direkt consolda hesap açma
            string keys=KeyGenerator(atm_users);
            atm_users.Add(new ATM_User(name,pw,keys));
            
        }

        public void AddUser(string name,string pw,string keys,double cash,double credit){ // dosyadan kayıtlı hesapları okuma
            //string keys=KeyGenerator(atm_users);
            atm_users.Add(new ATM_User(name,pw,keys,cash,credit));
            
        }

       /* public string FindUser(string key){
            return atm_users.Find(x=>x.GetID()=key);
        }*/

        public void ShowUserList(){
            foreach(var usr in atm_users){
                Console.WriteLine(usr.GetUserName()+"/"+usr.GetID()+"/"+usr.GetPW());
            }
        }

        public static ATM_User FindUserByID(string ID){
            return atm_users.Find(x=>x.GetID()==ID);
        }

        public static ATM_User FindUserByPW(string pw){
            return atm_users.Find(x=>x.GetPW()==pw);
        }

        public void UpdateCash(string key,double money){

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
