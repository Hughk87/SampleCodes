using System;
public class GetRandomVar
{
    static Random r_random = null;
    public static int Range(int _min, int _max)
    {
        if (_min == _max)
        {
            Console.WriteLine("Wrong Value :: min == max");
            return _min;
        }

        if (r_random == null)
            r_random = new Random(DateTime.Now.Millisecond);

        //총 4가지 경우의 수.
        //_min >= 0일때,
        if (_min >= 0)
        {
            //case 1 :: _max >= 0
            if (_max >= 0)
            {
                if (_min <= _max)
                    return r_random.Next(_min, _max);
                else
                    return r_random.Next(_max, _min);
            }
            //case 2 :: _max < 0
            else
            {
                int _temp_additive = _max;

                _min += -_temp_additive;
                _max += -_temp_additive; // max = 0 / min = ori value - (max :: 음수).

                //현재 min은 양수 / max는 0 이므로, max부터~ min까지의 랜덤값을 구한뒤, 다시 초기의 _max만큼 빼주고 리턴.
                return r_random.Next(_max, _min) + _temp_additive;
            }
        }
        //_min < 0일때,
        else
        {
            //case 3 :: _max >= 0
            if (_max >= 0)
            {
                int _temp_additive = _min;

                _min += -_temp_additive;
                _max += -_temp_additive; // min = 0 / max = ori value - (min :: 음수).

                //현재 min은 양수 / max는 0 이므로, max부터~ min까지의 랜덤값을 구한뒤, 다시 초기의 _max만큼 빼주고 리턴.
                return r_random.Next(_min, _max) + _temp_additive;
            }
            //case 4 :: _max < 0
            else
            {
                if (_min < _max)
                {
                    int _temp_additive = _min;

                    _min += -_temp_additive;
                    _max += -_temp_additive;

                    return r_random.Next(_min, _max) + _temp_additive;

                }
                else if (_min > _max)
                {
                    int _temp_additive = _max;

                    _min += -_temp_additive;
                    _max += -_temp_additive;

                    return r_random.Next(_max, _min) + _temp_additive;
                }
            }
        }
        return -1;
    }

    public static bool GetRandomBool()
    {
        if (Range(0, 2) == 1)
            return true;
        else
            return false;
    }

    public static int GetRandomIndex(int[] _randomsection)//랜덤구간은 밀리단위로 받음.
    {
        //결과값.
        int retval_index = -1;

        //랜덤 구간의 갯수.
        int _sectioncount = _randomsection.Length;

        RandomDATA[] _ori_arr = new RandomDATA[_sectioncount];
        for (int i = 0; i < _sectioncount; i++)
        {
            _ori_arr[i] = new RandomDATA(i, _randomsection[i]);
        }


        //인자로 받은 int배열을 큰순서대로 정렬.
        RandomDATA[] _randomsection_sort = new RandomDATA[_sectioncount];
        int _index = 0;
        for (int i = 0; i < _sectioncount; i++)
        {
            _index = 0;

            for (int j = 0; j < _sectioncount; j++)
            {
                if (_ori_arr[j].m_chance > _ori_arr[_index].m_chance)
                {
                    _index = j;
                }
            }
            _randomsection_sort[i] = new RandomDATA(_ori_arr[_index]);
            _ori_arr[_index] = new RandomDATA();
        }

        //최대값(모든 랜덤 구간의 총합)
        int _max = 0;
        int _randomvar = 0;

        //확률 계산. 구간의 수만큼 반복.
        for (int i = 0; i < _sectioncount; i++)
        {
            //마지막 루프인경우 따로 처리 하지 않고 결과값 저장 밑 루프탈출.
            if (i == _sectioncount - 1)
            {
                retval_index = _sectioncount - 1;
                break;
            }
            else
            {
                _max = 0;

                //인자로 받은 확률의 경우를 i부터 시작해 모두 더해 최대 값을계산.
                for (int j = i; j < _sectioncount; j++)
                {
                    _max += _randomsection_sort[j].m_chance;
                }

                //0 ~ _max 까지 랜덤.
                _randomvar = GetRandomVar.Range(0, _max);

                // 랜덤값이 _randomsection_sort[i]보다 작은경우, 루프를 끝내고 해당 i번째 인덱스를 리턴.
                if (_randomvar < _randomsection_sort[i].m_chance)
                {
                    retval_index = i;
                    break;
                }
            }
        }

        //결과로 뽑은 인덱스의 원래 인덱스를 반환함.
        return _randomsection_sort[retval_index].m_index;
    }
}
public class RandomDATA
{
    public int m_index;
    public int m_chance;
    public RandomDATA()
    {
        m_index = -1;
        m_chance = 0;
    }
    public RandomDATA(int _index, int _chance)
    {
        m_index = _index;
        m_chance = _chance;
    }
    public RandomDATA(RandomDATA _copy_origin)
    {
        m_index = _copy_origin.m_index;
        m_chance = _copy_origin.m_chance;
    }

}