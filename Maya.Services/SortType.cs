using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.Services
{
	/// <summary>
	/// 和数据表里面的数据保持一致
	/// </summary>
	public enum SortType
	{
		/// <summary>
		/// 电流电压
		/// </summary>
		[Description("电流电压")]
		DLDY = 10001,
		/// <summary>
		/// 插座标准
		/// </summary>
		[Description("插座标准")]
		CZBZ,
		/// <summary>
		/// 全球通使用方法
		/// </summary>
		[Description("全球通使用方法")]
		QQTSYFF,
		/// <summary>
		/// 大使馆资料
		/// </summary>
		[Description("大使馆资料")]
		DSGZL,
		/// <summary>
		/// 当地紧急电话
		/// </summary>
		[Description("当地紧急电话")]
		DDJJDH,
		/// <summary>
		/// 机场信息
		/// </summary>
		[Description("机场信息")]
		JCXX,
		/// <summary>
		/// 出入境卡填写
		/// </summary>
		[Description("出入境卡填写")]
		CRJKTX,
		/// <summary>
		/// 经纬度
		/// </summary>
		[Description("经纬度")]
		JWD = 10011,
		/// <summary>
		/// 实时汇率
		/// </summary>
		[Description("实时汇率")]
		SSHL,
		/// <summary>
		/// 货币兑换
		/// </summary>
		[Description("货币兑换")]
		HBDH,
		/// <summary>
		/// 产品专题
		/// </summary>
		[Description("产品专题")]
		CPZT,
		/// <summary>
		/// 旅游音乐
		/// </summary>
		[Description("旅游音乐")]
		LYYY
	}
}
