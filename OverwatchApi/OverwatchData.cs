using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using AngleSharp.Dom;

namespace OverwatchApi
{
    public interface IOverwatchData
    {
    }

    public abstract class OverwatchData<TData> : IOverwatchData where TData : IOverwatchData
    {
        private readonly Dictionary<string, Action<string>> _tableValueHandlers = new Dictionary<string, Action<string>>();

        public Player Player { get; set; }

        protected OverwatchData(Player player)
        {
            Player = player;
        }

        protected void RegisterDataProperty<T>(string rowName, Expression<Func<TData, T>> propertyLocator) =>
            RegisterDataProperty(rowName, propertyLocator, Converters.GetDefaultConverter<T>());

        protected void RegisterDataProperty<T>(string rowName, Expression<Func<TData, T>> propertyLocator, Func<string, T> converter)
        {
            _tableValueHandlers.Add(rowName.ToLowerInvariant(), value => GetProperty(propertyLocator).SetValue(this, converter(value)));
        }

        protected void LoadData(IElement dataTable)
        {
            var tableContents = dataTable.QuerySelectorAll("tbody > tr");
            foreach (var row in tableContents)
            {
                var cells = row.QuerySelectorAll("td");
                var rowName = cells[0].TextContent;
                var rowValue = cells[1].TextContent;

                Action<string> handler;
                if (_tableValueHandlers.TryGetValue(rowName.ToLowerInvariant(), out handler))
                    handler(rowValue);
            }
        }

        private PropertyInfo GetProperty<T>(Expression<Func<TData, T>> locator)
        {
            return (PropertyInfo) ((MemberExpression) locator.Body).Member;
        }
    }
}