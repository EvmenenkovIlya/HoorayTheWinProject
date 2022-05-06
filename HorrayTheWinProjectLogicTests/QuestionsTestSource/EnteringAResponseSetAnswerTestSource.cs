using System;
using System.Collections;
using Telegram.Bot.Types;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoorayTheWinProjectLogic;

namespace HorrayTheWinProjectLogicTests.QuestionsTestSource
{
	public class EnteringAResponseSetAnswerTestSource : IEnumerable
	{
		public IEnumerator GetEnumerator()
		{
			Update update = new Update();
			yield return new object[] { update.Message!.Text = "a", Enums.BehaviorOptions.nextQuestion };
		}
	}
}
