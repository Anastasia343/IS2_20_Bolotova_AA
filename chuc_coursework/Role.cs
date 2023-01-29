using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chuc_coursework
{
    public class Role
    {
        private string commodity_expert;

        public string GetCommodity()
        {
            return commodity_expert;
        }

        public void SetCommodity(string commodity_expert)
        {
            this.commodity_expert = commodity_expert;
        }

    }
}
