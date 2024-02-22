using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_Sport_Products.Classes
{
    public class ProductExtended
    {
        public Model.Product product { get; set; }

        public string ProductImagePath
        {
            get
            {
                string temp;
                if (this.product.ProductImage != null)
                {
                    temp = Directory.GetCurrentDirectory() + "/Images/" + this.product.ProductImage.ToString();
                }
                else
                {
                    temp = "/Resources/picture.png";
                }
                return temp;
            }
        }

        public double ProductDiscountCost
        {
            get
            {
                return Math.Round(this.product.ProductCost * (100.0 - this.product.ProductDiscountCurrent) / 100.0, 2);
            }
        }
    }
}
