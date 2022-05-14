Console.WriteLine("Число сочетаний");
int k = int.Parse(Console.ReadLine());
int n = int.Parse(Console.ReadLine());
if (n < 0 || k < 0 || k > n)
    return;
Console.WriteLine(Recurs1(k, n));
Console.WriteLine(Recurs2(k, n));
double[,] Arr = new double[k + 1, n + 1];
Console.WriteLine(Iter1(Arr, k, n));
Console.WriteLine(Iter2(Arr, k, n));

static double Recurs1(int k, int n)
{
    if (k == 0)
        return 1;
    return (n * Recurs1(k - 1, n - 1)) / k;
}

static int Recurs2(int k, int n) //долгий с большими числами, неэффективный метод
{
    if (k == 0 || k == n)
        return 1;
    return Recurs2(k - 1, n - 1) + Recurs2(k, n - 1);
}

static double Iter1(double[,] Arr, int k, int n)
{
    for (int i = 0; i <= k; i++)
    {
        for (int j = 0; j <= n; j++)
        {
            if (i > j)
                Arr[i, j] = 0;
            if (i == 0)
                Arr[i, j] = 1;
            if (i <= j && i > 0)
                Arr[i, j] = Arr[i, j - 1] + Arr[i - 1, j - 1];
        }
    }
    return Arr[k, n];
}

static double Iter2(double[,] Arr, int k, int n)
{
    for (int i = 0; i <= k; i++)
    {
        for (int j = 0; j <= n; j++)
        {
            if (i > j)
                Arr[i, j] = 0;
            if (i == 0)
                Arr[i, j] = 1;
            if (i <= j && i > 0)
                Arr[i, j] = (j * Arr[i - 1, j - 1]) / i;
        }
    }
    return Arr[k, n];
}