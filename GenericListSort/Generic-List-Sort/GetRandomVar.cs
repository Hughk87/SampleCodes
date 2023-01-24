using System;

public class GetRandomVar
{
    static System.Random r_random = null;
    public static int Range(int _min, int _max)
    {
        if (_min == _max)
        {
            Debug.Log("Wrong Value :: min == max");
            return _min;
        }

        if (r_random == null)
            r_random = new System.Random(DateTime.Now.Millisecond);

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
}

