﻿using Masuit.Tools.RandomSelector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Masuit.Tools
{
	public static partial class Extensions
	{
		public static int TotalWeight<T>(this WeightedSelector<T> selector)
		{
			return selector.Items.Count == 0 ? 0 : selector.Items.Sum(t => t.Weight);
		}

		public static List<WeightedItem<T>> OrderByWeightDescending<T>(this WeightedSelector<T> selector)
		{
			return selector.Items.OrderByDescending(item => item.Weight).ToList();
		}

		public static List<WeightedItem<T>> OrderByWeightAscending<T>(this WeightedSelector<T> selector)
		{
			return selector.Items.OrderBy(item => item.Weight).ToList();
		}

		public static T WeightedItem<T>(this IEnumerable<WeightedItem<T>> list)
		{
			return new WeightedSelector<T>(list).Select();
		}

		public static IEnumerable<T> WeightedItems<T>(this IEnumerable<WeightedItem<T>> list, int count)
		{
			return new WeightedSelector<T>(list).SelectMultiple(count);
		}

		/// <summary>
		/// 执行权重筛选，取多个元素
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">原始数据</param>
		/// <param name="count">目标个数</param>
		/// <param name="keySelector">按哪个属性进行权重筛选</param>
		/// <param name="option">抽取选项</param>
		/// <returns></returns>
		public static IEnumerable<T> WeightedItems<T>(this IEnumerable<T> source, int count, Func<T, int> keySelector, SelectorOption option = null)
		{
			if (!source.Any())
			{
				return source;
			}

			var selector = new WeightedSelector<T>(source.Select(t => new WeightedItem<T>(t, keySelector(t))), option);
			return selector.SelectMultiple(count);
		}

		/// <summary>
		/// 执行权重筛选，取1个元素
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">原始数据</param>
		/// <param name="keySelector">按哪个属性进行权重筛选</param>
		/// <param name="option">抽取选项</param>
		/// <returns></returns>
		public static T WeightedBy<T>(this IEnumerable<T> source, Func<T, int> keySelector, SelectorOption option = null)
		{
			var selector = new WeightedSelector<T>(source.Select(t => new WeightedItem<T>(t, keySelector(t))), option);
			return selector.Select();
		}
	}
}
