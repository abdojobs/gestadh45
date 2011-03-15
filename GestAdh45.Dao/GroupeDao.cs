namespace GestAdh45.Dao
{
	public class GroupeDao
	{
		private static GroupeDao Instance;
		private Entities Context
		{
			get;
			set;
		}
		private GroupeDao()
		{
		}
		public static GroupeDao GetInstance(Entities pContexte)
		{
			if (GroupeDao.Instance == null)
			{
				GroupeDao.Instance = new GroupeDao();
			}
			GroupeDao.Instance.Context = pContexte;
			return GroupeDao.Instance;
		}
		public void Create(Groupe pGroupe)
		{
			GroupeDao.Instance.Context.Groupes.AddObject(pGroupe);
			GroupeDao.Instance.Context.SaveChanges();
		}
		public Groupe Read(int pGroupeId)
		{
			GroupeDao.<>c__DisplayClass0 <>c__DisplayClass0 = new GroupeDao.<>c__DisplayClass0();
			<>c__DisplayClass0.pGroupeId = pGroupeId;
			IQueryable<Groupe> arg_80_0 = GroupeDao.Instance.Context.Groupes;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Groupe), "g");
			IQueryable<Groupe> source = Queryable.Where<Groupe>(arg_80_0, Expression.Lambda<Func<Groupe, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID()))), Expression.Convert(Expression.Field(Expression.Constant(<>c__DisplayClass0), FieldInfo.GetFieldFromHandle(ldtoken(pGroupeId))), typeof(long))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.First<Groupe>(source);
		}
		public void Update(Groupe pGroupe)
		{
			EFExtension.SetAllModified<Groupe>(pGroupe, GroupeDao.Instance.Context);
			try
			{
				GroupeDao.Instance.Context.SaveChanges();
			}
			catch (OptimisticConcurrencyException arg_22_0)
			{
				OptimisticConcurrencyException optimisticConcurrencyException = arg_22_0;
				throw optimisticConcurrencyException;
			}
		}
		public void Delete(Groupe pGroupe)
		{
			GroupeDao.Instance.Context.Attach(pGroupe);
			GroupeDao.Instance.Context.DeleteObject(pGroupe);
			GroupeDao.Instance.Context.SaveChanges();
		}
		public List<Groupe> List()
		{
			IQueryable<Groupe> arg_5E_0 = GroupeDao.Instance.Context.Groupes;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Groupe), "g");
			IOrderedQueryable<Groupe> arg_A1_0 = Queryable.OrderBy<Groupe, long>(arg_5E_0, Expression.Lambda<Func<Groupe, long>>(Expression.Property(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_JourSemaine()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Numero()))), new ParameterExpression[]
			{
				parameterExpression
			}));
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(Groupe), "g");
			IOrderedQueryable<Groupe> source = Queryable.ThenBy<Groupe, long>(arg_A1_0, Expression.Lambda<Func<Groupe, long>>(Expression.Property(parameterExpression2, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_HeureDebut()))), new ParameterExpression[]
			{
				parameterExpression2
			}));
			return Enumerable.ToList<Groupe>(source);
		}
		public List<Groupe> ListSaisonCourante()
		{
			IQueryable<Groupe> arg_79_0 = GroupeDao.Instance.Context.Groupes;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Groupe), "g");
			IQueryable<Groupe> arg_D0_0 = Queryable.Where<Groupe>(arg_79_0, Expression.Lambda<Func<Groupe, bool>>(Expression.Equal(Expression.Property(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Saison()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_EstSaisonCourante()))), Expression.Constant(1, typeof(long))), new ParameterExpression[]
			{
				parameterExpression
			}));
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(Groupe), "g");
			IOrderedQueryable<Groupe> arg_116_0 = Queryable.OrderBy<Groupe, long>(arg_D0_0, Expression.Lambda<Func<Groupe, long>>(Expression.Property(Expression.Property(parameterExpression2, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_JourSemaine()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Numero()))), new ParameterExpression[]
			{
				parameterExpression2
			}));
			ParameterExpression parameterExpression3 = Expression.Parameter(typeof(Groupe), "g");
			IOrderedQueryable<Groupe> source = Queryable.ThenBy<Groupe, long>(arg_116_0, Expression.Lambda<Func<Groupe, long>>(Expression.Property(parameterExpression3, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_HeureDebut()))), new ParameterExpression[]
			{
				parameterExpression3
			}));
			return Enumerable.ToList<Groupe>(source);
		}
		public bool Exist(Groupe pGroupe)
		{
			GroupeDao.<>c__DisplayClass2 <>c__DisplayClass2 = new GroupeDao.<>c__DisplayClass2();
			<>c__DisplayClass2.pGroupe = pGroupe;
			IQueryable<Groupe> arg_182_0 = GroupeDao.Instance.Context.Groupes;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Groupe), "g");
			IQueryable<Groupe> source = Queryable.Where<Groupe>(arg_182_0, Expression.Lambda<Func<Groupe, bool>>(Expression.AndAlso(Expression.AndAlso(Expression.Equal(Expression.Property(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Saison()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID()))), Expression.Property(Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pGroupe))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Saison()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID())))), Expression.Equal(Expression.Property(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_JourSemaine()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID()))), Expression.Property(Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pGroupe))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_JourSemaine()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID()))))), Expression.Call(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Libelle()))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(Equals())), new Expression[]
			{
				Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass2), FieldInfo.GetFieldFromHandle(ldtoken(pGroupe))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Libelle())))
			})), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.Count<Groupe>(source) > 0;
		}
		public bool IsUsed(Groupe pGroupe)
		{
			GroupeDao.<>c__DisplayClass4 <>c__DisplayClass4 = new GroupeDao.<>c__DisplayClass4();
			<>c__DisplayClass4.pGroupe = pGroupe;
			IQueryable<Inscription> arg_85_0 = GroupeDao.Instance.Context.Inscriptions;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Inscription), "i");
			IQueryable<Inscription> source = Queryable.Where<Inscription>(arg_85_0, Expression.Lambda<Func<Inscription, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID_Groupe()))), Expression.Property(Expression.Field(Expression.Constant(<>c__DisplayClass4), FieldInfo.GetFieldFromHandle(ldtoken(pGroupe))), (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID())))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.Count<Inscription>(source) > 0;
		}
		public void Refresh(Groupe pGroupe)
		{
			this.Context.Refresh(RefreshMode.StoreWins, pGroupe);
			this.Context.Refresh(RefreshMode.StoreWins, pGroupe.Saison);
		}
	}
}
