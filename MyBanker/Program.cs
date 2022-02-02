using MyBanker;
using System.ComponentModel;

Customer c = new Customer("Lis Sørensen", 18, new Account(2500));

Console.WriteLine("\n--- DEBIT CARD TEST ---\n");

if (new DebitCard(c).CheckRequirements())
{
    DebitCard dc = new DebitCard(c);

    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(dc))
    {
        string name = descriptor.Name;
        object value = descriptor.GetValue(dc);
        Console.WriteLine("{0}={1}", name, value);
    }

    Console.WriteLine("Under limit: \n");
    Console.WriteLine(dc.DomesticTransaction(2400));
    Console.WriteLine("Over limit: \n");
    Console.WriteLine(dc.DomesticTransaction(200));
}

c.Account.Deposit(-100); // DEBUG

Console.WriteLine("\n---MAESTRO TEST---\n");

if (new Maestro(c).CheckRequirements() == true)
{
    Maestro m = new Maestro(c);

    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(m))
    {
        string name = descriptor.Name;
        object value = descriptor.GetValue(m);
        Console.WriteLine("{0} = {1}", name, value);
    }

    c.Account.Deposit(10030);

    Console.WriteLine("Under limit: \n");

    Console.WriteLine(m.DomesticTransaction(5000));
    Console.WriteLine(m.InternationalTransaction(30, "Euros"));
    Console.WriteLine(m.OnlineTransaction(5000, "SEK"));

    Console.WriteLine("Over limit: \n");

    Console.WriteLine(m.DomesticTransaction(5000));
    Console.WriteLine(m.InternationalTransaction(30, "Euros"));
    Console.WriteLine(m.OnlineTransaction(5000, "SEK"));
}

Console.WriteLine("\n---VISA ELECTRON TEST---\n");

if (new VisaElectron(c).CheckRequirements())
{
    VisaElectron v = new VisaElectron(c);

    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(v))
    {
        string name = descriptor.Name;
        object value = descriptor.GetValue(v);
        Console.WriteLine("{0} = {1}", name, value);
    }

    Console.WriteLine("\nUnder limit: \n");
    Console.WriteLine(v.DomesticTransaction(5000));
    Console.WriteLine(v.InternationalTransaction(30, "Euros"));
    Console.WriteLine(v.OnlineTransaction(5000, "SEK"));

    Console.WriteLine("\nOver limit: \n");
    Console.WriteLine(v.DomesticTransaction(11000));
    Console.WriteLine(v.InternationalTransaction(11000, "Euros"));
    Console.WriteLine(v.OnlineTransaction(11000, "SEK"));
}

Console.WriteLine("\n---VISA/DANKORT TEST\n");

if (new VisaDankort(c).CheckRequirements())
{
    VisaDankort v = new VisaDankort(c);

    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(v))
    {
        string name = descriptor.Name;
        object value = descriptor.GetValue(v);
        Console.WriteLine("{0} = {1}", name, value);
    }

    Console.WriteLine("\nUnder limit: \n");
    Console.WriteLine(v.DomesticTransaction(5000));
    Console.WriteLine(v.InternationalTransaction(30, "Euros"));
    Console.WriteLine(v.OnlineTransaction(5000, "SEK"));

    Console.WriteLine("\nOver limit: \n");
    Console.WriteLine(v.DomesticTransaction(11000));
    Console.WriteLine(v.InternationalTransaction(11000, "Euros"));
    Console.WriteLine(v.OnlineTransaction(11000, "SEK"));
}

Console.WriteLine("\n---MASTERCARD TEST---\n");

if (new Mastercard(c).CheckRequirements())
{
    Mastercard m = new Mastercard(c);

    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(m))
    {
        string name = descriptor.Name;
        object value = descriptor.GetValue(m);
        Console.WriteLine("{0} = {1}", name, value);
    }

    Console.WriteLine("\nUnder limit: \n");
    Console.WriteLine(m.DomesticTransaction(5000));
    Console.WriteLine(m.InternationalTransaction(30, "Euros"));
    Console.WriteLine(m.OnlineTransaction(5000, "SEK"));

    Console.WriteLine("\nOver limit: \n");
    Console.WriteLine(m.DomesticTransaction(11000));
    Console.WriteLine(m.InternationalTransaction(11000, "Euros"));
    Console.WriteLine(m.OnlineTransaction(11000, "SEK"));
}

Console.ReadKey();