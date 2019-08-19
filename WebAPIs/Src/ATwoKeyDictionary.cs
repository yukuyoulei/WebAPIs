using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ATwoKeyDictionary<TKey, TValue>
{
	private Dictionary<KeyValuePair<TKey, TKey>, TValue> dDictionary = new Dictionary<KeyValuePair<TKey, TKey>, TValue>();
	public void Add(TKey key1, TKey key2, TValue value)
	{
		if (dDictionary.ContainsKey(new KeyValuePair<TKey, TKey>(key1, key2))
			|| dDictionary.ContainsKey(new KeyValuePair<TKey, TKey>(key2, key1)))
		{
			throw new Exception("ATwoKeyDictionary already contains key1 " + key1 + " and key2 " + key2);
		}
		dDictionary.Add(new KeyValuePair<TKey, TKey>(key1, key2), value);
	}
	public bool ContainsKey(TKey k1, TKey k2)
	{
		return dDictionary.ContainsKey(new KeyValuePair<TKey, TKey>(k1, k2))
			|| dDictionary.ContainsKey(new KeyValuePair<TKey, TKey>(k2, k1));
	}
	public void Remove(TKey k1, TKey k2)
	{
		if (dDictionary.ContainsKey(new KeyValuePair<TKey, TKey>(k1, k2)))
		{
			dDictionary.Remove(new KeyValuePair<TKey, TKey>(k1,k2));
		}
		if (dDictionary.ContainsKey(new KeyValuePair<TKey, TKey>(k2, k1)))
		{
			dDictionary.Remove(new KeyValuePair<TKey, TKey>(k2, k1));
		}
	}

	public TValue this[TKey k1, TKey k2]
	{
		get
		{
			if (dDictionary.ContainsKey(new KeyValuePair<TKey, TKey>(k1, k2)))
			{
				return dDictionary[new KeyValuePair<TKey, TKey>(k1, k2)];
			}
			if (dDictionary.ContainsKey(new KeyValuePair<TKey, TKey>(k2, k1)))
			{
				return dDictionary[new KeyValuePair<TKey, TKey>(k2, k1)];
			}
			throw new Exception("There is no key " + k1 + "," + k2 + " contains");
		}
		set
		{
			if (dDictionary.ContainsKey(new KeyValuePair<TKey, TKey>(k1, k2)))
			{
				dDictionary[new KeyValuePair<TKey, TKey>(k1, k2)] = value;
				return;
			}
			else if (dDictionary.ContainsKey(new KeyValuePair<TKey, TKey>(k2,k1)))
			{
				dDictionary[new KeyValuePair<TKey, TKey>(k2, k1)] = value;
				return;
			}
			throw new Exception("There is no key " + k1 + "," + k2 + " contains");
		}
	}
	public int Count
	{
		get
		{
			return dDictionary.Count;
		}
	}
}
