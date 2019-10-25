using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace INIReaderV2
{
    class Section 
    {
        public string sectionName { get; private set; }
        List<Data<int>> intList;
        List<Data<decimal>> decList;
        List<Data<string>> stList;

        public Section (string sectionName)
        {
            if (!Regex.IsMatch(sectionName, @"^[A-Za-z_\d]*$"))
                throw new FormatException("Имя секции дожно содержать строку состоящюю из латинского алфавита, цифр и знаков нижнего подчеркивания!");
            this.sectionName = sectionName;
            this.intList = new List<Data<int>>();
            this.decList = new List<Data<decimal>>();
            this.stList = new List<Data<string>>();
        }

        public void Add(Data<int> dataInt)
        {
            this.intList.Add(dataInt);
        }

        public void Add(Data<decimal> dataDec)
        {
            this.decList.Add(dataDec);
        }

        public void Add(Data<string> dataStr)
        { 
            this.stList.Add(dataStr);
        }

        public string SearchByType (string type, string parametrName)
        {
            if (type == "int" || type == "Int" || type == "INT")
                foreach(var i in this.intList)
                {
                    if (i.ParametrName == parametrName)
                    {
                        return i.Value.ToString(); 
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException($"Заданный параметр с именем {parametrName} не найден!");
                    }
                }
            if (type == "decimal" || type == "dec" || type == "DEC")
                foreach (var i in this.decList)
                {
                    if (i.ParametrName == parametrName)
                    {
                        return i.Value.ToString();
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException($"Заданный параметр с именем {parametrName} не найден!");
                    }
                }
            if (type == "string" || type == "str" || type == "STR")
                foreach (var i in this.stList)
                {
                    if (i.ParametrName == parametrName)
                    {
                        return i.Value.ToString();
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException($"Заданный параметр с именем {parametrName} не найден!");
                    }
                }
            return null;
        }


        public override string ToString()
        {
            string str = String.Format($"{this.sectionName}\n\tData in int:\n");

            foreach (var i in this.intList)
            {
                str = str + "\t\t" + i.ToString() + '\n';
            }
            str = str + "\tData in decimal\n";
            foreach (var d in this.decList)
            {
                str = str + "\t\t" + d.ToString() + '\n';
            }
            str = str + "\tData in string\n";
            foreach (var s in this.stList)
            {
                str = str + "\t\t" + s.ToString() + '\n';
            }
            return str;
        }
    }
}
