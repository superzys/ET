using ETModel;

namespace ETHotfix
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

		/// <summary>
		///每天分享次数 
		/// </summary>
		public int ShareMaxNum { get; set; }

		/// <summary>
		///分享的奖励 
		/// </summary>
		public int ShareRewardNum { get; set; }

	}
}
