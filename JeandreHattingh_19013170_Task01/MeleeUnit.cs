﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeandreHattingh_19013170_Task01
{
    class MeleeUnit : Unit
    {
        Map mapTracker = new Map(10);

        public MeleeUnit(int meleeX, int meleeY, string meleeTeam, char meleeSym, bool meleeEngaging) : base(meleeX, meleeY, 6, 3, 2, 1, meleeTeam, meleeSym, meleeEngaging)
        {

        }

        public override string Move(Unit unitToEngage)
        {
            string returnVal = "";
            string typeCheck = unitToEngage.GetType().ToString();
            string[] splitArray = typeCheck.Split('.');
            typeCheck = splitArray[splitArray.Length - 1];

            if (typeCheck == "MeleeUnit")
            {
                MeleeUnit m = (MeleeUnit)unitToEngage;
                if ((Math.Abs(m.MuXPos - this.MuXPos) > Math.Abs(m.MuYPos - this.MuYPos)))
                {
                    if ((m.MuXPos - this.MuXPos) > 0)
                    {
                        this.MuXPos++;
                        returnVal = "Right";
                    }
                    else if ((m.MuXPos - this.MuXPos) < 0)
                    {
                        this.MuXPos--;
                        returnVal = "Left";
                    }
                }
                else
                {
                    if ((m.MuYPos - this.MuYPos) > 0)
                    {
                        this.MuYPos++;
                        returnVal = "Up";
                    }
                    else if ((m.MuYPos - this.MuYPos) < 0)
                    {
                        
                            this.MuYPos--;
                            returnVal = "Down";
                        
                    }
                }
            }
            else
            {
                RangedUnit r = (RangedUnit)unitToEngage;
                if ((Math.Abs(r.RuXPos - this.MuXPos) > Math.Abs(r.RuYPos - this.MuYPos)))
                {
                    if ((r.RuXPos - this.MuXPos) > 0)
                    {
                        this.MuXPos++;
                        returnVal = "Right";
                    }
                    else if ((r.RuXPos - this.MuXPos) < 0)
                    {
                        this.MuYPos--;
                        returnVal = "Left";
                    }
                }
                else
                {
                    if ((r.RuYPos - this.MuYPos) > 0)
                    {
                        this.MuYPos++;
                        returnVal = "Up";
                    }
                    else if ((r.RuYPos - this.MuYPos) < 0)
                    {
                        this.MuYPos--;
                        returnVal = "Down";
                    }
                }
            }

            return returnVal;
        }

        public override void Combat(Unit attackingUnit)
        {
            string typeCheck = attackingUnit.GetType().ToString();
            string[] splitArray = typeCheck.Split('.');
            typeCheck = splitArray[splitArray.Length - 1];
            switch (typeCheck)
            {
                case "MeleeUnit":
                    MeleeUnit mu = (MeleeUnit)attackingUnit;
                    mu.MuHealth -= this.MuAtk;
                    this.MuEngaging = false;
                    break;
                case "RangedUnit":
                    RangedUnit ru = (RangedUnit)attackingUnit;
                    ru.RuHealth -= this.MuAtk;
                    this.MuEngaging = false;
                    break;
                default:
                    break;
            }
        }

        public override bool Engaging(Unit unitInRange)
        {
            bool inRange = false;
            string typeCheck = unitInRange.GetType().ToString();
            string[] splitArray = typeCheck.Split('.');
            typeCheck = splitArray[splitArray.Length - 1];
            switch (typeCheck)
            {
                case "MeleeUnit":
                    {
                        MeleeUnit mu = (MeleeUnit)unitInRange;
                        if ((mu.MuYPos == this.MuYPos && Math.Abs(mu.MuXPos - this.MuXPos) == 2) || (mu.MuXPos == this.MuXPos && Math.Abs(mu.MuYPos - this.MuYPos) == 2))
                        {
                            inRange = true;
                        }
                        else
                        {
                            inRange = false;
                        }
                    }
                    break;
                case "RangedUnit":
                    {
                        RangedUnit ru = (RangedUnit)unitInRange;
                        if ((ru.RuYPos == this.MuYPos && Math.Abs(ru.RuXPos - this.MuXPos) == 2) || (ru.RuXPos == this.MuXPos && Math.Abs(ru.RuYPos - this.MuYPos) == 2))
                        {
                            inRange = true;
                        }
                        else
                        {
                            inRange = false;
                        }
                    }
                    break;
                default:
                    break;
            }
            return inRange;
        }

        public override Unit EnemyPos(Unit[] unitClosetCheck)
        {
            int result;
            int xDistance;
            int yDistance;
            int closest = 1000;
            Unit returnVal = this;
            foreach (Unit temp in unitClosetCheck)
            {
                string typeCheck = temp.GetType().ToString();
                string[] splitArray = typeCheck.Split('.');
                typeCheck = splitArray[splitArray.Length - 1];

                switch (typeCheck)
                {
                    case "MeleeUnit":
                        {
                            MeleeUnit m = (MeleeUnit)temp;
                            if (m.MuXPos != this.MuXPos && m.MuYPos != this.MuYPos)
                            {
                                xDistance = Math.Abs(this.MuXPos - m.MuXPos);
                                yDistance = Math.Abs(this.MuYPos - m.MuYPos);
                                result = Convert.ToInt32(Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance)));

                                if (result < closest)
                                {
                                    closest = result;
                                    returnVal = m;
                                }
                            }
                        }
                        break;
                    case "RangeUnit":
                        {
                            RangedUnit r = (RangedUnit)temp;
                            if (r.RuXPos != this.MuXPos && r.RuYPos != this.MuYPos)
                            {
                                xDistance = Math.Abs(this.MuXPos - r.RuXPos);
                                yDistance = Math.Abs(this.MuYPos - r.RuYPos);
                                result = Convert.ToInt32(Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance)));

                                if (result < closest)
                                {
                                    closest = result;
                                    returnVal = r;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return returnVal;
        }

        public override bool Death()
        {
            bool unitDead;

            if (this.MuHealth <= 0)
            {
                unitDead = true;
            }
            else
            {
                unitDead = false;
            }

            return unitDead;
        }

        public string RandomMove()
        {
            Random rngod = new Random();
            int move = rngod.Next(0, 4);
            string moveDirection = "";

            switch (move)
            {
                case 0:
                    {
                        moveDirection = "Right";
                        break;
                    }
                case 1:
                    {
                        moveDirection = "Left";
                        break;
                    }
                case 2:
                    {
                        moveDirection = "Up";
                        break;
                    }
                case 3:
                    {
                        moveDirection = "Down";
                        break;
                    }
            }

            return moveDirection;
        }

        public override string ToString()
        {
            string returnVal = "";
            returnVal += "A new Melee Unit has entered the battlefield" + Environment.NewLine;
            returnVal += "The units' X position is: " + this.MuXPos + Environment.NewLine;
            returnVal += "The units' Y position is: " + this.MuYPos + Environment.NewLine;
            returnVal += "The units' current HP is: " + this.MuHealth + Environment.NewLine;
            returnVal += "The units' maximum HP is: " + this.MuMaxHealth + Environment.NewLine;
            returnVal += "The units' attack damage is: " + this.MuAtk + Environment.NewLine;
            returnVal += "The units' attack range is: " + this.MuAttkRange + Environment.NewLine;
            returnVal += "The units' movement speed is: " + this.MuMovementSpeed + Environment.NewLine;
            returnVal += "The units' team is: " + this.MuTeam + Environment.NewLine;
            returnVal += "The units' symbol is: " + this.MuSymbol + Environment.NewLine;
            returnVal += "--------------------------------------------" + Environment.NewLine;
            returnVal += Environment.NewLine;

            return returnVal;
        }

        public int MuXPos { get => base.xPos; set => base.xPos = value; }
        public int MuYPos { get => base.yPos; set => base.yPos = value; }
        public int MuHealth { get => base.hp; set => base.hp = value; }
        public int MuMaxHealth { get => base.maxHP; }
        public int MuMovementSpeed { get => base.speed; }
        public int MuAtk { get => base.atk; }
        public int MuAttkRange { get => base.atkRange; }
        public string MuTeam { get => base.team; }
        public char MuSymbol { get => base.symbol; }
        public bool MuEngaging { get => base.engage; set => base.engage = value; }
    }
}
