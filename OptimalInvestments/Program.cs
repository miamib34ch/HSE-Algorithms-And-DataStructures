namespace OptimalInvestments
{
    class Program
    {
        public static Profit[,] arr_best;
        static void Main(string[] args)
        {
            do
            {
                Console.Write("Введите кратность: ");
                int kr = int.Parse(Console.ReadLine());
                Console.Write("Введите количество проектов: ");
                int num_proj = int.Parse(Console.ReadLine());
                Console.Write("Введите количество вложений: ");
                int num_vloj = int.Parse(Console.ReadLine());
                Profit[,] arr = new Profit[num_vloj, num_proj];
                for (int i = 0; i < num_vloj; i++)
                {
                    bool check = true;
                    int vloj;
                    do
                    {
                        Console.Write("Введите вложение: ");
                        vloj = int.Parse(Console.ReadLine());
                        if (vloj % kr == 0)
                            check = false;
                    } while (check);
                    for (int j = 0; j < num_proj; j++)
                    {
                        Console.Write($"Введите прибыль для {j + 1}-ого проекта: ");
                        int prof = int.Parse(Console.ReadLine());
                        arr[i, j] = new Profit(vloj, prof, j + 1, vloj, 0);
                    }
                } //ввод таблички
                arr = Get_best(arr, num_proj, num_vloj); //столбик последний, где самые лучшие для последнего проекта
                Profit max = new Profit(arr[0, 0].vloj, arr[0, 0].prof, arr[0, 0].num, arr[0, 0].ex_vloj, arr[0, 0].tek_vloj); //максимальный элемент в этом последнем столбике
                for (int j = 1; j < num_vloj; j++) //его поиск
                {
                    if (arr[j, 0].prof > max.prof)
                        max = arr[j, 0];
                }
                Console.WriteLine($"Максимальная прибыль: {max.prof}");
                Kak(max, num_vloj, num_proj);
                Console.ReadLine();
            } while (true);
        }

        static Profit[,] Get_best(Profit[,] arr, int num_proj, int num_vloj)  //функция получения лучших вариантов прибыли
        {
            arr_best = new Profit[num_vloj, num_proj - 1]; //массив, хранящий все лучшие результаты
            int how_much_proj = 0; //счётчик, считающий сколько проектов в arr_best
            int ex_vloj = 0; //для ссылки на прошлое вложение
            int tek_vloj = 0; //для ссылки на текущие вложение в строке
            while (num_proj != 1)   //итерация, пока количество проектов не станет равно 1
            {
                Profit[,] new_arr = new Profit[num_vloj, 1]; //столбец получаемый из двух
                int max = -1;
                for (int i = 0; i < num_vloj; i++)  //для каждого вложения перебираем все варинты суммы, выбираем лучший
                {
                    for (int j = 0; j < num_vloj; j++)
                    {
                        for (int k = 0; k < num_vloj; k++)
                        {
                            if (arr[j, 0].vloj + arr[k, 1].vloj == arr[i, 0].vloj) //условие что сумма вложений варинтов должна быть равна вложению для перебираемого
                                if (arr[k, 0].prof + arr[j, 1].prof > max) //условие для определения максимума 
                                {
                                    max = arr[k, 0].prof + arr[j, 1].prof;
                                    ex_vloj = arr[k, 0].vloj;
                                    tek_vloj = arr[j, 1].vloj;
                                }
                        }
                    }
                    if (arr[i, 0].prof >= max && arr[i, 0].prof >= arr[i, 1].prof)   //определяем что лучше, данные изначально значения во вложении по проектам или полученный нами максимальный
                    {
                        new_arr[i, 0] = new Profit(arr[i, 0].vloj, arr[i, 0].prof, arr[i, 1].num, arr[i, 0].vloj, 0);
                    }
                    else
                    {
                        if (arr[i, 1].prof >= max && arr[i, 1].prof > arr[i, 0].prof)
                        {
                            new_arr[i, 0] = new Profit(arr[i, 1].vloj, arr[i, 1].prof, arr[i, 1].num, 0, arr[i, 1].vloj);
                        }
                        else
                        {
                            new_arr[i, 0] = new Profit(arr[i, 0].vloj, max, arr[i, 1].num, ex_vloj, tek_vloj);
                        }
                    }
                }
                Profit[,] tmp_arr = new Profit[num_vloj, --num_proj];   //временный массив для соединения оставшихся столбцов и с лучшими результатами 
                for (int j = 0; j < num_vloj; j++) //соединение
                {
                    tmp_arr[j, 0] = new_arr[j, 0];
                    for (int k = 1; k < num_proj; k++)
                    {
                        tmp_arr[j, k] = arr[j, k + 1];
                    }
                }
                for (int j = 0; j < num_vloj; j++) //сохраняем лучшие варианты в отдельный массив
                {
                    arr_best[j, how_much_proj] = new_arr[j, 0];
                }
                how_much_proj++;
                arr = tmp_arr; //присваиваем arr новую табличку, где вместо двух первых столбцов - лучший полученный
            }
            return arr;
        }

        public static void Kak(Profit max, int num_vloj, int num_proj) //функция вывода того, как вкладывать 
        {
            int check = max.ex_vloj + max.tek_vloj;
            for (int i = num_proj - 2; i >= 0; i--)
            {
                for (int j = 0; j < num_vloj; j++)
                {
                    if (arr_best[j, i].vloj == check) //определяем какая прибыль использовалась 
                    {
                        Console.WriteLine($"В проект {i + 2} нужно вложить {arr_best[j, i].tek_vloj}");
                        check = arr_best[j, i].ex_vloj;
                        break;
                    }
                }
            }
            if (check != 0)
                Console.WriteLine($"В проект 1 нужно вложить {check}");
        }
    }
}