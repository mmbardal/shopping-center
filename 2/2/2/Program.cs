using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace _2
{
    enum access { User,Admin}
    
    class Usermenu
    {
        public static void user_menu()
        {
            Console.WriteLine("input your type:\n1-teacher\n2-student\n3-customer");
            string cmd1 = Console.ReadLine();
            if(cmd1=="1")
            {
                Console.WriteLine("input your name and your un in two line");
                string name = Console.ReadLine();
                string un = Console.ReadLine();
                 
                save(name, un,"teacher");
                Teacher.telist.Add(new Teacher(name, un));
                menu_2("teacher");
            }
            if (cmd1 == "2")
            {
                Console.WriteLine("input your name and your num in two line");
                string name = Console.ReadLine();
                string  num= Console.ReadLine();
                 
                student.check_stunum(ref num);
                save(name, num, "student");
                student.stulist.Add(new student(name, num));
                menu_2("student");
            }
            if (cmd1 == "3")
            {
                Console.WriteLine("input your name and your code in two line");
                string name = Console.ReadLine();
                string num = Console.ReadLine();
                 
                Customer.check_code(ref num);
                save(name, num, "customer");
                Customer.customerlist.Add(new Customer(name, num));
                menu_2("customer");
            }
        }
        static void menu_2(string type)
        {
            cart main = new cart();
            while (true)
            {
                string cmd;
                Console.WriteLine("menu\n1-select\n2-edit\n3-buy\n4-chance\n" +
                    "5-EXIT");
                cmd = Console.ReadLine();
                double[] chance = { 0, 0.02, 0.03, 0.05, 0.07, 0.1, 0.15, 0.25, 0.3 };
                int index = 0;
                if (cmd == "5")
                {
                    break;
                }                
                if (cmd == "4")
                {
                    
                    Random rand = new Random();
                    index = rand.Next(0, 8);
                }
                if (cmd == "3")
                {
                    main.buy(type,main,chance[index]);
                }
                if (cmd == "2")
                {
                    main.edit(main);
                }
                if (cmd == "1")
                {
                    main.select();
                }
            }
        }
        static void save(string a , string b , string c)
        {
            StreamWriter writer = File.AppendText("CustomersInfo.txt");
            writer.WriteLine($"{c} {a} {b}");
            writer.Close();
        }
    }
    class Username
    {
       protected string username;
        public Username(string a )
        {
            username = a;
        }
    }
    class Seller:Username
    {
        public static List<Seller> sellerlist = new List<Seller>();
        public static List<string> emailx = new List<string>();
        string passsword;//MyShop1234$



        public Seller(string a , string b):base(a)
        {
            passsword = b;
        }

        public static void check_email(ref string e)
        {
            while (!Regex.IsMatch(e, @"^.+@.+\..+$"))
            {
                Console.WriteLine("pattern is invalid try again");
                e = Console.ReadLine();
            }
        }
        public static void checkpass(ref string a,string em)
        {
            int index = 0;
            for(int i = 0;i<sellerlist.Count;i++)
            {
                if(sellerlist[i].username==em)
                {
                    index = i;
                    break;
                }
            }
            while(a!= sellerlist[index].passsword)
            {
                Console.WriteLine("pattern is invalid try again");
                a = Console.ReadLine();
            }
        }

        static bool  check_be( string c )
        {
            return emailx.Contains(c);
        }
        public static void admin_menu1()
        {
            Console.WriteLine("1-sign up\n2-sign in");
            string sign = Console.ReadLine();
            if(sign=="2")
            {
                while (true)
                {
                    Console.WriteLine("email & pass in two line");
                    string email = Console.ReadLine();
                    check_email(ref email);
                    if (!check_be(email))
                        continue;

                    string pass = Console.ReadLine();
                    checkpass(ref pass, email);
                     
                    Seller main = new Seller(email, pass);
                    sellerlist.Add(main);
                    if (main.admin_menu2() == true)
                        break;
                }
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("email & pass in two line");
                    string email = Console.ReadLine();
                    check_email(ref email);
                    if (check_be(email))
                        continue;

                    string pass = Console.ReadLine();
                    
                     
                    Seller main = new Seller(email, pass);
                    sellerlist.Add(main);
                    emailx.Add(email);
                    
                        break;
                }
            }
            
        }
        bool admin_menu2()
        {
            while(true)
            {
                string cmd;
                Console.WriteLine("menu\n1-ADD\n2-DELETE\n3-SEARCH\n4-SHOWCUSTOMERS\n" +
                    "5-CHANGEPASS\n6-EXIT");
                cmd = Console.ReadLine();
                 
                if (cmd == "6")
                {
                    break;
                }
                if (cmd == "5")
                {
                    while(true)
                    {
                        Console.WriteLine("input your password");
                        string ps = Console.ReadLine();
                        if (ps == passsword)
                        {
                            Console.WriteLine("input your new password");
                            string ps2 = Console.ReadLine();
                            passsword = ps;

                            break;
                        }
                        else
                        {
                            Console.WriteLine("invald try again");
                            continue;
                        }
                    }
                }
                if (cmd == "4")
                {
                    StreamReader reader = new StreamReader("CustomersInfo.txt");
                    string read = reader.ReadLine();
                    while (read !=null)
                    {
                        Console.WriteLine(read);
                        read = reader.ReadLine();
                    }
                    reader.Close();
                }
                if (cmd == "3")
                {
                    library.SearchMedia();
                }
                if (cmd == "2")
                {
                    library.DelMedia();
                }
                if (cmd == "1")
                {
                    library.AddMedia();
                }
            }
            return true;
        }
    }
    class Media
    {
        
       public string name_product;
       public double price;// without tax
        
        public string  code;
        public Media(string name , double p, string code)
        {
            name_product = name;
            price = p;
            this.code = code;
            
        }

        public virtual double tax()
        {

            return 0;
        }
    }
    class Books:Media
    {
        public static List<Books> booklist = new List<Books>();
      public  string name_writer;
      public  string name_publisher;
        public Books(string np,string nw ,string npub,double p,string code):base(np,p,code)
        {
            name_writer = nw;
            name_publisher = npub;
        }

        public override double tax()
        {
            return  10;
        } 

    }
    class Videos:Media
    {
        public static List<Videos> videolist = new List<Videos>();
       public int time;
       public int count;
        public Videos(int t , int c , double p , string name,string code):base(name,p,code)
        {
            time = t;
            count = c;
        }
        public override double tax()
        {
            return count * 3 + (time / 60) * 5;
        }
    }
    class Magazines :Media
    {
        public static List<Magazines> maglist = new List<Magazines>();
       public string name_publisher;
       public int num;
        public Magazines(int nu , string npub,string name , double price,string code):base(name,price,code)
        {
            num = nu;
            name_publisher = npub;
        }
        public override double tax()
        {
            if(num>=1&&num<=20)
            {
                return 2;
            }
            else if (num >= 21 && num <= 50)
            {
                return 3;
            }
            else
            {
                return 5;
            }
        }
    }
    class library
    {
        public static List<Media> medlist = new List<Media>();


        public static  int findmedia(string code)
        {
            for(int i  = 0;i<medlist.Count;i++)
            {
                if (medlist[i].code == code)
                    return i;
            }
            return 0;
        }
        public static int findbook(string code)
        {
            for (int i = 0; i < Books.booklist.Count; i++)
            {
                if (Books.booklist[i].code == code)
                    return i;
            }
            return 0;
        }
        public static int findvideo(string code)
        {
            for (int i = 0; i < Videos.videolist.Count; i++)
            {
                if (Videos.videolist[i].code == code)
                    return i;
            }
            return 0;
        }
        public static int findmagazine(string code)
        {
            for (int i = 0; i < Magazines.maglist.Count; i++)
            {
                if (Magazines.maglist[i].code == code)
                    return i;
            }
            return 0;
        }
        public static bool check_code(string c )
        {
            for(int i = 0;i<medlist.Count;i++)
            {
                if(medlist[i].code==c)
                {
                    return true;
                    
                }
            }
            return false;
        }
        public static void AddMedia()
        {
           
                Console.WriteLine("choose type \n1-book\n2-magazine\n3-video");
                string cmd1 = Console.ReadLine();
                if(cmd1=="1")
                {
                Console.WriteLine("input info\nname\nwriter\npublisher\ncode\nprice");
                string name = Console.ReadLine();
                string writer=Console.ReadLine();
                string publisher=Console.ReadLine();
                double price;
                string code = Console.ReadLine();
                while(check_code(code)==true)
                {
                    Console.WriteLine("this code is  used try another");
                    code = Console.ReadLine();
                }
                try
                {
                    price = double.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("invalid try agian");
                    price = double.Parse(Console.ReadLine());
                }

                medlist.Add(new Books(name, writer, publisher, price, code));
                Books.booklist.Add(new Books(name, writer, publisher, price, code));
                }
                if (cmd1 == "2")
                {
                Console.WriteLine("input info\nname\ncode\nprice\ncount\ntime");
                string name = Console.ReadLine();
                double price;
                string code = Console.ReadLine();
                int count;
                int time;
                while (check_code(code) == true)
                {
                    Console.WriteLine("this code is  used try another");
                    code = Console.ReadLine();
                }
                try
                {
                    price = double.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("invalid try agian");
                    price = double.Parse(Console.ReadLine());
                }

                try
                {
                    count = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("invalid try agian");
                    count = int.Parse(Console.ReadLine());
                }

                try
                {
                    time = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("invalid try agian");
                    time = int.Parse(Console.ReadLine());
                }

                medlist.Add(new Videos(time, count, price, name, code));
                Videos.videolist.Add(new Videos(time, count, price, name, code));

                }
                if (cmd1 == "3")
                {
                Console.WriteLine("input info\nname\nnumber of page\npublisher\ncode\nprice");
                string name = Console.ReadLine();
                int num;
                try
                {
                    num = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("invalid try agian");
                    num = int.Parse(Console.ReadLine());
                }
                string publisher = Console.ReadLine();
                double price;
                string code = Console.ReadLine();
                while (check_code(code) == true)
                {
                    Console.WriteLine("this code is  used try another");
                    code = Console.ReadLine();
                }
                try
                {
                    price = double.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("invalid try agian");
                    price = double.Parse(Console.ReadLine());
                }
                medlist.Add(new Magazines(num, publisher, name, price, code));
                Magazines.maglist.Add(new Magazines(num, publisher, name, price, code));
            }
            
        }
        public static void DelMedia()
        {
            Console.WriteLine("choose type \n1-book\n2-magazine\n3-video");
            string cmd1 = Console.ReadLine();
            if (cmd1 == "1")
            {
                Console.WriteLine("input code");
                
                string code = Console.ReadLine();
                while (check_code(code) == false)
                {
                    Console.WriteLine("this code isn't found try another");
                    code = Console.ReadLine();
                }
                int med = findmedia(code);
                int book = findbook(code);
                medlist.RemoveAt(med);
                Books.booklist.RemoveAt(book);
                
            }
            if (cmd1 == "2")
            {
                Console.WriteLine("input code");
                
                string code = Console.ReadLine();

                while (check_code(code) == false)
                {
                    Console.WriteLine("this code isn't found try another");
                    code = Console.ReadLine();
                }
                int med = findmedia(code);
                int mag = findbook(code);
                medlist.RemoveAt(med);
                Magazines.maglist.RemoveAt(mag);
            }
            if (cmd1 == "3")
            {
                Console.WriteLine("input code");
                
                string code = Console.ReadLine();
                while (check_code(code) == false)
                {
                    Console.WriteLine("this code isn't found try another");
                    code = Console.ReadLine();
                }
                int med = findmedia(code);
                int vid = findbook(code);
                medlist.RemoveAt(med);
                Videos.videolist.RemoveAt(vid);
            }
        }
        public static void SearchMedia()
        {
            Console.WriteLine("choose type \n1-book\n2-magazine\n3-video");
            string cmd1 = Console.ReadLine();
            if (cmd1 == "1")
            {
                Console.WriteLine("input code");

                string code = Console.ReadLine();
                while (check_code(code) == false)
                {
                    Console.WriteLine("this code isn't found try another");
                    code = Console.ReadLine();
                }
                
                int book = findbook(code);
                Console.WriteLine("name: {0} - writer: {1} - publisher: {2} - price: {3} ", Books.booklist[book].name_product
                    , Books.booklist[book].name_writer, Books.booklist[book].name_publisher
                    , Books.booklist[book].price);

            }
            if (cmd1 == "2")
            {
                Console.WriteLine("input code");

                string code = Console.ReadLine();

                while (check_code(code) == false)
                {
                    Console.WriteLine("this code isn't found try another");
                    code = Console.ReadLine();
                }
                int med = findmedia(code);
                int mag = findbook(code);
                Console.WriteLine("name : {0} - publisher : {1} - pages : {2} - price : {3} "
                    , Magazines.maglist[mag].name_product, Magazines.maglist[mag].name_publisher
                    , Magazines.maglist[mag].num, Magazines.maglist[mag].price);


            }
            if (cmd1 == "3")
            {
                Console.WriteLine("input code");

                string code = Console.ReadLine();
                while (check_code(code) == false)
                {
                    Console.WriteLine("this code isn't found try another");
                    code = Console.ReadLine();
                }
                int med = findmedia(code);
                int vid = findbook(code);
                Console.WriteLine("name : {0} - time : {1} - num of cd : {2} - price : {3}",
                    Videos.videolist[vid].name_product, Videos.videolist[vid].time
                    , Videos.videolist[vid].count, Videos.videolist[vid].price);
            }
        }

    }
    class cart
    {
        List<Videos> buyvideo = new List<Videos>();
        List<Books> bookbuy = new List<Books>();
        List<Magazines> magbuy = new List<Magazines>();
       public List<Media> medbuy = new List<Media>();
        public  void select()
        {
            for(int i = 0;i<library.medlist.Count;i++)
            {
                Console.WriteLine(library.medlist[i].name_product);
            }
            Console.WriteLine("input your product name");
            string nx = Console.ReadLine();
            if(library.medlist.Any(x => x.name_product == nx))
            {
                int index = library.medlist.FindIndex(x => x.name_product == nx);
                medbuy.Add(library.medlist[index]);
            }
            else
            {
                Console.WriteLine("not found");
                return;
            }
        }
        public  void edit(cart help)
        {
            for (int i = 0; i < medbuy.Count; i++)
            {
                Console.WriteLine(medbuy[i].name_product);
                Console.WriteLine(medbuy[i].price);

            }
            Console.WriteLine("input your product name");
            string nx = Console.ReadLine();
            if (help.medbuy.Any(x => x.name_product == nx))
            {
                int index = library.medlist.FindIndex(x => x.name_product == nx);
                medbuy.RemoveAt(index);
                 
            }
            else
            {
                Console.WriteLine("not found");
                 
                return;
            }
        }
         double final=0;
        public  void buy(string type,cart help, double chance)
        {
            //1-teacher\n2-student\n3-customer
            for(int i = 0;i<help.medbuy.Count;i++)
            {
                final += help.medbuy[i].price * ((100 + help.medbuy[i].tax()) / 100);
            }

            if (type == "teacher")
            {
                Console.WriteLine("price is {0}", final * (0.85-chance));
            }
            if (type == "student")
            {
                Console.WriteLine("price is {0}", final * (0.80-chance));
            }
            if (type == "customer")
            {
                if(help.medbuy.Count>=5)
                {
                    Console.WriteLine("price is {0}", final * (0.95-chance));
                }
                else
                    Console.WriteLine("price is {0}", final );
            }
            Console.WriteLine("accept??  yes or no");
            string dfg = Console.ReadLine();
            if (dfg == "yes")
                return;
            else
                Console.WriteLine("canceled");
            return;
        }
    }
    class student:Username
    {
        public static List<student> stulist = new List<student>();
        
        string stunum;//9XXXXXXX

        public student(string a, string b) : base(a)
        {
            stunum = b;
        }
        public static void check_stunum(ref string s)
        {
            while (!Regex.IsMatch(s, @"^9.{7}$"))
            {
                Console.WriteLine("pattern is invalid try again");
                s = Console.ReadLine();
            }
        }
        public static bool checkbenum(string a)
        {
            return stulist.Any(x => x.username == a);
        }
    }
    class Teacher:Username
    {
        public static List<Teacher> telist = new List<Teacher>();
        
        string un;

        public Teacher(string a, string b) : base(a)
        {
            un = b;
        }
        public static bool checkbenum(string a)
        {
            return telist.Any(x => x.username == a);
        }
        //public static void saveinfo()
        //{
        //    StreamWriter writer = new StreamWriter("CustomersInfo.txt");
        //    for(int  i = 0;i< telist.Count;i++)
        //    {
        //        writer.WriteLine(telist[i].username);
        //        writer.WriteLine(telist[i].un);
        //    }
        //    writer.Close();
        //}
    }
    class Customer:Username
    {
        public static List<Customer> customerlist = new List<Customer>();
        
        string codemellio;//code melli


        public Customer(string a, string b) : base(a)
        {
            codemellio = b;
        }
        public static void check_code(ref string e)
        {
            char[] helper = e.ToCharArray();
            int[] help2 = new int[helper.Length];
            for(int i = 0;i<helper.Length;i++)
            {
                help2[i] = Convert.ToInt32(helper[i]);
            }

            int a = help2[9];
            int b = help2[0] * 10 + help2[1] * 9 + help2[2] * 8 + help2[3] * 7
                + help2[4] * 6 + help2[5] * 5 + help2[6] * 4 +
                help2[7] * 3 + help2[8] * 2;
            int c = b % 11;
            while (!((a==c&&c==0)||(a == c && c == 1)||(c>1&&a==Math.Abs(c-11))))
            {
                Console.WriteLine("pattern is invalid try again");
                e = Console.ReadLine();
                check_code(ref e);
            }
        }
        public static bool checkbename(string a)
        {
            return customerlist.Any(x => x.username == a);
        }
    }
    class Program
    {
        static void apply()
        {
            StreamReader reader = new StreamReader("CustomersInfo.txt");
            string read = reader.ReadLine();
            while (read != null)
            {
                string[] info = read.Split(' ');
                if(info[0]=="teacher")
                {
                    Teacher.telist.Add(new Teacher(info[1], info[2]));
                }
                if (info[0] == "student")
                {
                    student.stulist.Add(new student(info[1], info[2]));
                }
                if (info[0] == "customer")
                {
                    Customer.customerlist.Add(new Customer(info[1], info[2]));
                }
                read = reader.ReadLine();
            }
            reader.Close();
        }
        static void Main(string[] args)
        {
            apply();
            while(true)
            {
                bool exit = false;
                Console.WriteLine("1-Admin\n2-User");
                string cmd1 = Console.ReadLine();
                 
                switch (cmd1)
                {
                    case "1": Seller.admin_menu1();break;
                    case "2": Usermenu.user_menu();break;
                    case "exit":exit=true;break;
                }
                if (exit)
                    break;
            }
        }
    }
}
