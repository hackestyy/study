using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZTE.Game
{
    public class Game
    {
        public Game()
        {
            _time = 0;
        }

        private int [] _pins = new int[24];
        private int _time;
        private int _score;
        public int Score
        { 
            get
            {
                _score = 0;
                for (int i = 1; i < Math.Min(_time / 2 + 1, 11); i++)
                {
                    _score += ScoreForFrame(i);
                }
                return _score;
            }
        }

        public void Add(int pin)
        {
            _pins[_time++] = pin;

            SetQuanZhongSecondPinToZero(pin);
        }

        private void SetQuanZhongSecondPinToZero(int pin)
        {
            if (_time % 2 == 1 && pin == 10)
                _pins[_time++] = 0;
        }

        public int ScoreForFrame(int frame)
        {
            return frame == 0 ? 0 : ScoreForFrameNonZero(frame);
        }

        private int ScoreForFrameNonZero(int frame)
        {
            if (IsQuanZhong(frame))
                return QuanZhongHandle(frame);
            else if (IsBuZhong(frame))
                return 10 + GetFirstPin(frame + 1);
            else
                return GetFirstPin(frame) + GetSecondPin(frame); 
        }

        private int QuanZhongHandle(int frame)
        {
            if (IsQuanZhong(frame + 1))
                return 10 + GetFirstPin(frame + 1) + GetFirstPin(frame + 2);
            else
                return 10 + GetFirstPin(frame + 1) + GetSecondPin(frame + 1);
        }

        private bool IsBuZhong(int frame)
        {
            return GetFirstPin(frame) + GetSecondPin(frame) == 10;
        }

        private int GetSecondPin(int frame)
        {
            return _pins[2 * (frame - 1) + 1];
        }

        private int GetFirstPin(int frame)
        {
            return _pins[2 * (frame - 1)];
        }

        private bool IsQuanZhong(int frame)
        {
            return _pins[2 * (frame - 1)] == 10;
        }
    }
}
