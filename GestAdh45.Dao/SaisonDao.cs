namespace GestAdh45.Dao
{
	public class SaisonDao
	{
		private static SaisonDao Instance;
		private Entities Context
		{
			get;
			set;
		}
		private SaisonDao()
		{
		}
		public static SaisonDao GetInstance(Entities pContexte)
		{
			if (SaisonDao.Instance == null)
			{
				SaisonDao.Instance = new SaisonDao();
			}
			SaisonDao.Instance.Context = pContexte;
			return SaisonDao.Instance;
		}
		public void Create(Saison pSaison)
		{
			SaisonDao.Instance.Context.Saisons.AddObject(pSaison);
			SaisonDao.Instance.Context.SaveChanges();
		}
		public Saison Read(int pSaisonId)
		{
			SaisonDao.<>c__DisplayClass0 <>c__DisplayClass0 = new SaisonDao.<>c__DisplayClass0();
			<>c__DisplayClass0.pSaisonId = pSaisonId;
			IQueryable<Saison> arg_80_0 = SaisonDao.Instance.Context.Saisons;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Saison), "s");
			IQueryable<Saison> source = Queryable.Where<Saison>(arg_80_0, Expression.Lambda<Func<Saison, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID()))), Expression.Convert(Expression.Field(Expression.Constant(<>c__DisplayClass0), FieldInfo.GetFieldFromHandle(ldtoken(pSaisonId))), typeof(long))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.First<Saison>(source);
		}
		public void Update(Saison pSaison)
		{
			EFExtension.SetAllModified<Saison>(pSaison, SaisonDao.Instance.Context);
			try
			{
				SaisonDao.Instance.Context.SaveChanges();
			}
			catch (OptimisticConcurrencyException arg_22_0)
			{
				OptimisticConcurrencyException optimisticConcurrencyException = arg_22_0;
				throw optimisticConcurrencyException;
			}
		}
		public void Delete(Saison pSaison)
		{
			SaisonDao.Instance.Context.Attach(pSaison);
			SaisonDao.Instance.Context.DeleteObject(pSaison);
			SaisonDao.Instance.Context.SaveChanges();
		}
		public List<Saison> List()
		{
			IQueryable<Saison> arg_4A_0 = SaisonDao.Instance.Context.Saisons;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Saison), "s");
			IOrderedQueryable<Saison> source = Queryable.OrderBy<Saison, long>(arg_4A_0, Expression.Lambda<Func<Saison, long>>(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_AnneeDebut()))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Enumerable.ToList<Saison>(source);
		}
		public Saison ReadSaisonCourante()
		{
			IQueryable<Saison> arg_65_0 = SaisonDao.Instance.Context.Saisons;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Saison), "s");
			IQueryable<Saison> source = Queryable.Where<Saison>(arg_65_0, Expression.Lambda<Func<Saison, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_EstSaisonCourante()))), Expression.Constant(1, typeof(long))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.First<Saison>(source);
		}
		public bool Exist(Saison pSaison)
		{
			SaisonDao.<>c__DisplayClass2 <>c__DisplayClass2 = new SaisonDao.<>c__DisplayClass2();
			<>c__DisplayClass2.pSaison = pSaison;
			IQueryable<Saison> arg_15D_0 = SaisonDao.Instance.Context.Saisons;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Saison), "s");
			IQueryable<Saison> source = Queryable.Where<Saison>(arg_15D_0, Expression.Lambda<Func<Saison, bool>>(Expression.OrElse(Expression.AndAlso(Expression.LessThanOrEqual(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_AnneeDebut()))), Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pSaison))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_AnneeDebut())))), Expression.LessThan(Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pSaison))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_AnneeDebut()))), Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_AnneeFin()))))), Expression.AndAlso(Expression.LessThan(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_AnneeDebut()))), Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pSaison))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_AnneeFin())))), Expression.LessThanOrEqual(Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pSaison))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_AnneeFin()))), Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_AnneeFin())))))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.Count<Saison>(source) > 0;
		}
		public bool IsUsed(Saison pSaison)
		{
			SaisonDao.<>c__DisplayClass4 <>c__DisplayClass4 = new SaisonDao.<>c__DisplayClass4();
			<>c__DisplayClass4.pSaison = pSaison;
			IQueryable<Groupe> arg_85_0 = SaisonDao.Instance.Context.Groupes;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Groupe), "g");
			IQueryable<Groupe> source = Queryable.Where<Groupe>(arg_85_0, Expression.Lambda<Func<Groupe, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID_Saison()))), Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass4), FieldInfo.GetFieldFromHandle(ldtoken(pSaison))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID())))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.Count<Groupe>(source) > 0;
		}
		public void Refresh(Saison pSaison)
		{
			this.Context.Refresh(RefreshMode.StoreWins, pSaison);
		}
	}
}
