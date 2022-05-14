using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalInvestments
{
    public class Profit
    {
        public int vloj; //вложение
        public int prof; //прибыль
        public int num; //проект
        public int ex_vloj; //ссылка на прошлое вложение
        public int tek_vloj = 0; //ссылка на текущие вложение в строке

        public Profit(int vloj, int prof, int num, int ex_vloj, int tek_vloj)
        {
            this.vloj = vloj;
            this.prof = prof;
            this.num = num;
            this.ex_vloj = ex_vloj;
            this.tek_vloj = tek_vloj;
        }
    }
}
