using Project.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.SingletonPattern
{
    public class DBTool
    {

        DBTool() { }

        static MyContext _dbINstance;

        public static MyContext DBINstance
        {
            get
            {
                if (_dbINstance == null)
                {
                    _dbINstance = new MyContext();
                }
                return _dbINstance;
            }
        }

    }
}
