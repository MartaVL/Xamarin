using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace L5RHelper
{
    public partial class PagePlayer : ContentPage
    {
        private static int PortInPlayer = 60001;
        private static int PortOutPlayer = 60002;

        private Client clientConnection;
        public PagePlayer()
        { 
            InitializeComponent();                        
        }

        private void ProbabilityOfRoll()
        {
            float numMuestras = 1000f;
            float porcentaje = 100f / numMuestras;
            string porcentajeString = "";

            for (int keep = 1; keep <= 10; keep++)
            {
                for (int diceNum = 1; diceNum <= 10; diceNum++)
                {
                    float tn5 = 0f, tn10 = 0f, tn15 = 0f, tn20 = 0f, tn25 = 0f, tn30 = 0f, tn35 = 0f, tn40 = 0f, tn45 = 0f, tn50 = 0f, tn55 = 0f, tn60 = 0f, tn0 = 0f;

                    for (int x = 1; x <= numMuestras; x++)
                    {
                        List<Die> result = (List<Die>)Roll.RollDices(diceNum, keep, true);

                        int Total = 0;

                        foreach (Die dice in result)
                        {
                            Total += dice.Total;
                        }

                        if (Total >= 0)
                        {
                            tn0++;
                        }

                        if (Total >= 5)
                        {
                            tn5++;
                        }

                        if (Total >= 10)
                        {
                            tn10++;
                        }

                        if (Total >= 15)
                        {
                            tn15++;
                        }

                        if (Total >= 20)
                        {
                            tn20++;
                        }

                        if (Total >= 25)
                        {
                            tn25++;
                        }

                        if (Total >= 30)
                        {
                            tn30++;
                        }

                        if (Total >= 35)
                        {
                            tn35++;
                        }

                        if (Total >= 40)
                        {
                            tn40++;
                        }

                        if (Total >= 45)
                        {
                            tn45++;
                        }

                        if (Total >= 50)
                        {
                            tn50++;
                        }

                        if (Total >= 55)
                        {
                            tn55++;
                        }

                        if (Total >= 60)
                        {
                            tn60++;
                        }
                    }

                    porcentajeString = diceNum + "k" + keep + "\n TN0: " + tn0 * porcentaje + "%" +
                        "\n TN5: " + tn5 * porcentaje + "%" +
                        "\n TN10: " + tn10 * porcentaje + "%" +
                        "\n TN15: " + tn15 * porcentaje + "%" +
                        "\n TN20: " + tn20 * porcentaje + "%" +
                        "\n TN25: " + tn25 * porcentaje + "%" +
                        "\n TN30: " + tn30 * porcentaje + "%" +
                        "\n TN35: " + tn35 * porcentaje + "%" +
                        "\n TN40: " + tn40 * porcentaje + "%" +
                        "\n TN45: " + tn45 * porcentaje + "%" +
                        "\n TN50: " + tn50 * porcentaje + "%" +
                        "\n TN55: " + tn55 * porcentaje + "%" +
                        "\n TN60: " + tn60 * porcentaje + "%";

                    Debug.WriteLine(porcentajeString);

                }
            }
        }

        private async void ButtonConnection_Clicked(object sender, EventArgs e)
        {
            string ipServer = EditorIpServerConnection.Text;

            if(string.IsNullOrEmpty(ipServer))
            {
                LabelError.Text = "Ip Address not valid";
                return;
            }

            clientConnection = new Client(ipServer, PortOutPlayer);

            Debug.WriteLine("Connecting with master");

            bool resultConnection = await clientConnection.ConnectAsync();

            Debug.WriteLine("Connected with master " +  resultConnection);

            if(resultConnection)
            {
                roll.IsEnabled = true;
                LabelError.Text = "";
            }
            
        }
        private void ButtonRoll_Clicked(object sender, EventArgs e)
        {
            List<Die> result = (List<Die>)Roll.RollDices(7, 5, true);

            int Total = 0;
            string StringResult = "";

            foreach (Die dice in result)
            {
                Total += dice.Total;
                StringResult += dice.getString() + "\n";
                    
            }

            LabelResult.Text = StringResult + "Total: " + Total;

            Message message = new Message
            {
                Dice = result
            };

            clientConnection.SendRoll(message);
        }
    }
}