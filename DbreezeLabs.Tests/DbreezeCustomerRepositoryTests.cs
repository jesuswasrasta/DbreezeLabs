﻿#region Usings
using System.Collections.Generic;
using System.IO;
using System.Threading;

using NUnit.Framework;


#endregion


namespace DbreezeLabs.Tests
{
	[TestFixture]
	public class DbreezeCustomerRepositoryTests
	{
		private DirectoryInfo _tempFolder;
		private string _tempPath;
		private DbreezeCustomerRepository _repository;


		#region Constructors
		public  DbreezeCustomerRepositoryTests()
		{
			_tempPath = Path.Combine(Path.GetTempPath(), "DbreezeLabs");
			_tempFolder = new DirectoryInfo(_tempPath);
			_tempFolder.Create();

			_repository = new DbreezeCustomerRepository(_tempFolder);
		}
		#endregion

		[SetUp]
		public void SetUp()
		{
			//_tempFolder.Clean();
			_repository.Clear();
		}

		[TearDown]
		public void TearDown()
		{
			//_tempFolder.Clean();
			_repository.Clear();
		}

		[Test]
		public void Upsert_Test()
		{
			var customer = new Customer
			{
				Id = 1,
				Name = "John Doe"
			};

			_repository.Upsert(customer);
			var selectedCustomer = _repository.Select(1);

			Assert.AreEqual(customer, selectedCustomer);
		}

		[Test, Timeout(100000)]
		public void Upsert_SIngle_Bulk_Test()
		{
			//Slow
			for (int i = 0; i < 1000; i++)
			{
				var customer = new Customer { Id = i, Name = "John Doe" };
				_repository.Upsert(customer);
			}
		}

		[Test, Timeout(100000)]
		public void Upsert_Bulk_Test()
		{
			//Quick!!!
			var customers = new List<Customer>();
			for (int i = 0; i < 1000; i++)
			{
				var customer = new Customer { Id = i, Name = "John Doe" };
				customers.Add(customer);
			}

			_repository.Upsert(customers);
		}
	}
}
