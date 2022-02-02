using MyBanker;
using System.ComponentModel;

Console.BackgroundColor = ConsoleColor.Blue;
Console.ForegroundColor = ConsoleColor.White;
Console.Clear();

int gameState = 0;
Account acc = new Account(0);
Customer c = new Customer("",0,acc);

List<Card> list = new List<Card>();

// Create new customer
while (gameState == 0)
{
    
    // Get user input variables for new account + customer
    Console.WriteLine("Navn: ");
    string? name = Console.ReadLine();
    Console.WriteLine("Alder: ");

    int age = 0;
    try
    {
        age = Convert.ToInt32(Console.ReadLine());
    }
    catch
    {
        age = 0;
    }

    Console.WriteLine("Balance: ");

    int balance = 0;
    try
    {
        balance = Convert.ToInt32(Console.ReadLine());
    }
    catch
    {
        balance = 0;
    }

    // Create new accoutn & customer
    acc = new Account(balance);
    c = new Customer(name, age, acc);

    gameState = 1;
}

// Issue new card
while (gameState == 1)
{
    Console.WriteLine("Choose card type to issue:");

    string menu = "1. Debit card\n" +
        "2. Maestro card\n" +
        "3. Visa Electron\n" +
        "4. VISA/Dankort\n" +
        "5. Mastercard";

    Console.WriteLine(menu);

    int menuChoice = 0;

    menuChoice = int.Parse(Console.ReadKey().KeyChar.ToString());

    switch(menuChoice)
    {
        case 1:
            Console.Clear();
            if (new DebitCard(c).CheckRequirements())
            {
                DebitCard dc = new DebitCard(c);
                list.Add(dc);
                gameState = 2;
            }
            else
            {
                gameState = 1;
            }
            break;

        case 2:
            Console.Clear();
            if (new Maestro(c).CheckRequirements())
            {
                Maestro mc = new Maestro(c);
                list.Add(mc);
                gameState = 2;
            }
            else
            {
                gameState = 1;
            }
            break;

        case 3:
            Console.Clear();
            if (new VisaElectron(c).CheckRequirements())
            {
                VisaElectron v = new VisaElectron(c);
                list.Add(v);
                gameState = 2;
            }
            else
            {
                gameState = 1;
            }
            break;

        case 4:
            Console.Clear();
            if (new VisaDankort(c).CheckRequirements())
            {
                VisaDankort v = new VisaDankort(c);
                list.Add(v);
                gameState = 2;
            }
            else
            {
                gameState = 1;
            }
            break;

        case 5:
            Console.Clear();
            if (new Mastercard(c).CheckRequirements())
            {
                Mastercard mc = new Mastercard(c);
                list.Add(mc);
                gameState = 2;
            }
            else
            {
                gameState = 1;
            }
            break;
    }
}

// Print card details + actions TODO
while (gameState == 2)
{
    // Print card details
    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(list[0]))
    {
        string name = descriptor.Name;
        object value = descriptor.GetValue(list[0]);
        Console.WriteLine("{0} = {1}", name, value);
    }

    Console.WriteLine("\n\n");
    string menu = "1. Domestic Transaction\n" +
        "2. International transaction\n" +
        "3. Online transaction\n" +
        "4. Deposit money\n\n" +
        "5. New customer";

    Console.WriteLine(menu);

    gameState = 3;
}

if (gameState == 3)
{
    Console.ReadKey();
}