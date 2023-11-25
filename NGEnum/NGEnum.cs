using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NGEnum
{
	public abstract class NGEnums<TSource> where TSource : notnull, NGEnums<TSource>, new()
	{
		//public static readonly TSource None = new();
		public static readonly TSource None = (TSource)Activator.CreateInstance(typeof(TSource), new object[] { "None" });
		///////////////////////////////////////////////////////////
		private List<TSource> _list { get; set; }
		private object _object;
		private string _string;
		private int _int;
		#region /////// CONSTRUCTORES  ///////////////
		protected NGEnums()
		{
			_object = None.ToObject();
			_string = None.ToString();
			_int = _string.GetHashCode();
			_list = new List<TSource>() { (TSource)this };
		}
		protected NGEnums(object pObject)
		{
			_object = pObject;
			_string = pObject.ToString();
			_int = _string.GetHashCode();
			_list = new List<TSource>() { (TSource)this };
		}
		protected NGEnums(int pId, object pObject)
		{
			_object = pObject;
			_string = pObject.ToString();
			_int = pId;
			_list = new List<TSource>() { (TSource)this };
		}
		#endregion

		public static IEnumerable<T> GetAll<T>() where T : TSource
		{
			var fields = typeof(T).GetFields(BindingFlags.Public |
											 BindingFlags.Static |
											 BindingFlags.FlattenHierarchy);

			return fields.Select(f => f.GetValue(null)).Cast<T>();
		}

		#region ////// METODOS DE CONVERSÃO  /////////////////
		public int ToInt() => _int;
		public object ToObject() => _object;
		public override string ToString() => _string;
		public override int GetHashCode() => base.GetHashCode();
		#endregion

		#region ////// MÉTODOS DE COMPARAÇÃO  //////////////////////////
		public override bool Equals(object obj) => base.Equals(obj);
		public bool Equals(TSource value) => this.GetType().Equals(value.GetType()) && (this._int == value.ToInt());
		public static bool operator == (NGEnums<TSource> object1, NGEnums<TSource> object2)
		{
			if ((object1 is null) || (object2 is null))
				return false;
			else
				return object1.Equals(object2);
		}
		public static bool operator != (NGEnums<TSource> object1, NGEnums<TSource> object2)
		{
			if ((object1 is null) || (object2 is null))
				return false;
			else
				return !object1.Equals(object2);
		}
		/// <summary>
		/// COMPARA SE O ID DO OJBETO É O MESMO QUE O VALOR PASSADO NO PARAMETRO
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool CompareId(int value) => value == _int;
		/// <summary>
		/// COMPARA SE A STRING DO OJBETO É A MESMO QUE O VALOR PASSADO NO PARAMETRO
		/// </summary>
		public bool CompareKey(string value) => value == _string;
		/// <summary>
		/// COMPARA SE O OJBETO DO OJBETO É A MESMO QUE O VALOR PASSADO NO PARAMETRO
		/// </summary>
		public bool CompareObject(object value) => _object.Equals(value);
		/// <summary>
		/// COMPARA OS ENUNS COMPOSTOS, VERIFICAR SE EXISTE ALGUM DOS VALORES PASSADOS NO PARAMETRO
		/// </summary>
		public bool CompareAny(TSource firstValue, params TSource[] otherValues)
		{
			var values = new List<TSource> { firstValue };
			values.AddRange(otherValues);
			foreach (TSource source in values)
			{
				if (_list.Any(a => a == source))
					return true;
			}
			return false;
		}
		/// <summary>
		/// COMPARA OS ENUNS COMPOSTOS, VERIFICAR SE EXISTE OS VALORES PASSADOS NO PARAMETRO
		/// </summary>
		public bool CompareSome(TSource firstValue, params TSource[] otherValues)
		{
			//	List<TSource> lListObj = new List<TSource>(_list);
			//	if (lListObj.Count <= 0)
			//		lListObj.Add(this as TSource);
			//	List<TSource> lListPar = new List<TSource>(values.ToList());
			var values = new List<TSource> { firstValue };
			values.AddRange(otherValues);

			return !values.ToList().Except(_list).Any();
		}
		/// <summary>
		/// COMPARA OS ENUNS COMPOSTOS, VERIFICAR OS VALORES PASSADOS NO PARAMETRO SÃO EXATAMENTE OS MESMO DO OBJETO
		/// </summary>
		public bool CompareExact(TSource firstValue, params TSource[] otherValues)
		{
			var values = new List<TSource> { firstValue };
			values.AddRange(otherValues);

			int id = string.Join('|', values).GetHashCode();
			return (_int == id);
		}
		#endregion

		#region ///// METODOS PARA ADICIONAR ELEMENTOS E VAZER UM ENUM COMPOSTO///////////
		/// <summary>
		/// METODO PARA ADICINAR MULTIPLOS ELEMENTOS AO OBJETO.
		/// RETORNA UM NOVO OBJETO, DEVE SER ATRIBUIDO.
		/// </summary>
		public TSource Add(TSource firstValue, params TSource[] otherValues)
		{
            TSource ret = new TSource
            {
                _list = new List<TSource>(_list) { firstValue }
            };
            ret._list.AddRange(otherValues);
			ret._object = ret._list;
			ret._string = string.Join('|', ret._list);
			ret._int = ret._string.GetHashCode();

			return ret;
		}
		/// <summary>
		/// METODO PARA ADICINAR MULTIPLOS ELEMENTOS AO OBJETO.
		/// RETORNA UM NOVO OBJETO, DEVE SER ATRIBUIDO.
		/// </summary>
		public static TSource New(TSource firstValue, params TSource[] otherValues)
		{
            ////// ADD OS VALORE A LISTA
            TSource ret = new TSource
            {
                _list = new List<TSource> { firstValue }
            };
            ret._list.AddRange(otherValues);
			ret._object = ret._list;
			ret._string = string.Join('|', ret._list);
			ret._int = ret._string.GetHashCode();
			return ret;
		}
		#endregion
	}
}