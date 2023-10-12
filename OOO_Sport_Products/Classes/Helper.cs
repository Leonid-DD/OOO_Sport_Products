using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOO_Sport_Products.Model;

namespace OOO_Sport_Products.Classes
{
    public class Helper
    {
        //Объект связи с БД
        public static Model.DBSportProducts DB { get; set; }

        //Объект авторизованного пользователя
        public static Model.User user { get; set; }
    }
}
