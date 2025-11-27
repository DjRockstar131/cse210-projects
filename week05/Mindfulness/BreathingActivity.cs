using System;

namespace MindfulnessProgram
{
    public class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base(
                "Breathing Activity",
                "This activity will help you relax by walking you through breathing in and out slowly. " +
                "Clear your mind and focus on your breathing.")
        {
        }

        public override void RunActivity()
        {
            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);
            bool breatheIn = true;

            while (DateTime.Now < endTime)
            {
                if (breatheIn)
                {
                    Console.Write("Breathe in... ");
                }
                else
                {
                    Console.Write("Breathe out... ");
                }

                // 4-second count for each breath phase (you can adjust)
                ShowCountDown(4);
                Console.WriteLine();
                breatheIn = !breatheIn;
            }
        }
    }
}
