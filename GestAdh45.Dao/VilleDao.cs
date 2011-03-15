namespace GestAdh45.Dao
{
	public class VilleDao
	{
		private static VilleDao Instance;
		private Entities Context
		{
			get;
			set;
		}
		private VilleDao()
		{
		}
		public static VilleDao GetInstance(Entities pContexte)
		{
			if (VilleDao.Instance == null)
			{
				VilleDao.Instance = new VilleDao();
			}
			VilleDao.Instance.Context = pContexte;
			return VilleDao.Instance;
		}
		public void Create(Ville pVille)
		{
			VilleDao.Instance.Context.Villes.AddObject(pVille);
			VilleDao.Instance.Context.SaveChanges();
		}
		public Ville Read(int pVilleId)
		{
			VilleDao.<>c__DisplayClass0 <>c__DisplayClass0 = new VilleDao.<>c__DisplayClass0();
			<>c__DisplayClass0.pVilleId = pVilleId;
			IQueryable<Ville> arg_80_0 = VilleDao.Instance.Context.Villes;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Ville), "v");
			IQueryable<Ville> source = Queryable.Where<Ville>(arg_80_0, Expression.Lambda<Func<Ville, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID()))), Expression.Convert(Expression.Field(Expression.Constant(<>c__DisplayClass0), FieldInfo.GetFieldFromHandle(ldtoken(pVilleId))), typeof(long))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.First<Ville>(source);
		}
		public void Update(Ville pVille)
		{
			EFExtension.SetAllModified<Ville>(pVille, VilleDao.Instance.Context);
			try
			{
				VilleDao.Instance.Context.SaveChanges();
			}
			catch (OptimisticConcurrencyException arg_22_0)
			{
				OptimisticConcurrencyException optimisticConcurrencyException = arg_22_0;
				throw optimisticConcurrencyException;
			}
		}
		public void Delete(Ville pVille)
		{
			VilleDao.Instance.Context.Attach(pVille);
			VilleDao.Instance.Context.DeleteObject(pVille);
			VilleDao.Instance.Context.SaveChanges();
		}
		public List<Ville> List()
		{
			IQueryable<Ville> arg_4A_0 = VilleDao.Instance.Context.Villes;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Ville), "v");
			IOrderedQueryable<Ville> source = Queryable.OrderBy<Ville, string>(arg_4A_0, Expression.Lambda<Func<Ville, string>>(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Libelle()))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Enumerable.ToList<Ville>(source);
		}
		public bool Exist(Ville pVille)
		{
			VilleDao.<>c__DisplayClass2 <>c__DisplayClass2 = new VilleDao.<>c__DisplayClass2();
			<>c__DisplayClass2.pVille = pVille;
			IQueryable<Ville> arg_107_0 = VilleDao.Instance.Context.Villes;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Ville), "v");
			IQueryable<Ville> source = Queryable.Where<Ville>(arg_107_0, Expression.Lambda<Func<Ville, bool>>(Expression.AndAlso(Expression.Call(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_CodePostal()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(Equals())), new Expression[]
			{
				Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pVille))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_CodePostal())))
			}), Expression.Call(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Libelle()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(Equals())), new Expression[]
			{
				Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pVille))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Libelle())))
			})), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.Count<Ville>(source) > 0;
		}
		public bool IsUsed(Ville pVille)
		{
			VilleDao.<>c__DisplayClass4 <>c__DisplayClass4 = new VilleDao.<>c__DisplayClass4();
			<>c__DisplayClass4.pVille = pVille;
			IQueryable<Adresse> arg_85_0 = VilleDao.Instance.Context.Adresses;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Adresse), "a");
			IQueryable<Adresse> source = Queryable.Where<Adresse>(arg_85_0, Expression.Lambda<Func<Adresse, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID_Ville()))), Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass4), FieldInfo.GetFieldFromHandle(ldtoken(pVille))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID())))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.Count<Adresse>(source) > 0;
		}
		public void Refresh(Ville pVille)
		{
			this.Context.Refresh(RefreshMode.StoreWins, pVille);
		}
	}
}
