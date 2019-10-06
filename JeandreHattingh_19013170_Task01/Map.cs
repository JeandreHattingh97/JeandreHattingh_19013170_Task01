using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeandreHattingh_19013170_Task01
{
    class Map
    {
        Random rngod = new Random();
        public const int HEIGHT = 20;
        public const int WIDTH = 20;
        public char[,] mapVisuals = new char[WIDTH, HEIGHT];
        public Unit[] unitArr;

        int unitAmount;

        public int UnitAmount { get => unitAmount; set => unitAmount = value; }

        public Map(int numberOfUnits)
        {
            this.UnitAmount = numberOfUnits;
            unitArr = new Unit[numberOfUnits];
        }

        public void genMap()
        {
            genUnits();
            populateWithUnits();
            fillMap();
        }

        private void genUnits()
        {
            string teamName;
            char symbol;
            int xPos;
            int yPos;
            int teamNumber;

            for (int j = 0; j <= unitArr.Length - 1; j++)
            {
                int type = rngod.Next(0, 2);
                xPos = rngod.Next(0, 20);
                yPos = rngod.Next(0, 20);
                teamNumber = rngod.Next(0, 2);
                switch (type)
                {
                    case 0:
                        {
                            if (teamNumber == 0)
                            {
                                teamName = "Big Boys";
                                symbol = 'M';
                            }
                            else
                            {
                                teamName = "Lil Peeps";
                                symbol = 'm';
                            }
                            unitArr[j] = new MeleeUnit(xPos, yPos, teamName, symbol, false);
                            break;
                        }

                    case 1:
                        {
                            if (teamNumber == 0)
                            {
                                teamName = "Big Boys";
                                symbol = 'R';
                            }
                            else
                            {
                                teamName = "Lil Peeps";
                                symbol = 'r';
                            }
                            unitArr[j] = new RangedUnit(xPos, yPos, teamName, symbol, false);
                            break;
                        }
                }
            }
        }

        private void populateWithUnits()
        {
            foreach (Unit unit in unitArr)
            {
                string typeCheck = unit.GetType().ToString();
                string[] splitArray = typeCheck.Split('.');
                typeCheck = splitArray[splitArray.Length - 1];

                if (typeCheck == "MeleeUnit")
                {
                    MeleeUnit obj = (MeleeUnit)unit;
                    mapVisuals[obj.MuYPos, obj.MuXPos] = obj.MuSymbol;
                }
                else
                {
                    RangedUnit obj = (RangedUnit)unit;
                    mapVisuals[obj.RuYPos, obj.RuXPos] = obj.RuSymbol;
                }
            }
        }

        public string drawMap()
        {
            string mapDraw = "";

            for (int i = 0; i <= HEIGHT - 1; i++)
            {
                for (int j = 0; j <= WIDTH - 1; j++)
                {
                    mapDraw += Convert.ToString(mapVisuals[j, i]);
                }
                mapDraw += Environment.NewLine;
            }

            return mapDraw;
        }

        private void fillMap()
        {
            for (int i = 0; i <= HEIGHT - 1; i++)
            {
                for (int j = 0; j <= WIDTH - 1; j++)
                {
                    if (mapVisuals[i, j] != 'R' && mapVisuals[i, j] != 'r' && mapVisuals[i, j] != 'M' && mapVisuals[i, j] != 'm')
                    {
                        mapVisuals[i, j] = '.';
                    }

                }
            }
        }

    }
}

