
using System;
using System.Timers;
public class Program
{
   
    public static void Main()
    {
        //Sets up the map
        bool MapSetup = false;
        string[,,] Map = 
        {
            //Floor 0
            {
             { "P", "-", "-", "-" },
             {"-","-","-","-" },
             { "S", "-", "-", "-" },
             { "-", "-", "-", "-" }
            },
            //Floor 1    
            {
             { "-", "-", "-", "-" },
             {"-","-","-","-" },
             { "S", "-", "-", "-" },
             { "-", "-", "-", "D" }
            }





        };




        
        Random GenerateWall = new Random();
        int Xs = 0;
        while( MapSetup == false)
        {
            
            
            int XPostionX = 0;
            int XPostionY = 0;
            int XPostionZ = 0;

            XPostionX = GenerateWall.Next(0,4);
            XPostionY = GenerateWall.Next(0,4);
            XPostionZ = GenerateWall.Next(0,2);
            

            if (Map[XPostionZ, XPostionY, XPostionY] == Map[0, 0,0]|| Map[XPostionZ, XPostionY, XPostionY] == "S" || Map[XPostionZ, XPostionY, XPostionY] == "X") { Xs = Xs; }
            else if(Map[XPostionZ, XPostionY, XPostionY] == Map[1,3,3]) { Xs = Xs; }
            else { Map[XPostionZ, XPostionY, XPostionY] = "X"; Xs++;  }


            if(Xs == 6) { MapSetup = true; }

        }

        bool AddedKey = false;
        int KeyX = 0;
        int KeyY = 0;
        int KeyZ = 0;
        while (AddedKey == false)
        {
            KeyX = GenerateWall.Next(0, 4);
            KeyY = GenerateWall.Next(0, 4);
            KeyZ = GenerateWall.Next(0, 2);

            if (Map[KeyZ, KeyX, KeyY] == Map[0, 0,0] && Map[KeyZ, KeyX, KeyY] == Map[1, 3,3] && Map[KeyZ, KeyX, KeyY] == "X" && Map[KeyZ, KeyX, KeyY] == "S") { Console.WriteLine("KKK"); }
            else { Map[KeyZ, KeyX, KeyY] = "K"; AddedKey = true; }
        }
        









        string PlayerName = GetPlayerName();
        DisplayArt(PlayerName);
        
        bool IsPlaying = true;
        string Input = "";
        int PlayerX = 0;
        int PlayerY = 0;
        int PlayerZ = 0;
        int NewX = PlayerX;
        int NewY = PlayerY;
        int NewZ = PlayerZ;
        int Q = 1000;
        List<string> Inventory = new List<string>();

        System.Diagnostics.Stopwatch myStopWatch = new System.Diagnostics.Stopwatch();




        while (IsPlaying)
        {
            myStopWatch.Start();


            Console.Clear();
            
                DisplayMap(Map, PlayerZ);
            if (Map[PlayerZ, PlayerX, PlayerY] == "S") { Console.WriteLine(PlayerName + " enter W,A,S or D to move. Space to move up or down a level I for the inventory. Q to quit"); }
            else { Console.WriteLine(PlayerName + " enter W,A,S or D to move. I for the inventory. Q to quit"); }
                DisplayInventory(Inventory);
                Input = Console.ReadLine().ToUpper();
                
            if(Input == "W")
            {
                    NewX = PlayerX - 1;
                    if(NewX == -1) { Console.WriteLine("Out of bounds"); NewX = PlayerX;  }//Checks if player will be out of bounds
                    if (Map[PlayerZ, NewX, PlayerY] == "X") { Console.WriteLine("You've hit a wall"); NewX = PlayerX; }//Check if player will hit a wall
                if (Map[PlayerZ, NewX, NewY] == "S") { Map[PlayerZ, NewX, PlayerY] = "S"; PlayerX = NewX; }
                else { Map[PlayerZ, PlayerX, PlayerY] = "-"; Map[PlayerZ, NewX, PlayerY] = "P"; PlayerX = NewX; }//Updates player position
            }
                
            else if (Input == "S")
            {
                    NewX = PlayerX + 1;
                if(NewX == 4) { Console.WriteLine("Out of bounds"); NewX = PlayerX; }//Checks if player will be out of bounds
                if (Map[PlayerZ, NewX, PlayerY] == "X") { Console.WriteLine("You've hit a wall"); NewX = PlayerX; }//Check if player will hit a wall
                if (Map[PlayerZ, NewX, NewY] == "S") { Map[PlayerZ, NewX, PlayerY] = "S"; PlayerX = NewX; }
                else { Map[PlayerZ, PlayerX, PlayerY] = "-"; Map[PlayerZ, NewX, PlayerY] = "P"; PlayerX = NewX; }//Updates player position
            }
                
            else if (Input == "A")
            {
                    NewY = PlayerY -1;
                    if (NewY == -1) { Console.WriteLine("Out of bounds"); NewY = PlayerY; }//Checks if player will be out of bounds
                    if (Map[PlayerZ, PlayerX, NewY] == "X") { Console.WriteLine("You've hit a wall"); NewY = PlayerY; }//Check if player will hit a wall
                    if (Map[PlayerZ, PlayerX, NewY] == "S") { Map[PlayerZ, PlayerX, NewY] = "S"; PlayerY = NewY; }
                    else { Map[PlayerZ, PlayerX, PlayerY] = "-"; Map[PlayerZ, PlayerX, NewY] = "P"; PlayerY = NewY; }//Updates player position
            }
                
            else if (Input == "D")
            {
                    NewY = PlayerY + 1;    
                    if (NewY == 4) { Console.WriteLine("Out of bounds"); NewY = PlayerY; }//Checks if player will be out of bounds
                    if (Map[PlayerZ, PlayerX, NewY] == "X") { Console.WriteLine("You've hit a wall"); NewY = PlayerY; }//Check if player will hit a wall
                    if (Map[PlayerZ, PlayerX, NewY] == "S") { Map[PlayerZ, PlayerX, NewY] = "S"; PlayerY = NewY; }
                    else { Map[PlayerZ, PlayerX, PlayerY] = "-"; Map[PlayerZ, PlayerX, NewY] = "P"; PlayerY = NewY; }//Updates player position
            }
                
            else if(Input == "Q") { IsPlaying = false; }

            else if(Input == "Z") { Inventory.Add("KKK"); }
                
            else if(Input == "I")
            {
                bool Done = false;
                
                while (Done == false)
                {
                    Console.Clear();
                    Console.WriteLine("What do you want to do S - sort D - Drop Q - Leave");
                    Input = Console.ReadLine().ToUpper();
                    if (Input == "Q") { Done = true; }
                    if(Input == "S") { Inventory.Sort(); }
                    if( Input == "D") 
                    {
                        Console.WriteLine("What item slot do you want to drop 1 - " + Inventory.Count);
                        Input = Console.ReadLine();
                        Inventory.RemoveAt(Convert.ToInt32(Input)-1);
                    }
                    

                }


            }
            else if (Input == " ") { PlayerZ = 1; }





                        if (Map[PlayerZ, PlayerX, PlayerY] == Map[1, 3,3] && Inventory.Contains("KKK"))//Checks if you have reached the end
                        {
                            
                            IsPlaying = false ;
                        }
                        if (Map[PlayerZ, PlayerX, PlayerY] == Map[KeyZ,KeyX,KeyY])//Adds the key to the inventory
                        {
                            Console.WriteLine("You found the key. The key has being added to your inventory");
                            Inventory.Add("KKK");
                            Map[NewX, NewY,NewZ] = "P";
                        }
                        else
                        {
                            Console.Clear();
                            DisplayMap(Map, PlayerZ);
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                        }
                       
                    }
        Console.Clear() ;
        DisplayMap(Map,PlayerZ);
        Console.WriteLine("You escaped");
        myStopWatch.Stop();
        Console.WriteLine("Time Taken " + myStopWatch.Elapsed.Hours.ToString() + " : " + myStopWatch.Elapsed.Minutes.ToString() + " : " + myStopWatch.Elapsed.Seconds.ToString() + " : " + myStopWatch.Elapsed.Milliseconds.ToString());
        IsPlaying = false;






    }

    static string GetPlayerName()// Gets Player's name
    {
        string PlayerName = "";
        string ShortenedPlayerName = "";
        while (string.IsNullOrEmpty(ShortenedPlayerName) || PlayerName.Length > 15)
        {
            Console.Clear();
            Console.WriteLine("Enter Player Name. Player name must be between 3-15 charecters Long");
            PlayerName = Console.ReadLine();
            ShortenedPlayerName = PlayerName[0] + PlayerName.Substring(PlayerName.Length - 2, 2); // takes the first letter of the inputted name and the last name
            if (string.IsNullOrEmpty(ShortenedPlayerName) || PlayerName.Length > 15 || PlayerName.Length < 2)
            {
                Console.WriteLine("Player name is invalid. Please try again");
                Console.ReadLine();
            }
        }
        return ShortenedPlayerName;
    }
    static void DisplayArt(string playerName)
    {

        Console.WriteLine("----++***#%%%%%%%#*+++++++*#%%@@%%%%%%%%%+===\r\n----+******%%%*==--========--:*%%%%######=-==\r\n---=#++++++*#=-=++=-==+====-:--=+*#%#####=---\r\n---=%%*+++++=-=+++=----======----=*#%%%%%=---\r\n---=%%%***+*=+*+*++=--:-====-===+*#%%%%%%=---\r\n---=%%%%****++****+=--::----==++*#%%%%%%%=---\r\n---=%%%%%#**=+++**+==--------=+=*%%%%%%%%=-==\r\n---=%%%%%%#+++++**+==--::----=+++*###**#*====\r\n-===%%%%%@%*+=++*+==--:::--==-++*%%%%%%%%+===\r\n-===*%%%%%%%++++*++=--==:=-::-++##*#***#*====\r\n--==**%%%%@%%+++++=---+=---::-==%%@@%%@%#====\r\n---=***#%@@%%%***++=--+=:::::-*%%#%@##%%%====\r\n---=****%@@%%%-**++=--==---:-=%@@@@@@@@@@+===\r\n---=*****###%%=-+*+====------*#%%@@%%%%%%====\r\n---=**#%%%%%%%=-:=+=====----+%%%%%%%%%%%%====\r\n---=%%%%@%%%%%*--::-*+==-=+=*%%%%@@@@@@@%+===\r\n---=%%%%@%%%%%%=--:::=#%+--=+%@@@@@@@@@@@+===\r\n---=%%%@@%%%%@@*==-:=#%#%++=+#@@@@@@@@@@@+===\r\n---=%%@@@@@@@@@@+==+*#%%*++++#@@@@@@@@@@@+===\r\n---=%@@@@@@@@@@@#+++*%@%%++++*@@@@@@@@@@@+===\r\n---=@@@@@@@@@@@@@*++*%@%%%*+++%@@@@*%@@@@+==+\r\n---=@@@@@@@@@@@@@%*++%%%%%#+++#@@@@@@@@@@+=++\r\n---=@@@@@@@@@@@@@@#**%%%%%%*+*#@@@@@@@@@@*+++\r\n---=@@@@@@@@@@@@@@%**%@@@%%%**#@@@@@@@@@@*+++");
        Console.WriteLine($"Welcome to the 4x4 MUD Game, {playerName}!\nPress Enter to start...");
        Console.ReadLine();
    }//Displays the welcome screen
    static void DisplayMap(string[,,] Map,int PlayerZ)
    {

        Console.WriteLine("Floor " + PlayerZ);
        
           // Console.WriteLine("Floor " + k);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(Map[PlayerZ,i, j] + " ");
                }
                Console.WriteLine();
            }
        
    }//Displays the map
    static void DisplayInventory(List <string> Inventory)
    {
        Inventory.Sort();
        if (Inventory.Count > 0)
        {
            // List all items in the inventory
            foreach (string item in Inventory)
            {
                Console.WriteLine("- " + item);
            }
        }
        else
        {
            // Let the player know their inventory is empty
            Console.WriteLine("Your inventory is empty.");
        }
        
    }//Displays the players inventory

   











}


