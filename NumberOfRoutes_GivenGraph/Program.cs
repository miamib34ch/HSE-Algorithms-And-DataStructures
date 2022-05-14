do
{
    try
    {
        Console.Write("Старт: ");
        char Vershina1 = Console.ReadLine()[0];
        Console.Write("Финиш: ");
        char Vershina2 = Console.ReadLine()[0];
        Console.Write("Шаги: ");
        int res = int.Parse(Console.ReadLine());
        if (res == 0)
        {
            Console.WriteLine(0);
            continue;
        }
        switch (Vershina2)
        {
            case 'A':
                res = An(res, Vershina1);
                break;
            case 'B':
                res = Bn(res, Vershina1);
                break;
            case 'C':
                res = Cn(res, Vershina1);
                break;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Всего маршрутов: " + res);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }
    catch
    {
        Console.Clear();
        Console.WriteLine("Ошибка");
        Console.WriteLine();
    }
} while (true);

static int An(int n, char V1)
{
    if (n == 1)
    {
        if (V1 == 'A')
            return 1;
        if (V1 == 'B')
            return 1;
        if (V1 == 'C')
            return 1;
    }
    return An(n - 1, V1) + Bn(n - 1, V1) + Cn(n - 1, V1);
}

static int Bn(int n, char V1)
{
    if (n == 1)
    {
        if (V1 == 'A')
            return 1;
        if (V1 == 'B')
            return 0;
        if (V1 == 'C')
            return 1;
    }
    return An(n - 1, V1) + Cn(n - 1, V1);
}

static int Cn(int n, char V1)
{
    if (n == 1)
    {
        if (V1 == 'A')
            return 0;
        if (V1 == 'B')
            return 1;
        if (V1 == 'C')
            return 1;
    }
    return Bn(n - 1, V1) + Cn(n - 1, V1);
}