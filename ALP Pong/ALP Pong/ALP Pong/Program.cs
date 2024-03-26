using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;

namespace Pong
{
    public class Program
    {
        public static void Start()
        {
            Console.WriteLine("        WELCOME TO PONG GAME  ");
            Console.WriteLine("   PRESS ANY KEY TO PLAY THE GAME  ");
            Console.ReadKey();
            Console.Clear();
        }
        public static void Main(string[] args)
        {
        mulai:
            Start(); //call the start function
            //play again
            int betul = 0;
            string playagain = " ";

            //Random letak setelah poin
            int angkaY;
            Random rnd1 = new Random();

            //Ruang game
            const int pjgRuang = 50, lbrRuang = 15;
            const char Border = '-';
            string line = string.Concat(Enumerable.Repeat(Border, pjgRuang));

            //Stick yg digerakkan 
            const int pjgStick = lbrRuang / 4;
            const char Stick = '|';

            int leftRacketHeight = 0;
            int rightRacketHeight = 0;

            //Posisi bola
            int ballX = pjgRuang / 2;
            int ballY = lbrRuang / 2;

            const char bola = 'O';

            bool isBallGoingDown = true;
            bool isBallGoingRight = true;

            //Points
            int pointKiri = 0;
            int pointKanan = 0;

            //Scoreboard
            int scoreboardX = pjgRuang / 2 - 2; //Jarak Scoreboard dari kiri
            int scoreboardY = lbrRuang + 1; //Jarak scoreboard dari atas

            while (true)
            {

                //Print borders
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(line);

                Console.SetCursorPosition(0, lbrRuang);
                Console.WriteLine(line);

                //Print stick
                for (int i = 0; i < pjgStick; i++)
                {
                    Console.SetCursorPosition(0, i + 1 + leftRacketHeight);
                    Console.WriteLine(Stick);
                    Console.SetCursorPosition(pjgRuang - 1, i + 1 + rightRacketHeight);
                    Console.WriteLine(Stick);
                }

                //Do until a key is pressed
                while (!Console.KeyAvailable)
                {
                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(bola);
                    Thread.Sleep(100); //timer jeda waktu

                    Console.SetCursorPosition(ballX, ballY);
                    Console.WriteLine(" "); //Clear posisi bola sebelumnya

                    //Update posisi bola
                    if (isBallGoingDown)
                    {
                        ballY++;
                    }
                    else
                    {
                        ballY--;
                    }

                    if (isBallGoingRight)
                    {
                        ballX++;
                    }
                    else
                    {
                        ballX--;
                    }

                    if (ballY == 1 || ballY == lbrRuang - 1)
                    {
                        isBallGoingDown = !isBallGoingDown; //Ganti arah bola agar bola tidak keluar dari border
                    }

                    if (ballX == 1)
                    {
                        if (ballY >= leftRacketHeight && ballY <= leftRacketHeight + pjgStick) //Bola menyentuh racket kiri dan terpantul
                        {
                            isBallGoingRight = !isBallGoingRight;
                        }
                        else //Bola keluar dari area; Poin buat player kanan
                        {
                            pointKanan++;
                            angkaY = rnd1.Next(2, lbrRuang - 2);
                            ballY = angkaY; //Random posisi Y setelah player kanan mencetak poin
                            ballX = pjgRuang / 2;
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{pointKiri} | {pointKanan}");
                            isBallGoingRight = !isBallGoingRight;
                            if (pointKanan == 10)
                            {
                                goto hasil;
                            }
                        }
                    }

                    if (ballX == pjgRuang - 2)
                    {
                        if (ballY >= rightRacketHeight && ballY <= rightRacketHeight + pjgStick) //Bola menyentuh racket kanan dan terpantul
                        {
                            isBallGoingRight = !isBallGoingRight;
                        }
                        else //Bola keluar dari area; Poin buat player kiri
                        {
                            pointKiri++;
                            angkaY = rnd1.Next(2, lbrRuang - 2);
                            ballY = angkaY; //Random posisi Y setelah player kiri mencetak poin
                            ballX = pjgRuang / 2;
                            Console.SetCursorPosition(scoreboardX, scoreboardY);
                            Console.WriteLine($"{pointKiri} | {pointKanan}");
                            isBallGoingRight = !isBallGoingRight;
                            if (pointKiri == 10)
                            {
                                goto hasil;
                            }

                        }
                    }
                }

                //Mengecek keyboard apa yang dipencet (noel)
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        if (rightRacketHeight > 0)
                        {
                            rightRacketHeight--;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (rightRacketHeight < lbrRuang - pjgStick - 1)
                        {
                            rightRacketHeight++;
                        }
                        break;

                    case ConsoleKey.W:
                        if (leftRacketHeight > 0)
                        {
                            leftRacketHeight--;
                        }
                        break;

                    case ConsoleKey.S:
                        if (leftRacketHeight < lbrRuang - pjgStick - 1)
                        {
                            leftRacketHeight++;
                        }
                        break;
                }

                //Menghapus racket pada posisi sebelumnya
                for (int i = 1; i < lbrRuang; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(pjgRuang - 1, i);
                    Console.WriteLine(" ");
                }
            }
        hasil:;
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            if (pointKanan == 10)//Player kanan menang
            {
                Console.WriteLine("   |-------------------|");
                Console.WriteLine("   | Right player won! |");
                Console.WriteLine("   |     GAME OVER     |");
                Console.WriteLine("   |-------------------|");
                Console.WriteLine();
                while (betul == 0)
                {
                    Console.WriteLine("DO YOU WANT TO PLAY AGAIN?");
                    Console.WriteLine("   [YES]           [NO]   ");
                    Console.Write("Play again? ");
                    playagain = Console.ReadLine().ToUpper();
                    if (playagain == "yes" || playagain == "YES")
                    {
                        betul = 1;
                        Console.Clear();
                        goto mulai;
                    }
                    else if (playagain == "no" || playagain == "NO")
                    {
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("INVALID");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }

            }
            else//Player kiri menang
            {
                Console.WriteLine("   |-------------------|");
                Console.WriteLine("   | Left player won!  |");
                Console.WriteLine("   |     GAME OVER     |");
                Console.WriteLine("   |-------------------|");
                Console.WriteLine();
                while (betul == 0)
                {
                    Console.WriteLine("DO YOU WANT TO PLAY AGAIN?");
                    Console.WriteLine("   [YES]           [NO]   ");
                    Console.Write("Play again? ");
                    playagain = Console.ReadLine().ToUpper();
                    if (playagain == "yes" || playagain == "YES")
                    {
                        betul = 1;
                        Console.Clear();
                        goto mulai;
                    }
                    else if (playagain == "no" || playagain == "NO")
                    {
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("INVALID");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }
            }
        }
    }
}
