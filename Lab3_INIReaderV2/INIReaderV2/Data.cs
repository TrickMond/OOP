using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace INIReaderV2
{
    class Data <T>
    {
        public string ParametrName { get; private set; }
        public T Value { get; private set; }

        public Data(string parametr, T data )
        {
            if (!Regex.IsMatch(parametr, @"^[A-Za-z_\d]*$"))
                throw new FormatException("Имя параметра дожно содержать строку состоящюю из латинского алфавита, цифр и знаков нижнего подчеркивания!");
            this.ParametrName = parametr;
            if (data.GetType() == typeof(string) && !Regex.IsMatch(data.ToString(), @"^[A-Za-z_.\d]*$"))
            {
                throw new FormatException("Значение параметра дожно содержать строку состоящюю из латинского алфавита, цифр и знаков нижнего подчеркивания, а также точки!");
            }
            this.Value = data;

        }

        public override string ToString()
        {
            return String.Format($"{this.ParametrName} = {this.Value.ToString()}");
        }

    }
}
