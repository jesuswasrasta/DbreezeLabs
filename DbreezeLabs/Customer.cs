namespace DbreezeLabs
{
	[ProtoBuf.ProtoContract]
	public class Customer
	{
		[ProtoBuf.ProtoMember(1, IsRequired = true)]
		public long Id { get; set; }

		[ProtoBuf.ProtoMember(2, IsRequired = true)]
		public string Name { get; set; }

		#region Overridden Methods
		//See: http://msdn.microsoft.com/en-us/library/ms173147%28VS.80%29.aspx
		public override bool Equals(object obj)
		{
			Customer customer = obj as Customer;
			return customer != null 
				&& customer.Id == Id 
				&& customer.Name == Name;
		}

		public override int GetHashCode()
		{
			//see http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
			unchecked // Overflow is fine, just wrap
			{
				int hash = 17;
				hash = hash * 23 + (string.IsNullOrEmpty(Name) ? 0 : Name.GetHashCode());
				hash = hash * 23 + Id.GetHashCode();
				return hash;
			}
		}
		#endregion
	}
}