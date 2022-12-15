using PersonBuilder;

var lines = File.ReadAllLines("195a_persons_with_address.csv").Skip(1).ToList();

List<string> execptions = new();

var persons = lines.Select(x =>
{
    var parts = x.Split(';');
    var p = new Person.Builder(parts[0], parts[1]);

    if (parts[2] != "")
        p.WithAge(Convert.ToInt32(parts[2]));

    if (parts[3] != "")
        p.WithPhone(parts[3]);

    if (parts[4] != "")
        p.WithAddress(parts[4]);

    try
    {
        return p.Build();
    }
    catch (Exception e)
    {
        execptions.Add(e.Message);
        return null;
    }
}).Where(x=>x is not null).ToList();

Console.WriteLine("Ok Persons: {0}",persons.Count);
//persons.ForEach(x=>Console.WriteLine(x));;
Console.WriteLine("--------------------");
Console.WriteLine("Exceptions:");
execptions.ForEach(x=>Console.WriteLine(x));
