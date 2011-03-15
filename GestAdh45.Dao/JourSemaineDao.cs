namespace GestAdh45.Dao
{
	public class JourSemaineDao
	{
		private static JourSemaineDao Instance;
		private Entities Context
		{
			get;
			set;
		}
		private JourSemaineDao()
		{
		}
		public static JourSemaineDao GetInstance(Entities pContexte)
		{
			if (JourSemaineDao.Instance == null)
			{
				JourSemaineDao.Instance = new JourSemaineDao();
			}
			JourSemaineDao.Instance.Context = pContexte;
			return JourSemaineDao.Instance;
		}
		public JourSemaine Read(int pJourId)
		{
			JourSemaineDao.<>c__DisplayClass0 <>c__DisplayClass0 = new JourSemaineDao.<>c__DisplayClass0();
			<>c__DisplayClass0.pJourId = pJourId;
			IQueryable<JourSemaine> arg_80_0 = JourSemaineDao.Instance.Context.JourSemaines;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(JourSemaine), "j");
			IQueryable<JourSemaine> source = Queryable.Where<JourSemaine>(arg_80_0, Expression.Lambda<Func<JourSemaine, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID()))), Expression.Convert(Expression.Field(Expression.Constant(<>c__DisplayClass0), FieldInfo.GetFieldFromHandle(ldtoken(pJourId))), typeof(long))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.First<JourSemaine>(source);
		}
		public List<JourSemaine> List()
		{
			IQueryable<JourSemaine> arg_4A_0 = JourSemaineDao.Instance.Context.JourSemaines;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(JourSemaine), "j");
			IOrderedQueryable<JourSemaine> source = Queryable.OrderBy<JourSemaine, long>(arg_4A_0, Expression.Lambda<Func<JourSemaine, long>>(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_Numero()))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Enumerable.ToList<JourSemaine>(source);
		}
		public void Refresh(JourSemaine pJourSemaine)
		{
			this.Context.Refresh(RefreshMode.StoreWins, pJourSemaine);
		}
	}
}
