namespace GestAdh45.Dao
{
	public class InfosClubDao
	{
		private static InfosClubDao Instance;
		private Entities Context
		{
			get;
			set;
		}
		private InfosClubDao()
		{
		}
		public static InfosClubDao GetInstance(Entities pContexte)
		{
			if (InfosClubDao.Instance == null)
			{
				InfosClubDao.Instance = new InfosClubDao();
			}
			InfosClubDao.Instance.Context = pContexte;
			return InfosClubDao.Instance;
		}
		public InfosClub Read()
		{
			IQueryable<InfosClub> arg_36_0 = InfosClubDao.Instance.Context.InfosClubs;
			ParameterExpression parameterExpression = Expression.Parameter(typeof(InfosClub), "i");
			IQueryable<InfosClub> source = Queryable.Select<InfosClub, InfosClub>(arg_36_0, Expression.Lambda<Func<InfosClub, InfosClub>>(parameterExpression, new ParameterExpression[]
			{
				parameterExpression
			}));
			return Queryable.First<InfosClub>(source);
		}
		public void Update(InfosClub pInfosClub)
		{
			EFExtension.SetAllModified<InfosClub>(pInfosClub, InfosClubDao.Instance.Context);
			try
			{
				InfosClubDao.Instance.Context.SaveChanges();
			}
			catch (OptimisticConcurrencyException arg_22_0)
			{
				OptimisticConcurrencyException optimisticConcurrencyException = arg_22_0;
				throw optimisticConcurrencyException;
			}
		}
		public void Refresh(InfosClub pInfosClub)
		{
			this.Context.Refresh(RefreshMode.StoreWins, pInfosClub);
		}
	}
}
