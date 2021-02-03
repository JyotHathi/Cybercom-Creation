using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class Class1
    {
        internal string str = "hello";
        protected string str2 = "hello";
        private protected  string str3 = "hello";

    }
    public class Class2 :Class1
    {
        public void Test()
        {
            base.str = "Hello";
            base.str2 = "Hi";
            base.str3 = "Hei..";
        }
    }
}
