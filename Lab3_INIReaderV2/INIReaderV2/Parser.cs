using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace INIReaderV2
{
    class Parser
    {
        string fileName;
        List<Section> sections;

        public Parser (string fileName)
        {
            this.fileName = fileName;
            this.sections = new List<Section>();
        }

        public List<string> DeleteTrash()
        {
            string[] file;
            List<string> r = new List<string>();
            try
            {
                file = File.ReadAllLines(this.fileName);
            }   
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"Файл с именем {this.fileName} не найден!");
            }
            for (int i = 0; i < file.Length; i++)
            {
                var temp = file[i].Split(';');
                if (temp[0].Trim(new Char[] { '[', ' ', ']' }) != "")
                    r.Add(temp[0].Trim(new Char[] { '[', ' ', ']' }));
            }
            return r;
        }

        public List<Section> ToSections()
        {
            var strs = this.DeleteTrash();
            string[] temp;
            foreach (var s in strs)
            {
                temp = s.Split('=');
                if(temp.Length == 1)
                {
                    sections.Add(new Section(temp[0].Trim()));
                }
                else
                {
                    try
                    {
                        sections[sections.Count - 1].Add(new Data<int>(temp[0].Trim(), Convert.ToInt32(temp[1].Trim())));
                    }
                    catch (FormatException)
                    {
                        try
                        {
                            sections[sections.Count - 1].Add(new Data<decimal>(temp[0].Trim(), Convert.ToDecimal(temp[1].Trim().Replace(".", ","))));
                        }
                        catch (FormatException)
                        {
                            sections[sections.Count - 1].Add(new Data<string>(temp[0].Trim(), temp[1].Trim()));
                        }
                    }
                }
            }
            return this.sections;
        }

        public string GetValue(string typeOfValue, string nameOfParametr, string nameOfSection)
        {
            string str;
            foreach (var sec in sections)
            {
                if (sec.sectionName == nameOfSection)
                {
                    str = sec.SearchByType(typeOfValue, nameOfParametr);
                    return str;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Заданная секция с именем {nameOfSection} не найдена!");
                }
            }
            return null;
        }
    }
}
