namespace ETModel
{
	[ETModel.Config(ETModel.AppType.Client)]
	public partial class ChapterDataCategory : ACategory<ChapterData>
	{
	}

	public class ChapterData: IConfig
	{
		public long Id { get; set; }
		/// <summary>
		///章节名称 
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///描述 
		/// </summary>
		public string Desc { get; set; }

		/// <summary>
		///解锁消耗 
		/// </summary>
		public int ChargeNum { get; set; }

		/// <summary>
		///关卡 
		/// </summary>
		public long[] PlotIDArr { get; set; }

	}
}
