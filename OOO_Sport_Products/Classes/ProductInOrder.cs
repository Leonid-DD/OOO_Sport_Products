using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_Sport_Products.Classes
{
    public class ProductInOrder
    {
        //Выбранный товар с добавленными свойствами к товару из БД
        public Classes.ProductExtended ProductExtendedInOrder { get; set; }
        //Кол-во этого товара в заказе
        public int countProductInOrder { get; set; }
    }
}
