namespace GestAdh45.Dao
{
	public class InscriptionDao
	{
		private static InscriptionDao Instance;
		private Entities Context
		{
			get;
			set;
		}
		private InscriptionDao()
		{
		}
		public static InscriptionDao GetInstance(Entities pContexte)
		{
			if (InscriptionDao.Instance == null)
			{
				InscriptionDao.Instance = new InscriptionDao();
			}
			InscriptionDao.Instance.Context = pContexte;
			return InscriptionDao.Instance;
		}
		public void Create(Inscription pInscription)
		{
			pInscription.DateCreation = DateTime.Now;
			pInscription.DateModification = DateTime.Now;
			InscriptionDao.Instance.Context.Inscriptions.AddObject(pInscription);
			InscriptionDao.Instance.Context.SaveChanges();
		}
		public Inscription Read(int pInscriptionId)
		{
			InscriptionDao.<>c__DisplayClass0 <>c__DisplayClass0 = new InscriptionDao.<>c__DisplayClass0();
			<>c__DisplayClass0.pInscriptionId = pInscriptionId;
			IQueryable<Inscription> arg_80_0 = InscriptionDao.Instance.Context.Inscriptions;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Inscription), "i");
			IQueryable<Inscription> source = Queryable.Where<Inscription>(arg_80_0, Expression.Lambda<Func<Inscription, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID()))), Expression.Convert(Expression.Field(Expression.Constant(<>c__DisplayClass0), FieldInfo.GetFieldFromHandle(ldtoken(pInscriptionId))), typeof(long))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.First<Inscription>(source);
		}
		public void Update(Inscription pInscription)
		{
			pInscription.DateModification = DateTime.Now;
			EFExtension.SetAllModified<Inscription>(pInscription, InscriptionDao.Instance.Context);
			try
			{
				InscriptionDao.Instance.Context.SaveChanges();
			}
			catch (OptimisticConcurrencyException arg_2D_0)
			{
				OptimisticConcurrencyException optimisticConcurrencyException = arg_2D_0;
				throw optimisticConcurrencyException;
			}
		}
		public void Delete(Inscription pInscription)
		{
			InscriptionDao.Instance.Context.Attach(pInscription);
			InscriptionDao.Instance.Context.DeleteObject(pInscription);
			InscriptionDao.Instance.Context.SaveChanges();
		}
		public List<Inscription> List()
		{
			IQueryable<Inscription> arg_72_0 = InscriptionDao.Instance.Context.Inscriptions;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Inscription), "i");
			IOrderedQueryable<Inscription> arg_C9_0 = Queryable.OrderBy<Inscription, long>(arg_72_0, Expression.Lambda<Func<Inscription, long>>(Expression.Property(Expression.Property(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Groupe()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Saison()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID()))), new ParameterExpression[]
			{
				parameterExpression
			}));
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(Inscription), "i");
			IOrderedQueryable<Inscription> arg_123_0 = Queryable.ThenBy<Inscription, string>(arg_C9_0, Expression.Lambda<Func<Inscription, string>>(Expression.Property(Expression.Property(parameterExpression2, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Adherent()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Nom()))), new ParameterExpression[]
			{
				parameterExpression2
			}));
			ParameterExpression parameterExpression3 = Expression.Parameter(typeof(Inscription), "i");
			IOrderedQueryable<Inscription> source = Queryable.ThenBy<Inscription, string>(arg_123_0, Expression.Lambda<Func<Inscription, string>>(Expression.Property(Expression.Property(parameterExpression3, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Adherent()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Prenom()))), new ParameterExpression[]
			{
				parameterExpression3
			}));
			return Enumerable.ToList<Inscription>(source);
		}
		public List<Inscription> ListSaisonCourante()
		{
			IQueryable<Inscription> arg_8D_0 = InscriptionDao.Instance.Context.Inscriptions;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Inscription), "i");
			IQueryable<Inscription> arg_E4_0 = Queryable.Where<Inscription>(arg_8D_0, Expression.Lambda<Func<Inscription, bool>>(Expression.Equal(Expression.Property(Expression.Property(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Groupe()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Saison()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_EstSaisonCourante()))), Expression.Constant(1, typeof(long))), new ParameterExpression[]
			{
				parameterExpression
			}));
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(Inscription), "i");
			IOrderedQueryable<Inscription> arg_13E_0 = Queryable.OrderBy<Inscription, string>(arg_E4_0, Expression.Lambda<Func<Inscription, string>>(Expression.Property(Expression.Property(parameterExpression2, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Adherent()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Nom()))), new ParameterExpression[]
			{
				parameterExpression2
			}));
			ParameterExpression parameterExpression3 = Expression.Parameter(typeof(Inscription), "i");
			IOrderedQueryable<Inscription> source = Queryable.ThenBy<Inscription, string>(arg_13E_0, Expression.Lambda<Func<Inscription, string>>(Expression.Property(Expression.Property(parameterExpression3, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Adherent()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Prenom()))), new ParameterExpression[]
			{
				parameterExpression3
			}));
			return Enumerable.ToList<Inscription>(source);
		}
		public bool Exist(Inscription pInscription)
		{
			InscriptionDao.<>c__DisplayClass2 <>c__DisplayClass2 = new InscriptionDao.<>c__DisplayClass2();
			<>c__DisplayClass2.pInscription = pInscription;
			IQueryable<Inscription> arg_CD_0 = InscriptionDao.Instance.Context.Inscriptions;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Inscription), "i");
			IQueryable<Inscription> source = Queryable.Where<Inscription>(arg_CD_0, Expression.Lambda<Func<Inscription, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID_Adherent()))), Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pInscription))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID_Adherent())))), Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID_Groupe()))), Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pInscription))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID_Groupe()))))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.Count<Inscription>(source) > 0;
		}
		public void Refresh(Inscription pInscription)
		{
			this.Context.Refresh(RefreshMode.StoreWins, pInscription);
			this.Context.Refresh(RefreshMode.StoreWins, pInscription.Adherent);
			this.Context.Refresh(RefreshMode.StoreWins, pInscription.Groupe);
		}
	}
}
