namespace GestAdh45.Dao
{
	public class AdherentDao
	{
		private static AdherentDao Instance;
		private Entities Context
		{
			get;
			set;
		}
		private AdherentDao()
		{
		}
		public static AdherentDao GetInstance(Entities pContexte)
		{
			if (AdherentDao.Instance == null)
			{
				AdherentDao.Instance = new AdherentDao();
			}
			AdherentDao.Instance.Context = pContexte;
			return AdherentDao.Instance;
		}
		public void Create(Adherent pAdherent)
		{
			pAdherent.DateCreation = DateTime.Now;
			pAdherent.DateModification = DateTime.Now;
			AdherentDao.Instance.Context.Adherents.AddObject(pAdherent);
			AdherentDao.Instance.Context.SaveChanges();
		}
		public Adherent Read(int pAdherentId)
		{
			AdherentDao.<>c__DisplayClass0 <>c__DisplayClass0 = new AdherentDao.<>c__DisplayClass0();
			<>c__DisplayClass0.pAdherentId = pAdherentId;
			IQueryable<Adherent> arg_80_0 = AdherentDao.Instance.Context.Adherents;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Adherent), "a");
			IQueryable<Adherent> source = Queryable.Where<Adherent>(arg_80_0, Expression.Lambda<Func<Adherent, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID()))), Expression.Convert(Expression.Field(Expression.Constant(<>c__DisplayClass0), FieldInfo.GetFieldFromHandle(ldtoken(pAdherentId))), typeof(long))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.First<Adherent>(source);
		}
		public void Update(Adherent pAdherent)
		{
			pAdherent.DateModification = DateTime.Now;
			EFExtension.SetAllModified<Adherent>(pAdherent, AdherentDao.Instance.Context);
			try
			{
				AdherentDao.Instance.Context.SaveChanges();
			}
			catch (OptimisticConcurrencyException arg_2D_0)
			{
				OptimisticConcurrencyException optimisticConcurrencyException = arg_2D_0;
				throw optimisticConcurrencyException;
			}
		}
		public void Delete(Adherent pAdherent)
		{
			AdherentDao.Instance.Context.Attach(pAdherent);
			AdherentDao.Instance.Context.DeleteObject(pAdherent.Adresse);
			AdherentDao.Instance.Context.DeleteObject(pAdherent.Contact);
			AdherentDao.Instance.Context.DeleteObject(pAdherent.Inscriptions);
			AdherentDao.Instance.Context.DeleteObject(pAdherent);
			AdherentDao.Instance.Context.SaveChanges();
		}
		public List<Adherent> List()
		{
			IQueryable<Adherent> arg_4A_0 = AdherentDao.Instance.Context.Adherents;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Adherent), "a");
			IOrderedQueryable<Adherent> arg_8D_0 = Queryable.OrderBy<Adherent, string>(arg_4A_0, Expression.Lambda<Func<Adherent, string>>(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Nom()))), new ParameterExpression[]
			{
				parameterExpression
			}));
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(Adherent), "a");
			IOrderedQueryable<Adherent> source = Queryable.ThenBy<Adherent, string>(arg_8D_0, Expression.Lambda<Func<Adherent, string>>(Expression.Property(parameterExpression2, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Prenom()))), new ParameterExpression[]
			{
				parameterExpression2
			}));
			return Enumerable.ToList<Adherent>(source);
		}
		public bool Exist(Adherent pAdherent)
		{
			AdherentDao.<>c__DisplayClass2 <>c__DisplayClass2 = new AdherentDao.<>c__DisplayClass2();
			<>c__DisplayClass2.pAdherent = pAdherent;
			IQueryable<Adherent> arg_1D4_0 = AdherentDao.Instance.Context.Adherents;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Adherent), "a");
			IQueryable<Adherent> source = Queryable.Where<Adherent>(arg_1D4_0, Expression.Lambda<Func<Adherent, bool>>(Expression.AndAlso(Expression.AndAlso(Expression.Call(Expression.Call(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Nom()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(ToUpper())), new Expression[0]), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(Equals())), new Expression[]
			{
				Expression.Call(Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pAdherent))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Nom()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(ToUpper())), new Expression[0])
			}), Expression.Call(Expression.Call(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Prenom()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(ToUpper())), new Expression[0]), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(Equals())), new Expression[]
			{
				Expression.Call(Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pAdherent))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Prenom()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(ToUpper())), new Expression[0])
			})), Expression.Call(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_DateNaissance()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(Equals())), new Expression[]
			{
				Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pAdherent))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_DateNaissance())))
			})), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.Count<Adherent>(source) > 0;
		}
		public bool IsUsed(Adherent pAdherent)
		{
			AdherentDao.<>c__DisplayClass4 <>c__DisplayClass4 = new AdherentDao.<>c__DisplayClass4();
			<>c__DisplayClass4.pAdherent = pAdherent;
			IQueryable<Inscription> arg_85_0 = AdherentDao.Instance.Context.Inscriptions;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Inscription), "i");
			IQueryable<Inscription> source = Queryable.Where<Inscription>(arg_85_0, Expression.Lambda<Func<Inscription, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID_Adherent()))), Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass4), FieldInfo.GetFieldFromHandle(ldtoken(pAdherent))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID())))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.Count<Inscription>(source) > 0;
		}
		public void Refresh(Adherent pAdherent)
		{
			this.Context.Refresh(RefreshMode.StoreWins, pAdherent);
			this.Context.Refresh(RefreshMode.StoreWins, pAdherent.Adresse);
			this.Context.Refresh(RefreshMode.StoreWins, pAdherent.Contact);
		}
	}
}
