using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace L5RHelper
{
    public static class Roll
    {
        public static IList RollDices(int dices, int keep, bool speciality)
        {
            Random _random = new Random();

            List<Die> rollResult = new List<Die>();
            List<Die> allDices = new List<Die>();

            for(int i = 1; i <= dices; i++)
            {
                Die dice = new Die()
                {
                    Speciality = speciality
                };

                dice.Roll(_random);

                //Debug.WriteLine(dice.getString() + "\n");

                allDices.Add(dice);
            }

            rollResult = allDices.OrderByDescending(x => x.Total).ToList();

            int id = 1;

            foreach(Die dice in rollResult)
            {
                dice.Id = id;
                id++;
            }

            return rollResult.Where(x => x.Id <= keep).ToList();              
        }
    }
}
