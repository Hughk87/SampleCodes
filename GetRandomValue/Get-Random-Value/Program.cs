using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Get_Random_Value
{
    class Program
    {
        static void Main(string[] args)
        {
            //절대값이 min이 더 큰경우 에러 체크.

            //둘다 양수
            Console.WriteLine(GetRandomVar.Range(0, 100).ToString());
            Console.WriteLine(GetRandomVar.Range(10, 110).ToString());
            Console.WriteLine(GetRandomVar.Range(100, 0).ToString());

            //max가 음수
            Console.WriteLine(GetRandomVar.Range(0, -100).ToString());
            Console.WriteLine(GetRandomVar.Range(10, -110).ToString());

            //min이 음수
            Console.WriteLine(GetRandomVar.Range(0, 100).ToString());
            Console.WriteLine(GetRandomVar.Range(-10, 110).ToString());

            //둘다 음수
            Console.WriteLine(GetRandomVar.Range(0, -100).ToString());
            Console.WriteLine(GetRandomVar.Range(-10, -20).ToString());
            Console.WriteLine(GetRandomVar.Range(-10, 0).ToString());

            //int _succ_count = 0;
            //int _result = 0;

            //int _min = -200;
            //int _max = -100;

            //for (int i = 0; i < 10000; i++)
            //{
            //    _result = GetRandomVar.Range(_min, _max);
            //    if (_min <= _max)
            //    {
            //        if (_result >= _min && _result < _max)
            //            _succ_count++;
            //    }
            //    else if (_min > _max)
            //    {
            //        if (_result >= _max && _result < _min)
            //            _succ_count++;
            //    }
            //}
            //Console.WriteLine(_succ_count.ToString());
        }
    }

}
