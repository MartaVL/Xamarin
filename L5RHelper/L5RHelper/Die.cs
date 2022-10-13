using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace L5RHelper
{
    [XmlRoot(ElementName = "die")]
    public class Die
    {
        [XmlElement(ElementName = "id")]
        private int _id;

        public int Id
        {
            get => _id;
            set 
            {
                _id = value;
            }
        }
     
        private int _value;

        [XmlElement(ElementName = "value")]
        public int Value
        {
            get => _value;

            set
            {
                _value = value;
            }
        }

        private bool _speciality;

        [XmlElement(ElementName = "speciality")]
        public bool Speciality
        {
            get => _speciality;

            set
            {
                _speciality = value;
            }
        }

        private bool _reroll;

        public bool Reroll
        {
            get => _reroll;
            
            set
            {
                _reroll = value;
            }
        }

        [XmlElement(ElementName = "total")]
        public int Total
        {
            get
            {
                int total = Value;

                if(Explode != null)
                {
                    total += Explode.Total;
                }

                return total;
            }
        }

        [XmlElement(ElementName = "explode")]
        public Die Explode;

        public Die()
        {
            Value = 0;
            Speciality = false;
            Explode = null;
            Reroll = false;
        }

        public void Roll(Random random)
        {
            Value = random.Next(1, 11);

            if(Value == 10)
            {
                Explode = new Die()
                {
                    Reroll = true
                };

                Explode.Roll(random);
            }

            if(Value == 1 && Speciality && !Reroll)
            {
                Reroll = true;
                Roll(random);
            }

            //Debug.WriteLine(getString());
        }

        public string getString()
        {
            string StringDice = "";

            StringDice += Total.ToString();
            
            if (Explode != null)
            {
                StringDice += ": \n\t" + Value + "\n\t" + Explode.getString();
            }

            return StringDice;
        }

    }
}
