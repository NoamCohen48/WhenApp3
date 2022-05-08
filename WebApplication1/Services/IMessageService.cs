using System;
using whenAppModel.Models;

namespace whenAppModel.Services
	{
	public interface IMessageService
	{
		public Task AddMessage(Message message);

		public Task RemoveMessage(Message message);

		public Task<List<Message>> GetMessages(User from, User to);


	}
}
