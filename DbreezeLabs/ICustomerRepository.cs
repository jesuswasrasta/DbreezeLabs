﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DbreezeLabs
{
    public interface ICustomerRepository
    {
	    void Insert(Customer customer);

	    void Clear();
    }
}
