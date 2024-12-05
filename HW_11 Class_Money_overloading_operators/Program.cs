using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_11_Class_Money_overloading_operators
{
    class Money
    {
        public int hryvnia { get; set; }
        public int kopeck { get; set; }
        public Money() { }
        public Money(int hryvnia, int kopeck)
        {
            this.hryvnia = hryvnia;
            this.kopeck = kopeck;
        }
        public static Money operator +(Money cash1, Money cash2)
        {
            Money temp = new Money { hryvnia = cash1.hryvnia + cash2.hryvnia, kopeck = cash1.kopeck + cash2.kopeck };

            if (temp.kopeck >= 100)
            {
                temp.hryvnia++;
                temp.kopeck -= 100;
            }
            return temp;
        }
        public static Money operator -(Money cash1, Money cash2)
        
        {
            if (cash1 > cash2)
            {
                Money temp = new Money { hryvnia = cash1.hryvnia - cash2.hryvnia, kopeck = cash1.kopeck - cash2.kopeck };
                if (temp.kopeck < 0)
                {
                    temp.hryvnia --;
                    temp.kopeck = 100 + temp.kopeck;
                }
                return temp;
            }
            throw new Exception("Exception! Bankrupt! cash1 < cash2, negative amount of money!");
        }

        public static Money operator *(Money cash, int n)
        {
            Money temp = new Money { hryvnia = cash.hryvnia * n, kopeck = cash.kopeck * n };

            if (temp.kopeck >= 100)
            {
                while (temp.kopeck > 100)
                {
                    temp.hryvnia++;
                    temp.kopeck -= 100;
                }
                if (temp.kopeck == 100)
                {
                    temp.hryvnia++;
                    temp.kopeck -= 100;
                }
            }
            return temp;
        }
        public static Money operator /(Money cash, int n)
        {
            int cashInKopeck = cash.kopeck + (cash.hryvnia * 100);
            float res = (float)cashInKopeck / (float)n /100;

            Money temp = new Money { hryvnia = (int)res, kopeck = (int)((res - (int)res) *100) };

            if (temp.hryvnia < 0)
            {
                throw new Exception("Exception! Bankrupt! cash1 < cash2, negative amount of money!");
            }
            if (n == 0)
            {
                throw new Exception("Exception! Dividing by zero!");
            }

            if (temp.kopeck >= 100)
            {
                temp.hryvnia++;
                temp.kopeck -= 100;
            }
            return temp;
        }
        public static Money operator ++(Money cash)
        {
            Money temp = new Money { hryvnia = cash.hryvnia, kopeck = cash.kopeck + 1};

            if (temp.kopeck >= 100)
            {
                temp.hryvnia++;
                temp.kopeck -= 100;
            }
            return temp;
        }
        public static Money operator --(Money cash)
        {
            Money temp = new Money { hryvnia = cash.hryvnia, kopeck = cash.kopeck - 1 };

            if (temp.kopeck < 0)
            {
                temp.hryvnia--;
                temp.kopeck += 100;
            }
            if (temp.hryvnia < 0)
            {
                throw new Exception("Exception! Bankrupt! cash1 < cash2, negative amount of money!");
            }
            if (temp.hryvnia == 0 && temp.kopeck < 0)
            {
                throw new Exception("Exception! Bankrupt! cash1 < cash2, negative amount of money!");
            }
            return temp;
        }
        public static bool operator >(Money cash1, Money cash2)
        {
            return (cash1.hryvnia*100 + cash1.kopeck) > (cash2.hryvnia*100 + cash2.kopeck);
        }
        public static bool operator <(Money cash1, Money cash2)
        {
            return !(cash1 > cash2);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(Money cash1, Money cash2)
        {
            return (cash1.hryvnia*100 + cash1.kopeck) == (cash2.hryvnia*100 + cash2.kopeck);
        }
        public static bool operator != (Money cash1, Money cash2)
        {
            return !(cash1 == cash2);
        }
        public override string ToString()
        {
            if (kopeck >= 10 || kopeck <= -10)
            {
                return $"{hryvnia}.{kopeck}";
            }
            else 
            {
                return $"{hryvnia}.0{kopeck}";
            }
            
        }
    }
    
    class Program
    {
        public static void ShowMoney(string oper, Money cash1, Money cash2, Money res)
        {
            Console.WriteLine($"cash1 oper cash2 = res");
            Console.WriteLine($"{cash1} {oper} {cash2} = {res}"); 
        }
        public static void ShowMoney(string oper, Money cash1,int num, Money res)
        {
            Console.WriteLine($"cash1 oper cash2 = res");
            Console.WriteLine($"{cash1} {oper} {num} = {res}");
        }
        public static void ShowMoney(string oper, Money cash1, Money cash2, bool res)
        {
            Console.WriteLine($"cash1 oper cash2 = res");
            Console.WriteLine($"{cash1} {oper} {cash2} = {res}");
        }
        public static void ShowMoney(string oper, Money cash1, Money res)
        {
            Console.WriteLine($"cash1 oper cash2 = res");
            Console.WriteLine($"{cash1} {oper} = {res}");
        }

        static void Main(string[] args)
        {
            /*
               Написать класс Money, предназначенный для хранения денежной суммы (в гривнах и копейках). 
               Для класса реализовать перегрузку операторов + (сложение денежных сумм), - (вычитание сумм), 
            / (деление суммы на целое число), * (умножение суммы на целое число),
            ++ (сумма увеличивается на 1 копейку), -- (сумма уменьшается на 1 копейку), <, >, ==, !=.
               Класс не может содержать отрицательную сумму. 
            В случае, если при исполнении какой-либо операции получается отрицательная сумма денег, 
            то класс генерирует исключительную ситуацию «Банкрот».
               Программа должна с помощью меню продемонстрировать все возможности класса Money. 
            Обработка исключительной ситуации производится в программе.

             */

            Random random = new Random();

            int menu = 0;

            do
            {
                Console.WriteLine("\n=====================================================");
                Console.WriteLine($"\n   Select an operation: ");
                Console.WriteLine($"1. The Operation +, Cash 1 + Cash 2: ");
                Console.WriteLine($"2. The Operation -, Cash 1 - Cash 2: ");
                Console.WriteLine($"3. The Operation *, Cash * number: ");
                Console.WriteLine($"4. The Operation /, Cash / number: ");
                Console.WriteLine($"5. The Operation ==, Cash 1 == Cash 2: ");
                Console.WriteLine($"6. The Operation !=, Cash 1 != Cash 2: ");
                Console.WriteLine($"7. The Operation >, Cash 1 > Cash 2: ");
                Console.WriteLine($"8. The Operation <, Cash 1 < Cash 2: ");
                
                Console.WriteLine($"0. Exit: ");
                Console.WriteLine("=======================================================\n");

                menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:

                        do
                        {
                            try
                            {
                                Money cash1 = new Money(random.Next(0, 5), random.Next(0, 100)), cash2 = new Money(random.Next(0, 5), random.Next(0, 100));
                                ShowMoney("+", cash1, cash2, cash1 + cash2);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine("Go To continue generating fractions, press the Enter key, in the Go menu, input something");

                        } while (Console.ReadLine() == "");

                        break;

                    case 2:

                        do
                        {
                            try
                            {
                                Money cash1 = new Money(random.Next(0, 5), random.Next(0, 100)), cash2 = new Money(random.Next(0, 5), random.Next(0, 100));
                                ShowMoney("-", cash1, cash2, cash1 - cash2);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine("\nGo To continue generating fractions, press the Enter key, in the Go menu, input something");

                        } while (Console.ReadLine() == "");
                        break;

                    case 3:

                        do
                        {
                            try
                            {
                                Money cash1 = new Money(random.Next(0, 5), random.Next(0, 100)), cash2 = new Money(random.Next(0, 5), random.Next(0, 100));
                                int num = random.Next(0, 5);
                                ShowMoney("*", cash1, num, cash1 * num);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine("\nGo To continue generating fractions, press the Enter key, in the Go menu, input something");

                        } while (Console.ReadLine() == "");
                        break;

                    case 4:

                        do
                        {
                            try
                            {
                                Money cash1 = new Money(random.Next(0, 5), random.Next(0, 100)), cash2 = new Money(random.Next(0, 5), random.Next(0, 100));
                                int num = random.Next(0, 5);
                                ShowMoney("/", cash1, num, cash1 / num);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine("\nGo To continue generating fractions, press the Enter key, in the Go menu, input something");

                        } while (Console.ReadLine() == "");
                        break;

                    case 5:

                        do
                        {
                            try
                            {
                                Money cash1 = new Money(random.Next(0, 5), random.Next(0, 100)), cash2 = new Money(random.Next(0, 5), random.Next(0, 100));
                                ShowMoney("==", cash1, cash2, cash1 == cash2);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine("\nGo To continue generating fractions, press the Enter key, in the Go menu, input something");

                        } while (Console.ReadLine() == "");
                        break;

                    case 6:

                        do
                        {
                            try
                            {
                                Money cash1 = new Money(random.Next(0, 5), random.Next(0, 100)), cash2 = new Money(random.Next(0, 5), random.Next(0, 100));
                                ShowMoney("!=", cash1, cash2, cash1 != cash2);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine("\nGo To continue generating fractions, press the Enter key, in the Go menu, input something");

                        } while (Console.ReadLine() == "");
                        break;

                    case 7:

                        do
                        {
                            try
                            {
                                Money cash1 = new Money(random.Next(0, 5), random.Next(0, 100)), cash2 = new Money(random.Next(0, 5), random.Next(0, 100));
                                ShowMoney(">", cash1, cash2, cash1 > cash2);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine("\nGo To continue generating fractions, press the Enter key, in the Go menu, input something");

                        } while (Console.ReadLine() == "");
                        break;

                    case 8:

                        do
                        {
                            try
                            {
                                Money cash1 = new Money(random.Next(0, 5), random.Next(0, 100)), cash2 = new Money(random.Next(0, 5), random.Next(0, 100));
                                ShowMoney("<", cash1, cash2, cash1 < cash2);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine("\nGo To continue generating fractions, press the Enter key, in the Go menu, input something");

                        } while (Console.ReadLine() == "");
                        break;
                    case 9:

                        do
                        {
                            try
                            {
                                try
                                {
                                    Money cash1 = new Money(random.Next(0, 5), random.Next(0, 100)), cash2 = new Money(random.Next(0, 5), random.Next(0, 100));
                                    ShowMoney("++", cash1, ++cash1);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                Console.WriteLine("\nGo To continue generating fractions, press the Enter key, in the Go menu, input something");

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                        } while (Console.ReadLine() == "");
                        break;

                    case 10:

                        do
                        {
                            try
                            {
                                Money cash1 = new Money(random.Next(0, 5), random.Next(0, 100)), cash2 = new Money(random.Next(0, 5), random.Next(0, 100));
                                ShowMoney("--", cash1, --cash1);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            Console.WriteLine("\nGo To continue generating fractions, press the Enter key, in the Go menu, input something");

                        } while (Console.ReadLine() == "");
                        break;
                    default:
                        break;
                }

            } while (menu != 0);


            Console.ReadKey();
        }

       
    }
}
