namespace GestAdh45.Dao
{
	public class SexeDao
	{
		private static SexeDao Instance;
		private Entities Context
		{
			get;
			set;
		}
		private SexeDao()
		{
		}
		public static SexeDao GetInstance(Entities pContexte)
		{
			if (SexeDao.Instance == null)
			{
				SexeDao.Instance = new SexeDao();
			}
			SexeDao.Instance.Context = pContexte;
			return SexeDao.Instance;
		}
		public Sexe Read(int pSexeId)
		{
			SexeDao.<>c__DisplayClass0 <>c__DisplayClass0 = new SexeDao.<>c__DisplayClass0();
			<>c__DisplayClass0.pSexeId = pSexeId;
			IQueryable<Sexe> arg_80_0 = SexeDao.Instance.Context.Sexes;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Sexe), "s");
			IQueryable<Sexe> source = Queryable.Where<Sexe>(arg_80_0, Expression.Lambda<Func<Sexe, bool>>(Expression.Equal(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_ID()))), Expression.Convert(Expression.Field(Expression.Constant(<>c__DisplayClass0), FieldInfo.GetFieldFromHandle(ldtoken(pSexeId))), typeof(long))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.First<Sexe>(source);
		}
		public List<Sexe> List()
		{
			IQueryable<Sexe> arg_4A_0 = SexeDao.Instance.Context.Sexes;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(Sexe), "s");
			IOrderedQueryable<Sexe> source = Queryable.OrderBy<Sexe, string>(arg_4A_0, Expression.Lambda<Func<Sexe, string>>(Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(ldtoken(get_LibelleCourt()))), new ParameterExpression[]
			{
				parameterExpression
			}));
			return Enumerable.ToList<Sexe>(source);
		}
		public void Refresh(Sexe pSexe)
		{
			this.Context.Refresh(RefreshMode.StoreWins, pSexe);
		}
	}
}
