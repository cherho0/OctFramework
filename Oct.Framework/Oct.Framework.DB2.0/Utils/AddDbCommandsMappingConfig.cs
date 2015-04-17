using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Oct.Framework.DB.Core;
using Oct.Framework.DB.Emit.Conversion;
using Oct.Framework.DB.Emit.MappingConfiguration;
using Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations;
using Oct.Framework.DB.Emit.MappingConfiguration.MappingOperations.Interfaces;
using Oct.Framework.DB.Emit.Utils;
using Oct.Framework.DB.Linq;

namespace Oct.Framework.DB.Utils
{
	 public class AddDbCommandsMappingConfig : IMappingConfigurator
	{
		IEnumerable<string> _includeFields;
		IEnumerable<string> _excludeFields;
		string _configName;

		public AddDbCommandsMappingConfig(
			IEnumerable<string> includeFields,
			IEnumerable<string> excludeFields,
			string configName)
		{
			_includeFields = includeFields;
			_excludeFields = excludeFields;
			_configName = configName;

			if (_includeFields != null)
			{
				_includeFields = _includeFields.Select(f => f.ToUpper());
			}

			if (_excludeFields != null)
			{
				_excludeFields = _excludeFields.Select(f => f.ToUpper());
			}

		}


		#region IMappingConfigurator Members
		public IRootMappingOperation GetRootMappingOperation(Type from, Type to)
		{
			return null;
		}

		public IMappingOperation[] GetMappingOperations(Type from, Type to)
		{
			var members = ReflectionUtils.GetPublicFieldsAndProperties(from);
			if (_includeFields != null)
			{
				members = members
					.Where( m => _includeFields.Contains(m.Name.ToUpper()) )
					.ToArray();
			}

			if (_excludeFields != null)
			{
				members = members
					.Where( m => !_excludeFields.Contains(m.Name.ToUpper()) )
					.ToArray();
			}

			return members
				.Select(
					m => new SrcReadOperation
					{
						Source = new MemberDescriptor(new[] { m }),
                        Setter = (obj, v, s) => ((DbCommand)obj).AddParam(Constants.ParameterPrefix + m.Name, v)
					}
				)
				.ToArray();
		}

		public string GetConfigurationName()
		{
			return _configName;
		}


		#endregion

		public StaticConvertersManager GetStaticConvertersManager()
		{
			return null;
		}
	}
}
