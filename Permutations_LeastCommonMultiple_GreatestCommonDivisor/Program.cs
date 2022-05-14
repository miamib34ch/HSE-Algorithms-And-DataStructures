using static System.Console;

double N = double.Parse(ReadLine());
double K = double.Parse(ReadLine());
WriteLine($"НОД Евклид медленный: {NOD_E_S(N, K)}");
WriteLine($"НОД Евклид быстрый, через остаток: {NOD_E_F(N, K)}");
WriteLine($"НОК: {NOK(N, K)}");
NOD_E_S_I(N, K);
WriteLine($"НОД итерацией равен: {NOD_E_F_I(N, K)}");
WriteLine($"НОК итерацией равен: {NOK_I(N, K)}");
Write("Введите число перестановок: ");
int m = int.Parse(ReadLine());
int[,] z = Perestanovki(m);
for (int i = 0; i < factorial(m); i++)
{
    for (int b = 0; b < m; b++)
        Write(z[i, b]);
    WriteLine();
}

static double NOD_E_S(double N, double K) //Евклид но медленный
{
    if (N == 0) //база рекурсии
        return K;
    if (N <= K)
        return NOD_E_S(K - N, N);
    else
        return NOD_E_S(N - K, K);
}

static double NOD_E_F(double N, double K) //Евклид быстрый, через остаток
{
    if (N == 0) //база рекурсии
        return K;
    if (N <= K)
        return NOD_E_F(K % N, N);
    else
        return NOD_E_F(N % K, K);
}

static double NOK(double N, double K) //НОК, используется НОД
{
    return (N * K) / NOD_E_F(N, K);
}

static void NOD_E_S_I(double N, double K) //Евклид но медленный итерацией
{
    do
    {
        double tmp = 0;
        if (N <= K)
        {
            tmp = K - N;
            K = N;
            N = tmp;
        }
        else
        {
            N -= K;
        }
    } while (N != 0);
    WriteLine($"НОД (медленный, Евклидом) итерацией равен: {K}");
}

static double NOD_E_F_I(double N, double K) //Евклид но быстрый итерацией
{
    do
    {
        double tmp = 0;
        if (N <= K)
        {
            tmp = K % N;
            K = N;
            N = tmp;
        }
        else
        {
            N = N % K;
        }
    } while (N != 0);
    return K;
}

static double NOK_I(double N, double K) //НОК, используется НОД итерационный
{
    return (N * K) / NOD_E_F_I(N, K);
}

static int[,] Perestanovki(int m) //Дополнительное задание: перестановка (без повторений) первых N натуральных чисел
{
    if (m == 1) //база рекурсии
        return new int[1, 1] { { 1 } };
    int[,] tmp_z = Perestanovki(m - 1);
    int fac_m1 = factorial(m - 1);
    int fac_m = fac_m1 * m;
    int[,] z = new int[fac_m, m];
    int k = 0;
    for (int i = 0; i < m; i++) //заполнение m
    {
        for (int j = k; j < k + fac_m1; j++)
            z[j, i] = m;
        k += fac_m1;
    }
    for (int j = 0; j < fac_m; j += fac_m1)    //заполнение текущего массива (m), массивом перестановок m-1
    {
        for (int i = 0; i < m; i++)
            if (z[j, i] == m)
            {
                k = i;
                break;
            }
        for (int i = 0; i < fac_m1; i++)
        {
            for (int b = 0; b < k; b++)
                z[j + i, b] = tmp_z[i, b];
            for (int u = k; u < m - 1; u++)
                z[j + i, u + 1] = tmp_z[i, u];
        }
    }
    return z;
}

static int factorial(int m)
{
    int factorial = 1;
    for (int i = 2; i <= m; i++)
        factorial = factorial * i;
    return factorial;
}
