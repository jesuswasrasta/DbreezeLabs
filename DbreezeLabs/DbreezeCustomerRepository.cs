using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using DBreeze;
using DBreeze.DataTypes;
using DBreeze.Utils;

using DBreezeBased.Serialization;


namespace DbreezeLabs
{
	public class DbreezeCustomerRepository : IDisposable, ICustomerRepository
	{
		private const string CustomersTable = "Customers";

		private readonly DBreezeEngine _engine;
		private readonly DBreezeConfiguration _dBreezeConfiguration;


		#region Constructors
		public DbreezeCustomerRepository(DirectoryInfo folder)
		{
			_dBreezeConfiguration = new DBreezeConfiguration {DBreezeDataFolderName = folder.FullName};
			_engine = new DBreezeEngine(_dBreezeConfiguration);

			//Setting default serializer for DBreeze
			CustomSerializator.ByteArraySerializator = ProtobufSerializer.SerializeProtobuf;
			CustomSerializator.ByteArrayDeSerializator = ProtobufSerializer.DeserializeProtobuf;
		}
		#endregion


		public void Upsert(Customer customer)
		{
			using (var tran = _engine.GetTransaction())
			{
				tran.Insert<long, Customer>(CustomersTable, customer.Id, customer);
				tran.Commit();
			}
		}

		public void Upsert(IEnumerable<Customer> customers)
		{
			var dic = customers.ToDictionary(customer => customer.Id);
			using (var tran = _engine.GetTransaction())
			{
				tran.InsertDictionary<long, Customer>(CustomersTable, dic, false);
				tran.Commit();
			}
		}


		public Customer Select(long customerId)
		{
			Row<long, Customer> row;
			using (var tran = _engine.GetTransaction())
			{
				row = tran.Select<long, Customer>(CustomersTable, customerId);
				tran.Commit();
			}
			return row.Exists && row.Value != null ? row.Value : null;
		}

		public void Clear()
		{
			using (var tran = _engine.GetTransaction())
			{
				tran.RemoveAllKeys(CustomersTable, true);
				tran.Commit();
			}
		}

		public void Dispose()
		{
			_engine?.Dispose();
		}
	}
}