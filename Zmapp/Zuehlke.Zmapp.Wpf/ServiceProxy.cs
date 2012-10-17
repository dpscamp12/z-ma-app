using System;
using System.ServiceModel;

namespace Zuehlke.Zmapp.Wpf
{
	public class ServiceProxy
	{
		private readonly string endpointConfigurationName;

		protected ServiceProxy(string endpointConfigurationName)
		{
			if (endpointConfigurationName == null) throw new ArgumentNullException("endpointConfigurationName");

			this.endpointConfigurationName = endpointConfigurationName;
		}

		protected void ExecuteRemoteCall<TService>(Action<TService> action)
		{
			var fab = new ChannelFactory<TService>(this.endpointConfigurationName);
			fab.Open();
			var channel = fab.CreateChannel();

			try
			{
				((ICommunicationObject)channel).Open();
				action(channel);
			}
			finally
			{
				((ICommunicationObject)channel).Close();
				fab.Close();
			}
		}

		protected TResult ExecuteRemoteCall<TService, TResult>(Func<TService, TResult> function)
		{
			var fab = new ChannelFactory<TService>(this.endpointConfigurationName);
			fab.Open();
			var channel = fab.CreateChannel();

			TResult result;
			try
			{
				((ICommunicationObject)channel).Open();
				result = function(channel);
			}
			finally
			{
				((ICommunicationObject)channel).Close();
				fab.Close();
			}

			return result;
		}
	}
}