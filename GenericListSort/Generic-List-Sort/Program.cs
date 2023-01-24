using System;
using System.Collections;
using System.Collections.Generic;

namespace Generic_List_Sort
{
    class Program
    {
        static void Main(string[] args)
        {

            //List<string> _list_str = new List<string>();
            //_list_str.Add("가");
            //_list_str.Add("가가");
            //_list_str.Add("나였다면");
            //_list_str.Add("바람의윈드");
            //_list_str.Add("라디오");
            //_list_str.Add("가간");
            //_list_str.Add("Hero");
            //_list_str.Add("우연히");
            //_list_str.Add("간가");
            //_list_str.Add("똥쟁이");
            //_list_str.Add("가갇");
            //_list_str.Add("Name");

            ////test 1.
            //_list_str.Sort(new Comparer_String());

            //Console.WriteLine("문자열 List 출력");
            //for (int i = 0; i < _list_str.Count; i++)
            //{
            //    Console.WriteLine(_list_str[i]);
            //}
            //Console.WriteLine("------------------------------------");

            ////test 2.
            //_list_str.Sort();

            //Console.WriteLine("문자열 List 출력");
            //for (int i = 0; i < _list_str.Count; i++)
            //{
            //    Console.WriteLine(_list_str[i]);
            //}
            //Console.WriteLine("------------------------------------");


            //List<int> _list_value = new List<int>();

            //_list_value.Add(123546);
            //_list_value.Add(56431132);
            //_list_value.Add(4567789);
            //_list_value.Add(8645623);
            //_list_value.Add(12315465);
            //_list_value.Add(12356789);
            //_list_value.Add(10321);
            //_list_value.Add(3212301);
            //_list_value.Add(890564);
            //_list_value.Add(123065);

            //// test 3. 기본 sort 오름차순
            //_list_value.Sort();
            //Console.WriteLine("정수형 List 출력");
            //for (int i = 0; i < _list_value.Count; i++)
            //{
            //    Console.WriteLine(_list_value[i]);
            //}

            //// test 4. 내림차순
            //_list_value.Sort(new Comparer_Int_descend());
            //Console.WriteLine("정수형 List 출력");
            //for (int i = 0; i < _list_value.Count; i++)
            //{
            //    Console.WriteLine(_list_value[i]);
            //}

            //// test 5. 오름차순
            //_list_value.Sort(new Comparer_Int_ascend());
            //Console.WriteLine("정수형 List 출력");
            //for (int i = 0; i < _list_value.Count; i++)
            //{
            //    Console.WriteLine(_list_value[i]);
            //}

            ////test 6.Reverse 기능 사용
            //_list_value.Reverse();
            //Console.WriteLine("정수형 List Reverse 출력");
            //for (int i = 0; i < _list_value.Count; i++)
            //{
            //    Console.WriteLine(_list_value[i]);
            //}


            //test 7. 다중 조건 정렬 사용 예시 - 아이템
            List<ItemInfo> r_list_iteminfo = new List<ItemInfo>();
            int _create_amount = 2000;
            for (int i = 0; i < 5; i++)
                r_list_iteminfo.Add(new ItemInfo(r_list_iteminfo.Count, ITEM.MAN_T1_GLOVE_1_E, true));

            for (int i = 0; i < _create_amount; i++)
                r_list_iteminfo.Add(new ItemInfo(r_list_iteminfo.Count, ITEM.MAN_T1_GLOVE_1_E + GetRandomVar.Range(0, 7)));

            r_list_iteminfo.Sort(new Comparer_Item());

            for (int i = 0; i < r_list_iteminfo.Count; i++)
                r_list_iteminfo[i].Print();
        }
    }

    //문자열 정렬 (스펠링 순서) 영어 -> 한글 / 오름차순
    public class Comparer_String : IComparer, IComparer<string>
    {
        public int Compare(object x, object y)
        {
            return x.ToString().CompareTo(y.ToString());
        }
        public int Compare(string x1, string y1)
        {
            return x1.CompareTo(y1);
        }
    }
    //오름차순 정렬 - 작은것 -> 큰것
    public class Comparer_Int_ascend : IComparer, IComparer<int>
    {
        public int Compare(object x, object y)
        {
            int x1 = (int)x;
            int y1 = (int)y;

            if (x1 > y1)
                return 1;
            else if (x1 < y1)
                return -1;
            else
                return 0;
        }
        public int Compare(int x1, int y1)
        {
            if (x1 > y1)
                return 1;
            else if (x1 < y1)
                return -1;
            else
                return 0;
        }
    }
    //내림차순 정렬 - 큰것 -> 작은것
    public class Comparer_Int_descend : IComparer, IComparer<int>
    {
        public int Compare(object x, object y)
        {
            int x1 = (int)x;
            int y1 = (int)y;

            if (x1 > y1)
                return -1;
            else if (x1 < y1)
                return 1;
            else
                return 0;
        }
        public int Compare(int x1, int y1)
        {
            if (x1 > y1)
                return -1;
            else if (x1 < y1)
                return 1;
            else
                return 0;
        }
    }
    public class Comparer_Item : IComparer<ItemInfo>
    {
        //compare 함수를 구현할 때는, 우선시 되는 조건값을 먼저 비교함.
        public int Compare(ItemInfo x1, ItemInfo y1)
        {
            //둘중 하나의 아이템이 하나만 장착중
            if (x1.is_equip && !y1.is_equip)
                return -1;
            else if (y1.is_equip && !x1.is_equip)
                return 1;
            //둘다 장착중이 아니거나, 둘다 장착중일 때는 다른 조건으로 비교한다.
            else
            {
                int _grade_x = Getvalue.GetGrade_ByItem(x1.item_def);
                int _grade_y = Getvalue.GetGrade_ByItem(y1.item_def);

                //등급 비교
                if (_grade_x > _grade_y)
                    return -1;
                else if (_grade_x < _grade_y)
                    return 1;
                //등급이 같을 때
                else
                {
                    //레벨 비교
                    if (x1.enchant > y1.enchant)
                        return -1;
                    else if (x1.enchant < y1.enchant)
                        return 1;
                    //레벨이 같을 때
                    else
                    {
                        //경험치 비교
                        if (x1.exp > y1.exp)
                            return -1;
                        else if (x1.exp < y1.exp)
                            return 1;
                        //경험치 까지 모두 같을 때는 같다
                        else
                        {
                            //Id number 비교
                            if (x1.item_un > y1.item_un)
                                return -1;
                            else if (x1.item_un < y1.item_un)
                                return 1;
                            else
                                return 0;
                        }
                    }
                }
            }
        }
    }
   
}
