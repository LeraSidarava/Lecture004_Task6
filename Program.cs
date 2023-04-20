using System;

namespace Lightstrings
{
    abstract class Lightstring
    {
        protected Bulb[] bulbs;
        public Lightstring(int numBulbs)
        {
            bulbs = new Bulb[numBulbs];
            for (int i = 0; i < numBulbs; i++)
            {
                bulbs[i] = new Bulb();
            }
        }

        public abstract string GetCurrentState();

        protected bool IsBulbOn(int i, int currentMinute)
        {
            if (currentMinute % 2 == 0)
            {
                return i % 2 == 0;
            }
            else
            {
                return i % 2 == 1;
            }
        }
    }

    class SimpleLightstring : Lightstring
    {
        public SimpleLightstring(int numBulbs) : base(numBulbs)
        {
        }

        public override string GetCurrentState()
        {
            string state = "";
            int currentMinute = DateTime.Now.Minute;

            for (int i = 0; i < bulbs.Length; i++)
            {
                if (IsBulbOn(i, currentMinute))
                {
                    state += "on ";
                }
                else
                {
                    state += "off ";
                }
            }

            return state;
        }
    }

    class ColoredLightstring : Lightstring
    {
        private Color[] colors = { Color.Red, Color.Yellow, Color.Green, Color.Blue };

        public ColoredLightstring(int numBulbs) : base(numBulbs)
        {
            for (int i = 0; i < bulbs.Length; i++)
            {
                Color color = colors[i % colors.Length];
                bulbs[i] = new ColoredBulb(color);
            }
        }

        public override string GetCurrentState()
        {
            string state = "";
            int currentMinute = DateTime.Now.Minute;

            for (int i = 0; i < bulbs.Length; i++)
            {
                if (IsBulbOn(i, currentMinute))
                {
                    state += "on ";
                }
                else
                {
                    state += "off ";
                }
                if (bulbs[i] is ColoredBulb)
                {
                    state += ((ColoredBulb)bulbs[i]).GetColor() + " ";
                }
            }

            return state;
        }
    }

    class Bulb
    {
        public bool IsOn { get; set; }

        public Bulb()
        {
            IsOn = false;
        }
    }

    class ColoredBulb : Bulb
    {
        private Color color;

        public ColoredBulb(Color color)
        {
            this.color = color;
        }

        public string GetColor()
        {
            return color.ToString();
        }
    }

    enum Color
    {
        Red,
        Yellow,
        Green,
        Blue
    }

    class Program
    {
        static void Main(string[] args)
        {
            Lightstring simple = new SimpleLightstring(10);
            Lightstring colored = new ColoredLightstring(10);

            Console.WriteLine("Simple Lightstring:");
            Console.WriteLine(simple.GetCurrentState());
            Console.WriteLine();

            Console.WriteLine("Colored Lightstring:");
            Console.WriteLine(colored.GetCurrentState());
            Console.WriteLine();
        }
    }
}
