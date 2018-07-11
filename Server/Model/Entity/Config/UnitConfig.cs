namespace ETModel
{
	[ETModel.Config(ETModel.AppType.Client)]
	public partial class UnitConfigCategory : ACategory<UnitConfig>
	{
	}

	public class UnitConfig: IConfig
	{
		public long Id { get; set; }
		/// <summary>
		///登录签到 
		/// </summary>
		public int[] SignRewardArr { get; set; }

	}
}
